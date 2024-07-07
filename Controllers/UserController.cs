namespace library_automation_back_end.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly AuthService authService;
        readonly UserService userService;

        public UserController(AuthService authService, UserService userService)
        {
            this.authService = authService;
            this.userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> BorrowBook(BookRequest request)
        {
            User? user = await GetUserFromToken();
            if (user == null)
                return BadRequest("Invalid refresh token!");
            BookResponse bookResponse = await userService.SendBorrowRequest(request, user.Id);
            if (!bookResponse.Succeeded)
                return BadRequest(bookResponse.Message);
            ICollection<UserDesire>? userDesires = await authService.GetUserDesires(user.Id);
            SendDesireResponse response = new(bookResponse, userDesires);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> BorrowReservedBook(BookRequest request)
        {
            User? user = await GetUserFromToken();
            if (user == null)
                return BadRequest("Invalid refresh token!");
            BookResponse bookResponse = await userService.SendReservedBorrowRequest(request, user.Id);
            if (!bookResponse.Succeeded)
                return BadRequest(bookResponse.Message);
            ICollection<UserDesire>? userDesires = await authService.GetUserDesires(user.Id);
            SendDesireResponse response = new(bookResponse, userDesires);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> ReturnBook(BookRequest request)
        {
            User? user = await GetUserFromToken();
            if (user == null)
                return BadRequest("Invalid refresh token!");
            BookResponse? bookResponse = await userService.SendReturnRequest(request, user.Id);
            if (!bookResponse.Succeeded)
                return BadRequest(bookResponse.Message);
            ICollection<UserDesire>? userDesires = await authService.GetUserDesires(user.Id);
            SendDesireResponse response = new(bookResponse, userDesires);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> ReserveBook(BookRequest request)
        {
            User? user = await GetUserFromToken();
            if (user == null)
                return BadRequest("Invalid refresh token!");
            BookResponse? bookResponse = await userService.ReserveBook(request, user.Id);
            if (!bookResponse.Succeeded)
                return BadRequest(bookResponse.Message);
            ICollection<UserBookReserve>? userReservations = await authService.GetUserReservations(user.Id);
            ReserveResponse response = new(bookResponse, userReservations);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> CancelDesire(DesireRequest request)
        {
            User? user = await GetUserFromToken();
            if (user == null)
                return BadRequest("Invalid refresh token!");
            BookResponse? bookResponse = await userService.DeleteRequest(request, user.Id);
            if (!bookResponse.Succeeded)
                return BadRequest(bookResponse.Message);
            ICollection<UserDesire>? userDesires = await authService.GetUserDesires(user.Id);
            SendDesireResponse response = new(bookResponse, userDesires);
            return Ok(response);
        }

        async Task<User?> GetUserFromToken()
        {
            string? refreshToken = Request.Cookies["token"];
            if (refreshToken == null)
                return null;
            User? user = await authService.GetUserWithRefreshToken(refreshToken);
            return user;
        }
    }
}
