using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Internal;
using JsonApiDotNetCore.Models;

namespace Books.Api.Models
{
    public class Book : Identifiable
    {
        [Attr("authorId")]
        public int AuthorId { get; set; }
        
        [HasOne("author")]
        public virtual Author Author { get; set; }
        
        [Attr("title")]
        public string Title { get; set; }
        
        [Attr("isbn")]
        public string ISBN { get; set; }
        
        [Attr("publish-date")]
        public DateTime PublishDate { get; set; }
        
        [HasMany("reviews")]
        public virtual List<Review> Reviews { get; set; }
    }

    // Input validation done on model when api is called
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
            RuleFor(x => x.AuthorId).NotNull().NotEmpty();
            RuleFor(x => x.Title).NotNull().NotEmpty();
            RuleFor(x => x.ISBN).NotNull().NotEmpty();
            RuleFor(x => x.PublishDate).NotNull().NotEmpty();
            RuleFor(x => x.ISBN).Length(13)
                .WithMessage(x => $"Current length of ISBN is {x.ISBN.Length}, allowed length is 13.");
            RuleFor(x => x.PublishDate).LessThanOrEqualTo(DateTime.Now).WithMessage("PublishDate cannot be in the future.");
        }
    }
}