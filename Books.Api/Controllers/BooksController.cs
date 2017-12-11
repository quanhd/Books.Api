using System.Threading.Tasks;
using Books.Api.Controllers.Filters;
using Books.Api.Models;
using JsonApiDotNetCore.Controllers;
using JsonApiDotNetCore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Books.Api.Controllers
{
    [ValidateModelFilter]
    [Route("api/[controller]")]
    public class BooksController : JsonApiController<Book>
    {
        
        private readonly IResourceService<Review> _reviewService;
        public BooksController(IJsonApiContext jsonApiContext, IResourceService<Book> resourceService, IResourceService<Review> reviewService,
                                ILoggerFactory loggerFactory) : base(jsonApiContext, resourceService, loggerFactory)
        {
            _reviewService = reviewService;
        }
        
        [HttpPost("{bookId}/relationships/reviews")]
        public async Task<IActionResult> PostRelationshipAsync(int bookId, [FromBody] Review review)
        {
            review.BookId = bookId;
            var result = await _reviewService.CreateAsync(review);

            return Ok(result);
        }
        
        [HttpDelete("{bookId}/relationships/reviews/{reviewId}")]
        public async Task<IActionResult> PostRelationshipAsync(int bookId, int reviewId)
        {
            var result = await _reviewService.DeleteAsync(reviewId);

            return Ok(result);
        }
    }
}