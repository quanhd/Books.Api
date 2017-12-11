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
    public class BookService : EntityResourceService<Book>
    {
        private AppDataContext _dbContext;
        public BookService(IJsonApiContext jsonApiContext, IEntityRepository<Book> entityRepository, ILoggerFactory loggerFactory, AppDataContext dbContext) 
                                                                                                        : base(jsonApiContext, entityRepository, loggerFactory)
        {
            _dbContext = dbContext;
        }

        public override async Task<Book> CreateAsync(Book book)
        {
            await ValidateAuthor(book).ConfigureAwait(false);
            
            return await base.CreateAsync(book).ConfigureAwait(false);

        }

        private async Task ValidateAuthor(Book book)
        {
            var author = await _dbContext.Authors.FirstOrDefaultAsync(x => x.Id == book.AuthorId).ConfigureAwait(false);
                        
            if (author == null)
                throw new ArgumentException($"AuthorId: {book.AuthorId} does not exist in the system.");
        }

        public override async Task<Book> UpdateAsync(int id, Book book)
        {
            book.Id = id;
            await ValidateAuthor(book).ConfigureAwait(false);

            return await base.UpdateAsync(id, book).ConfigureAwait(false);
        }
    }
}