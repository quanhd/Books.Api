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
    [ValidateModelFilter]
    [Route("api/[controller]")]
    public class ReviewsController : JsonApiController<Review>
    {
        public ReviewsController(IJsonApiContext jsonApiContext, IResourceService<Review> reviewService, 
            ILoggerFactory loggerFactory) : base(jsonApiContext, reviewService, loggerFactory)
        {
        }
    }
}