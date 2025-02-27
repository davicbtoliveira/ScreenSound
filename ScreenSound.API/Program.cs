using Microsoft.AspNetCore.Mvc;
using ScreenSound.Banco;
using ScreenSound.Modelos;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ScreenSoundContext>();
builder.Services.AddTransient<DAL<Artista>>();
builder.Services.AddTransient<DAL<Musica>>();

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => 
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
var app = builder.Build();

app.MapGet("/Artistas", ([FromServices]DAL<Artista> dal) => 
{
    return Results.Ok(dal.Listar());
});

app.MapGet("/Artistas/{nome}", ([FromServices]DAL < Artista > dal, string nome) =>
{
    var artista = dal.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
    if (artista is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(artista);
});
                                                    
app.MapPost("/Artistas", ([FromServices] DAL<Artista> dal, [FromBody]Artista artista) =>
{
    dal.Adicionar(artista);
    return Results.Ok();
 });

app.MapDelete("/Artistas/{id}", ([FromServices] DAL<Artista> dal, int id) =>
{
    var artista = dal.RecuperarPor(a => a.Id == id);
    if (artista is null)
    {
        return Results.NotFound();
    }
    dal.Deletar(artista);
    return Results.Ok();
});

app.MapPut("/Artistas", ([FromServices] DAL<Artista> dal, [FromBody]Artista artista) =>
{
    var artistaAAtualizar = dal.RecuperarPor(a => a.Id == artista.Id);
    if (artistaAAtualizar is null)
    {
        return Results.NotFound();
    }
    artistaAAtualizar.Nome = artista.Nome;
    artistaAAtualizar.Bio = artista.Bio;
    artistaAAtualizar.FotoPerfil = artista.FotoPerfil;

    dal.Atualizar(artistaAAtualizar);
    return Results.Ok();
});

app.MapGet("Musicas/", ([FromServices] DAL<Musica> dal) =>
{
    return Results.Ok(dal.Listar());
});

app.MapGet("/Musicas/{nome}", ([FromServices] DAL<Musica> dal, string nome) =>
{
    var musica = dal.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
    if (musica is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(musica);
});

app.MapPost("/Musicas", ([FromServices] DAL<Musica> dal, [FromBody]Musica musica) =>
{
    dal.Adicionar(musica);
    return Results.Ok();
});

app.MapDelete("/Musicas/{id}", ([FromServices] DAL<Musica> dal, int id) =>
{
    var musica = dal.RecuperarPor(a => a.Id == id);
    if (musica is null)
    {
        return Results.NotFound();
    }
    dal.Deletar(musica);
    return Results.Ok();
});

app.MapPut("/Musicas", ([FromServices] DAL<Musica> dal, [FromBody]Musica musica) =>
{
    var musicaAAlterar = dal.RecuperarPor(a => a.Id == musica.Id);
    if (musicaAAlterar is null)
    {
        return Results.NotFound();
    }

    musicaAAlterar.Artista = musica.Artista;
    musicaAAlterar.AnoLancamento = musica.AnoLancamento;
    musicaAAlterar.Nome = musica.Nome;

    dal.Atualizar(musicaAAlterar);
    return Results.Ok();
});

app.Run();
