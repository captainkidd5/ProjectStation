using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ProjectStation.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorModel : PageModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        private readonly ILogger<ErrorModel> _logger;

        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }

        //public IActionResult HTTPStatusCodeHandler(int statusCode)
        //{
        //    var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

        //    switch (statusCode)
        //    {
        //        case 404:

        //    }



        //}

        public IActionResult OnGet(int id)
        {
            IStatusCodeReExecuteFeature statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch(id)
            {
                case 404:
                    _logger.LogWarning($"404 error occured. Path = {statusCodeResult.OriginalPath}");
                    _logger.LogWarning($"404 error occured. Query string = {statusCodeResult.OriginalQueryString}");
                    break;
            }
            

            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;


            _logger.LogError($"Error RequestID: {RequestId.ToString()} ");


            //_logger.LogError($"The path {exceptionDetails.Path} threw an exception " +
            //    $"{exceptionDetails.Error}");

            return Page();

        }
    }
}
