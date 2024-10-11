using AutoMapper;
using Concert.API.Controllers;
using Concert.API.CustomActionFilters;
using Concert.API.Mappings;
using Concert.API.Models.Domain;
using Concert.API.Models.DTO;
using Concert.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Moq;

namespace Concert.API.Tests
{
    public class ValidateModelAttributeTests
    {
        [Fact]
        public async Task OnActionExecuting_InvokeWithoutModelStateErrors_ReturnsNull()
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
    }
}