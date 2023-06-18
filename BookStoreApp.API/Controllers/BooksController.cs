using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoreApp.API.Data;
using BookStoreApp.API.Repositories.Abstract;
using BookStoreApp.API.Repositories.Concrete;
using BookStoreApp.API.Static;
using BookStoreApp.API.ViewModels.BooksViewModels;
using BookStoreApp.API.Validations.BookValidations;
using BookStoreApp.API.ViewModels.AuthorViewModels;
using Microsoft.AspNetCore.Authorization;

namespace BookStoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class BooksController : ControllerBase
    {
        private readonly IBooksRepository _booksRepository;
        private readonly ILogger<BooksController> _logger;

        public BooksController(IBooksRepository booksRepository,
                                ILogger<BooksController> logger)
        {
            this._booksRepository = booksRepository;
            this._logger = logger;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            _logger.LogInformation($"Processing GET request for {nameof(GetBooks)}");
            try
            {
                var books = await _booksRepository.GetBooksAsync();

                if(books == null )
                {
                    _logger.LogWarning($"Records Not Found");
                    return NoContent();
                }

                return Ok(value: books);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Error performing GET in {nameof(GetBooks)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            _logger.LogInformation($"Processing GET request for {nameof(GetBook)}");
            try
            {
                var book = await _booksRepository.GetBookAsync(id);

                if (book == null)
                {
                    _logger.LogWarning("Record Not Found: Id - {id}", id);
                    return NotFound();
                }

                return Ok(value: book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error performing GET in {nameof(GetBook)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutBook(int id, BookUpdateVM bookUpdate)
        {
            _logger.LogInformation($"Processing GET request for {nameof(PutBook)}");
            try
            {
                var validationResponse = await new BookUpdateVMValidation().ValidateAsync(bookUpdate);

                if(!validationResponse.IsValid)
                {
                    _logger.LogWarning("Book Validation Failed: Endpoint {PutBook} | BookUpdate - {@bookUpdate}", nameof(PutBook), bookUpdate);
                    List<string> errors = new();
                    foreach (var error in validationResponse.Errors)
                        errors.Add(error.ErrorMessage);
                    return BadRequest(error: errors);
                }

                var updateResult = await _booksRepository.UpdateBookAsync(id, bookUpdate);

                if (updateResult == null)
                {
                    _logger.LogWarning("Record Not Found: Endpoint - {PutBook} | Id - {id}", nameof(PutBook), id);
                    return NotFound();
                }

                return Ok(value: updateResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error performing GET in {nameof(PutBook)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PostBook(BookCreateVM bookCreate)
        {
            _logger.LogInformation($"Processing GET request for {nameof(PostBook)}");
            try
            {
                var validationResponse = await new BookCreateVMValidation().ValidateAsync(bookCreate);

                if (!validationResponse.IsValid)
                {
                    _logger.LogWarning("Book Validation Failed: Endpoint {PostBook} | BookCreate - {@bookCreate}", nameof(PutBook), bookCreate);
                    List<string> errors = new();
                    foreach (var error in validationResponse.Errors)
                        errors.Add(error.ErrorMessage);
                    return BadRequest(error: errors);
                }

                var updateResult = await _booksRepository.CreateBookAsync(bookCreate);

                if (updateResult == null)
                {
                    _logger.LogWarning("ISBN Value Already Exists Or Author Does Not Exist: Endpoint - {PostBook} | BookCreate - {bookCreate}", nameof(PutBook), bookCreate);
                    return BadRequest(error: "ISBN Value Already Exists Or Author Does Not Exist");
                }

                return CreatedAtAction(nameof(GetBook), new { id = updateResult.Id }, updateResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error performing GET in {nameof(PostBook)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            _logger.LogInformation($"Processing DELETE request for {nameof(DeleteBook)}");
            try
            {
                var deleteResult = await _booksRepository.DeleteBookAsync(id);

                if (deleteResult)
                    return Ok();

                _logger.LogWarning("Record Not Found: Endpoint - {DeleteBook} | Id - {id}", nameof(DeleteBook), id);
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error performing DELETE in {nameof(DeleteBook)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }
    }
}
