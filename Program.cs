using System;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity.Data;
using h2oAPI.Data;
using System.IdentityModel.Tokens.Jwt;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Configuring my connection string by adding DbContext  and use SQLServer
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer("MyConnectionString"));

//JWT authentication
builder.Services.AddAuthentication("JWT").AddJwtBearer("JWT", Options =>
{
    Options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySecurityKey")),
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero

    };
});

builder.Services.AddControllers();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//Adding middleware
app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

//Define endpoints

//Login endpoints
app.MapPost("/api/login",async (AppDbContext dbContext, LoginRequest request) => 
{
    var user = dbContext.Users.FirstOrDefault(u => u.Email == request.Email && u.Password == request.Password);

    if(user == null)
    {
        return Results.BadRequest("Invalid email/password");
    }

    var token = GenerateJWTToken();

    return Results.Ok(new {
        UserID = user.UserID,
        user.Name,
        user.Competition,
        user.Division,
        Token = token
    });
});



app.Run();


//Using Minimail API