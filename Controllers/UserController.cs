namespace library_automation_back_end.Controllers
{
    //[Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly UserService userService;
        readonly BookService bookService;
        public UserController(UserService userService, BookService bookService)
        {
            this.userService = userService;
            this.bookService = bookService;
        }

        [HttpPost]
        public async Task<IActionResult> BorrowBook(BookRequest request)
        {
            //User? user = await GetUserFromToken();
            //if (user == null)
            //    return BadRequest("Invalid refresh token!");
            BookResponse? response = await bookService.SendBorrowRequest(request, 1);
            if (!response.Succeeded)
                return BadRequest(response.Message);
            return Ok(response.Message);
        }

        [HttpDelete]
        public async Task<IActionResult> CancelBorrowBook(BookRequest request)
        {
            //User? user = await GetUserFromToken();
            //if (user == null)
            //    return BadRequest("Invalid refresh token!");
            BookResponse? response = await bookService.DeleteBorrowRequest(request, 1);
            if (!response.Succeeded)
                return BadRequest(response.Message);
            return Ok(response.Message);
        }

        [HttpPost]
        public async Task<IActionResult> ReturnBook(BookRequest request)
        {
            //User? user = await GetUserFromToken();
            //if (user == null)
            //    return BadRequest("Invalid refresh token!");
            BookResponse? response = await bookService.SendBorrowRequest(request, 1);
            if (!response.Succeeded)
                return BadRequest(response.Message);
            return Ok(response.Message);
        }

        [HttpDelete]
        public async Task<IActionResult> CancelReturnBook(BookRequest request)
        {
            //User? user = await GetUserFromToken();
            //if (user == null)
            //    return BadRequest("Invalid refresh token!");
            BookResponse? response = await bookService.DeleteReturnRequest(request, 1);
            if (!response.Succeeded)
                return BadRequest(response.Message);
            return Ok(response.Message);
        }

        [HttpPost]
        public async Task<IActionResult> ReserveBook(BookRequest request)
        {
            //User? user = await GetUserFromToken();
            //if (user == null)
            //    return BadRequest("Invalid refresh token!");
            BookResponse? response = await bookService.ReserveBook(request, 1);
            if (!response.Succeeded)
                return BadRequest(response.Message);
            return Ok(response.Message);
        }

        async Task<User?> GetUserFromToken()
        {
            string? refreshToken = Request.Cookies["token"];
            if (refreshToken == null)
                return null;
            User? user = await userService.GetUserWithRefreshToken(refreshToken);
            return user;
        }
    }
}
