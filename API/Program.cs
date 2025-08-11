using System.Reflection;
using System.Text;
using Application;
using Application.Accounts.AccountsValidator;
using Application.Interfaces.Repos;
using Domain.Entities.Custom_Entities;
using InfraStructure.Persistence;
using InfraStructure.Persistence.Repositaries;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using FluentValidation.AspNetCore;
using API.MiddleWare;
using API.Behaviours;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using API.MiddleWare;
using Application.Interfaces.Repositories;
using FluentValidation;
using System.Reflection.Metadata;
using Application.Exceptions;
using Duende.IdentityServer.Services;
using Application.Services;
using API.Middleware;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// -----------------------------------------------------
// Logging Configuration (Serilog)
// -----------------------------------------------------
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Logging.AddJsonConsole(options =>
{
    options.JsonWriterOptions = new System.Text.Json.JsonWriterOptions { Indented = true };
});

// -----------------------------------------------------
// Services Configuration
// -----------------------------------------------------

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("InfraStructure"))
);

// ASP.NET Identity
builder.Services.AddIdentity<APPUser, AppRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Repositories
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IAuditLogRepository, AuditLogRepository>();
builder.Services.AddScoped<IReceiptRepository, ReceiptRepository>();
builder.Services.AddScoped<ICurrencyExchangeRepository, CurrencyExchangeRateRepository>();
builder.Services.AddScoped< TokenService>();
builder.Services.AddSingleton<PdfReceiptGenerator>();


// MediatR pipeline validation behavior
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));



// MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly);
});

// Validators from Application assembly
builder.Services.AddValidatorsFromAssembly(typeof(CreateAccountValidator).Assembly);

// Application Services
builder.Services.AddApplicationServices();

builder.Services.AddHttpContextAccessor();

// JWT Authentication configuration
var jwtConfig = builder.Configuration.GetSection("JwtConfig");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtConfig["Issuer"],

        ValidateAudience = true,
        ValidAudience = jwtConfig["Audience"],

        ValidateLifetime = true,

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtConfig["Key"])
        ),

        RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
    };

    options.Events = new JwtBearerEvents
    {
        OnChallenge = context =>
        {
            context.HandleResponse(); // Prevent default 401 HTML
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";
            var result = JsonSerializer.Serialize(new
            {
                Succeeded = false,
                Message = "Unauthorized"
            });
            return context.Response.WriteAsync(result);
        }
    };
});


builder.Services.AddAuthorization();

// MVC + FluentValidation integration
builder.Services.AddControllers()
    .AddFluentValidation(fv =>
    {
        fv.RegisterValidatorsFromAssembly(typeof(CreateAccountValidator).Assembly);
    });

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Custom Exception Handler and ProblemDetails
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// Http Logging
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
});

// -----------------------------------------------------
// Build & Middleware
// -----------------------------------------------------
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    await IdentitySeeder.SeedRolesAsync(scope.ServiceProvider);
}

app.UseHttpsRedirection();


app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseExceptionHandler();

app.UseMiddleware<ProfilingMiddleWares>();
app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpLogging();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();


