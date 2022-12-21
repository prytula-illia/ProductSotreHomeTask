using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace ProductSotre.Filters
{
    public class ActionLoggingAttribute : ActionFilterAttribute
    {
        private bool _logParameters;

        public ActionLoggingAttribute(bool logParameters = false)
        {
            _logParameters = logParameters;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (_logParameters)
            {
                Log.Information($"Action starts executing: {context.HttpContext.Request.Method} {context.HttpContext.Request.Path}", context.ActionArguments);
            }
            else
            {
                Log.Information($"Action starts executing: {context.HttpContext.Request.Method} {context.HttpContext.Request.Path}");
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Log.Information($"Action ends executing: {context.HttpContext.Request.Method} {context.HttpContext.Request.Path}");
        }
    }
}