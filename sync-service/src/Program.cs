using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true)
    .AddEnvironmentVariables(prefix: "SYNC_")
    .Build();

var rootPath = configuration["Sync:RootPath"] ?? ".";

Console.WriteLine($"[sync-service] Root path configurato: {rootPath}");

if (!Directory.Exists(rootPath))
{
    Console.WriteLine("[sync-service] Percorso non trovato. Nessuna cartella da indicizzare.");
    return;
}

var directories = Directory.GetDirectories(rootPath, "*", SearchOption.TopDirectoryOnly);

if (directories.Length == 0)
{
    Console.WriteLine("[sync-service] Nessuna cartella trovata.");
    return;
}

Console.WriteLine("[sync-service] Cartelle trovate:");
foreach (var dir in directories)
{
    Console.WriteLine($" - {dir}");
}

Console.WriteLine("[sync-service] Scaffold completato. Logica completa non ancora implementata.");
