using ConsumidorMensagem.fila;
using Microsoft.Extensions.Hosting;

namespace ConsumidorMensagem
{
    internal class FilaWorker : BackgroundService
    {
        private readonly IConsumidorMensagemEmail _consumidorMensagemEmail;
        public FilaWorker(IConsumidorMensagemEmail consumidorMensagemEmail)
        {
            _consumidorMensagemEmail = consumidorMensagemEmail;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _consumidorMensagemEmail.ConsumirMensagemAsync();
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
