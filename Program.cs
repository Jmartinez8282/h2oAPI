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

//Login endpoint
app.MapPost("/api/login",async (AppDbContext dbContext, LoginRequest request) => 
{
    var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email && u.Password == request.Password);

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

//Teams endpoint
app.MapGet("/api/teams", async (AppDbContext dbContext, string competition, string division ) =>
{
    var teams = await dbContext.Teams.Where(t => t.Competition == competition && t.Division == division.ToList());
    return Results.Ok(teams);
});

//Questions endpoint
app.MapGet("/api/questions",async (AppDbContext dbContext, int teamId) => 
{
    var team = await dbContext.Teams.FirstOrDefaultAsync(t => t.TeamId == teamId);
    if(team == null)
    {
        return Results.BadRequest("Invalid TeamID");
    }
    var questions = dbContext.Questions.Where(q => q.Competition == team.Competition).OrderBy(q => q.SortOrder).ToListAsync();
    return Results.Ok(questions);
});




app.Run();


//Using Minimail API