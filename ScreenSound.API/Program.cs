using ScreenSound.API.Endpoints;
using ScreenSound.Banco;
using ScreenSound.Modelos;
using ScreenSound.Shared.Modelos.Modelos;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ScreenSoundContext>((options) =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ScreenSoundDB"));
});
builder.Services.AddTransient<DAL<Artista>>();
builder.Services.AddTransient<DAL<Musica>>();
builder.Services.AddTransient<DAL<Genero>>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddCors(options => 
{
    options.AddPolicy("NewPolicy", builder =>
        builder
            .WithOrigins(["https://localhost:7108", "https://localhost:7252"])
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

app.AddEndpointsArtistas();
app.AddEndpointsMusica();
app.AddEndPointGeneros();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("NewPolicy");

app.Run();
