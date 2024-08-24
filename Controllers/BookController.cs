using Microsoft.AspNetCore.Mvc;
using WebApplication1.Infrastructure;
using WebApplication1.Entities;
using System.Text.Json;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        private readonly IBookService _book;

        public BookController(IBookService book)
        {
            _book = book;
        }

        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] Book book)
        {
            string result = await _book.Add(book);

            if (result.Length == 0)
            {
                return Ok("Книга успешно добавлена");
            }

            return BadRequest(result);
        }

        [Route("GetAll")]
        public async Task<IActionResult> Get()
        {
            IEnumerable<Book> books = await _book.GetAll();
            if (books.Count() == 0)
            {
                return NoContent();
            } else
            {
                return Ok(books);
            }
        }

        [Route("Delete/{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            bool result = await _book.Remove(id);

            if (result == true)
            {
                return NoContent();
            } else
            {
                return BadRequest("Произошла ошибка. Книга для удаления не найдена");
            }
        }

        [Route("Update")]
        public async Task<IActionResult> Update(Book book)
        {
            string errorMessage = await _book.Update(book);

            if (errorMessage.Length == 0)
            {
                return NoContent();
            } else
            {
                return BadRequest(errorMessage);
            }
        }
    }
}
