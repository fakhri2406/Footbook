using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using System.Security;

namespace Footbook.API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    
    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Not Found");
            await HandleExceptionAsync(context, ex.Message, StatusCodes.Status404NotFound);
        }
        catch (SecurityException ex)
        {
            _logger.LogWarning(ex, "Security Exception");
            await HandleExceptionAsync(context, ex.Message, StatusCodes.Status401Unauthorized);
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning(ex, "Access Denied");
            await HandleExceptionAsync(context, ex.Message, StatusCodes.Status403Forbidden);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "Invalid Argument");
            await HandleExceptionAsync(context, ex.Message, StatusCodes.Status400BadRequest);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Invalid Operation");
            await HandleExceptionAsync(context, ex.Message, StatusCodes.Status400BadRequest);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected Error");
            await HandleExceptionAsync(context, ex.Message, StatusCodes.Status500InternalServerError);
        }
    }
    
    private static async Task HandleExceptionAsync(HttpContext context, string message, int statusCode)
    {
        if (context.Response.HasStarted)
            return;
        
        context.Response.Clear();
        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = statusCode;
        
        var problemDetails = new ProblemDetails
        {
            Title = "Error",
            Detail = message,
            Status = statusCode,
            Instance = context.Request.Path
        };
        
        var result = JsonSerializer.Serialize(problemDetails);
        await context.Response.WriteAsync(result);
    }
} 