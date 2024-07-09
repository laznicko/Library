using Common.Models;
using Library.BusinessLayer;
using Library.BusinessLayer.Interfaces;
using Library.BusinessLayer.Managers;
using Library.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [Route("Library/[controller]")]
    [ApiController]
    public class BorrowController : ControllerBase
    {
        private readonly IBorrowManager borrowManager;

        public BorrowController(IBorrowManager borrowManager)
        {
            this.borrowManager = borrowManager;
        }

        /// <summary>
        /// Get all borrowa from database
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var borrows = await borrowManager.GetAll();
            return Ok(borrows);
        }

        /// <summary>
        /// Creates a new borrow
        /// </summary>
        /// <param name="borrowModel"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public async Task<IActionResult> Add(Guid userPublicID, Guid bookPubliciD)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdBorrow = await borrowManager.Add(userPublicID, bookPubliciD);
                return Ok(createdBorrow);
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (BookNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (BookAlreadyBorrowedException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }

        }

        /// <summary>
        /// Confirms the return of a borrowed
        /// </summary>
        /// <param name="borrowPublicID"></param>
        /// <returns></returns>
        [HttpPost("ConfirmReturn")]
        public async Task<IActionResult> ConfirmReturn(Guid borrowPublicID)
        {
            try
            {
                await borrowManager.ConfirmReturn(borrowPublicID);
                return NoContent();
            }
            catch (BorrowNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (BookNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            };
        }
    }
}
