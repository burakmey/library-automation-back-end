namespace library_automation_back_end.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly AuthService authService;
        readonly TokenService tokenService;

        public AuthController(AuthService authService, TokenService tokenService)
        {
            this.authService = authService;
            this.tokenService = tokenService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            User? user = await authService.GetUser(request);
            if (user == null)
                return BadRequest("Incorrect email or password!");

            Token token = tokenService.CreateAccessToken(user);
            await authService.UpdateRefreshToken(user, token);
            ICollection<UserDesire>? userDesires = await authService.GetUserDesires(user.Id);
            LoginResponse response = new(user, userDesires, token);
            HttpContext.Response.Cookies.Append("token", token.RefreshToken, new CookieOptions { HttpOnly = true, Secure = true, IsEssential = true, SameSite = SameSiteMode.None });
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            RegisterResponse response = await authService.CreateUser(request);
            if (!response.Succeeded)
                return BadRequest(response.Message);

            return Ok(response.Message);
        }

        [HttpPost]
        public async Task<IActionResult> Refresh()
        {
            string refreshToken;
            if (Request.Cookies["token"] != null)
                refreshToken = Request.Cookies["token"] ?? throw new Exception("RefreshToken problem!");
            else
                return BadRequest("RefreshToken is not found!");

            User? user = await authService.GetUserWithRefreshToken(refreshToken);
            if (user == null)
                return BadRequest("Invalid refresh token!");

            Token token = tokenService.CreateAccessToken(user, refreshToken);
            ICollection<UserDesire>? userDesires = await authService.GetUserDesires(user.Id);
            LoginResponse response = new(user, userDesires, token);
            return Ok(response);
        }
    }
}
