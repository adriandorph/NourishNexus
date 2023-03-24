﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using server.Infrastructure;

#nullable disable

namespace server.Migrations
{
    [DbContext(typeof(NourishNexusContext))]
    partial class NourishNexusContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("server.Infrastructure.Avoidance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Keyword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Avoidances");
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

                    b.Property<float>("Nickel")
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

                    b.Property<float>("VitaminK1")
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

                    b.Property<bool>("IsPublic")
                        .HasColumnType("bit");

                    b.Property<string>("Method")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

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

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

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

            modelBuilder.Entity("server.Infrastructure.Avoidance", b =>
                {
                    b.HasOne("server.Infrastructure.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
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

            modelBuilder.Entity("server.Infrastructure.Recipe", b =>
                {
                    b.HasOne("server.Infrastructure.User", null)
                        .WithMany("SavedRecipes")
                        .HasForeignKey("UserId");
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

            modelBuilder.Entity("server.Infrastructure.User", b =>
                {
                    b.Navigation("SavedRecipes");
                });
#pragma warning restore 612, 618
        }
    }
}
