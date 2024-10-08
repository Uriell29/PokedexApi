using Asp.Versioning;
using PokeApiNet;
using PokedexAPI_.Providers;
using PokedexAPI_.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<PokeApiClient>();
builder.Services.AddSingleton<HttpClient>();
builder.Services.AddScoped<IPokemonApiClient, PokemonApiClientWrapper>();
builder.Services.AddScoped<IFunTranslationApiClient, FunTranslationApiClient>();
builder.Services.AddScoped<IPokemonInformationService, PokemonInformationService>();

builder.Services.AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1);
        options.ReportApiVersions = true;
        options.ApiVersionReader = new UrlSegmentApiVersionReader();
    })
    .AddMvc()
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'V";
        options.SubstituteApiVersionInUrl = true;
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();

public partial class Program
{
}