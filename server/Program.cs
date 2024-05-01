using server.Infrastructure;
using server.Core.EF.RepositoryInterfaces;
using server.Services;
using server.Services.MealPlan;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<NourishNexusContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("NourishNexus"))
);

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddScoped<IFoodItemRepository, FoodItemRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IMealRepository, MealRepository>();
builder.Services.AddScoped<IMealService, MealService>();
builder.Services.AddScoped<FoodItemSeeding>();
builder.Services.AddScoped<IMealPlanGenerator, MealPlanGenerator>();
builder.Services.AddScoped<IntakeTargetCalculator>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowOrigin",
            pol =>
            {
                pol.WithOrigins(
                    "https://localhost:7138", 
                    "http://localhost:5136", 
                    "http://client.localhost", 
                    "http://crosshost:5136", 
                    "http://165.227.158.105:5136",
                    "http://localhost:5173",
                    "http://127.0.0.1:5173"
                    )
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
});

// Add configuration
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var app = builder.Build();
app.UseCors("AllowOrigin");

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
//}

app.UseAuthorization();

app.MapControllers();

app.Run();
