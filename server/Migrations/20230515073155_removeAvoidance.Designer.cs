﻿// <auto-generated />
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using server.Infrastructure;

#nullable disable

namespace server.Migrations
{
    [DbContext(typeof(NourishNexusContext))]
    [Migration("20230515073155_removeAvoidance")]
    partial class removeAvoidance
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CategoryRecipe", b =>
                {
                    b.Property<int>("CategoriesId")
                        .HasColumnType("int");

                    b.Property<int>("RecipesId")
                        .HasColumnType("int");

                    b.HasKey("CategoriesId", "RecipesId");

                    b.HasIndex("RecipesId");

                    b.ToTable("CategoryRecipe");
                });

            modelBuilder.Entity("RecipeUser", b =>
                {
                    b.Property<int>("SavedRecipesId")
                        .HasColumnType("int");

                    b.Property<int>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("SavedRecipesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("RecipeUser");
                });

            modelBuilder.Entity("server.Infrastructure.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("MealId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MealId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("server.Infrastructure.FoodItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<float>("Calcium")
                        .HasColumnType("real");

                    b.Property<float>("Calories")
                        .HasColumnType("real");

                    b.Property<float>("Carbohydrates")
                        .HasColumnType("real");

                    b.Property<float>("Copper")
                        .HasColumnType("real");

                    b.Property<float>("Fibres")
                        .HasColumnType("real");

                    b.Property<float>("Folate")
                        .HasColumnType("real");

                    b.Property<float>("Iodine")
                        .HasColumnType("real");

                    b.Property<float>("Iron")
                        .HasColumnType("real");

                    b.Property<float>("Magnesium")
                        .HasColumnType("real");

                    b.Property<float>("MonounsaturatedFat")
                        .HasColumnType("real");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Niacin")
                        .HasColumnType("real");

                    b.Property<float>("Phosphorus")
                        .HasColumnType("real");

                    b.Property<float>("PolyunsaturatedFat")
                        .HasColumnType("real");

                    b.Property<float>("Potassium")
                        .HasColumnType("real");

                    b.Property<float>("Protein")
                        .HasColumnType("real");

                    b.Property<float>("Riboflavin")
                        .HasColumnType("real");

                    b.Property<float>("Salt")
                        .HasColumnType("real");

                    b.Property<float>("SaturatedFat")
                        .HasColumnType("real");

                    b.Property<float>("Selenium")
                        .HasColumnType("real");

                    b.Property<float>("Sugars")
                        .HasColumnType("real");

                    b.Property<float>("Thiamin")
                        .HasColumnType("real");

                    b.Property<float>("TotalFat")
                        .HasColumnType("real");

                    b.Property<float>("TransFat")
                        .HasColumnType("real");

                    b.Property<float>("VitaminA")
                        .HasColumnType("real");

                    b.Property<float>("VitaminB12")
                        .HasColumnType("real");

                    b.Property<float>("VitaminB6")
                        .HasColumnType("real");

                    b.Property<float>("VitaminC")
                        .HasColumnType("real");

                    b.Property<float>("VitaminD")
                        .HasColumnType("real");

                    b.Property<float>("VitaminE")
                        .HasColumnType("real");

                    b.Property<float>("Zinc")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("FoodItems");
                });

            modelBuilder.Entity("server.Infrastructure.FoodItemMeal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<float>("Amount")
                        .HasColumnType("real");

                    b.Property<int>("FoodItemId")
                        .HasColumnType("int");

                    b.Property<int>("MealId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FoodItemId");

                    b.HasIndex("MealId");

                    b.ToTable("FoodItemMeals");
                });

            modelBuilder.Entity("server.Infrastructure.FoodItemRecipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<float>("Amount")
                        .HasColumnType("real");

                    b.Property<int>("FoodItemId")
                        .HasColumnType("int");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FoodItemId");

                    b.HasIndex("RecipeId");

                    b.ToTable("FoodItemRecipes");
                });

            modelBuilder.Entity("server.Infrastructure.Meal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("MealType")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Meals");
                });

            modelBuilder.Entity("server.Infrastructure.Recipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsBreakfast")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDinner")
                        .HasColumnType("bit");

                    b.Property<bool>("IsLunch")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSnack")
                        .HasColumnType("bit");

                    b.Property<string>("Method")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("server.Infrastructure.RecipeMeal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<float>("Amount")
                        .HasColumnType("real");

                    b.Property<int>("MealId")
                        .HasColumnType("int");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MealId");

                    b.HasIndex("RecipeId");

                    b.ToTable("RecipeMeals");
                });

            modelBuilder.Entity("server.Infrastructure.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<float?>("BreakfastCalories")
                        .HasColumnType("real");

                    b.Property<float?>("CalciumII")
                        .HasColumnType("real");

                    b.Property<float?>("CalciumLB")
                        .HasColumnType("real");

                    b.Property<float?>("CalciumUB")
                        .HasColumnType("real");

                    b.Property<float?>("CarbohydratesII")
                        .HasColumnType("real");

                    b.Property<float?>("CarbohydratesLB")
                        .HasColumnType("real");

                    b.Property<float?>("CarbohydratesUB")
                        .HasColumnType("real");

                    b.Property<float?>("CopperII")
                        .HasColumnType("real");

                    b.Property<float?>("CopperLB")
                        .HasColumnType("real");

                    b.Property<float?>("CopperUB")
                        .HasColumnType("real");

                    b.Property<float?>("DinnerCalories")
                        .HasColumnType("real");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<float?>("FibresII")
                        .HasColumnType("real");

                    b.Property<float?>("FibresLB")
                        .HasColumnType("real");

                    b.Property<float?>("FibresUB")
                        .HasColumnType("real");

                    b.Property<float?>("FolateII")
                        .HasColumnType("real");

                    b.Property<float?>("FolateLB")
                        .HasColumnType("real");

                    b.Property<float?>("FolateUB")
                        .HasColumnType("real");

                    b.Property<float?>("IodineII")
                        .HasColumnType("real");

                    b.Property<float?>("IodineLB")
                        .HasColumnType("real");

                    b.Property<float?>("IodineUB")
                        .HasColumnType("real");

                    b.Property<float?>("IronII")
                        .HasColumnType("real");

                    b.Property<float?>("IronLB")
                        .HasColumnType("real");

                    b.Property<float?>("IronUB")
                        .HasColumnType("real");

                    b.Property<float?>("LunchCalories")
                        .HasColumnType("real");

                    b.Property<float?>("MagnesiumII")
                        .HasColumnType("real");

                    b.Property<float?>("MagnesiumLB")
                        .HasColumnType("real");

                    b.Property<float?>("MagnesiumUB")
                        .HasColumnType("real");

                    b.Property<float?>("MonounsaturatedFatII")
                        .HasColumnType("real");

                    b.Property<float?>("MonounsaturatedFatLB")
                        .HasColumnType("real");

                    b.Property<float?>("MonounsaturatedFatUB")
                        .HasColumnType("real");

                    b.Property<float?>("NiacinII")
                        .HasColumnType("real");

                    b.Property<float?>("NiacinLB")
                        .HasColumnType("real");

                    b.Property<float?>("NiacinUB")
                        .HasColumnType("real");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<float?>("PhosphorusII")
                        .HasColumnType("real");

                    b.Property<float?>("PhosphorusLB")
                        .HasColumnType("real");

                    b.Property<float?>("PhosphorusUB")
                        .HasColumnType("real");

                    b.Property<float?>("PolyunsaturatedFatII")
                        .HasColumnType("real");

                    b.Property<float?>("PolyunsaturatedFatLB")
                        .HasColumnType("real");

                    b.Property<float?>("PolyunsaturatedFatUB")
                        .HasColumnType("real");

                    b.Property<float?>("PotassiumII")
                        .HasColumnType("real");

                    b.Property<float?>("PotassiumLB")
                        .HasColumnType("real");

                    b.Property<float?>("PotassiumUB")
                        .HasColumnType("real");

                    b.Property<float?>("ProteinII")
                        .HasColumnType("real");

                    b.Property<float?>("ProteinLB")
                        .HasColumnType("real");

                    b.Property<float?>("ProteinUB")
                        .HasColumnType("real");

                    b.Property<float?>("RiboflavinII")
                        .HasColumnType("real");

                    b.Property<float?>("RiboflavinLB")
                        .HasColumnType("real");

                    b.Property<float?>("RiboflavinUB")
                        .HasColumnType("real");

                    b.Property<float?>("SaltII")
                        .HasColumnType("real");

                    b.Property<float?>("SaltLB")
                        .HasColumnType("real");

                    b.Property<float?>("SaltUB")
                        .HasColumnType("real");

                    b.Property<float?>("SaturatedFatII")
                        .HasColumnType("real");

                    b.Property<float?>("SaturatedFatLB")
                        .HasColumnType("real");

                    b.Property<float?>("SaturatedFatUB")
                        .HasColumnType("real");

                    b.Property<float?>("SeleniumII")
                        .HasColumnType("real");

                    b.Property<float?>("SeleniumLB")
                        .HasColumnType("real");

                    b.Property<float?>("SeleniumUB")
                        .HasColumnType("real");

                    b.Property<float?>("SnackCalories")
                        .HasColumnType("real");

                    b.Property<float?>("SugarsII")
                        .HasColumnType("real");

                    b.Property<float?>("SugarsLB")
                        .HasColumnType("real");

                    b.Property<float?>("SugarsUB")
                        .HasColumnType("real");

                    b.Property<float?>("ThiaminII")
                        .HasColumnType("real");

                    b.Property<float?>("ThiaminLB")
                        .HasColumnType("real");

                    b.Property<float?>("ThiaminUB")
                        .HasColumnType("real");

                    b.Property<float?>("TotalFatII")
                        .HasColumnType("real");

                    b.Property<float?>("TotalFatLB")
                        .HasColumnType("real");

                    b.Property<float?>("TotalFatUB")
                        .HasColumnType("real");

                    b.Property<float?>("TransFatII")
                        .HasColumnType("real");

                    b.Property<float?>("TransFatLB")
                        .HasColumnType("real");

                    b.Property<float?>("TransFatUB")
                        .HasColumnType("real");

                    b.Property<float?>("VitaminAII")
                        .HasColumnType("real");

                    b.Property<float?>("VitaminALB")
                        .HasColumnType("real");

                    b.Property<float?>("VitaminAUB")
                        .HasColumnType("real");

                    b.Property<float?>("VitaminB12II")
                        .HasColumnType("real");

                    b.Property<float?>("VitaminB12LB")
                        .HasColumnType("real");

                    b.Property<float?>("VitaminB12UB")
                        .HasColumnType("real");

                    b.Property<float?>("VitaminB6II")
                        .HasColumnType("real");

                    b.Property<float?>("VitaminB6LB")
                        .HasColumnType("real");

                    b.Property<float?>("VitaminB6UB")
                        .HasColumnType("real");

                    b.Property<float?>("VitaminCII")
                        .HasColumnType("real");

                    b.Property<float?>("VitaminCLB")
                        .HasColumnType("real");

                    b.Property<float?>("VitaminCUB")
                        .HasColumnType("real");

                    b.Property<float?>("VitaminDII")
                        .HasColumnType("real");

                    b.Property<float?>("VitaminDLB")
                        .HasColumnType("real");

                    b.Property<float?>("VitaminDUB")
                        .HasColumnType("real");

                    b.Property<float?>("VitaminEII")
                        .HasColumnType("real");

                    b.Property<float?>("VitaminELB")
                        .HasColumnType("real");

                    b.Property<float?>("VitaminEUB")
                        .HasColumnType("real");

                    b.Property<float?>("ZincII")
                        .HasColumnType("real");

                    b.Property<float?>("ZincLB")
                        .HasColumnType("real");

                    b.Property<float?>("ZincUB")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CategoryRecipe", b =>
                {
                    b.HasOne("server.Infrastructure.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("server.Infrastructure.Recipe", null)
                        .WithMany()
                        .HasForeignKey("RecipesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RecipeUser", b =>
                {
                    b.HasOne("server.Infrastructure.Recipe", null)
                        .WithMany()
                        .HasForeignKey("SavedRecipesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("server.Infrastructure.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("server.Infrastructure.Category", b =>
                {
                    b.HasOne("server.Infrastructure.Meal", null)
                        .WithMany("Categories")
                        .HasForeignKey("MealId");
                });

            modelBuilder.Entity("server.Infrastructure.FoodItemMeal", b =>
                {
                    b.HasOne("server.Infrastructure.FoodItem", "FoodItem")
                        .WithMany()
                        .HasForeignKey("FoodItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("server.Infrastructure.Meal", "Meal")
                        .WithMany()
                        .HasForeignKey("MealId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FoodItem");

                    b.Navigation("Meal");
                });

            modelBuilder.Entity("server.Infrastructure.FoodItemRecipe", b =>
                {
                    b.HasOne("server.Infrastructure.FoodItem", "FoodItem")
                        .WithMany()
                        .HasForeignKey("FoodItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("server.Infrastructure.Recipe", "Recipe")
                        .WithMany()
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FoodItem");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("server.Infrastructure.Meal", b =>
                {
                    b.HasOne("server.Infrastructure.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("server.Infrastructure.RecipeMeal", b =>
                {
                    b.HasOne("server.Infrastructure.Meal", "Meal")
                        .WithMany()
                        .HasForeignKey("MealId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("server.Infrastructure.Recipe", "Recipe")
                        .WithMany()
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Meal");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("server.Infrastructure.Meal", b =>
                {
                    b.Navigation("Categories");
                });
#pragma warning restore 612, 618
        }
    }
}
