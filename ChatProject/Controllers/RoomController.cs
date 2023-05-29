using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class RoomController : ControllerBase
    {
        public RoomController()
        {
        }

        [HttpPost]
        [Route("/CreatRoom")]
        public async Task<IActionResult> CreateRoomAsync(string email)
        {
            
        }
    }
}
