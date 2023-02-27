using ConsumidorMensagem;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ConsumidorMensagem.fila;

using IHost host = Host.CreateDefaultBuilder(args).
    ConfigureServices(services =>
    {
        services.AddSingleton<IConsumidorMensagemEmail, ConsumidorMensagemEmail>();
        services.AddHostedService<FilaWorker>();
    })
    .Build();

await host.RunAsync();
