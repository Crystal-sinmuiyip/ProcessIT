using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restaurant.Areas.Admin.Models;
using Restaurant.Data;
using Restaurant.MongoModel;
using Restaurant.MongoModel.Services;
using System.Configuration;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System.Text;



var builder = WebApplication.CreateBuilder(args); 

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? builder.Configuration.GetConnectionString("DefaultExpressConnection");

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    c.IgnoreObsoleteActions();
    c.IgnoreObsoleteProperties();
    c.CustomSchemaIds(type => type.FullName);
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
  options.UseSqlServer(connectionString));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultUI()
            .AddDefaultTokenProviders();

//MONGODB
// Add services to the container.
builder.Services.Configure<OrderDatabaseSettings>(
    builder.Configuration.GetSection("OrderDatabase"));

builder.Services.AddScoped<MenuItemsService>();
builder.Services.AddScoped<TableOrdersService>();


builder.Services.AddControllersWithViews();
    builder.Services.AddRazorPages();
builder.Services.AddAuthentication(o =>
        {
            o.DefaultScheme = "JWT_OR_COOKIE";
            o.DefaultChallengeScheme = "JWT_OR_COOKIE";
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,

                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),

                // Prevents tokens without an expiry from ever working, as that would be a security vulnerability.
                RequireExpirationTime = false,

                // ClockSkew generally exists to account for potential clock difference between issuer and consumer
                // But we are both, so we don't need to account for it.
                // For all intents and purposes, this is optional
                ClockSkew = TimeSpan.Zero
            };
        })
        .AddPolicyScheme("JWT_OR_COOKIE", null, o =>
        {
            o.ForwardDefaultSelector = c =>
            {
                string auth = c.Request.Headers[HeaderNames.Authorization];
                if (!string.IsNullOrWhiteSpace(auth) && auth.StartsWith("Bearer "))
                {
                    return JwtBearerDefaults.AuthenticationScheme;
                }

                return IdentityConstants.ApplicationScheme;
            };
        });


var app = builder.Build();

// CORS - Allow calling the API from WebBrowsers
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
    .SetIsOriginAllowed(origin => true));// Allow any origin 

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();

    app.UseSwagger();

    app.UseSwaggerUI(o =>
    {
        o.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });


}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

});


app.MapRazorPages();

app.Run();
//public class Startup
//{
//    public void ConfigureServices(IServiceCollection services)
//    {
//        services.AddControllers();

//        services.AddDbContext<ApplicationDbContext>(
//                 options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));
//    }


//    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//    {
//    }
//}

//public class ApplicationDbContext : DbContext
//{
//    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
//        : base(options)
//    {
//    }
//}
