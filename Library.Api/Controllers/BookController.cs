using Common.Models;
using Library.BusinessLayer.Interfaces;
using Library.BusinessLayer.Managers;
using Library.Common.Exceptions;
using Library.DataLayer;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [Route("LibraryAPI/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookManager bookManager;

        /// <summary>
        /// Initialize books controller
        /// </summary>
        /// <param name="bookManager"></param>
        public BookController(IBookManager bookManager)
        {
            this.bookManager = bookManager;
        }
        
        /// <summary>
        /// Get all books from database
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var books = await bookManager.GetAll();
            return Ok(books);
        }

        /// <summary>
        /// Get a book 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{bookPublicID}")]
        public async Task<IActionResult> Get(Guid bookPublicID)
        {
            try
            {
                var book = await bookManager.Get(bookPublicID);
                return Ok(book);
            }
            catch (BookNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Add a new book
        /// </summary>
        /// <param name="bookModel"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] BookModel bookModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await bookManager.Add(bookModel);
                return CreatedAtAction(nameof(Get), new { bookPublicID = bookModel.BookPublicID }, bookModel);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Update a book
        /// </summary>
        /// <param name="bookModel"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] BookModel bookModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedBook = await bookManager.Update(bookModel);
                return Ok(updatedBook);
            }
            catch (BookNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Remove a book
        /// </summary>
        /// <param name="bookPublicID"></param>
        /// <returns></returns>
        [HttpDelete("Delete/{bookPublicID}")]
        public async Task<IActionResult> Delete(Guid bookPublicID)
        {
            try
            {
                await bookManager.Delete(bookPublicID);
                return NoContent();
            }
            catch (BookNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
