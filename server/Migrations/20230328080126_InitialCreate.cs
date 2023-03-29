using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nickel",
                table: "FoodItems");

            migrationBuilder.DropColumn(
                name: "VitaminK1",
                table: "FoodItems");

            migrationBuilder.AddColumn<float>(
                name: "BreakfastCalories",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "CalciumII",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "CalciumLB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "CalciumUB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "CarbohydratesII",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "CarbohydratesLB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "CarbohydratesUB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "CopperII",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "CopperLB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "CopperUB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "DinnerCalories",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "FibresII",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "FibresLB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "FibresUB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "FolateII",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "FolateLB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "FolateUB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "IodineII",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "IodineLB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "IodineUB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "IronII",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "IronLB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "IronUB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "LunchCalories",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "MagnesiumII",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "MagnesiumLB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "MagnesiumUB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "MonounsaturatedFatII",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "MonounsaturatedFatLB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "MonounsaturatedFatUB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "NiacinII",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "NiacinLB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "NiacinUB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "PhosphorusII",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "PhosphorusLB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "PhosphorusUB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "PolyunsaturatedFatII",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "PolyunsaturatedFatLB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "PolyunsaturatedFatUB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "PotassiumII",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "PotassiumLB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "PotassiumUB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "ProteinII",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "ProteinLB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "ProteinUB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "RiboflavinII",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "RiboflavinLB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "RiboflavinUB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "SaltII",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "SaltLB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "SaltUB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "SaturatedFatII",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "SaturatedFatLB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "SaturatedFatUB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "SeleniumII",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "SeleniumLB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "SeleniumUB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "SnackCalories",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "SugarsII",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "SugarsLB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "SugarsUB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "ThiaminII",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "ThiaminLB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "ThiaminUB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "TotalFatII",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "TotalFatLB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "TotalFatUB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "TransFatII",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "TransFatLB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "TransFatUB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "VitaminAII",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "VitaminALB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "VitaminAUB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "VitaminB12II",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "VitaminB12LB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "VitaminB12UB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "VitaminB6II",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "VitaminB6LB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "VitaminB6UB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "VitaminCII",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "VitaminCLB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "VitaminCUB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "VitaminDII",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "VitaminDLB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "VitaminDUB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "VitaminEII",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "VitaminELB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "VitaminEUB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "ZincII",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "ZincLB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "ZincUB",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsBreakfast",
                table: "Recipes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDinner",
                table: "Recipes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsLunch",
                table: "Recipes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSnack",
                table: "Recipes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BreakfastCalories",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CalciumII",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CalciumLB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CalciumUB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CarbohydratesII",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CarbohydratesLB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CarbohydratesUB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CopperII",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CopperLB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CopperUB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DinnerCalories",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FibresII",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FibresLB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FibresUB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FolateII",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FolateLB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FolateUB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IodineII",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IodineLB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IodineUB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IronII",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IronLB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IronUB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LunchCalories",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MagnesiumII",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MagnesiumLB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MagnesiumUB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MonounsaturatedFatII",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MonounsaturatedFatLB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MonounsaturatedFatUB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "NiacinII",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "NiacinLB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "NiacinUB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PhosphorusII",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PhosphorusLB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PhosphorusUB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PolyunsaturatedFatII",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PolyunsaturatedFatLB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PolyunsaturatedFatUB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PotassiumII",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PotassiumLB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PotassiumUB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProteinII",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProteinLB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProteinUB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RiboflavinII",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RiboflavinLB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RiboflavinUB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SaltII",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SaltLB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SaltUB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SaturatedFatII",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SaturatedFatLB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SaturatedFatUB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SeleniumII",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SeleniumLB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SeleniumUB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SnackCalories",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SugarsII",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SugarsLB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SugarsUB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ThiaminII",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ThiaminLB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ThiaminUB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TotalFatII",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TotalFatLB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TotalFatUB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TransFatII",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TransFatLB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TransFatUB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "VitaminAII",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "VitaminALB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "VitaminAUB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "VitaminB12II",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "VitaminB12LB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "VitaminB12UB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "VitaminB6II",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "VitaminB6LB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "VitaminB6UB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "VitaminCII",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "VitaminCLB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "VitaminCUB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "VitaminDII",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "VitaminDLB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "VitaminDUB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "VitaminEII",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "VitaminELB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "VitaminEUB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ZincII",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ZincLB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ZincUB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsBreakfast",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "IsDinner",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "IsLunch",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "IsSnack",
                table: "Recipes");

            migrationBuilder.AddColumn<float>(
                name: "Nickel",
                table: "FoodItems",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "VitaminK1",
                table: "FoodItems",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
