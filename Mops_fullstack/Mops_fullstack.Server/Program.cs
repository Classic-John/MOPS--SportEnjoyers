using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Mops_fullstack.Server.Core;
using Mops_fullstack.Server.Core.Services;
using Mops_fullstack.Server.Datalayer.Database;
using Mops_fullstack.Server.Datalayer.Interfaces;
using Mops_fullstack.Server.Datalayer.Repositories;
using Mops_fullstack.Server.Datalayer.Service_interfaces;
using Mops_fullstack.Server.Datalayer.Jwt;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<FieldRepo>();
builder.Services.AddScoped<GroupRepo>();
builder.Services.AddScoped<MatchRepo>();
builder.Services.AddScoped<MessageRepo>();
builder.Services.AddScoped<OwnerRepo>();
builder.Services.AddScoped<PlayerRepo>();
builder.Services.AddScoped<ThreadRepo>();

builder.Services.AddScoped<UnitOfWork>();

builder.Services.AddScoped<IFieldService, FieldService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IMatchService, MatchService>();
builder.Services.AddScoped<IMessageService, MessageService>();
// builder.Services.AddTransient<IOwnerService, OwnerService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IThreadService, ThreadService>();

builder.Services.AddDbContext<SportEnjoyersDatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });

    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    option.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[]{ }
            }
        });
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("AppSettings:JwtSecret") ?? "")),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});
builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser()
        .Build();
});

builder.Services.AddScoped<IJwtUtils, JwtUtils>();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<JwtMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
