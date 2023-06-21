using AutoMapper;
using Chat.Application.DTOs.UserApp;
using Chat.Application.Exceptions;
using Chat.Application.Models.UserApp;
using Chat.Application.Respone;
using Chat.Application.Services.Abstractions;
using Chat.Domain.DAOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Chat.Application.Services.Implementions
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserApp> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserService(IConfiguration configuration, UserManager<UserApp> userManager, RoleManager<IdentityRole> roleManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _configuration = configuration;
        }

        public Task<UserApp> GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthenticationModel> SignInAsync(LoginDTO user,
            CancellationToken cancellationToken)
        {
            var userApp = await _userManager.FindByNameAsync(user.UserName);

            if (userApp is null)
                throw new UnauthorizedAccessException("Incorrect account or password");

            if (!userApp.LockoutEnabled)
                throw new UnauthorizedAccessException("Account has been disabled");

            if (userApp is not null && await _userManager.CheckPasswordAsync(userApp, user.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(userApp);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userApp.UserName ?? " "),
                    new Claim(ClaimTypes.Email, userApp.Email ?? " "),
                    new Claim(ClaimTypes.PrimarySid,userApp.Id ?? " "),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = Task.Run(() => GetToken(authClaims));
                var refreshToken = Task.Run(() => GenerateRefreshToken());
                await Task.WhenAll(token, refreshToken);
                //Get refresh token
                userApp.RefreshToken = refreshToken.Result;
                _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"],
                    out int refreshTokenValidityInDays);
                userApp.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);
                //Update refreshtoken and expiry time
                await _userManager.UpdateAsync(userApp);
                var accessToken = new JwtSecurityTokenHandler().WriteToken(token.Result);

                return new AuthenticationModel
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken.Result,
                };
            }
            throw new UnauthorizedAccessException("Incorrect account or password");
        }

        //Get token
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            TimeZoneInfo localTimeZone = TimeZoneInfo.Local;
            DateTime currentTime = DateTime.Now;
            DateTime resultTime = currentTime.AddMinutes(3000).ToUniversalTime().ToLocalTime();
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            //Generate token
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: resultTime,
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }

        //Get Refresh Token
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public async Task<BaseCommandResponse<bool>> SignUpAsync(UserDTO model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Email);

            if (userExists is not null)
                return new BaseCommandResponse<bool>
                {
                    Data = false,
                    Message = "User is exists",
                    Success = false
                };

            var user = _mapper.Map<UserApp>(model);

            if (model.Password is null) return new BaseCommandResponse<bool>
            {
                Data = false,
                Message = "User is exists",
                Success = false
            };

            var result = await _userManager.CreateAsync(user, model.Password.Trim());
            if (!result.Succeeded)
                return new BaseCommandResponse<bool>
                {
                    Data = true,
                    Message = "Fail",
                    Success = true
                };

            if (!await _roleManager.RoleExistsAsync(UserRoles.ADMIN))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.ADMIN));
            if (!await _roleManager.RoleExistsAsync(UserRoles.MANAGER))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.MANAGER));

            if (await _roleManager.RoleExistsAsync(UserRoles.ADMIN))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.ADMIN);
            }
            if (await _roleManager.RoleExistsAsync(UserRoles.MANAGER))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.MANAGER);
            }

            return new BaseCommandResponse<bool>
            {
                Data = true,
                Message = "Success",
                Success = true
            };
        }

        public async Task<AuthenticationModel> RefreshTokenAsync(string accessToken, string refreshToken)
        {
            var principal = await Task.Run(() => GetPrincipalFromExpiredToken(accessToken));

            if (principal is null)
                throw new ArgumentNullException(nameof(principal));

            string username = principal?.Identity?.Name ?? " ";
            var user = await _userManager.FindByNameAsync(username);

            if (!user.LockoutEnabled || user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                throw new SecurityTokenException("Invalid token");

            var newAccessToken = GetToken(principal.Claims.ToList());
            var newRefreshToken = GenerateRefreshToken();
            user.RefreshToken = newRefreshToken;
            _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);

            try
            {
                await _userManager.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                throw new SqlException(ex.ToString());
            }

            return new AuthenticationModel
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken
            };
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            ClaimsPrincipal principal;
            SecurityToken securityToken;

            try
            {
                principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            }
            catch (Exception ex)
            {
                throw new SecurityTokenException(ex.ToString());
            }

            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }
    }
}
