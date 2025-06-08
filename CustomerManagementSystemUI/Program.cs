using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CustomerManagementSystemUI.Data.APIUtility;
using CustomerManagementSystemUI.Data.IRepository;
using CustomerManagementSystemUI.Data.Repository;

var builder = WebApplication.CreateBuilder(args);

// Configure API base URL
ApiUtility.Configure(builder.Configuration);

// Add services to the container.
builder.Services.AddControllersWithViews();

// JWT Authentication configuration
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,  // agar aap issuer check karna chahte hain to true karen aur issuer define karen
        ValidateAudience = false, // agar audience check karna hai to true karen
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:TokenKey"]))
    };
});

// Register HttpClient with BaseUrl
builder.Services.AddHttpClient<IUserRepository, UserRepository>(client =>
{
    client.BaseAddress = new Uri(ApiUtility.BaseUrl);
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// **Important: Add authentication and authorization middleware**
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Users}/{action=SignIn}/{id?}");

app.Run();
