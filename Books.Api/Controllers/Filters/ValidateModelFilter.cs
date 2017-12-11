using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Books.Api.Controllers.Filters
{
    // Check to make sure model is valid before calling any subsequent action
    public class ValidateModelFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                // JsonApiDotNetCore isn't able to format the response for this if it is a patch and model state is invalid
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}