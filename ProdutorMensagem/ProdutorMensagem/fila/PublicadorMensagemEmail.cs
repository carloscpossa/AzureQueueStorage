using Azure.Storage.Queues;
using System.Text.Json;

namespace ProdutorMensagem.fila
{
    internal class PublicadorMensagemEmail : IPublicadorMensagemEmail
    {
        private readonly TimeSpan tempoDeExpiracaoInfinito = new TimeSpan(0, 0, -1);

        private readonly QueueClient queueClient;

        public PublicadorMensagemEmail(IConfiguration configuration)
        {
            var sessaoStorage = configuration.GetSection("Storage");
            var nomeFilaEmail = sessaoStorage.GetSection("FilaEmail").Value;
            var storageConnectionString = sessaoStorage.GetSection("StorageConnectionString").Value;

            queueClient = new QueueClient(storageConnectionString, nomeFilaEmail);
        }
        public async Task PublicarMensagemAsync(MensagemEmail mensagem)
        {
            await queueClient.CreateIfNotExistsAsync();

            if (await queueClient.ExistsAsync())
            {
                var textoMensagem = JsonSerializer.Serialize(mensagem);
                await queueClient.SendMessageAsync(textoMensagem, timeToLive: tempoDeExpiracaoInfinito);
            }
        }
    }
}
