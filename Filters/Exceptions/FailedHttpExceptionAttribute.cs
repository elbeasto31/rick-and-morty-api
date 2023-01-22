using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RickAndMortyAPI.Utils.Constants;
using RickAndMortyAPI.Utils.Exceptions;

namespace RickAndMortyAPI.Filters.Exceptions
{
    public class FailedHttpExceptionAttribute : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is not FailedHttpRequestException failedHttpRequestException)
                return;

            if (failedHttpRequestException.StatusCode == HttpStatusCode.NotFound)
            {
                SetActionResult(context, new NotFoundObjectResult(Messages.NotFoundMessage));
                return;
            }

            SetActionResult(context, new BadRequestObjectResult(Messages.BadRequestMessage));
        }

        private void SetActionResult(ExceptionContext context, ObjectResult result)
        {
            context.Result = result;
            context.ExceptionHandled = true;
        }
    }
}