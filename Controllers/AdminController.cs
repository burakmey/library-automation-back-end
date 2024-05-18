namespace library_automation_back_end.Controllers
{
    //[Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        readonly AdminService adminService;

        public AdminController(AdminService adminService)
        {
            this.adminService = adminService;
        }

        [HttpPost]
        public async Task<IActionResult> GetAllDesires()
        {
            GetAllDesiresResponse? response = await adminService.GetAllDesires();
            if (response == null)
                return BadRequest("There is no approvel!");
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AcceptBorrow(DesireRequest request)
        {
            DesireResponse response = await adminService.AcceptBorrow(request);
            if (!response.Succeeded)
                return BadRequest(response.Message);
            return Ok(response.Message);
        }

        [HttpPost]
        public async Task<IActionResult> AcceptReserveBorrow(DesireRequest request)
        {
            DesireResponse response = await adminService.AcceptReserveBorrow(request);
            if (!response.Succeeded)
                return BadRequest(response.Message);
            return Ok(response.Message);
        }

        [HttpPost]
        public async Task<IActionResult> AcceptReturn(DesireRequest request)
        {
            DesireResponse response = await adminService.AcceptReturn(request);
            if (!response.Succeeded)
                return BadRequest(response.Message);
            return Ok(response.Message);
        }

        [HttpDelete]
        public async Task<IActionResult> RejectDesire(DesireRequest request)
        {
            DesireResponse response = await adminService.RejectDesire(request);
            if (!response.Succeeded)
                return BadRequest(response.Message);
            return Ok(response.Message);
        }
    }
}
