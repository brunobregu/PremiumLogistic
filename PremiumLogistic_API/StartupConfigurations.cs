using AutoMapper;
using PremiumLogistic_BAL;

namespace PremiumLogistic_API;

public static class StartupConfigurations
{
    public static IServiceCollection AddConfigurationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PremiumLogisticDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("PremiumLogisticCS")), ServiceLifetime.Transient);
        services.AddExceptionHandler<CustomExceptionHandler>();

        //var assemblies = new List<Assembly>
        //{
        //    typeof(MappingProfiles).Assembly,
        //};

        //services.AddAutoMapper(assemblies, ServiceLifetime.Singleton);


        services.AddAutoMapper(typeof(MappingProfiles));
        

        #region services
            services.AddTransient<ITransportationService, TransportationService>();
        services.AddTransient<IContactService, ContactService>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IOrderDetailsService, OrderDetailsService>();
        services.AddTransient<IProviderService, ProviderService>();
        //services.AddTransient<IEmailSender, EmailSender>();
        #endregion

        #region repository
        services.AddTransient<IOceanRepository, OceanRepository>();
        services.AddTransient<ITransportationRepository, TransportationRepository>();
        services.AddTransient<IAuditLogsRepository, AuditLogsRepository>();
        services.AddTransient<IContactRepository, ContactRepository>();
        services.AddTransient<IOrderDetailsRepository, OrderDetailsRepository>();
        services.AddTransient<IProviderRepository, ProviderRepository>();
        #endregion

        services.AddTransient<IUnitOfWork, UnitOfWork>();

        //identity
        services.AddIdentity<ApplicationUser, IdentityRole>(opt =>
        {
            opt.Password.RequiredLength = 8;
            opt.User.RequireUniqueEmail = true;
            //opt.SignIn.RequireConfirmedEmail = true;
            //opt.Tokens.EmailConfirmationTokenProvider = "emailconfirmation";
        }).AddEntityFrameworkStores<PremiumLogisticDbContext>()
            .AddDefaultTokenProviders();

        //Add Authentication
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["JWT:Issuer"],
                ValidAudience = configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JWT:Secret"]))
                
            };
        });
        return services;
    }
}
