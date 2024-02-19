using System.Diagnostics;
using System.Globalization;
using MediatR;
using Microsoft.Extensions.Logging;
using Tekton.Application.Common.Interfaces;

namespace Tekton.Application.Common.Behaviours;
public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly Stopwatch _timer;
    private readonly ILogger<TRequest> _logger;
    private readonly IUser _user;
    private readonly IIdentityService _identityService;

    public PerformanceBehaviour(
        ILogger<TRequest> logger,
        IUser user,
        IIdentityService identityService)
    {
        _timer = new Stopwatch();

        _logger = logger;
        _user = user;
        _identityService = identityService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _timer.Start();

        var response = await next();

        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;
        var requestName = typeof(TRequest).Name;
        var userId = _user.Id ?? string.Empty;
        var userName = string.Empty;

        var responseTimeMsg = $"{DateTime.Now} - Tekton Request: {requestName} ({elapsedMilliseconds} milliseconds) UserId: {userId} Request: {request}";

        ResponseTimeWriteLog(responseTimeMsg, $"response_time_requests_{DateTime.Today.ToString("dd_MM_yyyy", CultureInfo.InvariantCulture)}.txt");

        if (elapsedMilliseconds > 500)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                userName = await _identityService.GetUserNameAsync(userId);
            }

            _logger.LogWarning("Tekton Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@UserName} {@Request}",
                requestName, elapsedMilliseconds, userId, userName, request);
        }
        return response;
    }

    private void ResponseTimeWriteLog(string msg, string fileName)
    {
        try
        {
            var currentDirectory = Directory.GetCurrentDirectory() + @"\RequestLogs";
            var fullPath = Path.Combine(currentDirectory, fileName);
            File.AppendAllText(fullPath, msg + Environment.NewLine);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error creating Request Log file: {ex.Message}");
        }
    }
}
