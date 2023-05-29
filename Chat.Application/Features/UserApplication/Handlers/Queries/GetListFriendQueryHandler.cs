using Chat.Application.DTOs.UserApp;
using Chat.Application.Features.UserApplication.Requests.Queries;
using Chat.Application.Features.UserApplication.Validators;
using Chat.Application.Helper.Extentions;
using Chat.Application.Persistence.Contracts;
using MediatR;
using Chat.Application.Exceptions;

namespace Chat.Application.Features.UserApplication.Handlers.Queries
{
    public class GetListFriendQueryHandle : IRequestHandler<GetListFriendQuery, PagedList<FriendToList>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetListFriendQueryHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<PagedList<FriendToList>> Handle(GetListFriendQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetListFriendQueryValidator();
            var validatorResult = validator.Validate(request);

            if (!validatorResult.IsValid)
            {
                throw new ValidationException(validatorResult);
            }

            var result = await _unitOfWork.UserRepository.GetListFriendAsync(request.PageIndex, request.PageSize, request.SearchKey);
            return result;
        }
    }
}
