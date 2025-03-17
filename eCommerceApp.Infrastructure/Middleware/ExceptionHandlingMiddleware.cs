using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace eCommerceApp.Infrastructure.Middleware;

/// <summary>
/// Middleware for handling exceptions that occur during the request processing pipeline.
/// </summary>
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExceptionHandlingMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next middleware in the pipeline.</param>
    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// Invokes the middleware to handle exceptions.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <returns>A task that represents the completion of request processing.</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (DbUpdateException ex)
        {
            if (ex.InnerException is SqlException innerException)
            {
                switch (innerException.Number)
                {
                    case 2627: // Unique constraint violation
                        context.Response.StatusCode = StatusCodes.Status409Conflict;
                        await context.Response.WriteAsync("Unique constraint violation");
                        break;

                    case 515: // Cannot insert null
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        await context.Response.WriteAsync("Cannot insert null");
                        break;

                    case 547: // Foregin key constraint violation
                        context.Response.StatusCode = StatusCodes.Status409Conflict;
                        await context.Response.WriteAsync("Foregin key constraint violation");
                        break;

                    default:
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        await context.Response.WriteAsync("An error occurred while proccessing your request");
                        break;
                }
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("An error occurred while saving the entity changes.");
            }
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync($"An error occurred {ex.Message}");
        }
    }
}
