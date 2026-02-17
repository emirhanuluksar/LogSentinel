using Spectre.Console;

namespace DHLog.Demo;

class Program
{
    static async Task Main(string[] args)
    {
        // 1. CLEAR & TITLE
        Console.Clear();
        AnsiConsole.Write(
            new FigletText("DHLog.AI")
                .LeftJustified()
                .Color(Color.Teal));

        AnsiConsole.MarkupLine("[bold grey]v1.2.0 • AIOps & Root Cause Analysis • Enterprise Edition[/]");
        AnsiConsole.WriteLine();
        
        // 2. MONITORING
        AnsiConsole.MarkupLine("[bold white]INITIALIZING NEURAL NETWORK...[/]");
        await Task.Delay(800);
        AnsiConsole.MarkupLine("[bold green]✓ AI Core Online (Gemini)[/]");
        await Task.Delay(400);
        AnsiConsole.MarkupLine("[bold green]✓ Log Stream Active (FileLogWatcher)[/]");
        AnsiConsole.WriteLine();

        await AnsiConsole.Status()
            .Spinner(Spinner.Known.Dots)
            .StartAsync("Watching logs...", async ctx =>
            {
                await Task.Delay(2000);
                AnsiConsole.MarkupLine("[grey]16:35:01 [[INF]] PaymentService: Processing Order #9941[/]");

                await Task.Delay(800);
                AnsiConsole.MarkupLine("[grey]16:35:01 [[INF]] InventoryService: Stock check OK (Item: 442)[/]");

                await Task.Delay(1500);
                
                // 3. THE CRASH
                ctx.Status("🚨 ANOMALY DETECTED");
                ctx.Spinner(Spinner.Known.BouncingBar);
                
                AnsiConsole.MarkupLine("[yellow]16:35:02 [[WRN]] Conquer.Services.Basket: Redis latency is high (250ms). Key: 'basket:u-10293'[/]");

                await Task.Delay(600);
                AnsiConsole.MarkupLine("[red]16:35:04 [[ERR]] Conquer.Services.Basket: RedisTimeoutException: Timeout performing GET (5000ms)[/]");

                await Task.Delay(300);
                AnsiConsole.MarkupLine("[bold red]16:35:05 [[FTL]] Conquer.Services.Ordering: Npgsql.PostgresException (0x80004005): Connection pool has been exhausted.[/]");

                await Task.Delay(1000); // Let it sink in
            });

        // 4. AI ANALYSIS
        AnsiConsole.WriteLine();
        await AnsiConsole.Status()
            .Spinner(Spinner.Known.Star)
            .StartAsync("[bold cyan]Analyzing Stack Trace (Generative AI)...[/]", async ctx => 
            {
                await Task.Delay(2500); // Simulate "Thinking"
            });

        // 5. THE REPORT
        var table = new Table();
        table.Border(TableBorder.Rounded);
        table.AddColumn(new TableColumn("[bold]Metric[/]").Centered());
        table.AddColumn(new TableColumn("[bold]Analysis[/]"));

        table.AddRow("[bold cyan]Root Cause[/]", "[red]Redis Timeout causing DB Connection Saturation[/]");
        table.AddRow("[bold cyan]Impact[/]", "Prevents new orders from being processed (Critical)");
        table.AddRow("[bold cyan]AI Confidence[/]", "[bold green]99.8%[/]");
        table.AddRow("[bold cyan]Suggested Fix[/]", "[yellow]1. Increase Redis ConnectTimeout in appsettings.\n2. Enable Circuit Breaker pattern (Polly).\n3. Scale Connection Pool Max Size.[/]");

        var panel = new Panel(table)
            .Header("[bold red]🚨 CRITICAL INCIDENT REPORT[/]")
            .BorderColor(Color.Red)
            .Padding(1, 1, 1, 1);

        AnsiConsole.Write(panel);
        
        AnsiConsole.MarkupLine("\n[grey]Press any key to acknowledge...[/]");
        Console.ReadKey();
    }
}
