﻿
using BussinessObjects;
using BussinessObjects.Profiles;
using DataAccessObjects;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.ModelBuilder;
using Repositories;
using Services;
using System.Text;

namespace MyAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<Sp25Prn231Pe2Context>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Cấu hình OData
            var modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<Article>("Articless");

            builder.Services.AddControllers()
            .AddOData(options => options
                .Select() // Cho phép $select
                .Expand() // Cho phép $expand
                .Filter() // Cho phép $filter (để lọc theo price)
                .OrderBy() // Cho phép $orderby (để sắp xếp theo title)
            .Count() // Cho phép $count
            .SetMaxTop(null) // Cho phép $top không giới hạn (hoặc đặt giới hạn phù hợp)
                .AddRouteComponents("odata", modelBuilder.GetEdmModel()));

            // Add services to the container.
            builder.Services.AddScoped<IArticleService, ArticleService>();
            builder.Services.AddScoped<IArticleRepository, ArticleRepository>();

            builder.Services.AddScoped<ICommentService, CommentService>();
            builder.Services.AddScoped<ICommentRepository, CommentRepository>();

            builder.Services.AddScoped<IAuthService, AuthService>();

            var jwtSettings = builder.Configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];

            builder.Services.AddSingleton<JwtService>();

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
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Register specific profile classes
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<ArticleMappingProfile>();
                cfg.AddProfile<CommentMappingProfile>();
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

            app.Run();
        }
    }
}
