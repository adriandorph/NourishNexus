using server.Services.MealPlan;

namespace server.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class PlanningController : ControllerBase
{
    private readonly ILogger<PlanningController> _logger;
    private readonly MealPlanGenerator _generator;
    private readonly IntakeTargetCalculator _intakeTargetCalculator;
    private readonly IUserRepository _userRepo;

    public PlanningController(ILogger<PlanningController> logger, MealPlanGenerator generator, IntakeTargetCalculator intakeTargetCalculator, IUserRepository userRepo)
    {
        _logger = logger;
        _generator = generator;
        _intakeTargetCalculator = intakeTargetCalculator;
        _userRepo = userRepo;
    }

    [HttpPost]
    public async Task<IActionResult> Generate7DayMealPlan(int userID, DateTime startingDate)
    {
        try
        {
            var r = await _generator.Generate7DayMealPlan(userID, startingDate);
            return Ok(r.Response);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpPut("targets")]
    public async Task<IActionResult> SetIntakeTargets([FromBody] IntakeTargetForm form)
    {
        try
        {
            TargetsResult r = _intakeTargetCalculator.CalculateTargets(form.Age, form.Gender, form.Weight, form.Height, form.PAL, form.WeightGoal);
            
            var user = new UserUpdateDTO
            {
                Id = form.UserID,
                BreakfastCalories = r.BreakfastCalories,
                LunchCalories = r.LunchCalories,
                DinnerCalories = r.DinnerCalories,
                SnackCalories = r.SnackCalories,

                ProteinLB = r.LowerBound.Protein,
                ProteinII = r.IdealIntake.Protein,
                ProteinUB = r.UpperBound.Protein,

                CarbohydratesLB = r.LowerBound.Carbohydrates,
                CarbohydratesII = r.IdealIntake.Carbohydrates,
                CarbohydratesUB = r.UpperBound.Carbohydrates,

                SugarsLB = r.LowerBound.Sugars,
                SugarsII = r.IdealIntake.Sugars,
                SugarsUB = r.UpperBound.Sugars,

                FibresLB = r.LowerBound.Fibres,
                FibresII = r.IdealIntake.Fibres,
                FibresUB = r.UpperBound.Fibres,

                TotalFatLB = r.LowerBound.TotalFat,
                TotalFatII = r.IdealIntake.TotalFat,
                TotalFatUB = r.UpperBound.TotalFat,

                SaturatedFatLB = r.LowerBound.SaturatedFat,
                SaturatedFatII = r.IdealIntake.SaturatedFat,
                SaturatedFatUB = r.UpperBound.SaturatedFat,

                MonounsaturatedFatLB = r.LowerBound.MonounsaturatedFat,
                MonounsaturatedFatII = r.IdealIntake.MonounsaturatedFat,
                MonounsaturatedFatUB = r.UpperBound.MonounsaturatedFat,

                PolyunsaturatedFatLB = r.LowerBound.PolyunsaturatedFat,
                PolyunsaturatedFatII = r.IdealIntake.PolyunsaturatedFat,
                PolyunsaturatedFatUB = r.UpperBound.PolyunsaturatedFat,

                TransFatLB = r.LowerBound.TransFat,
                TransFatII = r.IdealIntake.TransFat,
                TransFatUB = r.UpperBound.TransFat,

                VitaminALB = r.LowerBound.VitaminA,
                VitaminAII = r.IdealIntake.VitaminA,
                VitaminAUB = r.UpperBound.VitaminA,

                VitaminB6LB = r.LowerBound.VitaminB6,
                VitaminB6II = r.IdealIntake.VitaminB6,
                VitaminB6UB = r.UpperBound.VitaminB6,

                VitaminB12LB = r.LowerBound.VitaminB12,
                VitaminB12II = r.IdealIntake.VitaminB12,
                VitaminB12UB = r.UpperBound.VitaminB12,

                VitaminCLB = r.LowerBound.VitaminC,
                VitaminCII = r.IdealIntake.VitaminC,
                VitaminCUB = r.UpperBound.VitaminC,

                VitaminDLB = r.LowerBound.VitaminD,
                VitaminDII = r.IdealIntake.VitaminD,
                VitaminDUB = r.UpperBound.VitaminD,

                VitaminELB = r.LowerBound.VitaminE,
                VitaminEII = r.IdealIntake.VitaminE,
                VitaminEUB = r.UpperBound.VitaminE,

                ThiaminLB = r.LowerBound.Thiamin,
                ThiaminII = r.IdealIntake.Thiamin,
                ThiaminUB = r.UpperBound.Thiamin,

                RiboflavinLB = r.LowerBound.Riboflavin,
                RiboflavinII = r.IdealIntake.Riboflavin,
                RiboflavinUB = r.UpperBound.Riboflavin,

                NiacinLB = r.LowerBound.Niacin,
                NiacinII = r.IdealIntake.Niacin,
                NiacinUB = r.UpperBound.Niacin,

                FolateLB = r.LowerBound.Folate,
                FolateII = r.IdealIntake.Folate,
                FolateUB = r.UpperBound.Folate,

                SaltLB = r.LowerBound.Salt,
                SaltII = r.IdealIntake.Salt,
                SaltUB = r.UpperBound.Salt,

                PotassiumLB = r.LowerBound.Potassium,
                PotassiumII = r.IdealIntake.Potassium,
                PotassiumUB = r.UpperBound.Potassium,

                MagnesiumLB = r.LowerBound.Magnesium,
                MagnesiumII = r.IdealIntake.Magnesium,
                MagnesiumUB = r.UpperBound.Magnesium,

                IronLB = r.LowerBound.Iron,
                IronII = r.IdealIntake.Iron,
                IronUB = r.UpperBound.Iron,

                ZincLB = r.LowerBound.Zinc,
                ZincII = r.IdealIntake.Zinc,
                ZincUB = r.UpperBound.Zinc,

                PhosphorusLB = r.LowerBound.Phosphorus,
                PhosphorusII = r.IdealIntake.Phosphorus,
                PhosphorusUB = r.UpperBound.Phosphorus,

                CopperLB = r.LowerBound.Copper,
                CopperII = r.IdealIntake.Copper,
                CopperUB = r.UpperBound.Copper,

                IodineLB = r.LowerBound.Iodine,
                IodineII = r.IdealIntake.Iodine,
                IodineUB = r.UpperBound.Iodine,

                SeleniumLB = r.LowerBound.Selenium,
                SeleniumII = r.IdealIntake.Selenium,
                SeleniumUB = r.UpperBound.Selenium,

                CalciumLB = r.LowerBound.Calcium,
                CalciumII = r.IdealIntake.Calcium,
                CalciumUB = r.UpperBound.Calcium
            };

            var res = await _userRepo.UpdateAsync(user);

            return Ok(r);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(500, "Internal Server Error");
        }
    }
}