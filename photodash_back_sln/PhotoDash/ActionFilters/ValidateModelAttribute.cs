using Contracts.Logger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoDash.ActionFilters
{
    public class ValidateModelAttribute : IActionFilter
    {

        private readonly ILoggerManager _logger;

        public ValidateModelAttribute(ILoggerManager logger)
        {
            _logger = logger;
        }


        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var actionName = context.RouteData.Values["action"];
            var controllerName = context.RouteData.Values["controller"];

            if(!context.ModelState.IsValid)
            {
                _logger.LogError($"Invalid object sent from client. Controller:{controllerName} Action:{actionName}");
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
            }
        }
    }
}
