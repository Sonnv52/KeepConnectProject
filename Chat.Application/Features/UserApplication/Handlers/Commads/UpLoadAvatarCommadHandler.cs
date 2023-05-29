using Chat.Application.Features.UserApplication.Requests.Commads;
using Chat.Application.Persistence.Contracts;
using Chat.Application.Respone;
using Chat.Application.Services.Abstractions;
using Chat.Domain.DAOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Chat.Application.Features.UserApplication.Handlers.Commads
{
    public class UpLoadAvatarCommadHandle : IRequestHandler<UpLoadAvatarCommad, BaseCommandResponse<IList<string>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileHandleService _fileHandleService;
        private readonly UserManager<UserApp> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public UpLoadAvatarCommadHandle(IHttpContextAccessor contextAccessor, 
            IUnitOfWork unitOfWork, IFileHandleService fileHandleService,
            UserManager<UserApp> userManager)
        {
            _unitOfWork = unitOfWork;
            _fileHandleService = fileHandleService;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }

        async Task<BaseCommandResponse<IList<string>>> IRequestHandler<UpLoadAvatarCommad,
            BaseCommandResponse<IList<string>>>.Handle(UpLoadAvatarCommad request,
            CancellationToken cancellationToken)
        {
            var user = _contextAccessor?.HttpContext?.Items["User"] as UserApp;

            if (user == null)
            {
               throw new ArgumentNullException(nameof(user));
            }

            if (request.Avatar is null)
                throw new ArgumentNullException(nameof(request.Avatar));

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
            await _unitOfWork.CommitAsync(cancellationToken);

            return new BaseCommandResponse<IList<string>>
            {
                Success = true
            };
        }
    }
}
