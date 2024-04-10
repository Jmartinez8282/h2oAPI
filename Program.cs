
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using h2oAPI.Data;
using System.IdentityModel.Tokens.Jwt;
using h2oAPI.Models;
using LoginRequest = Microsoft.AspNetCore.Identity.Data.LoginRequest;
;







var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Configuring my connection string by adding DbContext  and use SQLServer
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

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
app.MapPost("/api/login", async (AppDbContext dbContext,LoginRequest request) =>
{
    var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email && u.Password == request.Password);

    if (user == null)
    {
        return Results.BadRequest("Invalid email/password");
    }

    if (user.UserRole == "Judge")
    {
        // Handle judge login
    }
    else if (user.UserRole == "Admin")
    {
        // Handle admin login
    }

    var token = GenerateJWTToken(user);

    return Results.Ok(new
    {
        user.UserID,
        user.Name,
        user.Competition,
        user.Division,
        user.UserRole,
        Token = token
    });
});

//Teams endpoint
app.MapGet("/api/teams", async (AppDbContext dbContext, string competition, string division) =>
{
    var teams = await dbContext.Teams.Where(t => t.Competition == competition && t.Division == division).ToListAsync();
    return Results.Ok(teams);
});

//Questions endpoint
app.MapGet("/api/questions", async (AppDbContext dbContext, int teamId) =>
{
    var team = await dbContext.Teams.FirstOrDefaultAsync(t => t.TeamID == teamId);
    if (team == null)
    {
        return Results.BadRequest("Invalid TeamID");
    }
    var questions = dbContext.Questions.Where(q => q.Competition == team.Competition).OrderBy(q => q.SortOrder).ToListAsync();
    return Results.Ok(questions);
});

//Scores endpoint
app.MapPost("/api/scores", async (AppDbContext dbContext, ScoreRequest request) =>
{
    var score = new Score
    {
        ScoreID = 0,
        UserID = request.UserID,
        QuestionID = request.QuestionID,
        TeamID = request.TeamID,
        Competition = request.Competition,
        ScoreValue = request.ScoreValue
    };

    await dbContext.Scores.AddAsync(score);
    await dbContext.SaveChangesAsync();

    return Results.Ok("Score saved successfully");
});



app.Run();


//helper methods

// Method to Generate JWT token for given user
string GenerateJWTToken(User user)
{
    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.ASCII.GetBytes("YourSecretKey");
    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new System.Security.Claims.ClaimsIdentity(new[]
        {
            new System.Security.Claims.Claim("UserID", user.UserID.ToString()),
            new System.Security.Claims.Claim("Name", user.Name),
            new System.Security.Claims.Claim("Competition", user.Competition),
            new System.Security.Claims.Claim("Division", user.Division)
        }),
        Expires = DateTime.UtcNow.AddHours(24),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    };
    var token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);
};





//Models









//Request models


//Using Minimail API