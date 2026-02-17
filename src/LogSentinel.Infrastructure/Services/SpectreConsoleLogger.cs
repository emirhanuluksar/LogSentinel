using Microsoft.Extensions.Logging;
using Spectre.Console;

namespace LogSentinel.Infrastructure.Services;

public class SpectreConsoleLogger : ILogger
{
    private readonly string _categoryName;

    public SpectreConsoleLogger(string categoryName)
    {
        _categoryName = categoryName;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel)) return;

        var message = formatter(state, exception);
        var timestamp = DateTime.Now.ToString("HH:mm:ss");

        switch (logLevel)
        {
            case LogLevel.Information:
                // Special handling for specific LogSentinel messages to make them pop
                if (message.Contains("Starting AI Analysis"))
                {
                    AnsiConsole.MarkupLine($"[grey]{timestamp}[/] [bold cyan]AI[/] [cyan]{Markup.Escape(message)}[/]");
                }
                else if (message.Contains("Debouncing"))
                {
                     AnsiConsole.MarkupLine($"[grey]{timestamp}[/] [bold yellow]DEBOUNCE[/] [yellow]{Markup.Escape(message)}[/]");
                }
                else
                {
                    AnsiConsole.MarkupLine($"[grey]{timestamp}[/] [green]INF[/] [grey]{Markup.Escape(message)}[/]");
                }
                break;
            case LogLevel.Warning:
                AnsiConsole.MarkupLine($"[grey]{timestamp}[/] [bold yellow]WRN[/] [yellow]{Markup.Escape(message)}[/]");
                break;
            case LogLevel.Error:
                AnsiConsole.MarkupLine($"[grey]{timestamp}[/] [bold red]ERR[/] [red]{Markup.Escape(message)}[/]");
                if (exception != null)
                {
                    AnsiConsole.MarkupLine($"[red]{Markup.Escape(exception.Message)}[/]");
                }
                break;
            case LogLevel.Critical:
                AnsiConsole.MarkupLine($"[grey]{timestamp}[/] [bold red]FTL[/] [bold red on black]{Markup.Escape(message)}[/]");
                 if (exception != null)
                {
                    AnsiConsole.MarkupLine($"[bold red]{Markup.Escape(exception.ToString())}[/]");
                }
                break;
            default:
                AnsiConsole.MarkupLine($"[grey]{timestamp}[/] [grey]{Markup.Escape(message)}[/]");
                break;
        }
    }

    public bool IsEnabled(LogLevel logLevel) => logLevel != LogLevel.None;

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;
}

public class SpectreConsoleLoggerProvider : ILoggerProvider
{
    public ILogger CreateLogger(string categoryName) => new SpectreConsoleLogger(categoryName);

    public void Dispose() { }
}
