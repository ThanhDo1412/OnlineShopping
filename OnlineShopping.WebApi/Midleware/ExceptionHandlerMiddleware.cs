using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OnlineShopping.Database.Entity;
using OnlineShopping.Model.Common;
using OnlineShopping.Service.Interface;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.WebApi.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _log;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> log)
        {
            _next = next;
            _log = log;
        }

        public async Task Invoke(HttpContext context, IActivityService activityService)
        {
            try
            {
                await LogActivity(context, activityService);
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            string response;
            switch (exception)
            {
                //Custom error showing to user
                case CustomException e:
                    response = JsonConvert.SerializeObject(new { e.ErrorCode, e.ErrorMessage });
                    _log.LogError(e, "Error Code: {@ErrorCode} with Message: {@ErrorMessage}", e.ErrorCode, e.ErrorMessage);
                    break;
                //Exception by system
                default:
                    //return system Error and log error to DB
                    var sb = new StringBuilder();
                    var currentException = exception;

                    while (currentException != null)
                    {
                        sb.AppendLine(currentException.StackTrace);
                        currentException = currentException.InnerException;
                    }

                    var returnError = new CustomException(HttpStatusCode.InternalServerError);
                    response = JsonConvert.SerializeObject(new { returnError.ErrorCode, returnError.ErrorMessage });

                    _log.LogError(exception, "Error: {@Message} with StackTrace: {@sb}", exception.Message, sb.ToString());
                    break;
            }
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync(response);
        }

        private async Task LogActivity(HttpContext context, IActivityService activityService)
        {
            if (context.Request.Path.Value.Contains("product/search"))
            {
                await activityService.CreateNewActivity(context.Request.QueryString.Value, ActivityType.Search);
            }
            if (context.Request.Path.Value.Contains("product") && context.Request.RouteValues.Count == 3 && context.Request.RouteValues.Keys.Contains("id"))
            {
                await activityService.CreateNewActivity(context.Request.RouteValues["id"].ToString(), ActivityType.ViewDetail);
            }
        }
    }
}
