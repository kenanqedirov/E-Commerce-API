using E_Commerce_API.Application;
using E_Commerce_API.Application.Validators.Products;
using E_Commerce_API.Infrastructure;
using E_Commerce_API.Infrastructure.Filters;
using E_Commerce_API.Infrastructure.Services.Storage.Local;
using E_Commerce_API.Persistence;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistanceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();

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

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin",options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateAudience = true,  // Hardan istek gele biler (www.mywebsite.com) meselen burdan istek gele biler
            ValidateIssuer = true,    // Kim tokeni paylayir meselen my.webapi.com
            ValidateLifetime = true,  // tokenin vaxtin kontrol eliyir
            ValidateIssuerSigningKey = true, // securityKeyi yoxlayir

            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"]))
        };
    });

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
