using EmployeeManagement.Application.Common.Interfaces;
using EmployeeManagement.Infrastructure.Persistence;
using EmployeeManagement.Infrastructure.Persistence.Interceptors;
using EmployeeManagement.Infrastructure.Persistence.Repositories;
using EmployeeManagement.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigureServices
    {
        private const string CONNECTION_STRING_NAME = "SqlServerConnection";

        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabase(configuration);
            //services.AddIdentity();
            services.AddServices();
        }

        private static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString(CONNECTION_STRING_NAME),
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<AuditableEntitySaveChangesInterceptor>();
            services.AddScoped<ApplicationDbContextInitialiser>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            services.AddScoped<IDateTime, DateTimeService>();

            services.AddTransient<IDateTime, DateTimeService>();
        }

        //private static void AddIdentity(this IServiceCollection services)
        //{
        //    services.AddIdentityCore<ApplicationUser>(opt =>
        //    {
        //        opt.User.RequireUniqueEmail = true;
        //    })
        //   .AddRoles<Role>()
        //   .AddEntityFrameworkStores<ApplicationDbContext>();
        //    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        //            .AddJwtBearer(opt =>
        //            {
        //                opt.TokenValidationParameters = new TokenValidationParameters
        //                {
        //                    ValidateIssuer = false,
        //                    ValidateAudience = false,
        //                    ValidateLifetime = true,
        //                    ValidateIssuerSigningKey = true,
        //                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthorizationConstants.JWT_SECRET_KEY))
        //                };
        //            });
        //    services.AddAuthorization();
        //}
    }
}
