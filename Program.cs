
using FluentValidation;
using InventoryV2.Data.DbContexts;
using InventoryV2.Interfaces.IServices;
using InventoryV2.Middlewares;
using InventoryV2.Models;
using InventoryV2.Profiles;
using InventoryV2.Seeders;
using InventoryV2.Services;
using InventoryV2.Shares;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens.Experimental;
using System.Net;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.Text;
using InventoryV2.Interfaces;
using InventoryV2.Repositeries;
using InventoryV2.Services.Auth;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Swagger 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//DB conenction
builder.Services.AddDbContext<SqlDbContext>(
      options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

//rate limiting
builder.Services.AddRateLimiter(options =>
{
    options.AddSlidingWindowLimiter("SlidingPolicy", opt =>
    {
        opt.PermitLimit = 20;
        opt.Window = TimeSpan.FromMinutes(1);
        opt.SegmentsPerWindow = 6; // 10 sec segments
        opt.QueueLimit = 0;
    });

    options.OnRejected = async (context, token) =>
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
        context.HttpContext.Response.ContentType = "application/json";

        var response = new
        {
            success = false,
            message = "Too many requests. Please try again later."
        };

        await context.HttpContext.Response.WriteAsJsonAsync(response, cancellationToken: token);
    };
});


// Add Jwt Configuration inside JwtSettings Model
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateLifetime = true,
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))

    });

//Identity
// Add Identity services
builder.Services.AddIdentityCore<ApplicationUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<SqlDbContext>()
    .AddDefaultTokenProviders();

// Redis Cache
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("RedisCache");
});

// FluentEmail
builder.Services
    .AddFluentEmail(builder.Configuration["Email:SenderEmail"])
    .AddSmtpSender(() => new SmtpClient("smtp.gmail.com", 587)
    {
        EnableSsl = true,
        UseDefaultCredentials = false,
        Credentials = new NetworkCredential(
            builder.Configuration["Email:SenderEmail"],
            builder.Configuration["Email:AppPassword"]
        )
    });

//validation
builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

//AutoMapper
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

//Services
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IOtpService, OtpService>();
builder.Services.AddScoped<ISendEmailService, SendEmailService>();
builder.Services.AddScoped<ICurrentUserService,CurrentUserService>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

//global exception Handler

// app.UseMiddleware<ExceptionHandlingMiddleware>();

// Generate fake lifeTime request scope for scoped services
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext =services.GetRequiredService<SqlDbContext>();

    //Generate Database if not exists
    await dbContext.Database.MigrateAsync();

    //Seed roles
    await IdentitySeeder.SeedRoles(services);

}

app.UseHttpsRedirection();

app.UseRateLimiter();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
