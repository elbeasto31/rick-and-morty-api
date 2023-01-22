using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RickAndMortyAPI.Utils.Extensions
{
    public static class ContextExtensions
    {
        public static void SetActionResult(this ExceptionContext context, ObjectResult result)
        {
            context.Result = result;
            context.ExceptionHandled = true;
        }
    }
}