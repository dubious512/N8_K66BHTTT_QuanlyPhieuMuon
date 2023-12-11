using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreWeb_MVC.Models;

namespace CoreWeb_MVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooks()
        {
            // Giả sử bạn đã có một danh sách các sách từ nguồn dữ liệu (database hoặc dữ liệu tĩnh)
            var books = new List<Book>
            {
                new Book { book_id = 1, book_name = "Tâm lý học" },
                new Book { book_id = 2, book_name = "Vấn đề kỹ năng" },
                new Book {book_id = 3, book_name = "Triết học Mác-Lênin" },
                new Book {book_id = 4, book_name = "Lập trình ASP.Net" },
                new Book {book_id = 5, book_name = "300 bài Code thiếu nhi" },
                new Book {book_id = 6, book_name = "Lan và Điệp" }
            };

            return books;
        }
    }
}
