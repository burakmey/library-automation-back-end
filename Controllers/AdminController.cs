namespace library_automation_back_end.Controllers
{
    [Authorize(Policy = "Admin")]
    [ApiController]
    [Route("api/[controller]/[action]")]
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

        [HttpPost]
        public async Task<IActionResult> AddBook(AddBookRequest request)
        {
            AddResponse response = await adminService.AddBook(request);
            if (!response.Succeeded)
                return BadRequest(response.Message);
            return Ok(response.Message);
        }

        [HttpPost]
        public async Task<IActionResult> AddPublisher(AddPublisherRequest request)
        {
            AddResponse response = await adminService.AddPublisher(request);
            if (!response.Succeeded)
                return BadRequest(response.Message);
            return Ok(response.Message);
        }


        [HttpPost]
        public async Task<IActionResult> AddAuthor(AddAuthorRequest request)
        {
            AddResponse response = await adminService.AddAuthor(request);
            if (!response.Succeeded)
                return BadRequest(response.Message);
            return Ok(response.Message);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(AddCategoryRequest request)
        {
            AddResponse response = await adminService.AddCategory(request);
            if (!response.Succeeded)
                return BadRequest(response.Message);
            return Ok(response.Message);
        }
    }
}
