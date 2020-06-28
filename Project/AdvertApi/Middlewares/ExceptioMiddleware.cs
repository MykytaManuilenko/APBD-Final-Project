using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using AdvertApi.Exceptions;
using Project_AdvertApi.Exceptions;

namespace AdvertApi.Middlewares
{
    public class ExceptioMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptioMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                if (_next != null) await _next(context);
            }
            catch(Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            if (ex is ClientHasAlreadyExistsException || ex is IncorrectPasswordException
                || ex is BuildingsOnDifferentStreetsException || ex is NotEnougBuildingsInTheDatabaseException
                || ex is WrongDateException || ex is BuildingsInDifferentCitiesException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                return context.Response.WriteAsync(new ErrorDetails
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = ex.Message
                }.ToString());
            }
            else if (ex is ClientDoesNotExistsException || ex is BuildingDoesNotExistsException
                     ||ex is RefreshTokenDoesNotExistsException)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                return context.Response.WriteAsync(new ErrorDetails
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = ex.Message
                }.ToString());
            }

            return context.Response.WriteAsync(ex.ToString());
        }
    }
}
