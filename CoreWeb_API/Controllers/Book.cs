using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreWeb_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Book : ControllerBase
    {
        [HttpGet("GetBookNames")]
        public ActionResult<IEnumerable<string>> GetBookNames()
        {
            // Dữ liệu sẵn về book_name
            var bookNames = new List<string> { "Book 1", "Book 2", "Book 3", "Book 4", "Book 5" };

            return Ok(bookNames);
        }
    }
}
