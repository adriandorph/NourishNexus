using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
namespace server.Migrations
{
    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FoodItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Calories = table.Column<float>(type: "real", nullable: false),
                    Protein = table.Column<float>(type: "real", nullable: false),
                    Carbohydrates = table.Column<float>(type: "real", nullable: false),
                    Sugars = table.Column<float>(type: "real", nullable: false),
                    Fibres = table.Column<float>(type: "real", nullable: false),
                    TotalFat = table.Column<float>(type: "real", nullable: false),
                    SaturatedFat = table.Column<float>(type: "real", nullable: false),
                    MonounsaturatedFat = table.Column<float>(type: "real", nullable: false),
                    PolyunsaturatedFat = table.Column<float>(type: "real", nullable: false),
                    TransFat = table.Column<float>(type: "real", nullable: false),
                    VitaminA = table.Column<float>(type: "real", nullable: false),
                    VitaminB6 = table.Column<float>(type: "real", nullable: false),
                    VitaminB12 = table.Column<float>(type: "real", nullable: false),
                    VitaminC = table.Column<float>(type: "real", nullable: false),
                    VitaminD = table.Column<float>(type: "real", nullable: false),
                    VitaminE = table.Column<float>(type: "real", nullable: false),
                    Thiamin = table.Column<float>(type: "real", nullable: false),
                    Riboflavin = table.Column<float>(type: "real", nullable: false),
                    Niacin = table.Column<float>(type: "real", nullable: false),
                    Folate = table.Column<float>(type: "real", nullable: false),
                    Salt = table.Column<float>(type: "real", nullable: false),
                    Potassium = table.Column<float>(type: "real", nullable: false),
                    Magnesium = table.Column<float>(type: "real", nullable: false),
                    Iron = table.Column<float>(type: "real", nullable: false),
                    Zinc = table.Column<float>(type: "real", nullable: false),
                    Phosphorus = table.Column<float>(type: "real", nullable: false),
                    Copper = table.Column<float>(type: "real", nullable: false),
                    Iodine = table.Column<float>(type: "real", nullable: false),
                    Selenium = table.Column<float>(type: "real", nullable: false),
                    Calcium = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Method = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    IsBreakfast = table.Column<bool>(type: "bit", nullable: false),
                    IsLunch = table.Column<bool>(type: "bit", nullable: false),
                    IsDinner = table.Column<bool>(type: "bit", nullable: false),
                    IsSnack = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nickname = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    BreakfastCalories = table.Column<float>(type: "real", nullable: true),
                    LunchCalories = table.Column<float>(type: "real", nullable: true),
                    DinnerCalories = table.Column<float>(type: "real", nullable: true),
                    SnackCalories = table.Column<float>(type: "real", nullable: true),
                    ProteinLB = table.Column<float>(type: "real", nullable: true),
                    ProteinII = table.Column<float>(type: "real", nullable: true),
                    ProteinUB = table.Column<float>(type: "real", nullable: true),
                    CarbohydratesLB = table.Column<float>(type: "real", nullable: true),
                    CarbohydratesII = table.Column<float>(type: "real", nullable: true),
                    CarbohydratesUB = table.Column<float>(type: "real", nullable: true),
                    SugarsLB = table.Column<float>(type: "real", nullable: true),
                    SugarsII = table.Column<float>(type: "real", nullable: true),
                    SugarsUB = table.Column<float>(type: "real", nullable: true),
                    FibresLB = table.Column<float>(type: "real", nullable: true),
                    FibresII = table.Column<float>(type: "real", nullable: true),
                    FibresUB = table.Column<float>(type: "real", nullable: true),
                    TotalFatLB = table.Column<float>(type: "real", nullable: true),
                    TotalFatII = table.Column<float>(type: "real", nullable: true),
                    TotalFatUB = table.Column<float>(type: "real", nullable: true),
                    SaturatedFatLB = table.Column<float>(type: "real", nullable: true),
                    SaturatedFatII = table.Column<float>(type: "real", nullable: true),
                    SaturatedFatUB = table.Column<float>(type: "real", nullable: true),
                    MonounsaturatedFatLB = table.Column<float>(type: "real", nullable: true),
                    MonounsaturatedFatII = table.Column<float>(type: "real", nullable: true),
                    MonounsaturatedFatUB = table.Column<float>(type: "real", nullable: true),
                    PolyunsaturatedFatLB = table.Column<float>(type: "real", nullable: true),
                    PolyunsaturatedFatII = table.Column<float>(type: "real", nullable: true),
                    PolyunsaturatedFatUB = table.Column<float>(type: "real", nullable: true),
                    TransFatLB = table.Column<float>(type: "real", nullable: true),
                    TransFatII = table.Column<float>(type: "real", nullable: true),
                    TransFatUB = table.Column<float>(type: "real", nullable: true),
                    VitaminALB = table.Column<float>(type: "real", nullable: true),
                    VitaminAII = table.Column<float>(type: "real", nullable: true),
                    VitaminAUB = table.Column<float>(type: "real", nullable: true),
                    VitaminB6LB = table.Column<float>(type: "real", nullable: true),
                    VitaminB6II = table.Column<float>(type: "real", nullable: true),
                    VitaminB6UB = table.Column<float>(type: "real", nullable: true),
                    VitaminB12LB = table.Column<float>(type: "real", nullable: true),
                    VitaminB12II = table.Column<float>(type: "real", nullable: true),
                    VitaminB12UB = table.Column<float>(type: "real", nullable: true),
                    VitaminCLB = table.Column<float>(type: "real", nullable: true),
                    VitaminCII = table.Column<float>(type: "real", nullable: true),
                    VitaminCUB = table.Column<float>(type: "real", nullable: true),
                    VitaminDLB = table.Column<float>(type: "real", nullable: true),
                    VitaminDII = table.Column<float>(type: "real", nullable: true),
                    VitaminDUB = table.Column<float>(type: "real", nullable: true),
                    VitaminELB = table.Column<float>(type: "real", nullable: true),
                    VitaminEII = table.Column<float>(type: "real", nullable: true),
                    VitaminEUB = table.Column<float>(type: "real", nullable: true),
                    ThiaminLB = table.Column<float>(type: "real", nullable: true),
                    ThiaminII = table.Column<float>(type: "real", nullable: true),
                    ThiaminUB = table.Column<float>(type: "real", nullable: true),
                    RiboflavinLB = table.Column<float>(type: "real", nullable: true),
                    RiboflavinII = table.Column<float>(type: "real", nullable: true),
                    RiboflavinUB = table.Column<float>(type: "real", nullable: true),
                    NiacinLB = table.Column<float>(type: "real", nullable: true),
                    NiacinII = table.Column<float>(type: "real", nullable: true),
                    NiacinUB = table.Column<float>(type: "real", nullable: true),
                    FolateLB = table.Column<float>(type: "real", nullable: true),
                    FolateII = table.Column<float>(type: "real", nullable: true),
                    FolateUB = table.Column<float>(type: "real", nullable: true),
                    SaltLB = table.Column<float>(type: "real", nullable: true),
                    SaltII = table.Column<float>(type: "real", nullable: true),
                    SaltUB = table.Column<float>(type: "real", nullable: true),
                    PotassiumLB = table.Column<float>(type: "real", nullable: true),
                    PotassiumII = table.Column<float>(type: "real", nullable: true),
                    PotassiumUB = table.Column<float>(type: "real", nullable: true),
                    MagnesiumLB = table.Column<float>(type: "real", nullable: true),
                    MagnesiumII = table.Column<float>(type: "real", nullable: true),
                    MagnesiumUB = table.Column<float>(type: "real", nullable: true),
                    IronLB = table.Column<float>(type: "real", nullable: true),
                    IronII = table.Column<float>(type: "real", nullable: true),
                    IronUB = table.Column<float>(type: "real", nullable: true),
                    ZincLB = table.Column<float>(type: "real", nullable: true),
                    ZincII = table.Column<float>(type: "real", nullable: true),
                    ZincUB = table.Column<float>(type: "real", nullable: true),
                    PhosphorusLB = table.Column<float>(type: "real", nullable: true),
                    PhosphorusII = table.Column<float>(type: "real", nullable: true),
                    PhosphorusUB = table.Column<float>(type: "real", nullable: true),
                    CopperLB = table.Column<float>(type: "real", nullable: true),
                    CopperII = table.Column<float>(type: "real", nullable: true),
                    CopperUB = table.Column<float>(type: "real", nullable: true),
                    IodineLB = table.Column<float>(type: "real", nullable: true),
                    IodineII = table.Column<float>(type: "real", nullable: true),
                    IodineUB = table.Column<float>(type: "real", nullable: true),
                    SeleniumLB = table.Column<float>(type: "real", nullable: true),
                    SeleniumII = table.Column<float>(type: "real", nullable: true),
                    SeleniumUB = table.Column<float>(type: "real", nullable: true),
                    CalciumLB = table.Column<float>(type: "real", nullable: true),
                    CalciumII = table.Column<float>(type: "real", nullable: true),
                    CalciumUB = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FoodItemRecipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    FoodItemId = table.Column<int>(type: "int", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodItemRecipes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodItemRecipes_FoodItems_FoodItemId",
                        column: x => x.FoodItemId,
                        principalTable: "FoodItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodItemRecipes_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Avoidances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Keyword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avoidances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Avoidances_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MealType = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meals_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeUser",
                columns: table => new
                {
                    SavedRecipesId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeUser", x => new { x.SavedRecipesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_RecipeUser_Recipes_SavedRecipesId",
                        column: x => x.SavedRecipesId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MealId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FoodItemMeals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    FoodItemId = table.Column<int>(type: "int", nullable: false),
                    MealId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodItemMeals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodItemMeals_FoodItems_FoodItemId",
                        column: x => x.FoodItemId,
                        principalTable: "FoodItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodItemMeals_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeMeals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    MealId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeMeals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeMeals_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeMeals_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryRecipe",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "int", nullable: false),
                    RecipesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryRecipe", x => new { x.CategoriesId, x.RecipesId });
                    table.ForeignKey(
                        name: "FK_CategoryRecipe_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryRecipe_Recipes_RecipesId",
                        column: x => x.RecipesId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Avoidances_UserId",
                table: "Avoidances",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_MealId",
                table: "Categories",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryRecipe_RecipesId",
                table: "CategoryRecipe",
                column: "RecipesId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodItemMeals_FoodItemId",
                table: "FoodItemMeals",
                column: "FoodItemId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodItemMeals_MealId",
                table: "FoodItemMeals",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodItemRecipes_FoodItemId",
                table: "FoodItemRecipes",
                column: "FoodItemId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodItemRecipes_RecipeId",
                table: "FoodItemRecipes",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_UserId",
                table: "Meals",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeMeals_MealId",
                table: "RecipeMeals",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeMeals_RecipeId",
                table: "RecipeMeals",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeUser_UsersId",
                table: "RecipeUser",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Avoidances");

            migrationBuilder.DropTable(
                name: "CategoryRecipe");

            migrationBuilder.DropTable(
                name: "FoodItemMeals");

            migrationBuilder.DropTable(
                name: "FoodItemRecipes");

            migrationBuilder.DropTable(
                name: "RecipeMeals");

            migrationBuilder.DropTable(
                name: "RecipeUser");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "FoodItems");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "Meals");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
