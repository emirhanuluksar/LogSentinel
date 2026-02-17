using DHLog.Domain.Entities;

namespace DHLog.Domain.Abstractions;

public interface ILogSource
{
    IAsyncEnumerable<LogEntry> StreamLogsAsync(CancellationToken cancellationToken);
}
