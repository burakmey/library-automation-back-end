using library_automation_back_end.Features.FeaturesUser.Requests;
using library_automation_back_end.Features.FeaturesUser.Responses;

namespace library_automation_back_end.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly AuthService authService;
        readonly UserService userService;
        readonly TokenService tokenService;

        public UserController(AuthService authService, UserService userService, TokenService tokenService)
        {
            this.authService = authService;
            this.userService = userService;
            this.tokenService = tokenService;
        }

        [HttpPost]
        public async Task<IActionResult> BorrowBook(BookRequest request)
        {
            int userId = await GetUserIdFromAccessToken();
            if (userId == 0)
                return BadRequest("Invalid access token!");
            MessageResponse bookResponse = await userService.SendBorrowRequest(request, userId);
            if (!bookResponse.Succeeded)
                return BadRequest(bookResponse.Message);
            ICollection<UserDesire>? userDesires = await authService.GetUserDesires(userId);
            SendDesireResponse response = new(bookResponse, userDesires);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> BorrowReservedBook(BookRequest request)
        {
            int userId = await GetUserIdFromAccessToken();
            if (userId == 0)
                return BadRequest("Invalid access token!");
            MessageResponse bookResponse = await userService.SendReservedBorrowRequest(request, userId);
            if (!bookResponse.Succeeded)
                return BadRequest(bookResponse.Message);
            ICollection<UserDesire>? userDesires = await authService.GetUserDesires(userId);
            SendDesireResponse response = new(bookResponse, userDesires);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> ReturnBook(BookRequest request)
        {
            int userId = await GetUserIdFromAccessToken();
            if (userId == 0)
                return BadRequest("Invalid access token!");
            MessageResponse? bookResponse = await userService.SendReturnRequest(request, userId);
            if (!bookResponse.Succeeded)
                return BadRequest(bookResponse.Message);
            ICollection<UserDesire>? userDesires = await authService.GetUserDesires(userId);
            SendDesireResponse response = new(bookResponse, userDesires);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> ReserveBook(BookRequest request)
        {
            int userId = await GetUserIdFromAccessToken();
            if (userId == 0)
                return BadRequest("Invalid access token!");
            MessageResponse? bookResponse = await userService.ReserveBook(request, userId);
            if (!bookResponse.Succeeded)
                return BadRequest(bookResponse.Message);
            ICollection<UserBookReserve>? userReservations = await authService.GetUserReservations(userId);
            ReserveResponse response = new(bookResponse, userReservations);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> CancelDesire(DesireRequest request)
        {
            int userId = await GetUserIdFromAccessToken();
            if (userId == 0)
                return BadRequest("Invalid access token!");
            MessageResponse? bookResponse = await userService.DeleteRequest(request, userId);
            if (!bookResponse.Succeeded)
                return BadRequest(bookResponse.Message);
            ICollection<UserDesire>? userDesires = await authService.GetUserDesires(userId);
            SendDesireResponse response = new(bookResponse, userDesires);
            return Ok(response);
        }

        async Task<int> GetUserIdFromAccessToken()
        {
            string accessToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            string? userEmail = tokenService.GetUserEmailFromAccessToken(accessToken);
            if (userEmail == null)
                return 0;
            int userId = await authService.GetUserIdFromEmail(userEmail);
            return userId;
        }
    }
}
