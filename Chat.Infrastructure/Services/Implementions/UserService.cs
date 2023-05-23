using AutoMapper;
using Chat.Application.DTOs.UserApp;
using Chat.Application.Models.UserApp;
using Chat.Application.Respone;
using Chat.Application.Services.Abstractions;
using Chat.Domain.DAOs;
using Microsoft.AspNetCore.Identity;

namespace Chat.Infrastructure.Services.Implementions
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserApp> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<UserApp> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public Task<UserApp> GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
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
                }; ;

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
    }
}
