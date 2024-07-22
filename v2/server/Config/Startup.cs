using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using server.Core.Infrastructure.DataBase;
using server.Core.Services.RecipeManagement;
using server.Core.Services.UserManagement;
using server.Core.Services.Authentication;
using server.Core.Services.SavedRecipes;

using server.Services.RecipeManagement;
using server.Services.UserManagement;
using server.Services.Authentication;

using server.Infrastructure.MongoDB;
using server.Services.SaveRecipe;

namespace server.Config;

public class Startup (IConfiguration configuration)
{
    private readonly IConfiguration _configuration = configuration;

    public void Start()
    {
        var builder = WebApplication.CreateBuilder();

        // Add services to the container.
        ConfigureAuthentication(builder.Services);
        ConfigureAuthorization(builder.Services);

        ConfigureControllerServices(builder.Services);
        ConfigureInfrastructure(builder.Services);
        ConfigureServices(builder.Services);
        ConfigureCors(builder.Services);
        
        var app = builder.Build();
        ConfigureRequestPipeline(app);
        app.Run();
    }

    private static void ConfigureControllerServices(IServiceCollection services)
    {
        services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IUserService, UserService>();
        services.AddSingleton<IRecipeService, RecipeService>();
        services.AddSingleton<IAuthenticationService, AuthenticationService>();
        services.AddSingleton<ISavedRecipeService, SaveRecipeService>();
    }

    private void ConfigureInfrastructure(IServiceCollection services)
    {
        // Configure MongoDB settings
        services.Configure<MongoDBSettings>(_configuration.GetSection("MongoDB"));

        // Register MongoDB client
        services.AddSingleton<IMongoDatabase>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<MongoDBSettings>>().Value;
                return new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
            }
        );

        services.AddSingleton<IRecipeRepository, RecipeRepository>();
        services.AddSingleton<IFoodItemRepository, FoodItemRepository>();
        services.AddSingleton<IUserRepository, UserRepository>();
        services.AddSingleton<IAuthenticationRepository, AuthenticationRepository>();
        services.AddSingleton<IImageRepository, ImageRepository>();
    }

    private static void ConfigureRequestPipeline(WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        //app.UseHttpsRedirection();

        app.UseCors("AllowOrigin");

        app.UseAuthorization();

        app.MapControllers();
    }


    private void ConfigureAuthentication(IServiceCollection services)
    {
        string? issuer = _configuration["Jwt:Issuer"];
        string? audience = _configuration["Jwt:Audience"];
        string? secret = _configuration["Jwt:Secret"];

        if (string.IsNullOrEmpty(issuer)) {
            throw new Exception("JWT Issuer is missing");
        }
        if (string.IsNullOrEmpty(audience)) {
            throw new Exception("JWT Audience is missing");
        }
        if (string.IsNullOrEmpty(secret)) {
            throw new Exception("JWT Key is missing");
        }

        services.AddAuthentication(options =>
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
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
            };
        });
    }

    private static void ConfigureAuthorization(IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
            .AddPolicy("Admin", policy => policy.RequireRole("Admin"))
            .AddPolicy("User", policy => policy.RequireRole("User"));
    }

    private static void ConfigureCors(IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(name: "AllowOrigin",
                pol =>
                {
                    pol.WithOrigins(
                        "https://localhost:7138",
                        "http://client.localhost",
                        "http://localhost:5173",
                        "http://127.0.0.1:5173"
                    )
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
        });
    }
}