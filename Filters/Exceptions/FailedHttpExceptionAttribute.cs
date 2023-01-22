using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RickAndMortyAPI.Utils.Constants;
using RickAndMortyAPI.Utils.Exceptions;
using RickAndMortyAPI.Utils.Extensions;

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
                context.SetActionResult(new NotFoundObjectResult(Messages.NotFoundMessage));
                return;
            }

            context.SetActionResult(new BadRequestObjectResult(Messages.BadRequestMessage));
        }
    }
}