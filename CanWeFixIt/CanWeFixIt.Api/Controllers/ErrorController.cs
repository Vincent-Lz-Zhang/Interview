using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace CanWeFixIt.Api.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult HandleError()
        {
            var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>()!;

            if (exceptionHandlerFeature.Error is ArgumentException) 
                // Or other exceptions caused by bad data
            {
                return Problem(
                           detail: "User provided invalid data",
                           title: "Client mistake",
                           statusCode: (int)HttpStatusCode.BadRequest);
            }

            return Problem(
                    detail: "Ooops!",
                    title: "Server mistake",
                    statusCode: (int)HttpStatusCode.InternalServerError);

        }
    }
}
