using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ShoppingMart.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingMart.ProductAPI.Controllers
{
    public class BaseController : ControllerBase
    {
        private readonly ILogger _logger;

        public BaseController(ILogger logger)
        {
            _logger = logger;
        }

        protected IActionResult Ok<T>(T result)
        {
            if (result?.GetType() == typeof(Envelope<>)) 
                return base.Ok(result);
            return base.Ok(Envelope.Ok(result));
        }

        protected IActionResult Done<T>(Envelope<T> envelope)
        {
            if (envelope.Status == ResultStatus.Failed) 
                return BadRequest(envelope);
            return base.Ok(envelope);
        }

        protected IActionResult Failure(Dictionary<string, string> errors, string message = "One or more errors occured")
        {
            return BadRequest(Envelope.Error(errors, message));
        }

        protected IActionResult NotFoundError(string errorMessage = "Item not found!")
        {
            return NotFound(Envelope.Error(errorMessage));
        }

        protected IActionResult Failure(string errorMessage)
        {
            return BadRequest(Envelope.Error(errorMessage));
        }

        protected void LogInfo(string message, object data = null)
        {
            Log(message, LogStatus.Info, requestData: data);
        }

        protected void LogError(string message, Exception ex = null, object data = null)
        {
            Log(message, LogStatus.Error, ex, requestData: data);
        }

        protected void LogCritical(string message, Exception ex = null, object requestData = null)
        {
             Log(message, LogStatus.Critical, ex, requestData);
        }


        private void Log(string message, LogStatus logStatus, Exception ex = null, object requestData = null)
        {
            string formattedMessage =
                ex == null && requestData == null ? message
                : JsonConvert.SerializeObject(new
                {
                    Message = message,
                    Exception = ex,
                    RequestData = requestData
                });

            switch (logStatus)
            {
                case LogStatus.Error:
                    _logger.LogError(formattedMessage);
                    break;
                case LogStatus.Info:
                    _logger.LogInformation(formattedMessage);
                    break;
                default:
                    _logger.LogCritical(formattedMessage);
                    break;
            }

        }
    }
}
