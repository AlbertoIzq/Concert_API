using Concert.API.CustomActionFilters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Concert.API.Tests
{
    public class ValidateModelAttributeTests
    {
        private readonly ValidateModelAttribute _validateModelAttributeActionFilter;
        private readonly DefaultHttpContext _httpContext;
        private readonly ActionContext _actionContext;

        // Arrange
        public ValidateModelAttributeTests()
        {
            _validateModelAttributeActionFilter = new ValidateModelAttribute();
            _httpContext = new DefaultHttpContext();
            _actionContext = new ActionContext(_httpContext, new(), new(), new());
        }

        [Fact]
        public async Task OnActionExecuting_InvokeWithoutModelStateErrors_ResultNull()
        {
            // Arrange 
            var actionExecutingContext = new ActionExecutingContext(_actionContext,
                new List<IFilterMetadata>(),
                new Dictionary<string, object?>(),
                controller: null);

            // Act
            _validateModelAttributeActionFilter.OnActionExecuting(actionExecutingContext);

            // Assert
            Assert.Null(actionExecutingContext.Result);
        }

        [Fact]
        public async Task OnActionExecuting_InvokeWithModelStateErrors_ResultBadRequest()
        {
            // Arrange 
            _actionContext.ModelState.AddModelError("test", "This is a validation error");
            var actionExecutingContext = new ActionExecutingContext(_actionContext,
                new List<IFilterMetadata>(),
                new Dictionary<string, object?>(),
                controller: null);

            // Act
            _validateModelAttributeActionFilter.OnActionExecuting(actionExecutingContext);

            // Assert
            Assert.IsType<BadRequestResult>(actionExecutingContext.Result);
        }
    }
}