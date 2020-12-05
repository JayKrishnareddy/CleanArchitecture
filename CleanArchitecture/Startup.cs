using AutoMapper;
using CFC_API.Versioning;
using DataAccessLayer.ApplicationDbContext;
using Services.RepositoryPattern.UserLogin;
using DataAccessLayer.ApplicationDbContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Services.RepositoryPattern.UserLogin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            #region API Versioning
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader =
                  new HeaderApiVersionReader("X-API-Version");
            });
            #endregion

            #region Connection String
            services.AddDbContext<CFCDbContext>(item => item.UseSqlServer(Configuration.GetConnectionString("myconn")));
            #endregion

            #region Enable Cors 
            services.AddCors();
            #endregion

            #region Swagger
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = " Clean Architecture v1 API's",
                    Description = $"Clean Architecture API's for integration with UI \r\n\r\n © Copyright {DateTime.Now.Year} JK. All rights reserved."
                });
                swagger.SwaggerDoc("v2", new OpenApiInfo
                {
                    Version = "v2",
                    Title = "Clean Architecture v2 API's",
                    Description = $"Clean Architecture API's for integration with UI \r\n\r\n © Copyright {DateTime.Now.Year} JK. All rights reserved."
                });
                swagger.ResolveConflictingActions(a => a.First());
                swagger.OperationFilter<RemoveVersionFromParameterv>();
                swagger.DocumentFilter<ReplaceVersionWithExactValueInPath>();

                #region Enable Authorization using Swagger (JWT) 
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });

                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                            new string[] {}

                    }
                });
                #endregion
            });
            #endregion

            #region Swagger Json property Support
            services.AddSwaggerGenNewtonsoftSupport();
            #endregion

            #region JWT 

            // Adding Authentication  
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            // Adding Jwt Bearer  
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["Jwt:Issuer"],
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });
            #endregion

            #region Dependency Injection
            services.AddTransient<IUserService, UserService>();
            #endregion

            #region Automapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "API v2");
                }) ;
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            #region Global Cors Policy
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials
            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
