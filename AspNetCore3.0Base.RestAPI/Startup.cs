using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using AspNetCore3._0Base.Application.Interface;
using AspNetCore3._0Base.Application.Services;
using AspNetCore3._0Base.CrossCutting.Interface.Repositories;
using AspNetCore3._0Base.Data;
using AspNetCore3._0Base.Data.Context;
using AspNetCore3._0Base.Data.Contract;
using AspNetCore3._0Base.Data.Repository;
using AspNetCore3._0Base.Domain.Entities;
using AspNetCore3._0Base.Domain.Interfaces.Repositories;
using AspNetCore3._0Base.RestAPI.AutoMapper;
using AspNetCore3._0Base.RestAPI.Helpers;
using AspNetCore3._0Base.RestAPI.Security.Configuration;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace AspNetCore3._0Base.RestAPI
{
    public class Startup
    {
        string _connectionString;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _connectionString = Configuration.GetConnectionString("DBConnection");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            #region "Entity Framework Configuration Context"
            services.AddDbContext<ApplicationNameContext>(options =>
            options.UseSqlServer(_connectionString));
            #endregion

            #region "JWT Login"
            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfiguration();

            new ConfigureFromConfigurationOptions<TokenConfiguration>(
                Configuration.GetSection("TokenConfigurations")
            )
            .Configure(tokenConfigurations);

            services.AddSingleton(tokenConfigurations);


            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                // Validates the signing of a received token
                paramsValidation.ValidateIssuerSigningKey = true;

                // Checks if a received token is still valid
                paramsValidation.ValidateLifetime = true;

                // Tolerance time for the expiration of a token (used in case
                // of time synchronization problems between different
                // computers involved in the communication process)
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            // Enables the use of the token as a means of
            // authorizing access to this project's resources
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build());
            });

            #endregion

            #region "Cors Configuration"
            //services.AddCors(options =>
            //{
            //    options.AddDefaultPolicy(
            //        builder => builder
            //            //.WithOrigins("*")
            //            .AllowAnyOrigin()
            //            .AllowAnyMethod()
            //            .AllowAnyHeader()
            //            .AllowCredentials()
            //    );

            //    options.AddPolicy("CorsPolicy",
            //        builder => builder
            //        //.WithOrigins("*")
            //        .AllowAnyOrigin()
            //        .AllowAnyMethod()
            //        .AllowAnyHeader()
            //        .AllowCredentials()
            //    );
            //});
            #endregion

            #region "Globalization"
            services.AddLocalization(opt => opt.ResourcesPath = "Resources");
            #endregion


            services.AddMvcCore(opt => opt.EnableEndpointRouting = false)
                    .AddApiExplorer()
                    .AddNewtonsoftJson(
                    options => options.SerializerSettings.ReferenceLoopHandling =
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    )
                    .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                    .AddViewLocalization(opt => opt.ResourcesPath = "Resources")
                    .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
                    .AddDataAnnotationsLocalization()
                    ;

            services.Configure<RequestLocalizationOptions>(opts =>
            {
                var supportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("pt-PT")
                };

                opts.DefaultRequestCulture = new RequestCulture("en-US");
                //formating numbers, dates, etc
                opts.SupportedCultures = supportedCultures;
                //UI strings that we have localized
                opts.SupportedUICultures = supportedCultures;
            });

            services.AddApiVersioning();

            services.AddSwaggerGen(cfg =>
            {
                cfg.SwaggerDoc("v1", new OpenApiInfo { Title = "Application Name RestApi", Version = "v1" });

                cfg.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
                    "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                cfg.AddSecurityRequirement(new OpenApiSecurityRequirement()
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                },
                                Scheme = "oauth2",
                                Name = "Bearer",
                                In = ParameterLocation.Header,

                            },
                            new List<string>()
                        }
                    });

                cfg.ResolveConflictingActions(apiDescriptions => apiDescriptions
                .Where(x => x.RelativePath.Contains("parameterName")).First());
                cfg.EnableAnnotations();

                //Locate the XML file being generated by ASP.NET...
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                //... and tell Swagger to use those XML comments.
                cfg.IncludeXmlComments(xmlPath);
            });

            #region "Dependency Injection"
            //dependency injection area 
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped(typeof(IAppServiceBase<>), typeof(AppServiceBase<>));
            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            services.AddScoped<ILoggingWebApiCrossCutting, LogEntryWebApiRepository>();


            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            #endregion

            #region "Auto Mapper Configurations"
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperConfiguration());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            #endregion

            #region "Asp Net Identity Configuration"
            services.AddIdentity<ApplicationUser, ApplicationRole>(
                cfg =>
                {
                    cfg.SignIn.RequireConfirmedEmail = false;
                    cfg.Password.RequireDigit = false;
                    cfg.Password.RequiredLength = 3;
                    cfg.Password.RequireNonAlphanumeric = false;
                    cfg.Password.RequireUppercase = false;
                    cfg.Password.RequireLowercase = false;
                }
                )
            .AddEntityFrameworkStores<ApplicationNameContext>()
            .AddDefaultTokenProviders();

            #endregion

            // cache in memory
            services.AddMemoryCache();
            // caching response for middlewares
            services.AddResponseCaching();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
UserManager<ApplicationUser> userManager,
RoleManager<ApplicationRole> roleManager)
        {
            // caching response for middlewares
            app.UseResponseCaching();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseSwagger();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Application Name Rest Api");
                    c.DefaultModelExpandDepth(0);
                    c.DefaultModelsExpandDepth(-1);
                    c.InjectStylesheet("/swagger-ui/custom.css");
                    c.RoutePrefix = string.Empty;
                });

            }
            else
            {
                app.UseHsts();
                app.UseSwaggerUI(c =>
                {
                    c.DefaultModelExpandDepth(0);
                    c.DefaultModelsExpandDepth(-1);
                    c.InjectStylesheet("/swagger-ui/custom.css");
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Application Name Rest Api");
                    c.RoutePrefix = string.Empty;
                });
            }

            UserRolesSeed.SeedRoles(roleManager);

            UserRolesSeed.SeedUser(userManager);

            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);


            //app.UseCors();

            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "DefaultApi",
                    template: "application/webapi/v1/[controller]/[action]"
                    );
                routes.MapRoute(
                    name: "SecondaryApi",
                    template: "application/webapi/v1/[controller]"
                    );
            });
        }
    }
}
