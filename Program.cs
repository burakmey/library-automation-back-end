global using library_automation_back_end.Configurations;
global using library_automation_back_end.Data;
global using library_automation_back_end.Features.CommonRequests;
global using library_automation_back_end.Features.CommonResponses;
global using library_automation_back_end.Models;
global using library_automation_back_end.Models.AbstractModels;
global using library_automation_back_end.Services;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using System.ComponentModel.DataAnnotations;
global using System.ComponentModel.DataAnnotations.Schema;
global using System.Security.Claims;
global using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace library_automation_back_end
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<DataContext>(options => { options.UseSqlServer(builder.Configuration["CONNECTION_STRING"]); });
            builder.Services.AddControllers();
            builder.Services.AddTransient<AdminService>();
            builder.Services.AddTransient<AuthService>();
            builder.Services.AddTransient<LibraryService>();
            builder.Services.AddTransient<TokenService>();
            builder.Services.AddTransient<UserService>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = builder.Configuration["Token:Audience"],
                    ValidIssuer = builder.Configuration["Token:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SECURITY_KEY"] ?? throw new Exception("SECURITY_KEY not found!"))),
                    LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false
                };
            });

            builder.Services.AddAuthorizationBuilder()
                .AddPolicy("Admin", policy => policy.RequireRole("Admin"))
                .AddPolicy("User", policy => policy.RequireRole("User"));

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins(builder.Configuration["FrontendSettings:Origin"] ?? throw new Exception("Origin not found!"))
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials();
                });
            });

            var app = builder.Build();
            app.UseHttpsRedirection();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}