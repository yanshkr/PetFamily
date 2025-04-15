using PetFamily.Web;
using PetFamily.Web.Exceptions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSerilog();
builder.Services.AddSwaggerGen();
builder.Services.AddVolunteersModule(builder.Configuration);
builder.Services.AddSpeciesModule(builder.Configuration);

var app = builder.Build();


app.UseExceptionHandler();
app.UseSerilogRequestLogging();
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

await app.RunAsync();

public partial class Program;