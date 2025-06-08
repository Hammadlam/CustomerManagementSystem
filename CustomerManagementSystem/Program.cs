using CustomerManagementSystemAPI.Data.IRepository;
using CustomerManagementSystemAPI.Data.Repository;
using CustomerManagementSystemAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ✅ Register DbContext
builder.Services.AddDbContext<ManagementSystemDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CMS")));

// ✅ Register repository
builder.Services.AddScoped<IUserRepository, UserRepository>();

// ✅ JWT Token setup (optional, if you’re using JWT auth)
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var key = builder.Configuration["JwtSettings:TokenKey"];
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // Important
app.UseAuthorization();

app.MapControllers();

app.Run();
