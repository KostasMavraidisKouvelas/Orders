using System.Text;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Orders.Api;
using Orders.Application;
using Orders.DataAccess;
using Orders.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IOperations, Operations>();
builder.Services.AddScoped<IPaymentService, PaymenMockService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddHttpClient();

builder.Services.AddIdentityCore<User>(options =>
    {
        //Disable account confirmation.
        options.SignIn.RequireConfirmedAccount = true;
        options.Password.RequireNonAlphanumeric = false;
        //options.SignIn.RequireConfirmedEmail = false;
        //options.SignIn.RequireConfirmedPhoneNumber = false;
    })
    .AddEntityFrameworkStores<OrdersDbContext>();

var connection = builder.Configuration                //#C
    .GetConnectionString("DefaultConnection");
var hangfireConnection = builder.Configuration.GetConnectionString("HangfireConnection");

    //hanfire configuration
builder.Services.AddHangfire(config 
    => config.SetDataCompatibilityLevel((CompatibilityLevel.Version_180))
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseSqlServerStorage(hangfireConnection));

builder.Services.AddHangfireServer();

builder.Services.AddDbContext<OrdersDbContext>(
    options => options.UseSqlServer(connection, b => b.MigrationsAssembly("Orders.DataAccess"))); //#D
builder.Services.Configure<JWTConfig>(configuration.GetSection("JWTConfig"));
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(jwt =>
    {
        var key = Encoding.ASCII.GetBytes(configuration["JWTConfig:Secret"]);
        jwt.SaveToken = true;
        jwt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateLifetime = true,
            RequireExpirationTime = false
        };
    });
builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("ViewOrders", policy => policy.RequireClaim("WebUser"));
        options.AddPolicy("EditOrders", policy => policy.RequireClaim("WebUser"));
        options.AddPolicy("ViewProducts", policy => policy.RequireClaim("WebUser","MobileUser"));
        options.AddPolicy("EditProducts", policy => policy.RequireClaim("WebUser"));
        options.AddPolicy("CreateOrder", policy => policy.RequireClaim("MobileUser"));
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseHangfireDashboard();
app.MapHangfireDashboard();

RecurringJob.AddOrUpdate<IOperations>(c => c.ImportProductsAsync(), Cron.Daily);
app.Run();
