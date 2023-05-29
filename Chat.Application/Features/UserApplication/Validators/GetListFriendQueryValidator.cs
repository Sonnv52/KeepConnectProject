using Chat.Application.Features.UserApplication.Requests.Commads;
using Chat.Application.Features.UserApplication.Requests.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Features.UserApplication.Validators
{
    internal class GetListFriendQueryValidator : AbstractValidator<GetListFriendQuery>
    {
        public GetListFriendQueryValidator()
        {
            RuleFor(r => r.PageSize)
                .LessThan(10000)
                .GreaterThan(-1).WithMessage("Page Size Should be greate than or equal 0");

            RuleFor(r => r.PageIndex)
                .LessThan(10000)
                .GreaterThan(-1).WithMessage("Page Index Should be greate than or equal 0");

            RuleFor(r => r.SearchKey)
                .MaximumLength(100).WithMessage("Search key is so long");
        }
    }
}
