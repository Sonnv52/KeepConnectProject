using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Features.Rooms.Requests.Commads
{
    public record CreateRoomCommad : IRequest<Guid>
    {
        [Required]
        public IList<string?>? IdPartners { get; init; }
        public string? RoomName { get; set; }
    }
}
