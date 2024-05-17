namespace library_automation_back_end.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        readonly LibraryService libraryService;

        public LibraryController(LibraryService libraryService)
        {
            this.libraryService = libraryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBook([FromQuery] GetBookRequest request)
        {
            GetBookResponse? response = await libraryService.GetBook(request);
            if (response == null)
                return BadRequest("Invalid book id!");
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> SearchBook([FromQuery] SearchBookRequest request)
        {
            SearchResultResponse? response = await libraryService.GetSearchedBooks(request);
            if (response == null)
                return BadRequest("There is no book!");
            return Ok(response);
        }
    }
}
