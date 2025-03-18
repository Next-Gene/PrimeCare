using eCommerceApp.Application.Services.Interfaces.Logging;
using Microsoft.Extensions.Logging;

namespace eCommerceApp.Infrastructure.Services;

/// <summary>
/// Adapter class for logging using Serilog.
/// </summary>
/// <typeparam name="T">The type for which the logger is being created.</typeparam>
public class SerilogLoggerAdapter<T> : IAppLogger<T>
{
    private readonly ILogger _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="SerilogLoggerAdapter{T}"/> class.
    /// </summary>
    /// <param name="logger">The logger instance to use for logging.</param>
    public SerilogLoggerAdapter(ILogger logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Logs an error message with an exception.
    /// </summary>
    /// <param name="ex">The exception to log.</param>
    /// <param name="message">The error message to log.</param>
    public void LogError(Exception ex, string message)
        => _logger.LogError(ex, message);

    /// <summary>
    /// Logs an informational message.
    /// </summary>
    /// <param name="message">The informational message to log.</param>
    public void LogInformation(string message)
        => _logger.LogInformation(message);

    /// <summary>
    /// Logs a warning message.
    /// </summary>
    /// <param name="message">The warning message to log.</param>
    public void LogWarning(string message)
        => _logger.LogWarning(message);
}
