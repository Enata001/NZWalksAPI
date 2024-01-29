using System.Net;

namespace NZWalks.API.Middlewares;

public class ExceptionsHandlerMiddleWare
{
    private readonly ILogger<ExceptionsHandlerMiddleWare> _logger;
    private readonly RequestDelegate _request;

    public ExceptionsHandlerMiddleWare(ILogger<ExceptionsHandlerMiddleWare> logger, RequestDelegate request)
    {
        _logger = logger;
        _request = request;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _request(httpContext);

        }
        catch (Exception e)
        {
            var errorId = Guid.NewGuid();
            _logger.LogError(e, $"{errorId}: e.Message");
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";

            var error = new
            {
                Id = errorId,
                Error = "Something went wrong! We are looking into it"
            };
            await httpContext.Response.WriteAsJsonAsync(error);
        }
    }

}