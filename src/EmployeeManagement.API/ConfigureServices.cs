using EmployeeManagement.API.Middlewares;
using EmployeeManagement.API.Services;
using EmployeeManagement.Application.Common.Helpers;
using EmployeeManagement.Application.Common.Interfaces;
using EmployeeManagement.Infrastructure.Persistence;
using Microsoft.OpenApi.Models;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigureServices
    {
        private const string BEARER = "Bearer";
        private const string SCHEME = "oauth2";
        private const string SWAGGER_AUTHORIZATION = "Authorization";
        private const string SWAGGER_DESCRIPTION = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                                                     Enter 'Bearer' [space] and then your token in the text input below.
                                                     \r\n\r\nExample: 'Bearer 12345abcdef'";
        private const string IDENTITY_URI = "https://localhost:5005";

        public static void AddAPIServices(this IServiceCollection services)
        {
            services.ConfigureControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.ConfigureAuthenticationHandler();
            services.AddServices();
            services.AddHttpContext();
            services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>();
            services.AddCors();
        }

        private static void ConfigureControllers(this IServiceCollection services)
        {
            services.AddControllers(config =>
            {
                config.RespectBrowserAcceptHeader = true;
                config.ReturnHttpNotAcceptable = true;
            })
            .AddXmlDataContractSerializerFormatters()
            .AddCustomCSVFormatter();
        }

        private static IMvcBuilder AddCustomCSVFormatter(this IMvcBuilder builder)
        {
            return builder.AddMvcOptions(config => config.OutputFormatters.Add(new CsvOutputFormatter()));
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddSingleton<ICurrentUserService, CurrentUserService>();

            services.AddTransient<ExceptionHandlingMiddleware>();
        }

        private static void AddHttpContext(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
        }

        private static void AddSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo 
                { 
                    Title = "API", 
                    Version = "v1" 
                });
                option.AddSecurityDefinition(BEARER, GenerateOpenApiSecurityScheme());
                option.AddSecurityRequirement(GenerateOpenApiSecurityRequirement());
            });
        }

        private static OpenApiSecurityRequirement GenerateOpenApiSecurityRequirement()
        {
            return new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = BEARER
                        },
                        Scheme = SCHEME,
                        Name = BEARER,
                        In = ParameterLocation.Header,

                    },
                    new List<string>()
                }
            };
        }

        private static OpenApiSecurityScheme GenerateOpenApiSecurityScheme()
        {
            return new OpenApiSecurityScheme
            {
                Description = SWAGGER_DESCRIPTION,
                Name = SWAGGER_AUTHORIZATION,
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = BEARER
            };
        }

        private static void ConfigureAuthenticationHandler(this IServiceCollection services) =>
            services.AddAuthentication(BEARER).AddJwtBearer(BEARER, options =>
            {
                options.Authority = IDENTITY_URI;
                options.Audience = "employeemanagementapi";
            });
    }
}
