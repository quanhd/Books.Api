using FluentValidation;
using JsonApiDotNetCore.Models;

namespace Books.Api.Models
{
    public class Review : Identifiable
    {
        public int BookId { get; set; }
        
        [HasOne("book")]
        public virtual Book Book { get; set; }
        
        [Attr("reviewer-name")]
        public string ReviewerName { get; set; }
        
        [Attr("body")]
        public string Body { get; set; }
    }
    
    // Input validation done on model when api is called
    public class ReviewValidator : AbstractValidator<Review>
    {
        public ReviewValidator()
        {
            RuleFor(x => x.ReviewerName).NotNull().NotEmpty();
            RuleFor(x => x.Body).NotNull().NotEmpty();
        }
    }
}