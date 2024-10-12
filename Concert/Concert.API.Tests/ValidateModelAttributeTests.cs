using Concert.API.CustomActionFilters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Concert.API.Tests
{
    public class ValidateModelAttributeTests
    {
        [Fact]
        public async Task OnActionExecuting_InvokeWithoutModelStateErrors_ResultNull()
        {
            // Arrange 
            var validateModelAttributeActionFilter = new ValidateModelAttribute();

            var httpContext = new DefaultHttpContext();

            var actionContext = new ActionContext(httpContext, new(), new(), new());

            var actionExecutingContext = new ActionExecutingContext(actionContext,
                new List<IFilterMetadata>(),
                new Dictionary<string, object?>(),
                controller: null);

            // Act
            validateModelAttributeActionFilter.OnActionExecuting(actionExecutingContext);

            // Assert
            Assert.Null(actionExecutingContext.Result);
        }

        [Fact]
        public async Task OnActionExecuting_InvokeWithModelStateErrors_ResultBadRequest()
        {
            // Arrange 
            var validateModelAttributeActionFilter = new ValidateModelAttribute();

            var httpContext = new DefaultHttpContext();

            var actionContext = new ActionContext(httpContext, new(), new(), new());
            actionContext.ModelState.AddModelError("test", "This is a validation error");
            var actionExecutingContext = new ActionExecutingContext(actionContext,
                new List<IFilterMetadata>(),
                new Dictionary<string, object?>(),
                controller: null);

            // Act
            validateModelAttributeActionFilter.OnActionExecuting(actionExecutingContext);

            // Assert
            Assert.IsType<BadRequestResult>(actionExecutingContext.Result);
        }
    }
}