using DotNet.Testcontainers.Builders;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using Testcontainers.PostgreSql;

var logger = new LoggerConfiguration()
    .WriteTo.Console(theme: AnsiConsoleTheme.Literate)
    .CreateLogger();

logger.Information("***** Exemplo de utilizacao de Testcontainers com PostgreSQL - Boas praticas *****");
var _container = new PostgreSqlBuilder()
    .WithImage("postgres:17.4")
    .WithWaitStrategy(Wait.ForUnixContainer().UntilCommandIsCompleted("pg_isready"))
    .Build();

logger.Information("***** Iniciando container PostgreSQL *****");
await _container.StartAsync().ConfigureAwait(false);

logger.Information($"Connection String: {_container.GetConnectionString()}");
logger.Information($"Nome do container: {_container.Name}");
logger.Information($"Hostname: {_container.Hostname}");
logger.Information($"Port: {_container.GetMappedPublicPort(5432)}");

logger.Information("Pressione ENTER para interromper a execucao do container...");
Console.ReadLine();

await _container.StopAsync();
logger.Information("Pressione ENTER para encerrar a aplicacao...");
Console.ReadLine();