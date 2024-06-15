using Microsoft.Extensions.Options;
using MongoDB.Driver;
using server.Services;
using server.Services.DataSource;
using server.Services.Recipe;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure MongoDB settings
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));


// Register MongoDB client
builder.Services.AddSingleton<IMongoDatabase>(sp =>
    {
        var settings = sp.GetRequiredService<IOptions<MongoDBSettings>>().Value;
        return new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
    }
);

builder.Services.AddSingleton<IRecipeRepository, RecipeRepository>();
builder.Services.AddSingleton<IFoodItemRepository, FoodItemRepository>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IAuthenticationRepository, AuthenticationRepository>();

builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IRecipeService, RecipeService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
