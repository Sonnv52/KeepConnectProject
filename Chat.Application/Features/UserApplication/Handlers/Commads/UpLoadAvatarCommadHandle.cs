using Chat.Application.Features.UserApplication.Requests.Commads;
using Chat.Application.Persistence.Contracts;
using Chat.Application.Respone;
using Chat.Application.Services.Abstractions;
using Chat.Domain.DAOs;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Chat.Application.Features.UserApplication.Handlers.Commads
{
    public class UpLoadAvatarCommadHandle : IRequestHandler<UpLoadAvatarCommad, BaseCommandResponse<IList<string>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileHandleService _fileHandleService;
        private readonly UserManager<UserApp> _userManager;

        public UpLoadAvatarCommadHandle(IUnitOfWork unitOfWork, IFileHandleService fileHandleService,
            UserManager<UserApp> userManager)
        {
            _unitOfWork = unitOfWork;
            _fileHandleService = fileHandleService;
            _userManager = userManager;
        }

        async Task<BaseCommandResponse<IList<string>>> IRequestHandler<UpLoadAvatarCommad, BaseCommandResponse<IList<string>>>.Handle(UpLoadAvatarCommad request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync("boy98vippro@gmail.com");

            if (user == null)
            {
                return new BaseCommandResponse<IList<string>> 
                {
                    Success= false
                };
            }

            if(request.Avatar is null)
                return new BaseCommandResponse<IList<string>>
                {
                    Success = false
                };

            var pathAvatar = await _fileHandleService.SaveAsync(request.Avatar);
            var avatar = new Avatar
            {
                CreateBy = user.FullName,
                CreateDate = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                Path = pathAvatar,
                UserApp = user,
                LastModifiedBy = user.FullName,
                LastModifiedDate = DateTime.UtcNow,
                Created = DateTime.UtcNow,
            };

            await _unitOfWork.AvatarRepository.AddAsync(avatar);
            await _unitOfWork.CommitAsync();

            return new BaseCommandResponse<IList<string>>
            {
                Success = true
            };
        }
    }
}
