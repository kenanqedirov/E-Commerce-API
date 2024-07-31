

using E_Commerce_API.Application.Validators.Products;
using E_Commerce_API.Infrastructure;
using E_Commerce_API.Infrastructure.Filters;
using E_Commerce_API.Infrastructure.Services.Storage.Local;
using E_Commerce_API.Persistence;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistanceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddStorage<LocalStorage>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});
// standart olaraq bu yazilir .AddFluentValidation(configuration =>
//configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())
// Elave olaraq - ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter=true); - bunu yaziriq ki bizim modelstate.isValid ile yoxlayanda bunun icine dushsun

builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>()).AddFluentValidation(configuration =>
configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>()).ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
