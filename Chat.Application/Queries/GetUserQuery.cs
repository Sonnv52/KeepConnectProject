using Chat.Core.DAO;
using MediatR;

namespace Chat.Application.Queries
{
    public class GetUserQuery :IRequest<UserDTO> 
{
    }
}
