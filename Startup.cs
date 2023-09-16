using System.Reflection;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using MerchantAPI.Configs;
using MerchantAPI.Extensions;
using MerchantAPI.Extensions.Auth;
using MerchantAPI.Extensions.Mappers;
using MerchantAPI.Middlewares;
using MerchantAPI.Repositories;
using MerchantAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;


namespace MerchantAPI;

public class Startup
{
    public IConfiguration Configuration { get; set; }
    private readonly MongoDbSettings _mongoDbSettings;
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
        _mongoDbSettings = Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>()!;
    } 
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<JwtTokenGenerator>(new JwtTokenGenerator("YourSecretKey", "YourIssuer", "YourAudience"));
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "YourIssuer", // Replace with your JWT issuer
            ValidateAudience = true,
            ValidAudience = "YourAudience", // Replace with your JWT audience
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSecretKeyYourSecretKeyYourSecretKey")), // Replace with your secret key
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero // Optional: Adjust the clock skew
        };

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = tokenValidationParameters;
            });
        

        services.AddSingleton(_mongoDbSettings);
        services.AddControllers();
        services.AddSwaggerGen();
        services.AddSwaggerGen(s =>
        {
            s.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "AdSystem API",
                Version = "v1",
                Description = "AdSystem API by Tesodev",
                TermsOfService = new Uri("https://example.com/terms"),
                Contact = new OpenApiContact
                {
                    Name = "Fatih Emir Guler",
                    Email = "fatihemirhguler@gmail.com",
                    Url = new Uri("https://open.spotify.com/user/5bzrcayun5qysad5wh65phdjl?si=69d8cd77bddd4f63"),
                },
                License = new OpenApiLicense
                {
                    Name = "AdSystem API LICX",
                    Url = new Uri("https://mit.com/license"),
                }
            });
            
            s.ExampleFilters();
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            s.IncludeXmlComments(xmlPath);
        });
        
        services.AddSwaggerExamplesFromAssemblyOf<Startup>();
        services.AddLogging();
        services.AddResponseCompression();
        services.AddCors(options => // Cross Origin Source
        {
            options.AddPolicy("AllowAll", builder =>
            {
                builder
                    .WithOrigins("*")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            }); //
        });
        
        // services.Configure<ApiBehaviorOptions>(options =>
        // {
        //     options.SuppressModelStateInvalidFilter = true;
        // });
        
        services.AddSingleton<IRepository, Repository>();
        services.AddSingleton<IService, Service>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddTransient<UserMappers>(); // You might need to use a different lifetime (e.g., Scoped) depending on your application's needs.

        services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssemblyContaining<Startup>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseHttpsRedirection();
        app.UseMiddleware<ErrorHandlingMiddleware>();
        app.UseMiddleware<LoggerMiddleware>();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(ep => { ep.MapControllers(); });
    }
}