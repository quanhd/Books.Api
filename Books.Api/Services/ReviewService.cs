using System;
using System.Threading.Tasks;
using Books.Api.Data;
using Books.Api.Models;
using JsonApiDotNetCore.Data;
using JsonApiDotNetCore.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Books.Api.Services
{
    public class ReviewService : EntityResourceService<Review>
    {
        private AppDataContext _dbContext;
        public ReviewService(IJsonApiContext jsonApiContext, IEntityRepository<Review> entityRepository, ILoggerFactory loggerFactory, AppDataContext dbContext) 
            : base(jsonApiContext, entityRepository, loggerFactory)
        {
            _dbContext = dbContext;
        }

        public override async Task<Review> CreateAsync(Review review)
        {
            await ValidateBook(review).ConfigureAwait(false);
            
            return await base.CreateAsync(review).ConfigureAwait(false);

        }

        private async Task ValidateBook(Review review)
        {
            var book = await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == review.BookId).ConfigureAwait(false);
                        
            if (book == null)
                throw new ArgumentException($"BookId: {review.BookId} does not exist in the system.");
        }

        public override async Task<Review> UpdateAsync(int id, Review review)
        {
            await ValidateBook(review).ConfigureAwait(false);

            return await base.UpdateAsync(id, review).ConfigureAwait(false);
        }
    }
}