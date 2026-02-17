using DHLog.Domain.Entities;

namespace DHLog.Domain.Abstractions;

public interface ILogAnalyzer
{
    Task<AnalysisResult> AnalyzeAsync(LogEntry logEntry, CancellationToken cancellationToken);
}

public interface IAlertDispatcher
{
    Task SendAlertAsync(LogEntry logEntry, AnalysisResult analysis, CancellationToken cancellationToken);
}
