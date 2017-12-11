using System;
using System.Collections.Generic;
using FluentValidation;
using JsonApiDotNetCore.Models;

namespace Books.Api.Models
{
    public class Author : Identifiable
    {
        [Attr("first-name")]
        public string FirstName { get; set; }
        
        [Attr("last-name")]
        public string LastName { get; set; }
        
        [HasMany("books")]
        public virtual List<Book> Books { get; set; }
    }
    
    // Input validation done on model when api is called
    public class AuthorValidator : AbstractValidator<Author>
    {
        public AuthorValidator()
        {
            RuleFor(x => x.FirstName).NotNull().NotEmpty();
            RuleFor(x => x.LastName).NotNull().NotEmpty();
        }
    }
}