using Chat.Application.DTOs.UserApp;
using Chat.Application.Models.UserApp;
using Chat.Application.Persistence.Contracts;
using Chat.Application.Respone;
using Chat.Domain.DAOs;
using Chat.Infrastructure.DataContext;
using Microsoft.AspNetCore.Identity;

namespace Chat.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<UserApp> , IUserRepository
    {
        private readonly ChatDbContext _chatDbContext;
        private readonly UserManager<UserApp> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRepository(ChatDbContext chatDbContext, UserManager<UserApp> userManager,
             RoleManager<IdentityRole> roleManager) : base(chatDbContext) 
        {
            _chatDbContext = chatDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
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
                    Data= false,
                    Message = "User is exists",
                    Success = false
                };

            UserApp user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Email,
                FullName = model.FullName,
                Adress = model.Adress,
                PhoneNumber = model.PhoneNumber
            };

            if (model.Password is null) return  new BaseCommandResponse<bool>
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

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRoles.AdminAll))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.AdminAll));
            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            }
            if (await _roleManager.RoleExistsAsync(UserRoles.AdminAll))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.AdminAll);
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
