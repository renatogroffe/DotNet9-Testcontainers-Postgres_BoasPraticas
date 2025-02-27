using DotNet.Testcontainers.Builders;
using Serilog.Sinks.SystemConsole.Themes;
using Serilog;

var logger = new LoggerConfiguration()
    .WriteTo.Console(theme: AnsiConsoleTheme.Literate)
    .CreateLogger();

logger.Information("***** Exemplo de utilizacao de Testcontainers com PostgreSQL - Praticas nao-recomendadas *****");
var port = 5432;
var password = "Postgres2025!";
var _container = new ContainerBuilder()
    .WithName("postgres-svc")
    .WithImage("postgres:latest")
    .WithPortBinding(port)
    .WithEnvironment("POSTGRES_PASSWORD", password)
    .Build();

logger.Information("***** Iniciando container PostgreSQL *****");
await _container.StartAsync();

var connectionString = $"Server=127.0.0.1;Port={port};Database=basecontagem;User Id=postgres;Password={password};";
logger.Information($"Connection String: {connectionString}");
logger.Information($"Nome do container: {_container.Name}");
logger.Information($"Hostname: {_container.Hostname}");
logger.Information($"Port: {_container.GetMappedPublicPort(port)}");

logger.Information("Pressione ENTER para interromper a execucao do container...");
Console.ReadLine();

await _container.StopAsync();
logger.Information("Pressione ENTER para encerrar a aplicacao...");
Console.ReadLine();