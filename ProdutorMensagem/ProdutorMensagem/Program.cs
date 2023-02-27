using ProdutorMensagem.fila;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Logging.AddConsole();

builder.Services.AddTransient<IPublicadorMensagemEmail, PublicadorMensagemEmail>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapPost("/email/enviar", async (MensagemEmail mensagem, IPublicadorMensagemEmail publicadorMensagem) =>
{
    await publicadorMensagem.PublicarMensagemAsync(mensagem);
    return Results.Created("Mensagem publicada na fila", mensagem);    
})
.WithName("enviarEmail")
.WithDescription("Endpoint para enfileirar mensagem de email no Azure Queue Storage")
.WithOpenApi();


app.Run();
