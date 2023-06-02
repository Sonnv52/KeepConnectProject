using Chat.Application.Features.Rooms.Requests.Commads;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Features.Rooms.Validators
{
    public class CreateRoomCommadValidator : AbstractValidator<CreateRoomCommad>
    {
        public CreateRoomCommadValidator()
        {
            RuleFor(c => c.RoomName)
             .MaximumLength(100).WithMessage("Room Name Is Too Long!!");

            RuleFor(c => c.IdPartners)
                .Must(ids => ids != null && ids.Count > 0 && ids.Count <= 30)
                .WithMessage("IdPartners must be a list with a maximum of 30 elements an != null.");
        }
    }
}
