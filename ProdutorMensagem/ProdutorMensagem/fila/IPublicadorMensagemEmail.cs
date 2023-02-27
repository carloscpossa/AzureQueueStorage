namespace ProdutorMensagem.fila
{
    internal interface IPublicadorMensagemEmail
    {
        Task PublicarMensagemAsync(MensagemEmail mensagem);
    }
}
