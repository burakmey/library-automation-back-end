﻿namespace library_automation_back_end.Controllers
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
            BookResponse? response = await userService.SendBorrowRequest(request, user.Id);
            if (!response.Succeeded)
                return BadRequest(response.Message);
            return Ok(response.Message);
        }

        [HttpPost]
        public async Task<IActionResult> BorrowReservedBook(BookRequest request)
        {
            User? user = await GetUserFromToken();
            if (user == null)
                return BadRequest("Invalid refresh token!");
            BookResponse? response = await userService.SendReservedBorrowRequest(request, user.Id);
            if (!response.Succeeded)
                return BadRequest(response.Message);
            return Ok(response.Message);
        }

        [HttpPost]
        public async Task<IActionResult> ReturnBook(BookRequest request)
        {
            User? user = await GetUserFromToken();
            if (user == null)
                return BadRequest("Invalid refresh token!");
            BookResponse? response = await userService.SendReturnRequest(request, user.Id);
            if (!response.Succeeded)
                return BadRequest(response.Message);
            return Ok(response.Message);
        }

        [HttpPost]
        public async Task<IActionResult> ReserveBook(BookRequest request)
        {
            User? user = await GetUserFromToken();
            if (user == null)
                return BadRequest("Invalid refresh token!");
            BookResponse? response = await userService.ReserveBook(request, user.Id);
            if (!response.Succeeded)
                return BadRequest(response.Message);
            return Ok(response.Message);
        }

        [HttpDelete]
        public async Task<IActionResult> CancelDesire(DesireRequest request)
        {
            User? user = await GetUserFromToken();
            if (user == null)
                return BadRequest("Invalid refresh token!");
            BookResponse? response = await userService.DeleteRequest(request, user.Id);
            if (!response.Succeeded)
                return BadRequest(response.Message);
            return Ok(response.Message);
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
