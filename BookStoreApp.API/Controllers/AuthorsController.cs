using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoreApp.API.Data;
using AutoMapper;
using BookStoreApp.API.ViewModels.AuthorViewModels;
using BookStoreApp.API.Validations.AuthorValidations;
using BookStoreApp.API.Repositories.Abstract;
using BookStoreApp.API.Static;

namespace BookStoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly ILogger<AuthorsController> _logger;

        public AuthorsController(IAuthorRepository authorRepository,
                                ILogger<AuthorsController> logger)
        {
            this._authorRepository = authorRepository;
            this._logger = logger;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            _logger.LogInformation($"Processing GET request for {nameof(GetAuthors)}");
            try
            {
                var authors = await _authorRepository.GetAuthorsAsync();

                if (authors == null)
                {
                    _logger.LogWarning($"Records Not Found");
                    return NoContent();
                }

                return Ok(value: authors);

            }catch(Exception ex)
            {
                _logger.LogError(ex, $"Error performing GET in {nameof(GetAuthors)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // GET: api/Authors/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAuthor(int id)
        {
            _logger.LogInformation($"Processing GET request for {nameof(GetAuthor)}");
            try
            {
                var authorVM = await _authorRepository.GetAuthorAsync(id);

                if (authorVM == null)
                {
                    _logger.LogWarning("Record Not Found: Id - {id}", id);
                    return NotFound();
                }

                return Ok(authorVM);

            }catch(Exception ex)
            {
                _logger.LogError(ex, $"Error performing GET in {nameof(GetAuthor)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutAuthor([FromRoute] int id, [FromBody] AuthorUpdateVM authorUpdate)
        {
            _logger.LogInformation($"Processing PUT request for {nameof(PutAuthor)}");
            try
            {
                var validationresponse = await new AuthorUpdateVMValidation().ValidateAsync(authorUpdate);

                if (!validationresponse.IsValid)
                {
                    _logger.LogWarning("Author Validation Failed: Endpoint - {PutAuthor} | AuthorUpdate - {@authorUpdate}", nameof(PutAuthor), authorUpdate);
                    List<string> errors = new();
                    foreach (var error in validationresponse.Errors)
                        errors.Add(error.ErrorMessage);
                    return BadRequest(error: errors);
                }

                var updateResult = await _authorRepository.UpdateAuthorAsync(id, authorUpdate);

                if (updateResult == null)
                {
                    _logger.LogWarning("Record Not Found: Endpoint - {PutAuthor} | Id - {id}", nameof(PutAuthor), id);
                    return NotFound();
                }

                return Ok(updateResult);

            }catch(Exception ex)
            {
                _logger.LogError(ex, $"Error performing PUT in {nameof(PutAuthor)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostAuthor(AuthorCreateVM authorCreate)
        {
            _logger.LogInformation($"Processing POST request for {nameof(PostAuthor)}");
            try
            {
                var validationResponse = await new AuthorCreateVMValidation().ValidateAsync(authorCreate);

                if (validationResponse.IsValid)
                {
                    var author = await _authorRepository.CreateAuthorAsync(authorCreate);
                    return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
                }
                else
                {
                    _logger.LogWarning("Author Validation Failed : Endpoint - {PostAuthor} | AuthorCreate - {@authorCreate}", nameof(PostAuthor), authorCreate);
                    List<string> errors = new List<string>();
                    foreach (var error in validationResponse.Errors)
                        errors.Add(error.ErrorMessage);
                    return BadRequest(error: errors);
                }
            }catch(Exception ex)
            {
                _logger.LogError(ex, $"Error performing POST in {nameof(PostAuthor)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAuthor([FromRoute] int id)
        {
            _logger.LogInformation($"Processing DELETE request for {nameof(DeleteAuthor)}");
            try
            {
                var deleteResult = await _authorRepository.DeleteAuthorAsync(id);

                if (deleteResult)
                    return Ok();

                _logger.LogWarning("Record Not Found: Endpoint - {DeleteAuthor} | Id - {id}", nameof(DeleteAuthor), id);
                return NotFound();
            }catch(Exception ex)
            {
                _logger.LogError(ex, $"Error performing DELETE in {nameof(DeleteAuthor)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }
    }
}
