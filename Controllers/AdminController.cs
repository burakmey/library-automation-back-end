using library_automation_back_end.Features.FeaturesAdmin.Requests;
using library_automation_back_end.Features.FeaturesAdmin.Responses;

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

        [HttpGet]
        public async Task<IActionResult> GetAllDesires()
        {
            GetAllDesiresResponse? response = await adminService.GetAllDesires();
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetBorrowedBooks()
        {
            GetBorrowedBooksResponse? response = await adminService.GetBorrowedBooks();
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetReservedBooks()
        {
            GetReservedBooksResponse? response = await adminService.GetReservedBooks();
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AcceptBorrow(DesireRequest request)
        {
            MessageResponse response = await adminService.AcceptBorrow(request);
            if (!response.Succeeded)
                return BadRequest(response.Message);
            return Ok(response.Message);
        }

        [HttpPost]
        public async Task<IActionResult> AcceptReserveBorrow(DesireRequest request)
        {
            MessageResponse response = await adminService.AcceptReserveBorrow(request);
            if (!response.Succeeded)
                return BadRequest(response.Message);
            return Ok(response.Message);
        }

        [HttpPost]
        public async Task<IActionResult> AcceptReturn(DesireRequest request)
        {
            MessageResponse response = await adminService.AcceptReturn(request);
            if (!response.Succeeded)
                return BadRequest(response.Message);
            return Ok(response.Message);
        }

        [HttpDelete]
        public async Task<IActionResult> RejectDesire(DesireRequest request)
        {
            MessageResponse response = await adminService.RejectDesire(request);
            if (!response.Succeeded)
                return BadRequest(response.Message);
            return Ok(response.Message);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(AddBookRequest request)
        {
            MessageResponse response = await adminService.AddBook(request);
            if (!response.Succeeded)
                return BadRequest(response.Message);
            return Ok(response.Message);
        }

        [HttpPost]
        public async Task<IActionResult> AddPublisher(AddPublisherRequest request)
        {
            MessageResponse response = await adminService.AddPublisher(request);
            if (!response.Succeeded)
                return BadRequest(response.Message);
            return Ok(response.Message);
        }


        [HttpPost]
        public async Task<IActionResult> AddAuthor(AddAuthorRequest request)
        {
            MessageResponse response = await adminService.AddAuthor(request);
            if (!response.Succeeded)
                return BadRequest(response.Message);
            return Ok(response.Message);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(AddCategoryRequest request)
        {
            MessageResponse response = await adminService.AddCategory(request);
            if (!response.Succeeded)
                return BadRequest(response.Message);
            return Ok(response.Message);
        }
    }
}
