using System.Linq;
using System.Threading.Tasks;
using Books.Api.Controllers.Filters;
using Books.Api.Data;
using Books.Api.Models;
using JsonApiDotNetCore.Controllers;
using JsonApiDotNetCore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Books.Api.Controllers
{
    [Route("api/[controller]")]
    [ValidateModelFilter]
    public class AuthorsController : JsonApiController<Author>
    {
        private readonly IResourceService<Book> _bookService;
        public AuthorsController(IJsonApiContext jsonApiContext, IResourceService<Author> resourceService, IResourceService<Book> bookService,
            ILoggerFactory loggerFactory) : base(jsonApiContext, resourceService, loggerFactory)
        {
            _bookService = bookService;
        }
        
        [HttpPost("{authorId}/relationships/books")]
        public async Task<IActionResult> PostRelationshipAsync(int authorId, [FromBody] Book book)
        {
            book.AuthorId = authorId;
            var result = await _bookService.CreateAsync(book);

            return Ok(result);
        }
        
        [HttpDelete("{authorId}/relationships/books/{bookId}")]
        public async Task<IActionResult> DeleteRelationshipAsync(int authorId, int bookId)
        {
            var result = await _bookService.DeleteAsync(bookId);

            return Ok(result);
        }
    }
}