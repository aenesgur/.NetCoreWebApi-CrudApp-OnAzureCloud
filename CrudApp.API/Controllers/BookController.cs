using CrudApp.API.Model;
using CrudApp.BLL.Abstract;
using CrudApp.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudApp.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private IBookManager _bookManager;
        private const string ERROR_MESSAGE = "There is no data related with ID: {0}";


        public BookController(IBookManager bookManager)
        {
            _bookManager = bookManager;
        }

        [HttpGet]
        [HttpGet("{page}")]
        public async Task<ActionResult> GetAll(int page)
        {
            var bookList = await _bookManager.Search(page);
            return StatusCode(200, bookList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var book = await _bookManager.GetById(id);
            if (book == null)
                return StatusCode(403, new ErrorModel { ErrorMessage = String.Format(ERROR_MESSAGE, id) } );

            return StatusCode(200, book);
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] Book book)
        {
            var isCreated = await _bookManager.Add(book);
            if (!isCreated)
                return StatusCode(500);

            return StatusCode(201);
        }
        [HttpPost]
        public async Task<ActionResult> AddList([FromBody] List<Book> bookList)
        {
            var isCreated = await _bookManager.AddList(bookList);
            if (!isCreated)
                return StatusCode(500);

            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var isDeleted = await _bookManager.Delete(id);
            if (!isDeleted)
                return StatusCode(403, new ErrorModel { ErrorMessage = String.Format(ERROR_MESSAGE, id) });

            return StatusCode(204);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Book model)
        {
            var isUpdated = await _bookManager.Update(id, model);
            if (!isUpdated)
                return StatusCode(403, new ErrorModel { ErrorMessage = String.Format(ERROR_MESSAGE, id) });

            return StatusCode(204);
        }

    }
}