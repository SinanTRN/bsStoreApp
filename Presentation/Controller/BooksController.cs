using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controller
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly IServicesManager _manager;

        public BooksController(IServicesManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books = _manager.BookService.GetAllBooks(false);
            return Ok(books);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetOneBook([FromRoute(Name = "id")] int id)
        {
            var book = _manager.BookService.GetOneBookById(id, false);
            return Ok(book);
        }
        [HttpPost]
        public IActionResult CreateOneBook([FromBody] Book book)
        {
            if (book is null) return BadRequest();

            _manager.BookService.CreateOneBook(book);
            return StatusCode(201, book);
        }
        [HttpPut("{id:int}")]
        public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] BookDtoForUpdate bookDto)
        {
            if (bookDto is null) return BadRequest();
            //Check book
            _manager.BookService.UpdateOneBook(id, bookDto, true);
            return NoContent();//204
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneBook([FromRoute(Name = "id")] int id)
        {
            _manager.BookService.DeleteOneBook(id, false);
            return NoContent();//204
        }
        [HttpPatch("{id:int}")]
        public IActionResult PartialyUpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<Book> bookPatch)
        {
            var entity = _manager.BookService.GetOneBookById(id, true);
            bookPatch.ApplyTo(entity);
            _manager.BookService.UpdateOneBook(id, new BookDtoForUpdate(entity.Id,entity.Title,entity.Price), true);
            return NoContent();//204
        }
    }
}
