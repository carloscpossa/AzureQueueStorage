using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace ConsumidorMensagem.fila
{
    internal class ConsumidorMensagemEmail : IConsumidorMensagemEmail
    {
        private readonly QueueClient queueClient;
        public ConsumidorMensagemEmail(IConfiguration configuration)
        {
            var sessaoStorage = configuration.GetSection("Storage");
            var nomeFilaEmail = sessaoStorage.GetSection("FilaEmail").Value;
            var storageConnectionString = sessaoStorage.GetSection("StorageConnectionString").Value;

            queueClient = new QueueClient(storageConnectionString, nomeFilaEmail);
        }
        public async Task ConsumirMensagemAsync()
        {
            if (queueClient.Exists())
            {
                QueueMessage[] retrievedMessage = await queueClient.ReceiveMessagesAsync();

                if (retrievedMessage.Any())
                {
                    var mensagem = retrievedMessage[0];
                    var mensagemEmail = JsonSerializer.Deserialize<MensagemEmail>(mensagem.Body);                    
                    
                    Console.WriteLine("--------------------------");
                    Console.WriteLine($"E-mail enviado para: {mensagemEmail.Destinatario}");
                    Console.WriteLine($"Texto do e-mail: {mensagemEmail.Texto}");
                    Console.WriteLine("");

                    queueClient.DeleteMessage(mensagem.MessageId, mensagem.PopReceipt);
                }
            }
        }
    }
}
