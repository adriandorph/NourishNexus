using System.Globalization;
using server.Infrastructure;

namespace server.Services;

#pragma warning disable CS8618

public class FoodItemSeeding
{
    private readonly IFoodItemRepository _repo;
    private readonly NourishNexusContext _context;


    public FoodItemSeeding(NourishNexusContext context, IFoodItemRepository repo)
    {
        _context = context;
        _repo = repo;
    }

    public void Clear()
    {
        var user = _context.Users.Where(u => u.Nickname == "NourishNexus").FirstOrDefault();
        _context.FoodItemRecipes.RemoveRange(_context.FoodItemRecipes);
        _context.SaveChanges();
        _context.Recipes.RemoveRange(_context.Recipes);
        _context.SaveChanges();
        _context.FoodItems.RemoveRange(_context.FoodItems);
        _context.SaveChanges();
        _context.Categories.RemoveRange(_context.Categories);
        if (user != null) _context.Users.Remove(user);
        _context.SaveChanges();
    }
    public async Task<Response> Seed()
    {
        CultureInfo.CurrentCulture = new CultureInfo("da-DK", false);
        using(var reader = new StreamReader(@"../data.csv"))
        {
            List<FoodItemCreateDTO> foodItems = new List<FoodItemCreateDTO>();
            reader.ReadLine();//Skip first line: Danish field declaration
            reader.ReadLine();//Skip second line: Engligsh field declaration
            reader.ReadLine();//Skip third line: Unit declaration
            reader.ReadLine();//Skip fourth line: IDs

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine()!;
                string[] values = line.Split(';');
                
                FoodItemCreateDTO foodItem = new FoodItemCreateDTO
                {
                    Name = values[1],
                    Calories = float.Parse(values[4] != "" ? values[4] : "0"), // kcal/100g
                    Protein = float.Parse(values[5] != "" ? values[5] : "0"), //g/100g
                    Carbohydrates = float.Parse(values[10] != "" ? values[10] : "0"), //Carbohydrate by labelling g/100g
                    Sugars = float.Parse(values[86] != "" ? values[86] : "0"), //g/100g
                    Fibres = float.Parse(values[11] != "" ? values[11] : "0"), //g/100g
                    TotalFat = float.Parse(values[171] != "" ? values[171] : "0"),//g/100g
                    SaturatedFat = float.Parse(values[166] != "" ? values[166] : "0"), //g/100g
                    MonounsaturatedFat = float.Parse(values[167] != "" ? values[167] : "0"), //g/100g
                    PolyunsaturatedFat = float.Parse(values[168] != "" ? values[168] : "0"), //g/100g
                    TransFat = float.Parse(values[169] != "" ? values[169] : "0"), //g/100 g
                    VitaminA = float.Parse(values[18] != "" ? values[18] : "0"), //RE (µg/100g)
                    VitaminB6 = float.Parse(values[39] != "" ? values[39] : "0"), //mg/100g
                    VitaminB12 = float.Parse(values[44] != "" ? values[44] : "0"), //µg/100g
                    VitaminC = float.Parse(values[45] != "" ? values[45] : "0"), //mg/100g
                    VitaminD = float.Parse(values[21] != "" ? values[21] : "0"), //µg/100g
                    VitaminE = float.Parse(values[26] != "" ? values[26] : "0"), // alfa-TE/100g eller mg/100g
                    Thiamin = float.Parse(values[33] != "" ? values[33] : "0"), //mg/100g
                    Riboflavin = float.Parse(values[36] != "" ? values[36] : "0"),//mg/100g
                    Niacin = float.Parse(values[38] != "" ? values[38] : "0"), //mg/100g
                    Folate = float.Parse(values[42] != "" ? values[42] : "0"), // µg/100g
                    Salt = float.Parse(values[14] != "" ? values[14] : "0"), //g/100g
                    Potassium = float.Parse(values[49] != "" ? values[49] : "0"), //mg/100g
                    Magnesium = float.Parse(values[51] != "" ? values[51] : "0"), //mg/100g
                    Iron = float.Parse(values[53] != "" ? values[53] : "0"), //mg/100g
                    Zinc = float.Parse(values[55] != "" ? values[55] : "0"), //mg/100g
                    Phosphorus = float.Parse(values[64] != "" ? values[64] : "0"), //mg/100g
                    Copper = float.Parse(values[54] != "" ? values[54] : "0"), //mg/100g
                    Iodine = float.Parse(values[68] != "" ? values[68] : "0"), //µg/100g
                    Selenium = float.Parse(values[60] != "" ? values[60] : "0"), //µg/100g
                    Calcium = float.Parse(values[50] != "" ? values[50] : "0"), //mg/100g
                };
                foodItems.Add(foodItem);
            }
            CultureInfo.CurrentCulture = new CultureInfo("en-US", false);

            foreach(var foodItem in foodItems)
            {
                await _repo.CreateAsync(foodItem);
            }
            return Response.Created;
        }
    }


    //Author
    User _author;

    //Categories
    Category _fruit = new Category("Fruit");
    Category _meat = new Category("Meat");
    Category _vegetarian = new Category("Vegetarian");

    //Breakfast
    Recipe _eggsAndBacon;
    Recipe _bunsWithStrawberryJam;
    Recipe _bunsWithRaspberryJam;
    Recipe _bunsWithBlackberryJam;
    Recipe _bunsWithRhubarbJam;
    Recipe _bunsWithCheese;
    Recipe _bunsWithPeanutbutter;
    Recipe _bunsWithMapleSyrup;
    Recipe _bunswithSpreadChocolate;
    Recipe _QuinoaBowl;
    Recipe _OatMealBowl;
    Recipe _OatMealBoiled;
    Recipe _BreadAndSalmon;
    Recipe _BreadAndHam;
    Recipe _EggsBaconAndSausages;
    Recipe _FruitBowl;
    Recipe _Cereal;
    Recipe _pancakes;

    //Lunch
    Recipe _durumKebab;
    Recipe _durumChicken;
    Recipe _durumMix;
    Recipe _mixBox;
    Recipe _kebabBox;
    Recipe _chickenBox;
    Recipe _fishFillet;
    Recipe _ryebreadWithSalmon;
    Recipe _ryebreadWithAvocado; 
    Recipe _ryebreadPlatte;
    Recipe _grilledToastWithTunaAndPesto;
    Recipe _grilledToastWithHamAndCheese;
    Recipe _paninoWithTomatoMozzarellaAndSalami;
    Recipe _hamAndCheeseSandwich;

    
    
    //Dinner DONE
    Recipe _risotto;
    Recipe _pastaCarbonara;
    Recipe _butterChicken;
    Recipe _chiliConCarne;
    Recipe _beefBernaise;
    Recipe _pastaPuttanesca;
    Recipe _pastaTuna;
    Recipe _tortillas;
    Recipe _meatballsCurry;
    Recipe _steakWithPepperSauce;
    Recipe _steakWithWhiskeySauce;
    Recipe _tacos;
    Recipe _honeyGarlicFriedPorkChops;
    Recipe _chickenFriedRice;
    Recipe _shrimpStirFry;
    Recipe _springRolls;
    Recipe _bakedSalmonAndAsparagus;
    Recipe _meatLasagna;
    Recipe _veganLasagna;
    Recipe _lambChopWithPotatoes;
    Recipe _frikadellerWithRice;
    

    //Snacks
    Recipe _cashewPeanutNutMix;
    Recipe _pistachioNutMix;
    Recipe _almondRaisinMix;
    Recipe _pineappleOrangefruitMix;
    Recipe _appleOrangePearfruitMix;
    Recipe _raspberryBlueberryfruitMix;
    Recipe _blackberryStrawberryfruitMix;
    Recipe _strawberryBananaSmoothie;
    Recipe _mangoSmoothie;
    Recipe _blueberrySmoothie;
    Recipe _appleCarrotOrangeSmoothie;
    Recipe _peanutbutterSandwich;
    Recipe _salamiSticks;
    Recipe _salamiSticksOlivesAndCheese;
    Recipe _crackersWithPesti;

    //FoodItems
    FoodItem _egg;
    FoodItem _bacon;
    FoodItem _buns;
    FoodItem _butter;
    FoodItem _strawberryJam;
    FoodItem _raspberryJam;
    FoodItem _blackberryJam;
    FoodItem _rhubarbJam;
    FoodItem _slicedCheese;
    FoodItem _Peanutbutter;
    FoodItem _mapleSyrup;
    FoodItem _spreadChocolate;
    FoodItem _quinoa;
    FoodItem _oats;
    FoodItem _milk;
    FoodItem _flute;
    FoodItem _ham;
    FoodItem _brunchSausages;
    FoodItem _cornflakes;
    FoodItem _flour;
    FoodItem _lemon;
    FoodItem _blueberry;
    FoodItem _blackberry;
    FoodItem _strawberry;
    FoodItem _raspberry;
    FoodItem _almond;
    FoodItem _raisin;
    FoodItem _cashewnut;
    FoodItem _peanut;
    FoodItem _mango;
    FoodItem _banana;
    FoodItem _apple;
    FoodItem _orange;
    FoodItem _carrot;
    FoodItem _pistachionut;
    FoodItem _pineapple;


    FoodItem _bakedBeans;
    FoodItem _kidneyBeans;
    FoodItem _slicedTomatoes;
    FoodItem _mincedBeef;
    FoodItem _basmatiRice;
    FoodItem _cremeFraiche;
    FoodItem _onion;
    FoodItem _garlic;
    FoodItem _risottoRice;
    FoodItem _chickenBreastFilet;
    FoodItem _champignon;
    FoodItem _whiteWine;
    FoodItem _eggplant;
    FoodItem _parmesanCheese;
    FoodItem _vegetableBouillion;
    FoodItem _steak;
    FoodItem _potatoes;
    FoodItem _bernaiseSauce;
    FoodItem _cream;
    FoodItem _pasta;
    FoodItem _greekYogurt;
    FoodItem _ginger;
    FoodItem _cumin;
    FoodItem _chili;
    FoodItem _coriander;
    FoodItem _oliveOil;
    FoodItem _salt;
    FoodItem _pepper;
    FoodItem _sugar;
    FoodItem _tortilla;
    FoodItem _lambChop;
    FoodItem _lasagnaSheets;
    FoodItem _curry;
    FoodItem _icebergSalad;
    FoodItem _tomato;
    FoodItem _bellPepper;
    FoodItem _crackers;

    
    FoodItem _cucumber;
    FoodItem _kebab;
    FoodItem _frenchFries;
    FoodItem _fishFillets;
    FoodItem _ryebread;
    FoodItem _smokedSalmon;
    FoodItem _avocado;
    FoodItem _mackarelInTomatoes;
    FoodItem _mayonaise;
    FoodItem _liverpate;
    FoodItem _gherkins;
    FoodItem _toast;
    FoodItem _tuna;
    FoodItem _mozzarella;
    FoodItem _salami;
    FoodItem _vinegar;
    FoodItem _springOnion;
    FoodItem _mincedVealAndPork;
    FoodItem _scallion;
    FoodItem _mincedPork;
    FoodItem _shrimp;
    FoodItem _tacosItem;
    FoodItem _salmon;
    FoodItem _remoulade;
    FoodItem _pesto;
    FoodItem _greenBeans;
    FoodItem _olives;
    FoodItem _pear;
    FoodItem _anchovies;
    FoodItem _capers;
    FoodItem _pepperSauce;
    FoodItem _whiskeySauce;
    FoodItem _lime;
    FoodItem _paprika;
    FoodItem _oregano;
    FoodItem _porkChops;
    FoodItem _honey;
    FoodItem _broccoli;
    FoodItem _asparagus;
    FoodItem _soySauce;
    



    //FOODITEMRECIPES

    //EggsAndBacon
    FoodItemRecipe _eggsInEggsAndBacon;
    FoodItemRecipe _baconInEggsAndBacon;
    FoodItemRecipe _bunsInBunsWithStrawberryJam;
    FoodItemRecipe _strawberryJamInBunsWithStrawberryJam;
    FoodItemRecipe _bunsInBunsWithRaspberryJam;
    FoodItemRecipe _raspberryJamInBunsWithRaspberryJam;
    FoodItemRecipe _bunsInBunsWithBlackberryJam;
    //
    FoodItemRecipe _blackberryJamInBunsWithBlackberryJam;
    FoodItemRecipe _bunsInBunsWithRhubarbJam;
    FoodItemRecipe _rhubarbJamInBunsWithRhubarbJam;
    
    //buns, buns with cheese    
    FoodItemRecipe _bunsInBunsWithCheese;
    FoodItemRecipe _cheeseInBunsWithCheese;
    
    //buns, peanut butter
    FoodItemRecipe _bunsInBunsWithPeanutbutter;
    FoodItemRecipe _peanutbutterInBunsWithPeanutbutter;
    
    //Buns, maple syrup
    FoodItemRecipe _bunsInBunsWithmapleSyrup;
    FoodItemRecipe _mapleSyrupInBunsWithmapleSyrup;

    //Buns with spread chicikate
    FoodItemRecipe _bunsInBunsWithSpreadChocolate;
    FoodItemRecipe _spreadChocolateInBunsWithSpreadChocolate;
    
    //Quinoa bowl
    FoodItemRecipe _quinoaInQuinoaBowl;
    FoodItemRecipe _tomatoInQuinoaBowl;
    FoodItemRecipe _champignonInQuinoaBowl;
    FoodItemRecipe _eggsInQuinoaBowl;
    FoodItemRecipe _lemonInQuinoaBowl;
    
    //OeatmealBowl
    FoodItemRecipe _oatsInOatmealBowl;
    FoodItemRecipe _milkInOatmealBowl;
    FoodItemRecipe _blueberryInOatmealBowl;
    FoodItemRecipe _raspberryInOatmealBowl;

    //Oatmeal boiled
    FoodItemRecipe _oatsInOatmealBoiled;
    FoodItemRecipe _butterInOatmealBoiled;

    //Bread and salmon
    FoodItemRecipe _breadInBreadAndSalmon;
    FoodItemRecipe _salmonInBreadAndSalmon;
    FoodItemRecipe _butterInBreadAndSalmon;
    
    //Bread and ham
    FoodItemRecipe _breadInBreadAndHam;
    FoodItemRecipe _hamInBreadAndHam;
    FoodItemRecipe _butterInBreadAndHam;
    
    //Eggs, Bacon, Sausages
    FoodItemRecipe _eggsInEggsBaconAndSausages;
    FoodItemRecipe _baconInEggsBaconAndSausages;
    FoodItemRecipe _sausagesInEggsBaconAndSausages;
    
    //Fruit bowl
    FoodItemRecipe _appleInFruitBowl;
    FoodItemRecipe _orangeInFruitBowl;
    FoodItemRecipe _pineapleInFruitBowl;
    FoodItemRecipe _cornflakesInCereal;
    FoodItemRecipe _milkInCereal;

    //Pancakes
    FoodItemRecipe _flourInPancakes;
    FoodItemRecipe _milkInPancakes;
    FoodItemRecipe _eggsInPancakes;
    FoodItemRecipe _sugarInPancakes;
    FoodItemRecipe _butterInPancakes;

    //Durum kebab
    FoodItemRecipe _durumWrapInDurumKebab;
    FoodItemRecipe _kebabInDurumKebab;
    FoodItemRecipe _dressingInDurumKebab;
    FoodItemRecipe _icebergInDurumKebab;
    FoodItemRecipe _onionInDurumKebab;
    FoodItemRecipe _tomatoInDurumKebab;
    FoodItemRecipe _cucumberInDurumKebab;
    
    //Durum chicken
    FoodItemRecipe _durumWrapInDurumChicken;
    FoodItemRecipe _chickenInDurumChicken;
    FoodItemRecipe _dressingInDurumChicken;
    FoodItemRecipe _icebergInDurumChicken;
    FoodItemRecipe _onionInDurumChicken;
    FoodItemRecipe _tomatoInDurumChicken;
    FoodItemRecipe _cucumberInDurumChicken;
    
    //Durum mix
    FoodItemRecipe _durumWrapInDurumMix;
    FoodItemRecipe _kebabInDurumMix;
    FoodItemRecipe _chickenInDurumMix;
    FoodItemRecipe _dressingInDurumMix;
    FoodItemRecipe _icebergInDurumMix;
    FoodItemRecipe _onionInDurumMix;
    FoodItemRecipe _tomatoInDurumMix;
    FoodItemRecipe _cucumberInDurumMix;
   
    //kebab box
    FoodItemRecipe _frenchFriesInKebabBox;
    FoodItemRecipe _kebabInKebabBox;
    FoodItemRecipe _dressingInKebabBox;
    FoodItemRecipe _icebergInKebabBox;
    FoodItemRecipe _onionInKebabBox;
    FoodItemRecipe _tomatoInKebabBox;
    FoodItemRecipe _cucumberInKebabBox;

    //Chicken box
    FoodItemRecipe _frenchFriesInChickenBox;
    FoodItemRecipe _chickenInChickenBox;
    FoodItemRecipe _dressingInChickenBox;
    FoodItemRecipe _icebergInChickenBox;
    FoodItemRecipe _onionInChickenBox;
    FoodItemRecipe _tomatoInChickenBox;
    FoodItemRecipe _cucumberInChickenBox;
    
    //Mix box
    FoodItemRecipe _frenchFriesInMixBox;
    FoodItemRecipe _kebabInMixBox;
    FoodItemRecipe _chickenInMixBox;
    FoodItemRecipe _dressingInMixBox;
    FoodItemRecipe _icebergInMixBox;
    FoodItemRecipe _onionInMixBox;
    FoodItemRecipe _tomatoInMixBox;
    FoodItemRecipe _cucumberInMixBox;
    
    //Fishfillet
    FoodItemRecipe _fishFilletInFishFillet;
    FoodItemRecipe _ryebreadInFishFillet;
    FoodItemRecipe _remouladeInFishFillet;
    FoodItemRecipe _butterInFishFillet;
    
    //Ryebread salmon
    FoodItemRecipe _ryebreadInRyebreadWithSalmon;
    FoodItemRecipe _salmonInRyebreadWithSalmon;
    FoodItemRecipe _butterInRyebreadWithSalmon;

    //Ryebread avocado
    FoodItemRecipe _ryebreadInRyebreadWithAvocado;
    FoodItemRecipe _avocadoInRyebreadWithAvocado;
    FoodItemRecipe _butterInRyebreadWithAvocado;
    
    //Platte
    FoodItemRecipe _ryebreadInRyebreadPlatte;
    FoodItemRecipe _butterInRyebreadPlatte;
    FoodItemRecipe _avocadoInRyebreadPlatte;
    FoodItemRecipe _salmonInRyebreadPlatte;
    FoodItemRecipe _liverPateInRyebreadPlatte;

    //Tuna pesto toast
    FoodItemRecipe _tunaInGrilledToastWithTunaAndPesto;
    FoodItemRecipe _pestoInGrilledToastWithTunaAndPesto;
    FoodItemRecipe _butterInGrilledToastWithTunaAndPesto;
    FoodItemRecipe _toastInGrilledToastWithTunaAndPesto;
    
    //Ham and cheese toast
    FoodItemRecipe _toastInGrilledToastWithHamAndCheese;
    FoodItemRecipe _butterInGrilledToastWithHamAndCheese;
    FoodItemRecipe _hamInGrilledToastWithHamAndCheese;
    FoodItemRecipe _cheeseInGrilledToastWithHamAndCheese;
    
    //Panino
    FoodItemRecipe _breadInPaninoWithTomatoMozzarellaAndSalami;
    FoodItemRecipe _mayoInPaninoWithTomatoMozzarellaAndSalami;
    FoodItemRecipe _tomatoInPaninoWithTomatoMozzarellaAndSalami;
    FoodItemRecipe _mozzarellaInPaninoWithTomatoMozzarellaAndSalami;
    FoodItemRecipe _salamiInPaninoWithTomatoMozzarellaAndSalami;
    
    //ham and cheese sandwich
    FoodItemRecipe _breadInHamAndCheeseSandwich;
    FoodItemRecipe _mayoInHamAndCheeseSandwich;
    FoodItemRecipe _hamInHamAndCheeseSandwich;
    FoodItemRecipe _cheeseInHamAndCheeseSandwich;
    
    //risotto
    FoodItemRecipe _onionInRisotto;
    FoodItemRecipe _garlicInRisotto;
    FoodItemRecipe _risottoRiceInRisotto;
    FoodItemRecipe _champignonInRisotto;
    FoodItemRecipe _bouillionInRisotto;
    FoodItemRecipe _parmesanCheeseInRisotto;
    
    //carbonara
    FoodItemRecipe _eggsInPastaCarbonara;
    FoodItemRecipe _cheeseInPastaCarbonara;
    FoodItemRecipe _baconInPastaCarbonara;
    FoodItemRecipe _pastaInPastaCarbonara;
    FoodItemRecipe _pepperInPastaCarbonara;


    //butterchicken
    FoodItemRecipe _greekYougurtInButterChicken;
    FoodItemRecipe _chickenInButterChicken;
    FoodItemRecipe _cuminInButterChicken;
    FoodItemRecipe _butterInButterChicken;
    FoodItemRecipe _creamInButterChicken;
    FoodItemRecipe _slicedTomatoesInButterChicken;
    FoodItemRecipe _gingerInButterChicken;
    FoodItemRecipe _riceInButterChicken;

    //chiliconcarne
    FoodItemRecipe _chiliInCCC;
    FoodItemRecipe _mincedMeatInCCC;
    FoodItemRecipe _slicedTomatoesInCCC;
    FoodItemRecipe _bakedBeansInCCC;
    FoodItemRecipe _kidneyBeansInCCC;
    FoodItemRecipe _garlicInCCC;
    FoodItemRecipe _onionInCCC;
    FoodItemRecipe _oliveOilInCCC;

    //meatballscurry
    FoodItemRecipe _mincedVealAndPorkInMBC;
    FoodItemRecipe _flourInMBC;
    FoodItemRecipe _onionInMBC;
    FoodItemRecipe _milkIMBC;
    FoodItemRecipe _eggInMBC;
    FoodItemRecipe _butterInMBC;
    FoodItemRecipe _curryInMBC;
    FoodItemRecipe _riceInMBC;

    //beef bernaise
    FoodItemRecipe _steakInBeefBernaise;
    FoodItemRecipe _potatoesInBeefBernaise;
    FoodItemRecipe _greenBeansInBeefBernaise;
    FoodItemRecipe _bernaiseInBeefBernaise;

    //Pasta puttanesca
    FoodItemRecipe _pastaInPastaPuttanesca;
    FoodItemRecipe _slicedTomatoesInPastaPuttanesca;
    FoodItemRecipe _olivesInPastaPuttanesca;
    FoodItemRecipe _anchoviesInPastaPuttanesca;
    FoodItemRecipe _garlicInPastaPuttanesca;
    FoodItemRecipe _capersInPastaPuttanesca;

    //Pasta with tuna
    FoodItemRecipe _pastaInPastaTuna;
    FoodItemRecipe _tunaInPastaTuna;
    FoodItemRecipe _capersInPastaTuna;
    FoodItemRecipe _lemonInPastaTuna;

    //Steak with pepper sauce
    FoodItemRecipe _steakInSteakWithPepperSauce;
    FoodItemRecipe _potatoesInSteakWithPepperSauce;
    FoodItemRecipe _greenBeansInSteakWithPepperSauce;
    FoodItemRecipe _pepperSauceInSteakWithPepperSauce;

    //Steak with whiskey sauce
    FoodItemRecipe _steakInSteakWithWhiskeySauce;
    FoodItemRecipe _potatoesInSteakWithWhiskeySauce;
    FoodItemRecipe _greenBeansInSteakWithWhiskeySauce;
    FoodItemRecipe _whiskeySauceInSteakWithWhiskeySauce;

    //Tacos
    FoodItemRecipe _tomatosInTacos;
    FoodItemRecipe _onionInTacos;
    FoodItemRecipe _avocadoInTacos;
    FoodItemRecipe _mincedBeefInTacos;
    FoodItemRecipe _garlicInTacos;
    FoodItemRecipe _kidneyBeansInTacos;
    FoodItemRecipe _limeInTacos;
    FoodItemRecipe _corianderInTacos;
    FoodItemRecipe _paprikaInTacos;
    FoodItemRecipe _oreganoInTacos;
    FoodItemRecipe _tacosInTacos;

    //Honey and Garlic Fried Pork Chops
    FoodItemRecipe _porkInHoneyGarlicFriedPorkChops;
    FoodItemRecipe _honeyInHoneyGarlicFriedPorkChops;
    FoodItemRecipe _garlicInHoneyGarlicFriedPorkChops;
    FoodItemRecipe _potatoesInHoneyGarlicFriedPorkChops;
    FoodItemRecipe _broccoliInHoneyGarlicFriedPorkChops;
    FoodItemRecipe _butterInHoneyGarlicFriedPorkChops;
    FoodItemRecipe _vinegarInHoneyCarlicFriedPorkChops;
    
    //Chicken Fried Rice
    FoodItemRecipe _chickenInChickenFriedRice;
    FoodItemRecipe _riceInChickenFriedRice;
    FoodItemRecipe _onionInChickenFriedRice;
    FoodItemRecipe _carrotInChickenFriedRice;
    FoodItemRecipe _garlicInChickenFriedRice;
    FoodItemRecipe _oilInChickenFriedRice;
    FoodItemRecipe _soysauceInChickenFriedRice;
    FoodItemRecipe _eggInChickenFriedRice;
    FoodItemRecipe _saltInChickenFriedRice;
    FoodItemRecipe _pepperInChickenFriedRice;
    
    //Shrimp Stir Fry
    FoodItemRecipe _shrimpsInShrimpStirFry;
    FoodItemRecipe _oilInShrimpStirFry;
    FoodItemRecipe _garlicInShrimpStirFry;
    FoodItemRecipe _gingerInShrimpStirFry;
    FoodItemRecipe _paprikaInShrimpStirFry;
    FoodItemRecipe _oreganoInShrimpStirFry;
    FoodItemRecipe _lemonInShrimpStirFry;
    FoodItemRecipe _onionInShrimpStirFry;
    FoodItemRecipe _saltInShrimpStirFry;
    FoodItemRecipe _pepperInShrimpStirFry;
    FoodItemRecipe _corianderInShrimpStirFry;
    FoodItemRecipe _riceInShrimpStirFry;

    //Spring Rolls
    FoodItemRecipe _oilInSpringRolls;
    FoodItemRecipe _mincedPorkInSpringRolls;
    FoodItemRecipe _garlicInSpringRolls;
    FoodItemRecipe _gingerInSpringRolls;
    FoodItemRecipe _springOnionsInSpringRolls;
    FoodItemRecipe _scallionsInSpringRolls;
    FoodItemRecipe _soysauceInSpringRolls;
    FoodItemRecipe _vinegarInSpringRolls;
    FoodItemRecipe _phylloDoughInSpringRolls;
    FoodItemRecipe _limeInSpringRolls;
    FoodItemRecipe _corianderInSpringRolls;
    
    //Baked Salmon and Asparagus
    FoodItemRecipe _lemonInBakedSalmonAndAsparagus;
    FoodItemRecipe _salmonInBakedSalmonAndAsparagus;
    FoodItemRecipe _asparagusInBakedSalmonAndAsparagus;
    FoodItemRecipe _butterInBakedSalmonAndAsparagus;
    FoodItemRecipe _garlicInBakedSalmonAndAsparagus;
    FoodItemRecipe _saltInBakedSalmonAndAsparagus;
    
    //Crackers with pesti
    FoodItemRecipe _crackersInCWP;
    FoodItemRecipe _pestiInCWP;

    //Salami stick, olive, cheese
    FoodItemRecipe _salamiInSSOAC;
    FoodItemRecipe _oliveInSSOAC;
    FoodItemRecipe _cheeseInSSOAC;

    //Salami stick
    FoodItemRecipe _salamiInSS;

    //Peanutbutter Sandwich

    FoodItemRecipe _peanutbutterInPeanutbutterSandwich;
    FoodItemRecipe _toastInPeanutbutterSandwich;
    
    //Apple Carrot Orange smoothie
    FoodItemRecipe _appleinACOS;
    FoodItemRecipe _orangeinACOS;
    FoodItemRecipe _carrotinACOS;
    FoodItemRecipe _milkinACOS;

    //Blueberry smoothie
    FoodItemRecipe _blueberriesinBS;
    FoodItemRecipe _milkInBS;

    //Mango smoothie
    FoodItemRecipe _mangoInMS;
    FoodItemRecipe _milkInMS;

    //Strawberry banana smoothie
    FoodItemRecipe _bananaInSBS;
    FoodItemRecipe _strawberryInSBS;
    FoodItemRecipe _milkInSBS;

    //Blackberry strawberry fruit mix
    FoodItemRecipe _blackberryInBSFM;
    FoodItemRecipe _strawberryInBSFM;

    //Raspberry, Blueberry fruit mix
    FoodItemRecipe _raspberryInRBFM;
    FoodItemRecipe _blueberryInRBFM;
    
    //Apple orange pear fruit mix
    FoodItemRecipe _appleInAOPFM;
    FoodItemRecipe _orangeInAOPFM;
    FoodItemRecipe _pearInAOPFM;

    //pineapple orange fruit mix
    FoodItemRecipe _pIneappleInPOFM;
    FoodItemRecipe _orangeInPOFM;

    //almonds raisin mix
    FoodItemRecipe _raisinInARM;
    FoodItemRecipe _almondInARM;

    //pistachio nut mix
    FoodItemRecipe _pistachioinPNM;

    //cashew peanut mix
    FoodItemRecipe _peanutInCPM;
    FoodItemRecipe _cashewInCPM;

    //Frikadeller with rice
    FoodItemRecipe _mincedVealAndPorkInFWR;
    FoodItemRecipe _eggInFWR;
    FoodItemRecipe _flourInFWR;
    FoodItemRecipe _cuminInFWR;
    FoodItemRecipe _riceInFWR;
    FoodItemRecipe _onionInFWR;
    FoodItemRecipe _oliveOilInFWR;
    FoodItemRecipe _saltInFWR;
    FoodItemRecipe _pepperInFWR;

    //Tortillas
    FoodItemRecipe _tortillasInTortillas;
    FoodItemRecipe _mincedMeatInTortillas;
    FoodItemRecipe _bellPepperInTortillas;
    FoodItemRecipe _cremeFraicheInTortillas;
    FoodItemRecipe _avocadoInTortillas;
    FoodItemRecipe _lemonInTortillas;
    FoodItemRecipe _garlicInTortillas;
    FoodItemRecipe _cucumberInTortillas;
    FoodItemRecipe _tomatoInTortillas;
    

    //Lamb chop with potatoes
    FoodItemRecipe _lambInLWP;
    FoodItemRecipe _potatoesInLWP;
    FoodItemRecipe _oliveOilInLWP;
    FoodItemRecipe _saltInLWP;
    FoodItemRecipe _pepperInLWP;
    FoodItemRecipe _garlicInLWP;

    //veganLasagna

    FoodItemRecipe _pastaInVL;
    FoodItemRecipe _carrotInVL;
    FoodItemRecipe _bellPepperInVL;
    FoodItemRecipe _oliveOilInVL;
    FoodItemRecipe _garlicInVL;
    FoodItemRecipe _saltInVL;
    FoodItemRecipe _slicedTomatoesInVL;
    FoodItemRecipe _mozzarellaInVL;

    //meat Lasagna

    FoodItemRecipe _mincedMeatInML;
    FoodItemRecipe _pastaInML;
    FoodItemRecipe _carrotInML;
    FoodItemRecipe _bellPepperInML;
    FoodItemRecipe _oliveOilInML;
    FoodItemRecipe _garlicInML;
    FoodItemRecipe _saltInML;
    FoodItemRecipe _slicedTomatoesInML;
    FoodItemRecipe _mozzarellaInML;



    public void SeedRecipes()
    {
        _context.Categories.Add(_fruit);
        _context.Categories.Add(_meat);
        _context.Categories.Add(_vegetarian);
        _context.SaveChanges();
        _author = new User("NourishNexus", "nn@nn.dk", new byte[32], new byte[32], new List<Recipe>());
        _context.Users.Add(_author);
        _context.SaveChanges();
        InitializeFoodItems();
        InitializeRecipes();
        AddFoodItemsToRecipes();
        Console.WriteLine($"Author ID is: {_author.Id}");
    }


    private void InitializeFoodItems()
    {
        _egg = new FoodItem("Eggs", 137f, 12.3f, 1.2f, 0f, 0f, 7.65f, 2.46f, 3.68f, 1.4f, 0.02f, 71.9f, 0.1f, 1.72f, 0.1f, 2.33f, 4.31f, 0.076f, 0.44f, 0.08f, 0f, 0.3f, 129.1f, 11.6f, 1.77f, 1.16f, 172f, 0.051f, 65.7f, 22.2f, 46.3f);
        _bacon = new FoodItem("Bacon", 430f, 13f, 0f, 0f, 0f, 39.06f, 17.09f, 18.9f, 3.07f, 0f, 0f, 0.15f, 0.65f, 0.8f, 0.43f, 0.3f, 0.48f, 0.16f, 3f, 2f, 2.85f, 185f, 15f, 0.51f, 1.98f, 220f, 0.063f, 0.7f, 8.3f, 5.1f);
        _buns = new FoodItem("Buns", 294f, 9.7f, 51.7f, 0f, 3.5f, 2.59f, 0.68f, 0.63f, 1.29f, 0f, 0f, 0.08f, 0f, 0f, 0f, 0.5f, 0.157f, 0.09f, 1.2f, 62.1f, 1.1f, 153.1f, 25.6f, 1.32f, 0.79f, 116f, 0.143f, 2.5f, 3.1f, 66.7f);
        _butter = new FoodItem("Butter", 741f, 0.7f, 0.6f, 0f, 0f, 77.02f, 53.54f, 16.98f, 2.2f, 3.52f, 749.3f, 0f, 0.17f, 0f, 0.41f, 1.76f, 0.007f, 0.04f, 0.1f, 3f, 0.9f, 29.1f, 1.6f, 0.01f, 0.04f, 21f, 0.022f, 2.7f, 1.56f, 16.8f);
        _strawberryJam = new FoodItem("Strawberry Jam", 226f, 0.5f, 54.3f, 55f, 1.2f, 0.4f, 0.04f, 0.07f, 0.29f, 0f, 4.2f, 0.03f, 0f, 14f, 0f, 0.2f, 0f, 0.04f, 0.2f, 5f, 0f, 44f, 4f, 0.5f, 0.03f, 13f, 0.12f, 1.4f, 0.3f, 25f);
        _raspberryJam = new FoodItem("Raspberry Jam", 203f, 0.6f, 48.1f, 44f, 1.2f, 0.48f, 0.05f, 0.05f, 0.39f, 0f, 4.2f, 0.03f, 0f, 5f, 0f, 0.15f, 0f, 0.04f, 0.2f, 5f, 0f, 44f, 4f, 0.5f, 0.03f, 13f, 0.12f, 1.4f, 0.3f, 15f);
        _blackberryJam = new FoodItem("Blackberry Jam", 203f, 0.6f, 48.1f, 46f, 1.2f, 0.48f, 0.05f, 0.05f, 0.39f, 0f, 4.2f, 0.03f, 0f, 5f, 0f, 0.15f, 0f, 0.04f, 0.2f, 5f, 0f, 44f, 4f, 0.5f, 0.03f, 13f, 0.12f, 1.4f, 0.3f, 15f);
        _rhubarbJam = new FoodItem("Rhubarb Jam", 203f, 0.6f, 48.1f, 46f, 1.2f, 0.48f, 0.05f, 0.05f, 0.39f, 0f, 4.2f, 0.03f, 0f, 5f, 0f, 0.15f, 0f, 0.04f, 0.2f, 5f, 0f, 44f, 4f, 0.5f, 0.03f, 13f, 0.12f, 1.4f, 0.3f, 15f);
        _slicedCheese = new FoodItem("Sliced Cheese", 335f, 24.7f, 0.1f, 0f, 0f, 24.27f, 16.36f, 5.68f, 0.69f, 1.17f, 229.7f, 0.09f, 1.38f, 0.5f, 0.26f, 0.67f, 0.05f, 0.33f, 0.1f, 60f, 1.475f, 65f, 27f, 0.11f, 3.2f, 528f, 0.044f, 10.4f, 9.35f, 686f);
        _Peanutbutter = new FoodItem("Peanut butter", 637f, 22.6f, 12.2f, 6.38f, 7.6f, 51.55f, 10.69f, 27.35f, 13.51f, 0f, 0f, 0.5f, 0f, 0f, 0f, 4.7f, 0.17f, 0.1f, 15f, 53f, 0.9f, 700f, 180f, 2.1f, 3f, 330f, 0.7f, 0.5f, 6.9f, 37f);
        _mapleSyrup = new FoodItem("Maple Syrup", 308f, 0.3f, 76.7f, 76.7f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0.01f, 0.1f, 0f, 0.2f, 220f, 34f, 2.5f, 0.13f, 3f, 0.24f, 5f, 1f, 75f);
        _spreadChocolate = new FoodItem("Chocolate Spread", 527f, 7f, 55.6f, 54f, 4.9f, 28.3f, 9.15f, 15.79f, 3.36f, 0f, 400f, 0.14f, 0.2f, 1f, 0f, 7.8f, 0.01f, 0.28f, 0.45f, 23.7f, 0.025f, 476f, 76f, 2.3f, 0.12f, 178f, 0f, 7f, 5f, 124f);
        _quinoa = new FoodItem("Quinoa", 347f, 13.7f, 42.3f, 3.2f, 13.9f, 4.4f, 0.51f, 1.18f, 2.71f, 0f, 0f, 0.14f, 0f, 0f, 0f, 2.33f, 0.064f, 0.29f, 0.73f, 195.5f, 0f, 870f, 175f, 3.75f, 2.8f, 415f, 0.515f, 0f, 4.6f, 46.5f);
        _oats = new FoodItem("Oat", 366f, 12.9f, 58.9f, 1.03f, 9.9f, 6.47f, 1.19f, 2.38f, 2.81f, 0f, 0f, 0.14f, 0f, 0f, 0f, 0.75f, 0.416f, 0.11f, 0.8f, 50.4f, 0.00405f, 386.3f, 154.5f, 3.86f, 2.99f, 440f, 0.39f, 0.5f, 5f, 115.3f);
        _milk = new FoodItem("Milk", 37f, 3.5f, 4.8f, 4.75f, 0f, 0.47f, 0.35f, 0.11f, 0.01f, 0f, 4.4f, 0.05f, 0.48f, 1.3f, 0.08f, 0.01f, 0.041f, 0.17f, 0.09f, 5.8f, 0.1f, 157.2f, 12.1f, 0.03f, 0.41f, 97f, 0.01f, 23.3f, 1.64f, 123.6f);
        _flute = new FoodItem("Flute", 259f, 8.1f, 47.9f, 0f, 4f, 2.33f, 0.94f, 0.73f, 0.66f, 0f, 0f, 0.1f, 0.07f, 0f, 0f, 0.5f, 0.192f, 0.07f, 1.57f, 39.3f, 1.1498611f, 161.5f, 28f, 1.16f, 0.82f, 112f, 0.134f, 23f, 2.06f, 35.2f);
        _ham = new FoodItem("Ham", 109f, 17.9f, 0.3f, 0f, 0f, 3.4f, 1.34f, 1.62f, 0.44f, 0f, 0f, 0.32f, 0.54f, 29f, 0.09f, 0.33f, 0.9f, 0.16f, 5.5f, 1.3f, 0f, 327f, 21f, 0.69f, 1.69f, 302f, 0.08f, 0f, 7.5f, 6.3f);
        _brunchSausages = new FoodItem("Brunch Sausages", 292f, 14f, 4.3f, 2.83f, 0.2f, 23.14f, 9.01f, 11.45f, 2.68f, 0f, 0f, 0.15f, 1.1f, 0f, 0.3f, 0.1f, 0.358f, 0.1f, 1.8f, 4f, 2.7f, 208.7f, 18.7f, 0.79f, 1.31f, 184f, 0.067f, 14f, 7.2f, 21.9f);
        _cornflakes = new FoodItem("Corn Flakes", 374f, 7.5f, 82f, 7.06f, 3.1f, 1.26f, 0.21f, 0.39f, 0.66f, 0f, 0f, 0.12f, 0f, 0f, 0f, 0.02f, 0.659f, 0.57f, 1.1f, 26.7f, 1.5195689f, 102.9f, 15.2f, 2.89f, 0.27f, 57f, 0.067f, 0.9f, 2.6f, 4.3f);
        _flour = new FoodItem("Flour", 343f, 9.7f, 56.5f, 1.13f, 6f, 1.24f, 0.38f, 0f, 0.85f, 0f, 0f, 0.07f, 0f, 0f, 0f, 0.43f, 0.164f, 0.03f, 0.37f, 30.6f, 0f, 154.7f, 25.4f, 1.17f, 0.87f, 115f, 0.134f, 1.4f, 4.23f, 17.6f);
        _lemon = new FoodItem("Lemon", 31f, 0.4f, 3.8f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0.3f, 0.05f, 0f, 46f, 0f, 0.04f, 0.02f, 0.01f, 0.1f, 7f, 0.015f, 160f, 7f, 0.4f, 0.13f, 21f, 0.053f, 0.6f, 0.12f, 110f);
        _blueberry = new FoodItem("Blueberry", 52f, 0.7f, 10.4f, 0f, 1.5f, 0.4f, 0.03f, 0.09f, 0.52f, 0f, 1.1f, 0.06f, 0f, 44f, 0f, 0f, 0.03f, 0.03f, 0.4f, 6f, 0.0075f, 103f, 7f, 0.8f, 0.1f, 9f, 0.11f, 1.2f, 0f, 15f);
        _blackberry = new FoodItem("Blackberry", 37f, 1.4f, 4.5f, 0f, 4.3f, 0.8f, 0.01f, 0.02f, 0.3f, 0f, 16.7f, 0.05f, 0f, 15f, 0f, 5.5f, 0.017f, 0.05f, 0.5f, 34f, 0.005f, 266f, 23f, 0.55f, 0.53f, 37f, 0.12f, 0.4f, 0.1f, 27f);
        _strawberry = new FoodItem("Strawberry", 38f, 0.7f, 6.1f, 0f, 1.5f, 0.48f, 0.05f, 0.08f, 0.35f, 0f, 3.3f, 0.05f, 0f, 66.9f, 0f, 0.41f, 0.015f, 0.01f, 0.43f, 117f, 0.0012695f, 178.6f, 12.5f, 0.25f, 0.1f, 24f, 0.038f, 0.1f, 0.18f, 18.5f);
        _raspberry = new FoodItem("Raspberry", 51f, 1.4f, 4.1f, 0f, 4.4f, 1.08f, 0.1f, 0.1f, 0.87f, 0f, 3.5f, 0.09f, 0f, 24f, 0f, 1.4f, 0.03f, 0.05f, 0.5f, 44f, 0.005f, 228f, 17f, 0.55f, 0.34f, 38f, 0.105f, 0.4f, 0.19f, 19.7f);
        _almond = new FoodItem("Almond", 606f, 21.2f, 6.6f, 6.55f, 10.6f, 46.91f, 4.05f, 31.4f, 11.36f, 0f, 0f, 0.13f, 0f, 0.8f, 0f, 23.35f, 0.137f, 0.94f, 1.88f, 76.1f, 0.00175f, 730f, 263.8f, 3.4f, 3.3f, 489f, 1f, 0.2f, 2.15f, 256.5f);
        _raisin = new FoodItem("Raisin", 333f, 3.2f, 69f, 68.85f, 3.6f, 1.06f, 0.52f, 0.06f, 0.47f, 0f, 2.3f, 0.11f, 0f, 3.3f, 0f, 0f, 0.085f, 0.03f, 0.5f, 4f, 0f, 785f, 35f, 2.4f, 0.3f, 107f, 0.32f, 2f, 0.4f, 45.1f);
        _cashewnut = new FoodItem("Cashew", 603f, 15.3f, 26.9f, 0f, 3f, 44.31f, 8.81f, 27.58f, 7.92f, 0f, 0f, 0.26f, 0f, 0f, 0f, 0.84f, 0.2f, 0.2f, 1.4f, 69f, 0.04f, 565f, 260f, 6f, 5.6f, 490f, 2.22f, 0f, 14.63f, 45f);
        _peanut = new FoodItem("Peanut", 596f, 25.8f, 8.1f, 3.1f, 7.7f, 46.78f, 6.83f, 24.42f, 15.55f, 0f, 0f, 0.35f, 0f, 0f, 0f, 8.2f, 0.91f, 0.1f, 20f, 106f, 0.005f, 703f, 170f, 1.9f, 3.1f, 409f, 0.86f, 0.5f, 6.9f, 55.6f);
        _mango = new FoodItem("Mango", 67f, 0.5f, 14.2f, 0f, 1.9f, 0.38f, 0.11f, 0.18f, 0.09f, 0f, 46.1f, 0.13f, 0f, 40.1f, 0f, 1.1f, 0.058f, 0.06f, 0.58f, 71.1f, 0.0051875f, 105.3f, 8.8f, 0.24f, 0.08f, 16f, 0.11f, 0.3f, 0.6f, 13.9f);
        _banana = new FoodItem("Banana", 93f, 1.1f, 19.7f, 0f, 1.6f, 0.15f, 0.08f, 0.04f, 0.03f, 0f, 4.4f, 0.31f, 0f, 11.2f, 0f, 0.27f, 0.04f, 0.01f, 0.6f, 38f, 0f, 348.1f, 28.1f, 0.25f, 0.17f, 26f, 0.106f, 0f, 0.35f, 6.6f);
        _apple = new FoodItem("Apple", 55f, 0.3f, 11.3f, 0f, 2.2f, 0.18f, 0.04f, 0.01f, 0.13f, 0f, 2.1f, 0.05f, 0f, 8.3f, 0f, 0.25f, 0.013f, 0.01f, 0.12f, 9f, 0.001503f, 117.9f, 4.5f, 0.12f, 0.02f, 10f, 0.031f, 0.1f, 0.01f, 4.1f);
        _orange = new FoodItem("Orange", 49f, 0.9f, 8.2f, 0f, 2f, 0.09f, 0.02f, 0.02f, 0.05f, 0f, 4f, 0.08f, 0f, 54.4f, 0f, 0.31f, 0.1f, 0.03f, 0.39f, 46.2f, 0.002846667f, 157.8f, 10.3f, 0.12f, 0.06f, 21f, 0.04f, 0.1f, 0.05f, 29.6f);
        _carrot = new FoodItem("Carrot", 29f, 0.3f, 4.4f, 0f, 2.5f, 0.04f, 0.01f, 0f, 0.03f, 0f, 524f, 0.07f, 0f, 2.5f, 0f, 0.33f, 0.008f, 0.03f, 0.28f, 11f, 0.095f, 233.3f, 9.1f, 0.21f, 0.24f, 19f, 0.05f, 0f, 0f, 29.5f);
        _pistachionut = new FoodItem("Pistachio", 606f, 21.6f, 8.1f, 7.2f, 8.8f, 45.53f, 5.81f, 25.33f, 14.28f, 0f, 0f, 0.9f, 0f, 5f, 0f, 1.64f, 0.424f, 0.24f, 1.1f, 68.4f, 0f, 1000f, 110f, 2.8f, 2.45f, 470f, 1.3f, 0f, 6.75f, 91.5f);
        _pineapple = new FoodItem("Pineapple", 55f, 0.5f, 10.8f, 0f, 1.4f, 0.32f, 0.01f, 0.01f, 0.02f, 0f, 5f, 0.09f, 0f, 25f, 0f, 0.1f, 0.08f, 0.02f, 0.2f, 12.1f, 0.01f, 174f, 14f, 0.2f, 0.08f, 14f, 0.09f, 1.4f, 0.6f, 18.9f);
        _bakedBeans= new FoodItem("Baked Beans", 88f, 5.1f, 9.6f, 5f, 6.6f, 0.38f, 0.08f, 0.05f, 0.25f, 0f, 0f, 0.12f, 0f, 0f, 0f, 0.6f, 0.07f, 0.05f, 0.5f, 29f, 1.2f, 300f, 31f, 1.4f, 0.7f, 91f, 0.21f, 0.6f, 3f, 45f);
        _kidneyBeans = new FoodItem("Kidney Beans", 312f, 18.9f, 45.6f, 3.2f, 17.8f, 1.6f, 0.2f, 0.1f, 0.9f, 0f, 1.1f, 0.42f, 0f, 0f, 0f, 0.34f, 0.35f, 0.14f, 2f, 140f, 0.02f, 1327f, 131f, 5f, 2f, 477f, 0.5f, 1.9f, 8.8f, 77.3f);
        _slicedTomatoes = new FoodItem("Sliced Tomatoes, canned", 21f, 1.2f, 3f, 0f, 0.9f, 0.24f, 0.06f, 0.04f, 0.15f, 0f, 29.3f, 0.11f, 0f, 11.3f, 0f, 0.74f, 0.045f, 0.06f, 0.58f, 24f, 0.3575f, 188f, 11f, 0.97f, 0.3f, 19f, 0.11f, 0f, 0.1f, 31f);
        _mincedBeef = new FoodItem("Minced Beef", 163f, 19.5f, 0f, 0f, 0f, 7.97f, 3.6f, 3.65f, 0.22f, 0.21f, 6.9f, 0.27f, 2.32f, 0f, 0.51f, 0.48f, 0.047f, 0.17f, 4.32f, 9.6f, 0.2f, 312f, 19.7f, 2.26f, 4.34f, 177f, 0.073f, 0.8f, 6.8f, 7.1f);
        _basmatiRice = new FoodItem("Basmati Rice", 354f, 8.4f, 78.1f, 0f, 0.7f, 0.98f, 0.27f, 0.29f, 0.42f, 0f, 0f, 0.11f, 0f, 0f, 0f, 0.05f, 0.07f, 0.04f, 1.4f, 31f, 0f, 150f, 35f, 1.2f, 1.7f, 130f, 0.2f, 2.2f, 6f, 52.7f);
        _cremeFraiche = new FoodItem("Creme Fraiche", 192f, 2.8f, 2.8f, 0f, 0f, 17.56f, 12.43f, 3.73f, 0.47f, 0.73f, 125.8f, 0.03f, 0.2f, 0.1f, 0.22f, 0.26f, 0.032f, 0.18f, 0.07f, 10f, 0.075f, 122.5f, 9.9f, 0.02f, 0.34f, 77f, 0.015f, 10.1f, 2.2f, 98.4f);
        _onion = new FoodItem("Onion", 43f, 1.2f, 5.4f, 0f, 1.9f, 0.07f, 0.02f, 0.01f, 0.04f, 0f, 2.5f, 0.17f, 0f, 8.1f, 0f, 0.06f, 0.038f, 0.01f, 0.19f, 36f, 0.006752632f, 186.1f, 9.2f, 0.28f, 0.19f, 31f, 0.037f, 0.2f, 0.13f, 23.3f);
        _garlic = new FoodItem("Garlic", 158f, 6.4f, 30.9f, 0f, 2.1f, 0.35f, 0.09f, 0.01f, 0.25f, 0f, 0f, 1.24f, 0f, 8.2f, 0f, 0.01f, 0.2f, 0.11f, 0.7f, 103f, 0.0425f, 401f, 25f, 1.7f, 1.16f, 160f, 0.299f, 0.2f, 2f, 20.6f);
        _risottoRice = new FoodItem("Risotto Rice", 362f, 6.8f, 76.3f, 0.2f, 2.4f, 2.24f, 0.54f, 0.97f, 0.96f, 0f, 0f, 0.51f, 0f, 0f, 0f, 0f, 0.413f, 0.04f, 0f, 20f, 0f, 268f, 143f, 1.8f, 2.02f, 307f, 0f, 2.1f, 0f, 12.1f);
        _chickenBreastFilet = new FoodItem("Chicken Breast Fillet", 149f, 21.5f, 0f, 0f, 0f, 6.31f, 1.96f, 2.88f, 1.48f, 0f, 24f, 0.53f, 0.34f, 1f, 1.5f, 0.5f, 0.06f, 0.09f, 9.9f, 4f, 0.2f, 220f, 25f, 0.9f, 0.8f, 198f, 0f, 0f, 10f, 11f);
        _champignon = new FoodItem("Champignon", 23f, 1.6f, 0.1f, 0.08f, 0.8f, 0.1f, 0.03f, 0f, 0.07f, 0f, 0f, 0.07f, 0f, 0f, 0f, 0f, 0.041f, 0.35f, 3.01f, 23.1f, 0f, 348.8f, 9.2f, 0.16f, 0.36f, 85f, 0.2f, 0f, 16.8f, 3.1f);
        _whiteWine = new FoodItem("White Wine", 79f, 0.1f, 2.4f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0.01f, 0f, 0f, 0f, 0f, 0f, 0.01f, 0.08f, 0.2f, 0.0525f, 88f, 9f, 1.21f, 0.06f, 8f, 0.043f, 35f, 0.04f, 14f);
        _eggplant = new FoodItem("Eggplant", 17f, 0.9f, 2.4f, 0f, 2.2f, 0f, 0f, 0f, 0f, 0f, 2.6f, 0.05f, 0f, 1.3f, 0f, 0f, 0.02f, 0.03f, 0.36f, 6.1f, 0.003333333f, 217.5f, 13.6f, 0.17f, 0.14f, 25f, 0.046f, 0f, 0f, 10.5f);
        _parmesanCheese = new FoodItem("Parmesan Cheese", 357f, 33.6f, 0.1f, 0.05f, 0f, 22.77f, 15.61f, 6.49f, 0.68f, 0f, 213.8f, 0.09f, 1.6f, 0f, 0.24f, 0.61f, 0.039f, 0.33f, 0.27f, 7f, 2.495f, 92f, 44f, 0.82f, 2.75f, 930f, 0.03f, 13.8f, 11f, 1180f);
        _vegetableBouillion = new FoodItem("Vegetable Bouillion", 4f, 0.3f, 0.3f, 0f, 0f, 0.21f, 0.05f, 0.08f, 0.07f, 0f, 0f, 0f, 0f, 0f, 0.01f, 0.01f, 0.002f, 0.01f, 0.04f, 1f, 0.7825f, 6f, 2f, 0.02f, 0.01f, 3f, 0.01f, 0.3f, 0.5f, 6f);
        _steak = new FoodItem("Steak", 265f, 18.2f, 0f, 0f, 0f, 20.02f, 8.99f, 10.17f, 0.86f, 0f, 25f, 0.4f, 1.4f, 0f, 0.8f, 0.66f, 0.039f, 0.16f, 5.3f, 5f, 0.1f, 293f, 19f, 1.9f, 3.9f, 170f, 0.062f, 1.1f, 6.5f, 4f);
        _potatoes = new FoodItem("Potatoes", 77f, 2f, 17.9f, 0f, 1.4f, 0.23f, 0.06f, 0.01f, 0.16f, 0f, 0.8f, 0.2f, 0f, 26.4f, 0f, 0.1f, 0.055f, 0.06f, 1.6f, 36f, 0.0175f, 413.5f, 20.4f, 1.04f, 0.3f, 55f, 0.052f, 1.2f, 0.27f, 6.8f);
        _salami = new FoodItem("Salami",  509f, 13.9f, 2.8f, 0f, 0f, 45.27f, 18.07f, 21.65f, 5.39f, 0f, 0f, 0.15f, 1.3f, 0f, 0.47f, 0.02f, 0.14f, 0.18f, 2.3f, 3f, 4.9689903f, 203.6f, 10f, 1.1f, 1.67f, 119f, 0.11f, 4.1f, 5f, 11.1f);
        _mozzarella = new FoodItem("Mozzarella", 326f, 24.1f, 0f, 0f, 0f, 23.72f, 16.25f, 6.76f, 0.71f, 0f, 222.2f, 0.09f, 1.38f, 0.5f, 0.25f, 0.64f, 0.05f, 0.33f, 0.1f, 60f, 1.395f, 65f, 27f, 0.11f, 3.2f, 390f, 0.086f, 10.4f, 8f, 720f);
        _tuna = new FoodItem("Canned tuna in water", 107f, 23.9f, 0f, 0f, 0f, 0.96f, 0.23f, 0.11f, 0.31f, 0f, 5.1f, 0.25f, 3.4f, 0f, 2.81f, 0.6f, 0f, 0.06f, 11.75f, 15f, 0.68625f, 224.5f, 27.1f, 1f, 0.58f, 164f, 0.047f, 15.6f, 82f, 8.9f);
        _toast = new FoodItem("Fine-grained toast bread", 255f, 7.9f, 47f, 0f, 3.3f, 2.55f, 0.79f, 0.96f, 0.8f, 0f, 0f, 0.06f, 0f, 0f, 0f, 0.5f, 0.161f, 0.05f, 1.18f, 25f, 1.1346428f, 110.6f, 17.8f, 0.96f, 0.58f, 80f, 0.102f, 18.6f, 1.57f, 46.2f);
        _gherkins = new FoodItem("Gherkins", 58f, 0.3f, 11.8f, 1f, 1.8f, 0.49f, 0.23f, 0.02f, 0.31f, 0f, 15f, 0.04f, 0f, 0f, 0f, 0.07f, 0.04f, 0.09f, 0.2f, 8f, 0f, 129f, 11f, 0.2f, 0.2f, 23f, 0.057f, 3f, 0f, 17f);
        _liverpate = new FoodItem("Liverpate", 238f, 11.3f, 4.7f, 3.7f, 0.4f, 17.36f, 6.9f, 8.2f, 2.26f, 0f, 3950f, 0.21f, 9.9f, 29f, 0f, 0.26f, 0.13f, 1.02f, 4.4f, 170f, 1.8f, 170f, 12.5f, 5.57f, 2.5f, 164f, 0.41f, 3.1f, 19.2f, 26f);
        _mayonaise = new FoodItem("Mayonaise",730f, 1.1f, 0.1f, 0.1f, 0f, 76f, 6.88f, 43.88f, 25.23f, 0f, 60f, 0.1f, 1f, 0f, 1f, 7.6f, 0.008f, 0.03f, 0.1f, 14f, 1.5f, 34f, 7f, 0.3f, 0.4f, 59f, 0.03f, 6f, 1.6f, 8f);
        _mackarelInTomatoes = new FoodItem("Mackerel in tomatoes", 155f, 11.7f, 3.9f, 0f, 0f, 9.57f, 2.21f, 4.1f, 3f, 0f, 29.1f, 0.17f, 5.85f, 0f, 2.6f, 1.52f, 0.045f, 0.17f, 4.75f, 19f, 0.9f, 357f, 23.1f, 0.7f, 0.59f, 122f, 0.107f, 12.4f, 19.5f, 16f);
        _avocado = new FoodItem("Avocado", 155f, 1.6f, 1.2f, 1.14f, 5.5f, 12.53f, 2.77f, 7.69f, 1.74f, 0f, 5.6f, 0.14f, 0f, 2.9f, 0f, 1.94f, 0.044f, 0.11f, 1.22f, 99.3f, 0.006f, 433.8f, 28.9f, 0.47f, 0.54f, 45f, 0.253f, 0f, 0f, 14.6f);
        _smokedSalmon = new FoodItem("Smoked Salmon", 170f, 20.9f, 1.6f, 0f, 0f, 7.04f, 1.1f, 3.7f, 2.02f, 0f, 6.5f, 0.66f, 4.6f, 0f, 3.1f, 2.24f, 0.26f, 0.08f, 7.5f, 0f, 3f, 408f, 28.2f, 0.22f, 0.34f, 248f, 0.036f, 5.9f, 17.33f, 5.9f);
        _ryebread = new FoodItem("Ryebread", 170f, 20.9f, 1.6f, 0f, 0f, 7.04f, 1.1f, 3.7f, 2.02f, 0f, 6.5f, 0.66f, 4.6f, 0f, 3.1f, 2.24f, 0.26f, 0.08f, 7.5f, 0f, 3f, 408f, 28.2f, 0.22f, 0.34f, 248f, 0.036f, 5.9f, 17.33f, 5.9f);
        _fishFillets = new FoodItem("Fish Fillet", 283f, 12.7f, 22.9f, 0f, 0f, 14.1f, 3.04f, 7.4f, 3.59f, 0f, 0f, 0.08f, 0.74f, 0f, 1.65f, 3.8f, 0.13f, 0.07f, 1.53f, 0f, 1.1f, 185f, 21.5f, 0.58f, 0.42f, 134f, 0.067f, 15.6f, 20f, 48.5f);
        _frenchFries = new FoodItem("French Fries",311f, 3.7f, 39f, 0f, 3.2f, 14.07f, 3.65f, 7.49f, 2.88f, 0.05f, 0f, 0.18f, 0f, 23.3f, 0f, 0.1f, 0.128f, 0.06f, 1.2f, 10f, 0.9f, 587.4f, 32.7f, 0.79f, 0.51f, 120f, 0.171f, 0f, 2.3f, 13.2f);
        _kebab = new FoodItem("Durum with kebab, salad and dressing", 225f, 12.6f, 20.3f, 0f, 2.1f, 8.56f, 2.69f, 4f, 1.53f, 0.32f, 0f, 0f, 0f, 0f, 0f, 0f, 0.098f, 0.1f, 0f, 0f, 1.3f, 286.3f, 20.6f, 1.25f, 2.6f, 134f, 0.08f, 0f, 0f, 19f);
        _cucumber = new FoodItem("Cucumber", 12f, 0.7f, 1.6f, 1.56f, 0.8f, 0.02f, 0.01f, 0f, 0.01f, 0f, 5.2f, 0.04f, 0f, 10.4f, 0f, 0.05f, 0.015f, 0.01f, 0.18f, 17.6f, 0.006015893f, 147.4f, 9.5f, 0.2f, 0.13f, 27f, 0.027f, 0.5f, 0.03f, 17.7f);
        _tomato = new FoodItem("Tomato",  20f, 0.7f, 3.2f, 3.18f, 1.9f, 0.24f, 0.06f, 0.04f, 0.15f, 0f, 82.7f, 0.09f, 0f, 15f, 0f, 1.1f, 0.043f, 0.02f, 0.73f, 31f, 0.01625f, 216f, 6.5f, 0.24f, 0.09f, 27f, 0.043f, 0f, 0.3f, 7.4f);
        _icebergSalad = new FoodItem("Iceberg Salad", 16f, 0.8f, 2.1f, 2.15f, 1.1f, 0.07f, 0.01f, 0f, 0.05f, 0f, 12.5f, 0.04f, 0f, 5.5f, 0f, 0.15f, 0.044f, 0.03f, 0.23f, 89f, 0.007075f, 186f, 7.3f, 0.27f, 0.16f, 22f, 0.012f, 1f, 0f, 15.5f);
        _lasagnaSheets = new FoodItem("Lasagna Sheets",128f, 5f, 26.9f, 0f, 2f, 0.28f, 0.06f, 0.04f, 0.18f, 0f, 0f, 0.01f, 0f, 0f, 0f, 0.07f, 0.04f, 0.01f, 0.3f, 2f, 0f, 78f, 24f, 0.6f, 0.7f, 50f, 0.2f, 1f, 1.8f, 13f);
        _lambChop = new FoodItem("Lamb chop", 128f, 19.6f, 0f, 0f, 0f, 4.38f, 2.21f, 1.65f, 0.15f, 0.21f, 45f, 0.2f, 1.2f, 0f, 0.4f, 0.7f, 0.18f, 0.31f, 4.3f, 1.4f, 0.195f, 350f, 27f, 2.2f, 3.3f, 210f, 0.122f, 0.7f, 6.05f, 12.8f);
        _tortilla = new FoodItem("Tortilla", 267f, 9.6f, 39.1f, 0f, 5.3f, 4.91f, 1.31f, 1.55f, 2.05f, 0f, 0f, 0.08f, 0.12f, 0f, 0f, 0.5f, 0.218f, 0.08f, 1.12f, 26f, 1.1f, 194.8f, 43.4f, 1.5f, 1.14f, 147f, 0.205f, 16.6f, 3.57f, 33.2f);
        _sugar = new FoodItem("Sugar", 399f, 0.5f, 99.3f, 99.3f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 89f, 15f, 0.9f, 0.1f, 20f, 0.06f, 0f, 0f, 53f);
        _pepper = new FoodItem("Pepper",304f, 11f, 44.5f, 0f, 26.5f, 3.12f, 1.39f, 0.74f, 1f, 0f, 9.5f, 0.34f, 0f, 21f, 0f, 0.72f, 0.109f, 0.24f, 1.14f, 10f, 0.11f, 1259f, 194f, 28.86f, 1.42f, 173f, 1.13f, 0f, 3.1f, 437f);
        _salt = new FoodItem("Salt", 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 97.125f, 0f, 290f, 0.2f, 0.08f, 8f, 0.16f, 1556.4f, 0.5f, 29f);
        _oliveOil = new FoodItem("Olive oil", 900f, 0f, 0f, 0f, 0f, 95.7f, 12.24f, 74.73f, 8.73f, 0f, 0f, 0f, 0f, 0f, 0f, 5.1f, 0f, 0f, 0f, 0f, 0f, 1f, 0f, 0.56f, 0f, 0f, 0f, 0f, 0f, 1f);
        _coriander = new FoodItem("Coriander",  346f, 12.4f, 13.1f, 0f, 41.9f, 16.31f, 0.94f, 13.62f, 1.75f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0.239f, 0.29f, 2.13f, 0f, 0.0875f, 1267f, 330f, 16.32f, 4.7f, 409f, 0f, 2.3f, 26.2f, 709f);
        _chili = new FoodItem("Chili", 44f, 2f, 7.7f, 0f, 1.8f, 0.14f, 0.02f, 0.01f, 0.11f, 0f, 365.8f, 0.28f, 0f, 166f, 0f, 2.9f, 0.09f, 0.09f, 0.9f, 23f, 0.0175f, 340f, 25f, 1.2f, 0.3f, 68f, 0.174f, 0f, 0.5f, 14f);
        _cumin = new FoodItem("Cumin", 428f, 17.8f, 30.6f, 2.25f, 10.5f, 0f, 0f, 0f, 0f, 0f, 63.5f, 0.44f, 0f, 7.7f, 0f, 3.33f, 0.628f, 0.33f, 4.58f, 10f, 0.4f, 1788f, 366f, 66.36f, 4.8f, 499f, 0.867f, 0f, 5.2f, 931f);
        _ginger = new FoodItem("Ginger", 83f, 1.8f, 16.7f, 0f, 1f, 0.53f, 0.21f, 0.15f, 0.16f, 0f, 0f, 0.16f, 0f, 5f, 0f, 0f, 0.025f, 0.03f, 0.75f, 11f, 0.0325f, 415f, 43f, 0.6f, 0.34f, 34f, 0.226f, 0.5f, 1f, 16f);
        _greekYogurt = new FoodItem("Greek yogurt", 117f, 4.9f, 6.2f, 3.75f, 0f, 7.12f, 4.66f, 1.61f, 0.14f, 0.56f, 58.5f, 0.05f, 0.33f, 0f, 0.13f, 0.39f, 0.032f, 0.16f, 0.09f, 0f, 0.2f, 150f, 10.4f, 0.03f, 0.44f, 109f, 0.005f, 14.4f, 2.16f, 113.5f);
        _pasta = new FoodItem("Pasta",361f, 12.3f, 73.9f, 0f, 3.2f, 1.24f, 0.25f, 0.19f, 0.8f, 0f, 0f, 0.05f, 0f, 0f, 0f, 0.2f, 0.15f, 0.04f, 1f, 29.4f, 0.005f, 215f, 62f, 1.7f, 1.2f, 140f, 0.25f, 0.6f, 4.8f, 20f);
        _cream = new FoodItem("Whipped Cream", 360f, 2.1f, 3f, 2.98f, 0f, 35.93f, 24.27f, 8.59f, 1.04f, 1.54f, 346.1f, 0.02f, 0.44f, 0.8f, 0.15f, 0.87f, 0.031f, 0.16f, 0.07f, 10.8f, 0.1f, 94.1f, 6.8f, 0.05f, 0.24f, 57f, 0.022f, 10.1f, 1.47f, 66.8f);
        _mincedVealAndPork = new FoodItem("Minced veal and pork", 218f, 17.6f, 0f, 0f, 0f, 15.21f, 6.27f, 7f, 1.34f, 0.22f, 9f, 0.27f, 1.31f, 0f, 1.1f, 0.5f, 0.406f, 0.18f, 4.71f, 8.3f, 0.2f, 293.1f, 18.4f, 1.2f, 2.78f, 170f, 0.072f, 1.5f, 5.63f, 8.1f);
        _curry = new FoodItem("Curry", 342f, 12.7f, 25.2f, 0f, 33.2f, 11.05f, 1.6f, 8.8f, 3.1f, 0f, 49.5f, 1.15f, 0f, 11.4f, 0f, 21.99f, 0.253f, 0.28f, 3.47f, 154f, 0.13f, 1543f, 254f, 29.59f, 4.05f, 349f, 1.04f, 0.5f, 17.1f, 478f);
        _bellPepper = new FoodItem("Bell pepper",31f, 0.9f, 5.2f, 5.24f, 1.7f, 0.08f, 0.02f, 0f, 0.06f, 0f, 105.8f, 0.43f, 0f, 162.8f, 0f, 2.26f, 0.047f, 0.08f, 0.98f, 88f, 0.000974643f, 239f, 11.8f, 0.32f, 0.13f, 25f, 0.051f, 0.1f, 0.14f, 6.6f);
        _vinegar = new FoodItem("Vinegar", 20f, 0.4f, 0.4f, 0.6f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0.05f, 89f, 22f, 0.5f, 0.01f, 32f, 0.007f, 1.4f, 0.1f, 12f);
        _springOnion = new FoodItem("Spring onion",33f, 1.8f, 4.7f, 0f, 2.6f, 0.13f, 0.03f, 0.03f, 0.07f, 0f, 250f, 0.06f, 0f, 22.1f, 0f, 0f, 0.055f, 0.08f, 0.53f, 64f, 0.04f, 276f, 20f, 1.48f, 0.39f, 37f, 0.083f, 2f, 0.6f, 72f);
        _scallion = new FoodItem("Scallion", 27f, 2.1f, 3f, 0.8f, 1.9f, 0.22f, 0.06f, 0.02f, 0.13f, 0f, 1.7f, 0.15f, 0f, 66f, 0f, 0.15f, 0.05f, 0f, 0.3f, 73f, 0.015f, 249f, 9f, 0.4f, 0.2f, 32f, 0.07f, 0.3f, 0.77f, 33.7f);
        _mincedPork = new FoodItem("Minced pork", 169f, 18.9f, 0f, 0f, 0f, 9.08f, 3.57f, 4.25f, 1.05f, 0.05f, 7.2f, 0.32f, 0.72f, 0f, 0.59f, 0.54f, 0.723f, 0.19f, 5.98f, 4.3f, 0.1f, 331.4f, 20.8f, 0.88f, 2.18f, 190f, 0.07f, 0.9f, 7.15f, 7.7f);
        _shrimp = new FoodItem("Shrimp",69f, 15.3f, 0f, 0f, 0f, 0.66f, 0.15f, 0.22f, 0.29f, 0f, 0f, 0f, 2.05f, 0f, 0f, 4f, 0.015f, 0.01f, 0.49f, 20f, 1.67875f, 53.5f, 22.3f, 0.11f, 0.78f, 109f, 0.146f, 7.6f, 19f, 43.7f);
        _tacosItem = new FoodItem("Tacos", 361f, 6.8f, 88.3f, 1.26f, 3.2f, 2.63f, 0.44f, 0.8f, 1.39f, 0f, 8.1f, 0.33f, 0f, 0f, 0f, 1.11f, 0.33f, 0.11f, 5.7f, 20f, 0f, 120f, 47f, 1.1f, 0.5f, 99f, 0f, 0.6f, 3.2f, 6f);
        _salmon = new FoodItem("Salmon", 228f, 15.8f, 0f, 0f, 0f, 15.28f, 2.21f, 8.05f, 4.5f, 0f, 7.1f, 0.64f, 4.1f, 2f, 6.79f, 3.3f, 0.25f, 0.07f, 7.65f, 0f, 0.1f, 361.6f, 25.6f, 0.22f, 0.32f, 219f, 0.038f, 2.7f, 16.6f, 8.7f);
        _remoulade = new FoodItem("Remoulade", 380f, 1.1f, 12f, 12f, 0.5f, 35.22f, 3.1f, 20.19f, 11.94f, 0f, 54f, 0.04f, 1.1f, 2f, 1.02f, 7.5f, 0.025f, 0.04f, 0f, 7f, 0.7f, 49f, 3f, 1.2f, 0.6f, 55f, 0.1f, 5f, 1.6f, 14f);
        _pesto = new FoodItem("Pesto", 678f, 14f, 9.8f, 4.85f, 6.4f, 56.37f, 4.34f, 15.67f, 25.67f, 0f, 1.5f, 0.22f, 0f, 0.8f, 0f, 11.85f, 0.485f, 0.26f, 3.33f, 63.4f, 0.003125f, 602.9f, 205f, 4.85f, 5.75f, 490f, 1.299f, 0f, 1f, 9.9f);
        _greenBeans = new FoodItem("Green Beans", 30f, 1.9f, 5.9f, 2.76f, 3f, 0.19f, 0.07f, 0.01f, 0.11f, 0f, 8.7f, 0.1f, 0f, 15f, 0f, 0.3f, 0.09f, 0.11f, 0.7f, 64f, 0.00425f, 237f, 17f, 1f, 0.39f, 39f, 0.057f, 0.8f, 0.3f, 60f);
        _olives = new FoodItem("Olives", 165f, 1f, -1.5f, 0f, 6.2f, 13.76f, 2.08f, 10.2f, 1.51f, 0f, 0f, 0.03f, 0f, 0f, 0f, 6.1f, 0.017f, 0f, 0f, 0f, 0f, 19f, 16f, 8.5f, 0.14f, 17f, 0f, 0f, 0.9f, 103f);
        _anchovies = new FoodItem("Anchovies", 215f, 13.4f, 10f, 10f, 0f, 10.35f, 3.34f, 4.33f, 2.68f, 0f, 390f, 0.18f, 3.5f, 0f, 14f, 0.9f, 0.006f, 0.12f, 1.1f, 16f, 9f, 165f, 18.3f, 1.8f, 3.4f, 210f, 0.16f, 30f, 20f, 145f);
        _pear = new FoodItem("Pear", 49f, 0.3f, 10.6f, 0f, 3.2f, 0.05f, 0.01f, 0.01f, 0.03f, 0f, 5.4f, 0.01f, 0f, 6.1f, 0f, 0.58f, 0.01f, 0.01f, 0.23f, 16f, 0.001945625f, 122.3f, 6.5f, 0.1f, 0.11f, 11f, 0.065f, 0.2f, 0.1f, 10.1f);
        _capers = new FoodItem("Capers", 17f, 1.2f, 2.1f, 0f, 1.3f, 0.08f, 0f, 0f, 0f, 0f, 23f, 0.04f, 0f, 11f, 0f, 0f, 0.01f, 0.05f, 0.2f, 18f, 0f, 230f, 15f, 0.3f, 0.2f, 40f, 0f, 0.5f, 0f, 32f);
        _bernaiseSauce = new FoodItem("Bearnaise Sauce", 741f, 0.7f, 0.6f, 0.58f, 0f, 77.02f, 53.54f, 16.98f, 2.2f, 3.52f, 749.3f, 0f, 0.17f, 0f, 0.41f, 1.76f, 0.007f, 0.04f, 0.1f, 3f, 0.9f, 29.1f, 1.6f, 0.01f, 0.04f, 21f, 0.022f, 2.7f, 1.56f, 16.8f);
        _pepperSauce = new FoodItem("Pepper Sauce", 360f, 2.1f, 3f, 2.98f, 0f, 35.93f, 24.27f, 8.59f, 1.04f, 1.54f, 346.1f, 0.02f, 0.44f, 0.8f, 0.15f, 0.87f, 0.031f, 0.16f, 0.07f, 10.8f, 0.1f, 94.1f, 6.8f, 0.05f, 0.24f, 57f, 0.022f, 10.1f, 1.47f, 66.8f);
        _whiskeySauce = new FoodItem("Whiskey Sauce", 360f, 2.1f, 3f, 2.98f, 0f, 35.93f, 24.27f, 8.59f, 1.04f, 1.54f, 346.1f, 0.02f, 0.44f, 0.8f, 0.15f, 0.87f, 0.031f, 0.16f, 0.07f, 10.8f, 0.1f, 94.1f, 6.8f, 0.05f, 0.24f, 57f, 0.022f, 10.1f, 1.47f, 66.8f);
        _lime = new FoodItem("Lime", 37f, 0.7f, 3.2f, 0f, 2.8f, 0.11f, 0.02f, 0.02f, 0.06f, 0f, 0.5f, 0.04f, 0f, 29.1f, 0f, 0.24f, 0.03f, 0.02f, 0.2f, 8f, 0.005f, 102f, 6f, 0.6f, 0.11f, 18f, 0.065f, 0f, 0.2f, 33f);
        _paprika = new FoodItem("Paprika", 44f, 2f, 7.7f, 0f, 1.8f, 0.14f, 0.02f, 0.01f, 0.11f, 0f, 365.8f, 0.28f, 0f, 166f, 0f, 2.9f, 0.09f, 0.09f, 0.9f, 23f, 0.0175f, 340f, 25f, 1.2f, 0.3f, 68f, 0.174f, 0f, 0.5f, 14f);
        _oregano = new FoodItem("Oregano", 18f, 1.7f, 0.7f, 0.6f, 1.2f, 0.16f, 0.08f, 0.02f, 0.13f, 0f, 334.2f, 0.13f, 0f, 60f, 0f, 1f, 0.1f, 0.1f, 0.6f, 9f, 0.15f, 310f, 17f, 1.6f, 0.2f, 68f, 0.14f, 13.5f, 1f, 82.8f);
        _crackers = new FoodItem("Crackers", 443f, 9.4f, 64.2f, 4.95f, 3f, 12.88f, 7.21f, 4.13f, 1.5f, 0.04f, 0f, 0.06f, 0f, 0f, 0f, 0f, 0.13f, 0.08f, 1.5f, 0f, 2f, 120f, 25f, 1.7f, 0.87f, 110f, 0.19f, 1.6f, 3f, 110f);
        _porkChops = new FoodItem("Pork Chops", 182f, 17.4f, 0f, 0f, 0f, 11.44f, 5.12f, 5.48f, 0.81f, 0.03f, 6.2f, 0.26f, 0.82f, 0f, 0.22f, 0.55f, 0.747f, 0.21f, 5.18f, 4.4f, 0.1f, 327f, 20.3f, 0.99f, 2.65f, 172f, 0.1f, 1f, 9.2f, 6.1f);
        _honey = new FoodItem("Honey", 327f, 0.3f, 75.1f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0.16f, 0f, 1.1f, 0f, 0f, 0f, 0.1f, 0.1f, 0f, 0f, 51f, 2.9f, 0.4f, 0.35f, 7f, 0.031f, 0.5f, 0f, 5f);
        _broccoli = new FoodItem("Broccoli", 35f, 3.6f, 2.1f, 2.07f, 3.2f, 0.19f, 0f, 0f, 0f, 0f, 44.4f, 0.22f, 0f, 116.7f, 0f, 1.45f, 0.087f, 0.11f, 0.57f, 239f, 0.031117857f, 389f, 20.8f, 0.7f, 0.42f, 78f, 0.05f, 0.2f, 2.44f, 44.4f);
        _asparagus = new FoodItem("Asparagus", 26f, 1.8f, 3.1f, 0f, 1.8f, 0.24f, 0.07f, 0.01f, 0.16f, 0f, 0f, 0.15f, 0f, 10f, 0f, 1.98f, 0.11f, 0.12f, 0f, 150f, 0.00925f, 267f, 12.2f, 0.7f, 0.9f, 86f, 0f, 4f, 0.2f, 22.8f);
        _soySauce = new FoodItem("Soy Sauce", 56f, 6.9f, 7f, 1.6f, 0f, 0.03f, 0f, 0f, 0.03f, 0f, 0f, 0.15f, 0f, 0f, 0f, 0f, 0.033f, 0.17f, 2.2f, 14f, 14.0925f, 217f, 43f, 1.93f, 0.52f, 125f, 0.104f, 4.5f, 0.5f, 19f);
        var list = new List<FoodItem>();
        list.Add(_bacon);
        list.Add(_buns);
        list.Add(_butter);
        list.Add(_strawberryJam);
        list.Add(_raspberryJam);
        list.Add(_blackberryJam);
        list.Add(_rhubarbJam);
        list.Add(_slicedCheese);
        list.Add(_Peanutbutter);
        list.Add(_mapleSyrup);
        list.Add(_spreadChocolate);
        list.Add(_quinoa);
        list.Add(_oats);
        list.Add(_milk);
        list.Add(_flute);
        list.Add(_ham);
        list.Add(_brunchSausages);
        list.Add(_cornflakes);
        list.Add(_flour);
        list.Add(_lemon);
        list.Add(_blueberry);
        list.Add(_blackberry);
        list.Add(_strawberry);
        list.Add(_raspberry);
        list.Add(_almond);
        list.Add(_raisin);
        list.Add(_cashewnut);
        list.Add(_peanut);
        list.Add(_mango);
        list.Add(_banana);
        list.Add(_apple);
        list.Add(_orange);
        list.Add(_carrot);
        list.Add(_pistachionut);
        list.Add(_pineapple);
        list.Add(_bakedBeans);
        list.Add(_kidneyBeans);
        list.Add(_slicedTomatoes);
        list.Add(_mincedBeef);
        list.Add(_basmatiRice);
        list.Add(_cremeFraiche);
        list.Add(_onion);
        list.Add(_garlic);
        list.Add(_risottoRice);
        list.Add(_chickenBreastFilet);
        list.Add(_champignon);
        list.Add(_whiteWine);
        list.Add(_eggplant);
        list.Add(_parmesanCheese);
        list.Add(_vegetableBouillion);
        list.Add(_steak);
        list.Add(_potatoes);
        list.Add(_salami);
        list.Add(_mozzarella);
        list.Add(_tuna);
        list.Add(_toast);
        list.Add(_gherkins);
        list.Add(_liverpate);
        list.Add(_mayonaise);
        list.Add(_mackarelInTomatoes);
        list.Add(_avocado);
        list.Add(_smokedSalmon);
        list.Add(_ryebread);
        list.Add(_fishFillets);
        list.Add(_frenchFries);
        list.Add(_kebab);
        list.Add(_cucumber);
        list.Add(_tomato);
        list.Add(_icebergSalad);
        list.Add(_lasagnaSheets);
        list.Add(_lambChop);
        list.Add(_tortilla);
        list.Add(_sugar);
        list.Add(_pepper);
        list.Add(_salt);
        list.Add(_oliveOil);
        list.Add(_coriander);
        list.Add(_chili);
        list.Add(_cumin);
        list.Add(_ginger);
        list.Add(_greekYogurt);
        list.Add(_pasta);
        list.Add(_cream);
        list.Add(_mincedVealAndPork);
        list.Add(_curry);
        list.Add(_bellPepper);
        list.Add(_vinegar);
        list.Add(_springOnion);
        list.Add(_scallion);
        list.Add(_mincedPork);
        list.Add(_shrimp);
        list.Add(_tacosItem);
        list.Add(_salmon);
        list.Add(_remoulade);
        list.Add(_pesto);
        list.Add(_greenBeans);
        list.Add(_olives);
        list.Add(_anchovies);
        list.Add(_pear);
        list.Add(_capers);
        list.Add(_bernaiseSauce);
        list.Add(_pepperSauce);
        list.Add(_whiskeySauce);
        list.Add(_lime);
        list.Add(_paprika);
        list.Add(_oregano);
        list.Add(_crackers);
        list.Add(_porkChops);
        list.Add(_honey);
        list.Add(_broccoli);
        list.Add(_asparagus);
        list.Add(_soySauce);
        _context.FoodItems.AddRange(list);
        _context.SaveChanges();

    }
    private void InitializeRecipes()
    {
        _eggsAndBacon = new Recipe("Eggs and Bacon", true, "Eggs and bacon is a classic breakfast dish that is popular all around the world. Eggs are a versatile and nutritious food that can be prepared in a variety of ways, while bacon is a salty and flavorful meat that adds a delicious taste and texture to any meal.\n\nEggs are a great source of protein, vitamins, and minerals, including vitamin D, vitamin B12, and selenium. They can be scrambled, fried, boiled, poached, or baked, and can be seasoned with salt, pepper, herbs, or cheese. Each preparation method offers a unique texture and flavor, and can be tailored to suit individual preferences.\n\nBacon, on the other hand, is a cured pork product that is typically sliced into thin strips and cooked until crispy. It is high in protein and fat, and adds a salty and smoky flavor to any dish. Bacon can be cooked on the stovetop, in the oven, or on the grill, and can be enjoyed on its own, in sandwiches, or as a topping for salads, soups, and other dishes.\n\nTogether, eggs and bacon create a delicious and satisfying breakfast that can be enjoyed any time of day. They are often served alongside toast, hash browns, or other breakfast foods, and are a popular choice for brunches, weekend breakfasts, and special occasions.", "Preheat a non-stick frying pan over medium heat.\nAdd the bacon to the pan and cook for 3-4 minutes on each side, until crispy. Remove the bacon from the pan and place it on a plate lined with paper towels to absorb any excess grease.\nCrack the eggs into the same pan and fry them until the whites are set and the yolks are still runny. For sunny-side-up eggs, cook for 2-3 minutes. For over-easy eggs, gently flip the eggs over and cook for an additional 1-2 minutes.\nSeason the eggs with salt and pepper, to taste.\nServe the eggs and bacon hot, garnished with chopped herbs or grated cheese, if desired.", _author.Id, new List<Category>{_meat}, true, false, false, false);
        _bunsWithStrawberryJam = new Recipe("Buns with Strawberry Jam", true, "Buns with jam is a simple and delicious treat that is often enjoyed as a snack or for breakfast. The soft and slightly sweet buns pair perfectly with the sweet and tangy jam, creating a flavor combination that is hard to resist.", "To enjoy buns with jam, simply slice the bun in half and spread a generous amount of jam on each half.", _author.Id, new List<Category>{_vegetarian}, true, false, false, true);
        _bunsWithRaspberryJam = new Recipe("Buns with Raspberry Jam", true, "Buns with jam is a simple and delicious treat that is often enjoyed as a snack or for breakfast. The soft and slightly sweet buns pair perfectly with the sweet and tangy jam, creating a flavor combination that is hard to resist.", "To enjoy buns with jam, simply slice the bun in half and spread a generous amount of jam on each half.", _author.Id, new List<Category>{_vegetarian}, true, false, false, true);
        _bunsWithBlackberryJam = new Recipe("Buns with Blackberry Jam", true, "Buns with jam is a simple and delicious treat that is often enjoyed as a snack or for breakfast. The soft and slightly sweet buns pair perfectly with the sweet and tangy jam, creating a flavor combination that is hard to resist.", "To enjoy buns with jam, simply slice the bun in half and spread a generous amount of jam on each half.", _author.Id, new List<Category>{_vegetarian}, true, false, false, true);
        _bunsWithRhubarbJam = new Recipe("Buns with Rhubarb Jam", true, "Buns with jam is a simple and delicious treat that is often enjoyed as a snack or for breakfast. The soft and slightly sweet buns pair perfectly with the sweet and tangy jam, creating a flavor combination that is hard to resist.", "To enjoy buns with jam, simply slice the bun in half and spread a generous amount of jam on each half.", _author.Id, new List<Category>{_vegetarian}, true, false, false, true);
        _bunsWithCheese = new Recipe("Buns with Cheese", true, "Buns with cheese is a delicious and savory dish that is often enjoyed as a snack or appetizer. The soft and slightly sweet buns pair perfectly with the rich and flavorful cheese, creating a tasty and satisfying combination.", "To enjoy buns with cheese, simply slice the bun in half and add a generous amount of cheese on each half. The buns can be placed under the broiler for a few minutes to melt the cheese, or can be enjoyed as is for a cold snack.", _author.Id, new List<Category>{_vegetarian}, true, false, false, true);
        _bunsWithPeanutbutter = new Recipe("Buns with Peanutbutter", true, "Buns with peanut butter is a delicious and satisfying snack or breakfast that is loved by many. The soft and slightly sweet buns pair perfectly with the creamy and nutty peanut butter, creating a flavor combination that is both comforting and filling.", "To enjoy buns with peanut butter, simply slice the bun in half and spread a generous amount of peanut butter on each half. ", _author.Id, new List<Category>{_vegetarian}, true, false, false, true);
        _bunsWithMapleSyrup = new Recipe("Buns with Maple Syrup", true, "Buns with maple syrup is a sweet and indulgent treat that is perfect for breakfast or as a dessert. The soft and slightly sweet buns pair perfectly with the rich and flavorful maple syrup, creating a flavor combination that is both comforting and satisfying.", "To enjoy buns with maple syrup, simply warm up the bun and drizzle a generous amount of maple syrup on top.", _author.Id, new List<Category>{_vegetarian}, true, false, false, true);
        _bunswithSpreadChocolate = new Recipe("Buns with Chocolate Spread", true, "Buns with chocolate spread are a decadent and indulgent treat that is perfect for satisfying your sweet tooth. The soft and slightly sweet buns pair perfectly with the creamy and rich chocolate spread, creating a flavor combination that is both comforting and indulgent.", "To enjoy buns with chocolate spread, simply slice the bun in half and spread a generous amount of chocolate spread on each half.", _author.Id, new List<Category>{_vegetarian}, true, false, false, true);
        _QuinoaBowl = new Recipe("Quinoa Bowl", true, "A quinoa bowl is a healthy and delicious dish that is perfect for a quick and easy meal. This recipe typically consists of a base of cooked quinoa, which is then topped with a variety of vegetables, proteins, and other flavorful ingredients.", "Rinse the quinoa thoroughly in a fine mesh strainer to remove any dirt or debris.\nIn a medium pot, bring the water to a boil over high heat.\nAdd the quinoa and a pinch of salt to the pot, stir well, and reduce the heat to low.\nCover the pot with a tight-fitting lid and let the quinoa simmer for about 15-20 minutes, or until the liquid has been absorbed and the quinoa is tender.\nRemove the pot from the heat and let the quinoa rest for 5 minutes before fluffing it with a fork.\nWhile the quinoa is cooking, prepare your toppings. You can chop up your favorite vegetables, cook up some protein like chicken or tofu, and add nuts, seeds, or sauces.\nTo assemble the quinoa bowl, start with a base of cooked quinoa in a bowl, then add your desired toppings on top. You can arrange the toppings in sections or mix them together for a more blended flavor.\nEnjoy your nutritious and delicious quinoa bowl!", _author.Id, new List<Category>{_vegetarian}, true, false, false, true);
        _OatMealBowl = new Recipe("Oatmeal Bowl", true, "A cold oatmeal bowl with uncooked oatmeal, is a healthy, delicious and convenient breakfast.", "Pour the oatmeal into the serving bowl.\nAdd milk and toppings, and enjoy!", _author.Id, new List<Category>{_vegetarian}, true, false, false, true);
        _OatMealBoiled = new Recipe("Oatmeal Boiled", true, "An oatmeal bowl is a warm and comforting breakfast dish that is perfect for a cozy and satisfying start to your day. This dish typically consists of cooked oatmeal, which is then topped with a variety of fruits, nuts, and other flavorful ingredients.", "In a small pot, combine the oats, water or milk, and salt. Bring to a boil over medium-high heat, stirring occasionally.\nReduce the heat to low and simmer for 5-7 minutes, or until the oatmeal has thickened and become creamy.\nOnce the oatmeal is cooked, remove it from the heat and transfer it to a bowl.\nAdd your desired toppings to the oatmeal, such as fresh fruit, nuts, seeds, sweeteners, or spices. Some popular toppings include sliced bananas, berries, chopped nuts, honey, cinnamon, and chia seeds.\nStir the toppings into the oatmeal until they are evenly distributed.\nServe the oatmeal bowl warm and enjoy!", _author.Id, new List<Category>{_vegetarian}, true, false, false, true);
        _BreadAndSalmon = new Recipe("Bread and Salmon", true, "Bread with salmon can be a delicious and nutritious way to start your day. The combination of the soft, creamy texture of the salmon and the crunchy texture of the bread creates a delicious contrast. The flavors of the smoked salmon and bread create a savory and satisfying breakfast.", "Slice some slices of bread and add the smoked salmon on top of each slice.\nEnjoy!", _author.Id, new List<Category>{_meat}, true, false, false, true);
        _BreadAndHam = new Recipe("Bread and Ham", true, "Bread and ham is a classic sandwich that is perfect for a quick and easy breakfast or lunch.", "This simple yet satisfying sandwich is made by layering thinly sliced ham on top of your choice of bread.", _author.Id, new List<Category>{_meat}, true, false, false, true);
        _EggsBaconAndSausages = new Recipe("Eggs with Bacon and Sausages", true, "Scrambled eggs with bacon and sausages is a delicious and comforting breakfast dish that is easy to prepare and full of flavor. This meal typically consists of scrambled eggs cooked to perfection, several strips of crispy bacon, and a couple of juicy breakfast sausages.", "To make the scrambled eggs, crack 2-3 eggs into a mixing bowl and whisk them with a fork until the yolks and whites are fully combined. Heat a non-stick skillet over medium heat, add a pat of butter or a drizzle of oil, and pour in the whisked eggs. Cook the eggs, stirring gently and constantly with a spatula, until they're cooked through but still moist and fluffy.\n\nMeanwhile, cook the bacon and sausages separately in a skillet until they're crispy and browned on the outside. Once everything is cooked, plate the scrambled eggs, bacon, and sausages together, and season with salt, pepper, and any other desired spices or herbs.", _author.Id, new List<Category>{_meat}, true, false, false, false);
        _FruitBowl = new Recipe("Fruit bowl", true, "A fruit bowl is a colorful and healthy dish that is perfect for breakfast, a snack, or even dessert.", "To prepare a fruit bowl, start by washing and cutting the fruits into bite-sized pieces. Arrange the fruit pieces in a bowl, either in separate sections or mixed together, depending on your preference.", _author.Id, new List<Category>{_fruit, _vegetarian}, true, false, false, true);
        _Cereal = new Recipe("Cornflakes", true, "A bowl of cornflakes is a classic breakfast dish that is simple, quick, and satisfying. It consists of crispy and crunchy flakes made from toasted corn, served with cold milk.", "To prepare a bowl of cornflakes, start by pouring your desired amount of cornflakes into a bowl. Then, pour cold milk over the top, enough to cover the flakes but not so much that they become soggy.", _author.Id, new List<Category>{_vegetarian}, true, false, false, false);
        _pancakes = new Recipe("Pancakes", true, "Pancakes are a beloved breakfast dish that are known for their fluffy texture, sweet flavor, and versatility.\n\nPancakes are not only delicious, but they are also a good source of carbohydrates and protein, making them a satisfying and energizing breakfast option. Whether you prefer them classic or with a twist, pancakes are a timeless breakfast staple that will never go out of style.", "To make pancakes, start by whisking together the dry ingredients in a mixing bowl, then add the wet ingredients and mix until the batter is smooth and free of lumps. Heat a non-stick skillet or griddle over medium heat and add a small amount of butter or oil. Pour a ladle of batter onto the skillet and cook until bubbles form on the surface, then flip and cook the other side until golden brown.", _author.Id, new List<Category>{_vegetarian}, true, false, false, true);
        
        //Lunch 
        _durumKebab = new Recipe("Durum with Kebab", true, "Durum with kebab is a popular Middle Eastern and Mediterranean street food that is known for its delicious combination of tender and flavorful kebab meat wrapped in a soft and chewy durum bread.", "To make a durum with kebab, the cooked kebab meat is placed on the durum bread along with lettuce, tomato, onion, and sauce. The bread is then rolled up tightly to form a wrap, which can be eaten on-the-go or enjoyed as a sit-down meal.", _author.Id, new List<Category>{_meat}, false, true, true, false);
        _durumChicken = new Recipe("Durum with Chicken", true, "Durum with chicken kebab is a flavorful and satisfying Middle Eastern and Mediterranean street food that features tender pieces of marinated chicken skewered and grilled to perfection, then wrapped in a soft and chewy durum bread.", "To make a durum with chicken kebab, the cooked chicken kebab meat is placed on the durum bread along with lettuce, tomato, onion, and sauce. The bread is then rolled up tightly to form a wrap, which can be eaten on-the-go or enjoyed as a sit-down meal.", _author.Id, new List<Category>{_meat}, false, true, true, false);
        _durumMix = new Recipe("Durum with Mix", true, "Durum with mix of chicken kebab and kebab is a flavorful and satisfying Middle Eastern and Mediterranean street food that features tender pieces of marinated chicken skewered and grilled to perfection and flavorful kebab meat wrapped in a soft and chewy durum bread.", "To make a durum with mix of chicken kebab and kebab, the cooked chicken kebab and kebab is placed on the durum bread along with lettuce, tomato, onion, and sauce. The bread is then rolled up tightly to form a wrap, which can be eaten on-the-go or enjoyed as a sit-down meal.", _author.Id, new List<Category>{_meat}, false, true, true, false);
        _mixBox = new Recipe("Mix Box", true, "A mix box is a delicious and convenient meal that consists of kebab, chicken kebab, vegetables, sauce and french fries, served in a takeout container.", "To make a mix box, place the french fries in the box and add the remaining ingredients mixed together", _author.Id, new List<Category>{_meat}, false, true, true, false);
        _kebabBox = new Recipe("Kebab Box", true, "A kebab box is a delicious and convenient meal that consists of kebab, vegetables, sauce and french fries, served in a takeout container.", "To make a kebab box, place the french fries in the box and add the remaining ingredients mixed together", _author.Id, new List<Category>{_meat}, false, true, true, false);
        _chickenBox = new Recipe("Chicken Box", true, "A chicken kebab box is a delicious and convenient meal that consists of chicken kebab, vegetables, sauce and french fries, served in a takeout container.", "To make a kebab box, place the french fries in the box and add the remaining ingredients mixed together", _author.Id, new List<Category>{_meat}, false, true, true, false);
        _fishFillet = new Recipe("Fish fillets", true, "Fish fillets are a type of seafood that is enjoyed around the world for their delicate texture and rich flavor. A fish fillet is a boneless, skinless portion of a fish that has been carefully cut away from the rest of the fish, leaving behind a piece of meat that is ready to be cooked.", "Heat the vegetable oil in a large skillet over medium-high heat.\nAdd the fish fillets to the skillet and cook for 3-4 minutes on each side, or until the fish is golden brown and cooked through.\nRemove the fish fillets from the skillet and place them on a paper towel-lined plate to drain any excess oil.", _author.Id, new List<Category>{_meat}, false, true, false, false);
        _ryebreadWithSalmon = new Recipe("Ryebread with Salmon", true, "Rye bread with salmon is a classic Scandinavian dish that consists of thinly sliced, smoked salmon served on top of a slice of hearty rye bread. The rye bread is typically dark and dense, with a slightly sour taste that pairs well with the rich, smoky flavor of the salmon.", "To prepare rye bread with salmon, start by selecting a high-quality rye bread that is sturdy enough to hold the weight of the salmon without becoming soggy. Spread butter onto the bread and take the slices of the smoked salmon and arrange it on top of the bread.", _author.Id, new List<Category>{_meat}, false, true, false, true);
        _ryebreadWithAvocado = new Recipe("Ryebread with Avocado", true, "Rye bread with avocado is a simple yet delicious dish that combines the hearty, slightly tangy flavor of rye bread with the creamy richness of avocado. This dish is a popular option for lunch.", "To make rye bread with avocado, start by selecting a fresh, high-quality rye bread. Toast the bread lightly to enhance its flavor and texture. Then, cut an avocado in half, remove the pit, and scoop out the flesh into a bowl. Mash the avocado with a fork until it is smooth and creamy.\nSpread the mashed avocado generously onto the toasted rye bread.", _author.Id, new List<Category>{_vegetarian}, false, true, false, true);
        _ryebreadPlatte = new Recipe("Ryebread Platte", true, "Rye bread platte is a classic Scandinavian dish that consists of rye bread with butter and a range of toppings.", "To make a rye bread platte, take slices of rye bread and spread butter on top and add the different toppings onto the bread slices.", _author.Id, new List<Category>{_meat}, false, true, false, true);
        _grilledToastWithTunaAndPesto = new Recipe("Grilled Toast with Tuna and Pesto", true, "Grilled toast with tuna and pesto is a tasty and healthy dish that is perfect for a quick and easy lunch or snack. This dish combines the savory flavors of grilled bread, flaky tuna, and tangy pesto to create a delicious and satisfying meal.", "To make grilled toast with tuna and pesto, toast the bread slices lightly on a grill or in a toaster until they are golden brown and crisp.\n\nNext, open a can of tuna and drain any excess liquid. Flake the tuna into small pieces with a fork and mix it with a spoonful of pesto until it is well combined.\n\nSpread the tuna mixture generously onto the grilled toast, making sure to cover the entire surface. You can also add additional toppings, such as sliced tomatoes, olives, or red onions, to enhance the flavor and texture of the dish.\n\nFinally, place the toast with the tuna mixture back onto the grill or in the toaster for a few minutes until the toppings are heated through and the bread is slightly crispy.", _author.Id, new List<Category>{_meat}, false, true, false, true);
        _grilledToastWithHamAndCheese = new Recipe("Grilled Toast with Ham and Cheese", true, "Grilled toast with ham and cheese is a classic sandwich that's easy to make and satisfying to eat. This dish combines the savory flavors of grilled bread, melted cheese, and salty ham to create a delicious and comforting meal.", "To make grilled toast with ham and cheese, slice the bread into thick pieces and butter one side of each slice.\n\nNext, layer thinly sliced ham and your favorite cheese, such as cheddar or Swiss, on top of one slice of bread. Top with the other slice of bread, butter side up.\n\nPlace the sandwich on a heated grill or in a pan and cook for a few minutes on each side, until the bread is golden brown and the cheese is melted and gooey.\n\nRemove the sandwich from the grill or pan and let it cool for a minute before slicing it in half and serving.", _author.Id, new List<Category>{_meat}, false, true, false, true);
        _paninoWithTomatoMozzarellaAndSalami = new Recipe("Panino with Tomato Mozzarella and Salami", true, "A panino with mozzarella, tomato, and salami is a delicious and savory Italian sandwich that is sure to satisfy your cravings. It's made with high-quality ingredients, including fresh mozzarella, juicy tomato, and flavorful salami.", "To make a panino with mozzarella, tomato, and salami, start by selecting a high-quality Italian bread, such as ciabatta or focaccia. Cut the bread into slices and brush with olive oil or melted butter.\n\nLayer thinly sliced fresh mozzarella, ripe tomato slices, and sliced salami on one slice of bread. Sprinkle with salt, pepper, and any desired herbs or spices, such as basil or oregano.\n\nPlace the other slice of bread on top of the salami and press down lightly. Place the sandwich on a heated panini press or in a pan and cook for a few minutes on each side, until the bread is toasted and the cheese is melted.\n\nRemove the panino from the press or pan and let it cool for a minute before slicing it in half and serving. The combination of the warm and gooey cheese with the salty and flavorful salami and juicy tomato is a taste explosion in every bite.", _author.Id, new List<Category>{_meat}, false, true, false, true);
        _hamAndCheeseSandwich = new Recipe("Ham and Cheese Sandwich", true, "A ham and cheese sandwich is a classic comfort food that is simple yet satisfying. This sandwich is made with sliced ham, cheese, and bread, and can be enjoyed for breakfast, lunch, or a quick snack.", "To make a ham and cheese sandwich, start by selecting your preferred bread, such as white, wheat, or sourdough. Toast the bread to your desired level of crispiness.\n\nLayer sliced ham on one slice of bread, followed by a slice or two of your favorite cheese, such as cheddar, Swiss, or provolone.\n\nPlace the other slice of bread on top of the fillings and press down lightly. Cut the sandwich in half or quarters, and enjoy it immediately while it's still warm and gooey.", _author.Id, new List<Category>{_meat}, false, true, false, true);
        
        //Dinner
        _risotto = new Recipe("Risotto", true, "Risotto is a classic Italian dish that consists of creamy, flavorful rice cooked in broth until it reaches a rich and indulgent consistency. The dish is usually made with Arborio rice, a short-grain rice that is high in starch, which helps create the creamy texture.", "To make risotto, start by heating up some butter or oil in a large saucepan. Add finely chopped onions or shallots and sauté until translucent. Then, add the Arborio rice and stir until it is coated with the oil and heated through.\n\nNext, start adding warm broth, about one ladleful at a time, to the rice mixture, and stir constantly until the liquid is absorbed. Repeat this process, stirring constantly and adding more broth, until the rice is cooked and has reached a creamy, soft consistency.\n\nTraditionally, risotto is finished with butter and grated Parmesan cheese, which gives it a rich and nutty flavor. Other ingredients can also be added to the dish, such as mushrooms, peas, asparagus, or seafood, depending on your preference.",_author.Id, new List<Category>{_vegetarian},false, false, true, false);
        _pastaCarbonara = new Recipe("Pasta Carbonara", true, "Pasta carbonara is a classic Italian dish that originated in Rome. It's a simple yet indulgent dish made with spaghetti, eggs, Pecorino Romano cheese, and guanciale (cured pork jowl), or sometimes pancetta (cured pork belly).", "To make the dish, start by boiling spaghetti until al dente. While the pasta is cooking, sauté the guanciale or pancetta until it's crispy and golden brown. In a separate bowl, beat together egg yolks, Pecorino Romano cheese, and black pepper until well combined.\nOnce the pasta is cooked, reserve a cup of the pasta cooking water, and then drain the pasta. Return the pasta to the pot and add the crispy guanciale or pancetta, stirring to combine. Then, remove the pot from the heat and add the egg yolk mixture, along with a splash of the reserved pasta cooking water. Toss the pasta until the egg mixture forms a creamy sauce that coats the spaghetti.", _author.Id, new List<Category>{_meat}, false, false, true, false);
        _butterChicken = new Recipe("Butter Chicken", true, "Butter chicken is a popular Indian dish made with succulent pieces of marinated chicken that are first cooked in a tandoor oven or pan-fried until golden and then simmered in a creamy, spiced tomato sauce.", "In a large bowl, combine the yogurt, lemon juice, turmeric, chili powder, cumin, coriander, garam masala, and salt. Mix well.\nAdd the chicken to the marinade and toss to coat. Cover and refrigerate for at least 30 minutes or up to 8 hours.\nIn a large skillet over medium-high heat, melt the butter. Add the onion, ginger, and garlic and sauté until the onion is soft and translucent, about 5 minutes.\nAdd the chicken and marinade to the skillet and cook until the chicken is browned and cooked through, about 10 minutes.\nAdd the crushed tomatoes to the skillet and bring to a simmer. Reduce heat to medium and cook for 10 minutes.\nStir in the cream and fenugreek leaves (if using) and cook for another 5 minutes.", _author.Id, new List<Category>{_meat}, false, false, true, false);
        _chiliConCarne = new Recipe("Chili Con Carne", true, "Chili con carne, also known as chili, is a hearty and flavorful stew that originated in the southwestern United States and northern Mexico. It is typically made with ground beef, kidney beans, and a variety of spices, including chili powder, cumin, and paprika.", "Heat a large pot or Dutch oven over medium-high heat. Add the ground beef and cook until browned, breaking it up into small pieces as it cooks.\nRemove the beef from the pot and set it aside. Leave any drippings in the pot.\nAdd the diced onion and garlic to the pot and sauté until softened, about 5 minutes.\nAdd the beef back to the pot, along with the kidney beans, diced tomatoes, chili powder, cumin, smoked paprika, salt, and pepper. Stir well to combine.\nBring the chili to a simmer and cook for about 20-30 minutes, stirring occasionally, until the flavors have melded together and the chili has thickened to your desired consistency.\nTaste and adjust the seasoning as needed.\nServe the chili hot, topped with shredded cheese, sour cream, and chopped green onions, if desired.", _author.Id, new List<Category>{_meat}, false, false, true, false);
        _beefBernaise = new Recipe("Beef Bernaise", true, "Beef Béarnaise is a classic French dish consisting of a tender, juicy grilled or pan-fried steak topped with a rich and creamy Béarnaise sauce. The steak is typically cooked to medium-rare or medium to retain its tenderness and flavor, while the sauce is made with egg yolks, butter, white wine vinegar, shallots, tarragon, and other herbs and spices. The sauce is then drizzled generously over the steak, creating a decadent and indulgent meal that is sure to satisfy any meat lover's cravings. This dish is often served with a side of sautéed vegetables and crispy French fries or roasted potatoes.", "Cut potatoes in to long sticks and season with salt and pepper. Put the potatoes onto a medium-hot pan with oil and let them cook until they have your desired consistency. Season the steaks generously with salt and black pepper on both sides.\nHeat the olive oil in a skillet over medium-high heat.\nAdd the steaks to the skillet and cook for 3-4 minutes per side for medium-rare, or longer for desired doneness.\nOnce the steaks are cooked, transfer them to a plate and let them rest for a few minutes.\nHeat the Béarnaise sauce in a small saucepan or microwave according to the package instructions.\nOnce heated, spoon the Béarnaise sauce over the steaks and serve immediately.", _author.Id, new List<Category>{_meat},false, false, true, false);
        _pastaPuttanesca = new Recipe("Pasta Putanesca", true, "Pasta puttanesca is a traditional Italian dish known for its bold, flavorful sauce. The name \"puttanesca\" is derived from the Italian word for \"prostitute\", and it is said that the dish was a favorite of prostitutes due to its quick preparation time and strong aroma.", "Cook the spaghetti according to package instructions until al dente.\nWhile the pasta is cooking, heat the olive oil in a large skillet over medium heat.\nAdd the minced garlic, anchovy fillets, and red pepper flakes, and cook for 1-2 minutes until fragrant.\nAdd the diced tomatoes (with their juices), chopped olives, and capers to the skillet, and bring the mixture to a simmer.\nLet the sauce simmer for about 10-15 minutes, stirring occasionally, until it has thickened and reduced slightly.\nWhen the pasta is done cooking, drain it and add it to the skillet with the sauce. Toss everything together until the pasta is coated with the sauce.\nRemove the skillet from the heat and add the chopped parsley, and salt and pepper to taste.", _author.Id, new List<Category>{_meat}, false, false, true, false);
        _pastaTuna = new Recipe("Pasta Tuna", true, "Pasta with tuna and lemon is a simple yet flavorful dish that's perfect for a quick and easy meal.", "To make pasta with tuna and lemon, start by cooking your pasta according to package instructions until it's al dente. While the pasta cooks, heat up some olive oil in a skillet over medium heat.\nAdd a can of drained tuna and capers to the skillet and stir together, breaking up any large chunks of tuna with a spoon. Add some lemon zest and squeeze in some fresh lemon juice, and continue to cook everything together for another minute or two.\nOnce your pasta is cooked, drain it and add it to the skillet with the tuna and lemon mixture. Toss everything together.", _author.Id, new List<Category>{_meat}, false, false, true, false);
        _tortillas = new Recipe("Tortillas", true, "Tortillas are a staple in many cuisines, and their versatility and delicious taste make them a popular choice for many different types of dishes. They can be enjoyed with a wide variety of fillings and toppings, making them a favorite for both casual meals and special occasions.", "Cut the avocados in half, remove the pits, and scoop out the flesh into a mixing bowl.\nUse a fork to mash the avocados to your desired consistency. Some people like their guacamole chunky, while others prefer it smooth.\nAdd the chopped red onion, diced tomato, cilantro, and lime juice to the mashed avocados. Stir gently to combine.\nSeason with salt and pepper to taste. You can also add additional lime juice or cilantro, depending on your preference.\nHeat a large skillet or frying pan over medium-high heat.\nAdd the cooking oil to the hot pan and swirl it around to coat the bottom.\nAdd the minced beef to the pan and break it up with a spatula or wooden spoon, spreading it out evenly in the pan.\nCook the minced beef, stirring occasionally, until it is browned and cooked through. This usually takes about 5-7 minutes, depending on the thickness of the minced beef and the heat of your pan.\nIf desired, you can add diced onions, minced garlic, or other seasonings or vegetables at this point and cook them along with the minced beef until they are softened and fragrant.\nSeason the cooked minced beef with salt and pepper to taste, or other seasonings of your choice.\n Heat the tortias and serve on the table so you can build you own tortillas as you eat.", _author.Id, new List<Category>{_meat}, false, false, true, false);
        _meatballsCurry = new Recipe("Meatballs Curry", true, "Curry meatballs are a delicious and flavorful twist on classic meatballs, infused with aromatic spices and simmered in a rich and creamy curry sauce.", "In a mixing bowl, combine all the meatball ingredients and mix well. Shape the mixture into small meatballs, about 1 inch in diameter.\nHeat oil or ghee in a large skillet or pot over medium heat. Add chopped onion and minced garlic and sauté until softened and fragrant.\nAdd curry powder to the skillet and cook for another 1-2 minutes, stirring constantly to release the flavors of the curry.\nGently add the meatballs to the curry sauce in the skillet. Cover and simmer for about 20-25 minutes, or until the meatballs are cooked through and the flavors have melded together.\nSeason with salt to taste.\nServe the meatball curry over cooked rice", _author.Id, new List<Category>{_meat}, false, false, true, false);
        _steakWithPepperSauce = new Recipe("Steak with Pepper Sauce", true, "Enjoy a delicious steak with a creamy pepper sauce. Tender and seasoned to perfection, the steak is smothered in a rich sauce bursting with tangy peppercorns. A simple and elegant dish that will satisfy any meat lover.", "Season the steaks generously with salt and pepper on both sides.\nHeat a large skillet or pan over medium-high heat and add a drizzle of olive oil or melted butter.\nAdd the steaks to the hot pan and cook for 3-4 minutes per side for medium-rare, or longer for desired level of doneness. Remove the steaks from the pan and let them rest for a few minutes.\nIn the same pan, add the beef or chicken broth and deglaze the pan, scraping up any browned bits from the bottom.\nAdd the heavy cream and crushed or ground peppercorns to the pan. Bring to a simmer and let it cook for a few minutes until slightly thickened, stirring occasionally.\nStir in the butter until melted, then remove the pepper sauce from the heat and set aside.\nIn another pan, heat some olive oil or melted butter over medium heat. Add the cubed potatoes and cook until golden and crispy on all sides, stirring occasionally.\nRemove the pan-fried potatoes from the pan and place them on paper towels to drain excess oil.\nTo serve, place a cooked steak on a plate, spoon some pepper sauce over the top, and serve with a generous portion of pan-fried potatoes on the side.\nGarnish with fresh parsley or chives, if desired.\nEnjoy your delicious homemade steak with pepper sauce and pan-fried potatoes!", _author.Id, new List<Category>{_meat}, false, false, true, false);
        _steakWithWhiskeySauce = new Recipe("Steak with Whiskey Sauce", true, "Indulge in a perfectly cooked steak with whiskey sauce, served with crispy pan-fried potatoes. Juicy steak with a savory crust is drizzled with smoky-sweet whiskey sauce, while golden pan-fried potatoes add a delightful crunch. A hearty and indulgent dish that's perfect for special occasions or memorable dinners.", "Season the steaks generously with salt and pepper on both sides.\nHeat 1 tbsp of vegetable oil in a large skillet over medium-high heat. Add the steaks and cook for 3-4 minutes per side for medium-rare, or until desired doneness is reached. Remove the steaks from the skillet and let them rest on a plate while you prepare the whiskey sauce.\nIn the same skillet, melt the butter over medium heat. Add the minced garlic and cook for 1 minute until fragrant. Carefully add the whiskey to the skillet and deglaze the pan, scraping up any browned bits from the bottom.\nStir in the beef broth, heavy cream, Dijon mustard, Worcestershire sauce, and brown sugar. Bring the mixture to a simmer and let it cook for 5 minutes, stirring occasionally. If you prefer a thicker sauce, you can whisk in 1 tbsp of cornstarch dissolved in water at this point.\nMeanwhile, in a separate skillet, heat 2 tbsp of butter and 2 tbsp of vegetable oil over medium-high heat. Add the sliced potatoes in a single layer and cook until golden brown and crispy on both sides, about 3-4 minutes per side. Season with salt and pepper to taste. Remove the pan-fried potatoes from the skillet and drain on paper towels.\nOnce the whiskey sauce has thickened, return the steaks to the skillet and spoon the sauce over them. Cook for an additional minute, basting the steaks with the sauce, until heated through.\nServe the steak with the whiskey sauce over pan-fried potatoes, and garnish with chopped parsley for added freshness and flavor.", _author.Id, new List<Category>{_meat}, false, false, true, false);
        _tacos = new Recipe("Tacos", true, "Get a taste of Mexico with soft tacos and salsa.", "Cut the tomatoes onion and avocado and mix together in a bowl with a sprinkle of lime juice.\nHeat a large pan with oil and saute the minced beef.\nOnce the minced beef is no longer red, add the spices and stir.\nHeat the soft tacos shells and serve so that you can build your taco as you eat.", _author.Id, new List<Category>{}, false, false, true, false);
        _honeyGarlicFriedPorkChops = new Recipe("Honey-garlic fried pork chops", true,"Honey garlic pork chops with pan fried potatoes are a mouthwatering and satisfying dish that combines juicy pork chops with a sweet and savory glaze, and crispy pan fried potatoes for a delicious and comforting meal.", "In a bowl, whisk together honey, minced garlic, soy sauce, olive oil, salt, and pepper to create the marinade for the pork chops.\nPlace the pork chops in a shallow dish and pour the marinade over them, making sure to coat both sides. Let the pork chops marinate for at least 30 minutes or up to overnight in the refrigerator.\nIn a large skillet over medium-high heat, melt the butter. Add the thinly sliced potatoes and cook for 5-7 minutes on each side, until they are golden brown and crispy. Sprinkle with paprika and garlic powder, and season with salt and pepper to taste. Remove the potatoes from the skillet and set aside.\nIn the same skillet, add a little more butter if needed, and place the marinated pork chops. Cook the pork chops for 4-5 minutes on each side, basting them with the marinade during cooking, until they are cooked through and have a golden crust.\nOnce the pork chops are cooked, remove them from the skillet and let them rest for a few minutes.\nServe the honey garlic pork chops alongside the pan fried potatoes. Garnish with chopped fresh parsley, if desired, for added freshness and color.\nEnjoy your delicious honey garlic pork chops with pan fried potatoes!", _author.Id, new List<Category>{_meat},false, false, true, false);
        _chickenFriedRice = new Recipe("Chicken Fried Rice", true, "Chicken fried rice is a classic Asian-inspired dish that combines tender pieces of chicken, fragrant rice, and a medley of vegetables, all stir-fried to perfection. It's a flavorful and satisfying one-pan meal that can be easily prepared at home.", "Heat a large skillet or wok over medium-high heat. Add the vegetable oil and swirl it around to coat the pan.\nAdd the diced chicken to the hot pan and cook until browned and cooked through. Remove the cooked chicken from the pan and set aside.\nIn the same pan, add a little more oil if needed, then add the minced garlic and cook until fragrant, about 30 seconds.\nAdd the vegetables to the pan and stir-fry for a few minutes until they are heated through and slightly tender.\nPush the vegetables to one side of the pan and pour the beaten eggs into the empty space. Scramble the eggs until they are fully cooked, then mix them with the vegetables.\nAdd the cooked rice to the pan and stir-fry everything together, breaking up any clumps of rice with a spatula.\nAdd the cooked chicken back to the pan, along with the soy sauce, oyster sauce, and sesame oil. Stir-fry everything together for a few more minutes until well combined and heated through.\nTaste and season with salt and pepper as needed.", _author.Id, new List<Category>{_meat}, false, false, true, false);
        _shrimpStirFry = new Recipe("Shrimp stir fry", true, "Plump, juicy shrimp are the star of this dish, perfectly cooked to retain their delicate texture and sweet taste. They're tossed in a sizzling hot wok or skillet with an array of colorful vegetables, such as bell peppers, onions, carrots, and snap peas, that add a burst of freshness and crunch. The stir-fry is then coated in a tantalizing sauce that combines flavors of soy sauce, ginger, garlic, and a hint of sweetness, adding a depth of flavor to the dish. The result is a mouthwatering and visually appealing dish that's both satisfying and healthy. Served over a bed of steamed rice or noodles, shrimp stir-fry makes for a delicious and quick meal that's perfect for busy weeknight dinners or entertaining guests.", "Heat a large wok or skillet over high heat. Add the vegetable oil and swirl to coat the pan.\nAdd the minced garlic and ginger to the pan and stir-fry for 30 seconds until fragrant.\nAdd the diced onion, sliced bell peppers, julienned carrots, and snap peas to the pan and stir-fry for 2-3 minutes until the vegetables are crisp-tender.\nAdd the shrimp to the pan and stir-fry for 2-3 minutes until they turn pink and opaque.\nIn a small bowl, whisk together the soy sauce, oyster sauce, cornstarch, sugar, salt, and pepper. Pour the sauce over the shrimp and vegetables in the pan.\nStir-fry for another 1-2 minutes until the sauce thickens and coats the shrimp and vegetables evenly.\nRemove from heat and serve the shrimp stir-fry hot over cooked rice ", _author.Id, new List<Category>{_meat},false, false, true, false);
        _springRolls = new Recipe("Spring rolls", true, "Spring rolls are a popular Asian appetizer that are light, crispy, and packed with delicious fillings. These delightful rolls are typically made by wrapping a mixture of fresh vegetables, herbs, and often meat or seafood in a thin, translucent rice paper wrapper, which is then fried or served fresh. The result is a delightful combination of flavors and textures, with a crunchy exterior and a refreshing, savory filling. Spring rolls are often served with a dipping sauce, such as sweet and sour sauce, peanut sauce, or hoisin sauce, which adds an extra layer of deliciousness to these tasty treats. Whether you're enjoying them as an appetizer, snack, or part of a meal, spring rolls are a delightful and satisfying addition to any dining experience.", "Prepare all the ingredients by cutting the vegetables, herbs, and cooked shrimp into thin strips or slices. Set aside.\nFill a shallow pan or a wide bowl with warm water. Dip one rice paper wrapper at a time into the warm water for about 10-15 seconds until it softens and becomes pliable.\nCarefully remove the softened rice paper wrapper from the water and place it on a clean, damp kitchen towel or a non-stick surface.\nPlace a few strips of the cooked shrimp, vermicelli noodles, and a small handful of the sliced vegetables and herbs on the lower half of the rice paper wrapper.\nFold the sides of the wrapper over the filling, then roll up tightly, tucking in the sides as you go, to form a neat, compact roll.\nRepeat the process with the remaining rice paper wrappers and filling ingredients.\nServe the spring rolls with a dipping sauce of your choice and enjoy!",_author.Id,new List<Category>{_vegetarian},false, false, true, false);
        _bakedSalmonAndAsparagus = new Recipe("Baked salmon and asparagus", true,"Indulge in a culinary delight with this delicious and healthy dish of baked salmon with tender asparagus, finished with a luscious lemon butter sauce. The fresh flavors and delicate textures of the dish come together harmoniously to create a memorable meal that's perfect for any occasion.", "Put the butter in a small cooking bowl, add the garlic and lemon juice and stir until properly mixed together.\nPlace salmon fillets and asparagus on a backing sheet and smother the butter sauce on top.\nBacke in the oven for 10-12 minutes at 200ºC.",_author.Id, new List<Category>{_meat}, false, false, true, false);
        _meatLasagna = new Recipe("Meat lasagna", true, "Indulge in the ultimate comfort food with a classic beef lasagna that's hearty, flavorful, and oh-so-satisfying. Layers of tender lasagna noodles, savory beef ragù, rich tomato sauce, and creamy cheese come together to create a mouthwatering masterpiece that's perfect for gatherings or a cozy family dinner.", "Heat a large skillet over medium heat and add the ground beef, onion, and garlic. Cook until the beef is browned and the onion is softened, breaking up the beef with a spoon as it cooks.\nAdd the crushed tomatoes, tomato sauce, oregano, basil, salt, and pepper to the skillet. Stir to combine and bring to a simmer. Reduce the heat to low and let the sauce simmer for about 20 minutes, stirring occasionally.\nPreheat your oven to 350°F (175°C). Cook the lasagna noodles according to package instructions until al dente. Drain and set aside.\nIn a separate bowl, mix together the ricotta cheese and half of the shredded mozzarella cheese.\nTo assemble the lasagna, spread a thin layer of the meat sauce in the bottom of a 9x13-inch baking dish. Place a layer of cooked lasagna noodles on top, followed by a layer of the ricotta cheese mixture, another layer of meat sauce, and a sprinkle of grated Parmesan cheese. Repeat the layers until all the ingredients are used up, finishing with a layer of meat sauce on top.\nSprinkle the remaining shredded mozzarella cheese over the top of the lasagna.\nBake the lasagna in the preheated oven for 25-30 minutes, or until the cheese is melted and bubbly on top.\nRemove the lasagna from the oven and let it rest for about 10 minutes before serving.\nGarnish with fresh basil leaves, if desired, before serving.", _author.Id, new List<Category>{_meat}, false, false, true, false);
        _veganLasagna = new Recipe("Vegan lasagna", true, "Vegan lasagna is a delicious plant-based twist on the classic Italian dish. It's made with layers of flavorful vegan sauce, protein-rich plant-based ingredients, and dairy-free cheese alternatives. The lasagna noodles are typically cooked to perfection, and the dish is assembled with care, creating a visually appealing and satisfying meal. The sauce is often made with tomatoes, onions, garlic, and herbs, creating a rich and savory flavor. Vegan lasagna is typically layered with plant-based ingredients such as tofu, tempeh, lentils, or a variety of roasted vegetables, providing a hearty and wholesome base. Dairy-free cheese alternatives, such as vegan mozzarella or cashew-based cheese, are used to create a creamy and melty layer between the noodles and sauce. The dish is then baked to perfection, allowing the flavors to meld together and create a mouthwatering, comforting meal. Vegan lasagna is a great option for those following a plant-based diet, or for anyone who enjoys flavorful, wholesome, and cruelty-free cuisine. It's a satisfying and delicious dish that's sure to impress vegans and non-vegans alike!", "Preheat your oven to 375°F (190°C). In a large saucepan, heat the olive oil over medium heat. Add the chopped onion and garlic, and sauté until softened, about 5 minutes.\nAdd the crushed tomatoes, dried basil, dried oregano, salt, and pepper to the saucepan. Simmer the sauce for about 15 minutes, stirring occasionally.\nIn a separate bowl, mix together the crumbled tofu, chopped vegetables, nutritional yeast, lemon juice, dried basil, dried oregano, salt, and pepper.\nIn a separate small saucepan, melt the vegan butter for the lemon butter topping. Stir in the lemon zest and lemon juice, and set aside.\nTo assemble the lasagna, spread a thin layer of the tomato sauce in the bottom of a 9x13-inch baking dish. Arrange a layer of cooked lasagna noodles over the sauce. Spread a layer of the tofu and vegetable mixture over the noodles. Drizzle with some of the lemon butter topping. Repeat the layers until all the ingredients are used up, finishing with a layer of sauce on top.\nCover the baking dish with foil and bake for 30 minutes. Remove the foil and bake for an additional 10-15 minutes, until the lasagna is bubbly and golden on top.\nLet the lasagna cool for a few minutes before slicing and serving. Optionally, sprinkle with vegan cheese alternative on top before serving.", _author.Id, new List<Category>{_vegetarian}, false, false, true, false);
        _lambChopWithPotatoes = new Recipe("Lamb chop with potatoes", true, "Lamb chops with potatoes is a classic and delicious dish that combines tender and flavorful lamb chops with crispy and savory potatoes.", "Preheat your oven to 400°F (200°C) and line a baking sheet with parchment paper.\nSeason the lamb chops generously with salt and pepper on both sides.\nHeat a pan over medium-high heat and add a drizzle of oil or melted butter. Sear the lamb chops for 2-3 minutes on each side until they develop a golden crust. Transfer the lamb chops to the prepared baking sheet and set aside.\nWash and peel the potatoes (if desired), then slice them into thin rounds. Place the potato slices in a bowl and toss with olive oil or melted butter, fresh or dried herbs (such as rosemary and thyme), minced or powdered garlic, and a pinch of salt and pepper.\nArrange the seasoned potato slices around the lamb chops on the baking sheet.\nBake in the preheated oven for 20-25 minutes, or until the lamb chops reach your desired level of doneness and the potatoes are crispy and golden.\nRemove from the oven and let the lamb chops rest for a few minutes before serving.\nOptionally, garnish with fresh herbs, such as chopped parsley or chives, for added freshness and flavor.\nServe the lamb chops with the crispy potatoes as a delicious and satisfying meal.", _author.Id, new List<Category>{_meat}, false, false, true, false);
        _frikadellerWithRice = new Recipe ("Frikadeller with rice", true, "Frikadeller, also known as Danish meatballs, are a delicious and comforting dish made from ground meat, typically pork, mixed with spices and often served with rice.", "In a mixing bowl, combine the ground meat, finely chopped onion, minced garlic, egg, breadcrumbs, salt, pepper, and chopped fresh parsley (if using). Mix well to combine.\nHeat a pan over medium heat and add a drizzle of butter or oil.\nUsing your hands or a spoon, shape the meat mixture into small meatballs (frikadeller) and place them in the pan.\nCook the meatballs for 4-5 minutes on each side, or until they are cooked through and golden brown on the outside.\nRemove the cooked meatballs from the pan and place them on a paper towel-lined plate to drain any excess grease.\nServe the frikadeller hot over cooked rice for a satisfying and flavorful meal.\nOptionally, you can garnish the frikadeller with additional chopped fresh parsley for added freshness and color.", _author.Id, new List<Category>{_meat}, false, false, true, false);

        _cashewPeanutNutMix = new Recipe("Cashew Peanut Mix", true, "Cashew peanut mix is a delightful combination of two popular nuts, cashews and peanuts, that are roasted to perfection to bring out their natural flavors. The cashews are buttery and slightly sweet, while the peanuts are crunchy and savory.", "Find a nice bowl and mix peanuts and cashews into the bowl.", _author.Id, new List<Category>{_vegetarian}, false, false, false, true);
        _pistachioNutMix = new Recipe("Pistachio Mix", true, "Pistachio mix is a delightful medley of roasted pistachio nuts combined with a variety of other tasty ingredients to create a crunchy and savory snack. The star of the mix is the pistachio, known for its distinctive flavor, vibrant green color, and slightly sweet and salty taste. The pistachios are roasted to perfection, bringing out their natural nutty flavors and creating a satisfying crunch.", "Find a nice bowl and add pistachios. ", _author.Id, new List<Category>{_vegetarian}, false, false, false, true);
        _almondRaisinMix = new Recipe("Alomnd Raisin Mix", true, "Almond raisin mix is a delightful combination of crunchy almonds and sweet raisins, making it a delicious and satisfying snack. The almonds provide a rich and nutty flavor, while the plump raisins add a natural sweetness and chewy texture. The mix is perfect for on-the-go snacking, adding to trail mix, or as a topping for yogurt, oatmeal, or baked goods. It's a healthy and convenient option that can be enjoyed by itself or incorporated into various recipes for an extra burst of flavor and nutrition.", "Find a nice bowl and add almonds and raisins.", _author.Id, new List<Category>{_vegetarian}, false, false, false, true);
        _pineappleOrangefruitMix = new Recipe("Pineapple Orange Mix", true, "Pineapple orange fruit mix is a refreshing and tangy blend of juicy pineapples and zesty oranges, creating a burst of tropical flavors in every bite. The sweet and tangy pineapple chunks provide a tropical sweetness, while the juicy orange segments add a citrusy zing. The combination of flavors creates a harmonious balance that is both delicious and invigorating. This fruit mix is perfect for a healthy and refreshing snack, or as a topping for yogurt, salads, or desserts. It's a vibrant and flavorful option that adds a taste of the tropics to your culinary creations, making it a delightful addition to any meal or snack time.", "Prepare the fresh pineapple by cutting off the skin and core, and cutting the flesh into small chunks.\nPeel the oranges and carefully separate the segments.\nPlace the pineapple chunks and orange segments in a bowl or airtight container.\nGently toss the pineapple and orange together to mix them evenly.\nCover the bowl or container and refrigerate for at least 30 minutes to allow the flavors to meld and the fruit to chill.", _author.Id, new List<Category>{_fruit, _vegetarian}, false, false, false, true);
        _appleOrangePearfruitMix = new Recipe("Apple Orange Pear Mix", true, "Indulge in the perfect blend of sweet and tangy flavors with this delicious apple orange pear fruit mix. Freshly sliced apples, juicy oranges, and succulent pears come together in a harmonious medley of taste and texture. The crispness of the apples, the zesty sweetness of the oranges, and the subtle juiciness of the pears create a delightful symphony of flavors that will tantalize your taste buds.", "Wash and prepare the fruits by removing cores/seeds and peeling as necessary.\nSlice the apple and pear into thin slices, and separate the orange into segments.\nPlace the sliced apple, pear, and segmented orange in a bowl.\nGently toss the fruits together to mix them evenly.", _author.Id, new List<Category>{_fruit, _vegetarian}, false, false, false, true);
        _raspberryBlueberryfruitMix = new Recipe("Raspberry Blueberry Mix", true, "Indulge in the vibrant flavors of summer with this delightful raspberry blueberry mix. Bursting with the natural sweetness of ripe raspberries and blueberries, this fruit medley is a perfect blend of tartness and sweetness, creating a symphony of flavors in every spoonful.", "Wash the raspberries and blueberries thoroughly under cold water and pat them dry with a clean towel.\nPlace the raspberries and blueberries in a bowl.", _author.Id, new List<Category>{_fruit, _vegetarian}, false, false, false, true);
        _blackberryStrawberryfruitMix = new Recipe("Blackberry Strawberry Mix", true, "Indulge in the perfect blend of sweet and tangy flavors with this delightful blackberry strawberry mix. Plump, juicy blackberries and succulent, ripe strawberries come together in a colorful medley that's bursting with natural sweetness. The deep purple hues of the blackberries and the vibrant red tones of the strawberries create a visually stunning mix that's as visually appealing as it is delicious.", "Wash the blackberries and strawberries thoroughly under cold water and pat them dry with a paper towel.\nHull the strawberries by removing the green tops and cut them into halves or quarters, depending on their size.\nPlace the blackberries and strawberries in a mixing bowl and gently toss them together to combine.", _author.Id, new List<Category>{_fruit, _vegetarian}, false, false, false, true);
        _strawberryBananaSmoothie = new Recipe("Strawberry Banana Smoothie", true, "Indulge in the sweet and creamy goodness of a homemade strawberry banana smoothie. Made with ripe strawberries and ripe bananas, this refreshing and satisfying drink is perfect for breakfast, as a snack, or even as a healthy dessert option. The vibrant red color of the strawberries combined with the mellow yellow of the bananas creates a visually appealing treat that's as visually appealing as it is tasty.", "To make this delicious smoothie, simply blend ripe strawberries and bananas together in a blender until smooth and creamy. You can also add in some milk, yogurt, or ice for extra creaminess and thickness, depending on your preference. The result is a luscious, nutrient-packed drink that's loaded with vitamins, minerals, and antioxidants from the strawberries and bananas.", _author.Id, new List<Category>{_fruit, _vegetarian}, false, false, false, true);
        _mangoSmoothie = new Recipe("Mango smoothie", true, "Indulge in the tropical flavors of a refreshing mango smoothie! Made with luscious ripe mangoes, this smoothie is a burst of sunshine in a glass. The vibrant orange color and sweet, tangy taste of mangoes make this smoothie a delightful treat for any time of the day.", "To make this delicious mango smoothie, simply blend ripe mangoes with some liquid of your choice, such as milk, yogurt, or coconut water, for added creaminess. You can also add in some ice for a chilled and refreshing drink. Optionally, you can add a squeeze of lime or a sprinkle of cinnamon for an extra burst of flavor.", _author.Id, new List<Category>{_fruit, _vegetarian}, false, false, false, true);
        _blueberrySmoothie = new Recipe("Blueberry smoothie", true, "Indulge in the sweet and tangy flavors of a refreshing blueberry smoothie! Bursting with antioxidants and natural sweetness, blueberries are the star of this delicious and nutritious drink. Their deep blue color and unique taste make this smoothie a delightful treat for any time of the day.", "To make a blueberry smoothie, simply blend fresh or frozen blueberries with some liquid of your choice, such as milk, yogurt, or coconut water. You can also add in some ice for a chilled and refreshing drink. Optionally, you can add in some honey, maple syrup, or agave nectar for added sweetness, or a squeeze of lemon or lime for a tangy twist.", _author.Id, new List<Category>{_fruit, _vegetarian}, false, false, false, true);
        _appleCarrotOrangeSmoothie = new Recipe("Apple Carrot Orange smoothie", true, "Indulge in a nutritious and flavorful apple carrot orange smoothie that is packed with vitamins, fiber, and natural sweetness. This refreshing drink is a perfect blend of crisp apples, sweet carrots, and tangy oranges, creating a harmonious and invigorating flavor profile.", "To make an apple carrot orange smoothie, start by peeling and chopping a sweet apple. Then, peel and slice a carrot, and juice an orange to obtain fresh orange juice. Add the chopped apple, sliced carrot, and orange juice into a blender, and blend until smooth.", _author.Id, new List<Category>{_fruit, _vegetarian}, false, false, false, true);
        _peanutbutterSandwich = new Recipe("Peanutbutter sandwich", true, "Indulge in a classic favorite with a delightful description for a peanut butter sandwich. This simple yet satisfying sandwich is a harmonious combination of creamy or crunchy peanut butter spread between slices of soft bread, creating a perfect blend of sweet, salty, and savory flavors.", "To make a peanut butter sandwich, start by choosing your favorite type of bread, whether it's white, whole wheat, multigrain, or a specialty bread of your choice. Spread a generous layer of creamy or crunchy peanut butter on one slice of bread, making sure to cover the entire surface evenly. If desired, you can also add other toppings or fillings, such as sliced bananas, honey, jelly, or even bacon, to customize your sandwich to your taste preferences.", _author.Id, new List<Category>{_vegetarian}, false, false, false, true);
        _salamiSticks = new Recipe("Salami sticks", true, "Salami sticks are a savory and satisfying snack that are perfect for meat lovers and those seeking a protein-rich, flavorful treat. These cylindrical delights are made from cured and dried sausage, typically made from beef or pork, that is seasoned with a blend of spices and then air-dried or smoked to perfection.", "Find a small bowl or plate and place the salami sticks.", _author.Id, new List<Category>{_meat}, false, false, false, true);
        _salamiSticksOlivesAndCheese = new Recipe("Salami sticks olives and cheese", true, "Salami sticks with olives and cheese are a mouthwatering and irresistible combination that brings together the savory flavors of cured sausage, briny olives, and creamy cheese. These delectable bites are perfect for a quick and easy appetizer, snack, or addition to a charcuterie board.", "To create this tantalizing treat, thinly sliced salami sticks are paired with plump and tangy olives, which add a burst of briny flavor that complements the rich and savory profile of the salami. The combination of the salty and tangy olives with the robust flavors of the salami creates a harmonious balance that is sure to satisfy your taste buds.\nAdd cubes or wedges of cheese, such as sharp cheddar, creamy brie, or tangy gouda, into the mix. The creamy and slightly tangy notes of the cheese add a luscious texture and enhance the overall taste experience of each bite.", _author.Id, new List<Category>{_meat}, false, false, false, true);
        _crackersWithPesti = new Recipe("Crackers with pesto", true, "Crackers with pesto are a delicious and flavorful snack that combines the crunchiness of crackers with the vibrant and aromatic flavors of pesto. Pesto is a classic Italian sauce made from fresh basil, garlic, pine nuts, Parmesan cheese, and olive oil, and it adds a burst of fresh and savory flavors to the crackers.", "To create this scrumptious snack, crispy and savory crackers are paired with a dollop or spread of homemade or store-bought pesto. The pesto adds a burst of aromatic and savory flavors, with the fresh basil and garlic providing a zesty kick, the pine nuts adding a rich and nutty note, and the Parmesan cheese adding a creamy and tangy undertone.", _author.Id, new List<Category>{_vegetarian}, false, false, false, true);
        var list = new List<Recipe>();
        list.Add(_bunsWithStrawberryJam);
        list.Add(_bunsWithRaspberryJam);
        list.Add(_bunsWithBlackberryJam);
        list.Add(_bunsWithRhubarbJam);
        list.Add(_bunsWithCheese);
        list.Add(_bunsWithPeanutbutter);
        list.Add(_bunsWithMapleSyrup);
        list.Add(_bunswithSpreadChocolate);
        list.Add(_QuinoaBowl);
        list.Add(_OatMealBowl);
        list.Add(_OatMealBoiled);
        list.Add(_BreadAndSalmon);
        list.Add(_BreadAndHam);
        list.Add(_eggsAndBacon);
        list.Add(_EggsBaconAndSausages);
        list.Add(_FruitBowl);
        list.Add(_Cereal);
        list.Add(_pancakes);
        list.Add(_durumKebab);
        list.Add(_durumChicken);
        list.Add(_durumMix);
        list.Add(_mixBox);
        list.Add(_kebabBox);
        list.Add(_chickenBox);
        list.Add(_fishFillet);
        list.Add(_ryebreadWithSalmon);
        list.Add(_ryebreadWithAvocado);
        list.Add(_ryebreadPlatte);
        list.Add(_grilledToastWithTunaAndPesto);
        list.Add(_grilledToastWithHamAndCheese);
        list.Add(_paninoWithTomatoMozzarellaAndSalami);
        list.Add(_hamAndCheeseSandwich);
        list.Add(_risotto);
        list.Add(_pastaCarbonara);
        list.Add(_butterChicken);
        list.Add(_chiliConCarne);
        list.Add(_beefBernaise);
        list.Add(_pastaPuttanesca);
        list.Add(_pastaTuna);
        list.Add(_tortillas);
        list.Add(_meatballsCurry);
        list.Add(_steakWithPepperSauce);
        list.Add(_steakWithWhiskeySauce);
        list.Add(_tacos);
        list.Add(_honeyGarlicFriedPorkChops);
        list.Add(_chickenFriedRice);
        list.Add(_shrimpStirFry);
        list.Add(_springRolls);
        list.Add(_bakedSalmonAndAsparagus);
        list.Add(_meatLasagna);
        list.Add(_veganLasagna);
        list.Add(_lambChopWithPotatoes);
        list.Add(_frikadellerWithRice);
        list.Add(_cashewPeanutNutMix);
        list.Add(_pistachioNutMix);
        list.Add(_almondRaisinMix);
        list.Add(_pineappleOrangefruitMix);
        list.Add(_appleOrangePearfruitMix);
        list.Add(_raspberryBlueberryfruitMix);
        list.Add(_blackberryStrawberryfruitMix);
        list.Add(_strawberryBananaSmoothie);
        list.Add(_mangoSmoothie);
        list.Add(_blueberrySmoothie);
        list.Add(_appleCarrotOrangeSmoothie);
        list.Add(_peanutbutterSandwich);
        list.Add(_salamiSticks);
        list.Add(_salamiSticksOlivesAndCheese);
        list.Add(_crackersWithPesti);
        _context.Recipes.AddRange(list);
        _context.SaveChanges();

        _author.SavedRecipes.AddRange(list);
        _context.SaveChanges();
    }

    private void AddFoodItemsToRecipes()
    {
        _eggsInEggsAndBacon = new FoodItemRecipe(_egg, _eggsAndBacon, 1.26f);
        _baconInEggsAndBacon = new FoodItemRecipe(_bacon, _eggsAndBacon, 0.4f);
        _bunsInBunsWithStrawberryJam = new FoodItemRecipe(_buns, _bunsWithStrawberryJam, 0.8f);
        _strawberryJamInBunsWithStrawberryJam = new FoodItemRecipe(_strawberryJam, _bunsWithStrawberryJam, 0.4f);
        _bunsInBunsWithRaspberryJam = new FoodItemRecipe(_buns, _bunsWithRaspberryJam, 0.8f);
        _raspberryJamInBunsWithRaspberryJam = new FoodItemRecipe(_raspberryJam, _bunsWithRaspberryJam, 0.4f);
        _bunsInBunsWithBlackberryJam = new FoodItemRecipe(_buns, _bunsWithBlackberryJam, 0.8f);
        _blackberryJamInBunsWithBlackberryJam = new FoodItemRecipe(_blackberryJam, _bunsWithBlackberryJam, 0.4f);
        _bunsInBunsWithRhubarbJam = new FoodItemRecipe(_buns, _bunsWithRhubarbJam, 0.8f);
        _rhubarbJamInBunsWithRhubarbJam = new FoodItemRecipe(_rhubarbJam, _bunsWithRhubarbJam, 0.4f);
        _bunsInBunsWithCheese = new FoodItemRecipe(_buns, _bunsWithCheese, 0.8f);
        _cheeseInBunsWithCheese = new FoodItemRecipe(_slicedCheese, _bunsWithCheese, 0.4f);
        _bunsInBunsWithPeanutbutter = new FoodItemRecipe(_buns, _bunsWithPeanutbutter, 0.8f);
        _peanutbutterInBunsWithPeanutbutter = new FoodItemRecipe(_Peanutbutter, _bunsWithPeanutbutter, 0.4f);
        _bunsInBunsWithmapleSyrup = new FoodItemRecipe(_buns, _bunsWithMapleSyrup, 0.8f);
        _mapleSyrupInBunsWithmapleSyrup = new FoodItemRecipe(_mapleSyrup, _bunsWithMapleSyrup, 0.3f);
        _bunsInBunsWithSpreadChocolate = new FoodItemRecipe(_buns, _bunswithSpreadChocolate, 0.8f);
        _spreadChocolateInBunsWithSpreadChocolate = new FoodItemRecipe(_spreadChocolate, _bunswithSpreadChocolate, 0.4f);
        _quinoaInQuinoaBowl = new FoodItemRecipe(_quinoa, _QuinoaBowl, 2.0f);
        _tomatoInQuinoaBowl = new FoodItemRecipe(_tomato, _QuinoaBowl, 0.5f);
        _champignonInQuinoaBowl = new FoodItemRecipe(_champignon, _QuinoaBowl, 0.5f);
        _eggsInQuinoaBowl = new FoodItemRecipe(_egg, _QuinoaBowl, 1.26f);
        _lemonInQuinoaBowl = new FoodItemRecipe(_lemon, _QuinoaBowl, 0.2f);
        _oatsInOatmealBowl = new FoodItemRecipe(_oats, _OatMealBowl, 1f);
        _milkInOatmealBowl = new FoodItemRecipe(_milk, _OatMealBowl, 1f);
        _blueberryInOatmealBowl = new FoodItemRecipe(_blueberry, _OatMealBowl, 0.2f);
        _raspberryInOatmealBowl = new FoodItemRecipe(_raspberry, _OatMealBowl, 0.2f);
        _oatsInOatmealBoiled = new FoodItemRecipe(_oats, _OatMealBoiled, 1f);
        _butterInOatmealBoiled = new FoodItemRecipe(_butter, _OatMealBoiled, 0.1f);
        _breadInBreadAndSalmon = new FoodItemRecipe(_flute, _BreadAndSalmon, 1f);
        _salmonInBreadAndSalmon = new FoodItemRecipe(_smokedSalmon, _BreadAndSalmon, 1f);
        _butterInBreadAndSalmon = new FoodItemRecipe(_butter, _BreadAndSalmon, 0.1f);
        _breadInBreadAndHam = new FoodItemRecipe(_flute, _BreadAndHam, 1f);
        _hamInBreadAndHam = new FoodItemRecipe(_ham, _BreadAndHam, 1f);
        _butterInBreadAndHam = new FoodItemRecipe(_butter, _BreadAndHam, 0.1f);
        _eggsInEggsBaconAndSausages = new FoodItemRecipe(_egg, _EggsBaconAndSausages, 1.26f);
        _baconInEggsBaconAndSausages = new FoodItemRecipe(_bacon, _EggsBaconAndSausages, 0.4f);
        _sausagesInEggsBaconAndSausages = new FoodItemRecipe(_brunchSausages, _EggsBaconAndSausages, 1f);
        _appleInFruitBowl = new FoodItemRecipe(_apple, _FruitBowl, 1f);
        _orangeInFruitBowl = new FoodItemRecipe(_orange, _FruitBowl, 1f);
        _pineapleInFruitBowl = new FoodItemRecipe(_pineapple, _FruitBowl, 1f);
        _cornflakesInCereal = new FoodItemRecipe(_cornflakes, _Cereal, 1.25f);
        _milkInCereal = new FoodItemRecipe(_milk, _Cereal, 1f);
        _flourInPancakes = new FoodItemRecipe(_flour, _pancakes, 1.2f);
        _milkInPancakes = new FoodItemRecipe(_milk, _pancakes, 2.5f);
        _eggsInPancakes = new FoodItemRecipe(_egg, _pancakes, 0.9f);
        _sugarInPancakes = new FoodItemRecipe(_sugar, _pancakes, 0.1f);
        _butterInPancakes = new FoodItemRecipe(_butter, _pancakes, 0.3f);
        _durumWrapInDurumKebab = new FoodItemRecipe(_tortilla, _durumKebab, 1f);
        _kebabInDurumKebab = new FoodItemRecipe(_kebab, _durumKebab, 1.5f);
        _dressingInDurumKebab = new FoodItemRecipe(_cremeFraiche, _durumKebab, 0.4f);
        _icebergInDurumKebab = new FoodItemRecipe(_icebergSalad, _durumKebab, 0.4f);
        _onionInDurumKebab = new FoodItemRecipe(_onion, _durumKebab, 0.3f);
        _tomatoInDurumKebab = new FoodItemRecipe(_tomato, _durumKebab, 0.3f);
        _cucumberInDurumKebab = new FoodItemRecipe(_cucumber, _durumKebab, 0.3f);
        _durumWrapInDurumChicken = new FoodItemRecipe(_tortilla, _durumChicken, 1f);
        _chickenInDurumChicken = new FoodItemRecipe(_chickenBreastFilet, _durumChicken, 1.5f);
        _dressingInDurumChicken = new FoodItemRecipe(_cremeFraiche, _durumChicken, 0.4f);
        _icebergInDurumChicken = new FoodItemRecipe(_icebergSalad, _durumChicken, 0.4f);
        _onionInDurumChicken = new FoodItemRecipe(_onion, _durumChicken, 0.3f);
        _tomatoInDurumChicken = new FoodItemRecipe(_tomato, _durumChicken, 0.3f);
        _cucumberInDurumChicken = new FoodItemRecipe(_cucumber, _durumChicken, 0.3f);
        _durumWrapInDurumMix = new FoodItemRecipe(_tortilla, _durumMix, 1f);
        _kebabInDurumMix = new FoodItemRecipe(_kebab, _durumMix, 0.75f);
        _chickenInDurumMix = new FoodItemRecipe(_chickenBreastFilet, _durumMix, 0.75f);
        _dressingInDurumMix = new FoodItemRecipe(_cremeFraiche, _durumMix, 0.4f);
        _icebergInDurumMix = new FoodItemRecipe(_icebergSalad, _durumMix, 0.4f);
        _onionInDurumMix = new FoodItemRecipe(_onion, _durumMix, 0.3f);
        _tomatoInDurumMix = new FoodItemRecipe(_tomato, _durumMix, 0.3f);
        _cucumberInDurumMix = new FoodItemRecipe(_cucumber, _durumMix, 0.3f);
        _frenchFriesInKebabBox = new FoodItemRecipe(_frenchFries, _kebabBox, 1f);
        _kebabInKebabBox = new FoodItemRecipe(_kebab, _kebabBox, 1.5f);
        _dressingInKebabBox = new FoodItemRecipe(_cremeFraiche, _kebabBox, 0.4f);
        _icebergInKebabBox = new FoodItemRecipe(_icebergSalad, _kebabBox, 0.4f);
        _onionInKebabBox = new FoodItemRecipe(_onion, _kebabBox, 0.4f);
        _tomatoInKebabBox = new FoodItemRecipe(_tomato, _kebabBox, 0.3f);
        _cucumberInKebabBox = new FoodItemRecipe(_cucumber, _kebabBox, 0.3f);
        _frenchFriesInChickenBox = new FoodItemRecipe(_frenchFries, _chickenBox, 1f);
        _chickenInChickenBox = new FoodItemRecipe(_chickenBreastFilet, _chickenBox, 1.5f);
        _dressingInChickenBox = new FoodItemRecipe(_cremeFraiche, _chickenBox, 0.4f);
        _icebergInChickenBox = new FoodItemRecipe(_icebergSalad, _chickenBox, 0.4f);
        _onionInChickenBox = new FoodItemRecipe(_onion, _chickenBox, 0.3f);
        _tomatoInChickenBox = new FoodItemRecipe(_tomato, _chickenBox, 0.3f);
        _cucumberInChickenBox = new FoodItemRecipe(_cucumber, _chickenBox, 0.3f);
        _frenchFriesInMixBox = new FoodItemRecipe(_frenchFries, _mixBox, 1f);
        _kebabInMixBox = new FoodItemRecipe(_kebab, _mixBox, 0.75f);
        _chickenInMixBox = new FoodItemRecipe(_chickenBreastFilet, _mixBox, 0.75f);
        _dressingInMixBox = new FoodItemRecipe(_cremeFraiche, _mixBox, 0.4f);
        _icebergInMixBox = new FoodItemRecipe(_icebergSalad, _mixBox, 0.4f);
        _onionInMixBox = new FoodItemRecipe(_onion, _mixBox, 0.3f);
        _tomatoInMixBox = new FoodItemRecipe(_tomato, _mixBox, 0.3f);
        _cucumberInMixBox = new FoodItemRecipe(_cucumber, _mixBox, 0.3f);
        _fishFilletInFishFillet = new FoodItemRecipe(_fishFillets, _fishFillet, 1.5f);
        _ryebreadInFishFillet = new FoodItemRecipe(_ryebread, _fishFillet, 1f);
        _remouladeInFishFillet = new FoodItemRecipe(_remoulade, _fishFillet, 0.3f);
        _butterInFishFillet = new FoodItemRecipe(_butter, _fishFillet, 0.1f);
        _ryebreadInRyebreadWithSalmon = new FoodItemRecipe(_ryebread, _ryebreadWithSalmon, 1f);
        _salmonInRyebreadWithSalmon = new FoodItemRecipe(_smokedSalmon, _ryebreadWithSalmon, 1f);
        _butterInRyebreadWithSalmon = new FoodItemRecipe(_butter, _ryebreadWithSalmon, 0.1f);
        _ryebreadInRyebreadWithAvocado = new FoodItemRecipe(_ryebread, _ryebreadWithAvocado, 1f);
        _avocadoInRyebreadWithAvocado = new FoodItemRecipe(_avocado, _ryebreadWithAvocado, 1.5f);
        _butterInRyebreadWithAvocado = new FoodItemRecipe(_butter, _ryebreadWithAvocado, 0.1f);
        _ryebreadInRyebreadPlatte = new FoodItemRecipe(_ryebread, _ryebreadPlatte, 1.5f);
        _butterInRyebreadPlatte = new FoodItemRecipe(_butter, _ryebreadPlatte, 0.15f);
        _avocadoInRyebreadPlatte = new FoodItemRecipe(_avocado, _ryebreadPlatte, 0.75f);
        _salmonInRyebreadPlatte = new FoodItemRecipe(_smokedSalmon, _ryebreadPlatte, 0.5f);
        _liverPateInRyebreadPlatte = new FoodItemRecipe(_liverpate, _ryebreadPlatte, 0.5f);
        _tunaInGrilledToastWithTunaAndPesto = new FoodItemRecipe(_tuna, _grilledToastWithTunaAndPesto, 0.5f);
        _pestoInGrilledToastWithTunaAndPesto = new FoodItemRecipe(_pesto, _grilledToastWithTunaAndPesto, 0.4f);
        _butterInGrilledToastWithTunaAndPesto = new FoodItemRecipe(_butter, _grilledToastWithTunaAndPesto, 0.2f);
        _toastInGrilledToastWithTunaAndPesto = new FoodItemRecipe(_toast, _grilledToastWithTunaAndPesto, 0.6f);
        _toastInGrilledToastWithHamAndCheese = new FoodItemRecipe(_toast, _grilledToastWithHamAndCheese, 0.6f);
        _butterInGrilledToastWithHamAndCheese = new FoodItemRecipe(_butter, _grilledToastWithHamAndCheese, 0.2f);
        _hamInGrilledToastWithHamAndCheese = new FoodItemRecipe(_ham, _grilledToastWithHamAndCheese, 0.5f);
        _cheeseInGrilledToastWithHamAndCheese = new FoodItemRecipe(_slicedCheese, _grilledToastWithHamAndCheese, 0.5f);
        _breadInPaninoWithTomatoMozzarellaAndSalami = new FoodItemRecipe(_flute, _paninoWithTomatoMozzarellaAndSalami, 1f);
        _mayoInPaninoWithTomatoMozzarellaAndSalami = new FoodItemRecipe(_mayonaise, _paninoWithTomatoMozzarellaAndSalami, 0.25f);
        _tomatoInPaninoWithTomatoMozzarellaAndSalami = new FoodItemRecipe(_tomato, _paninoWithTomatoMozzarellaAndSalami, 0.4f);
        _mozzarellaInPaninoWithTomatoMozzarellaAndSalami = new FoodItemRecipe(_mozzarella, _paninoWithTomatoMozzarellaAndSalami, 0.5f);
        _salamiInPaninoWithTomatoMozzarellaAndSalami = new FoodItemRecipe(_salami, _paninoWithTomatoMozzarellaAndSalami, 0.5f);
        _breadInHamAndCheeseSandwich = new FoodItemRecipe(_flute, _hamAndCheeseSandwich, 1f);
        _mayoInHamAndCheeseSandwich = new FoodItemRecipe(_mayonaise, _hamAndCheeseSandwich, 0.25f);
        _hamInHamAndCheeseSandwich = new FoodItemRecipe(_ham, _hamAndCheeseSandwich, 0.5f);
        _cheeseInHamAndCheeseSandwich = new FoodItemRecipe(_slicedCheese, _hamAndCheeseSandwich, 0.5f);
        _onionInRisotto = new FoodItemRecipe(_onion, _risotto, 0.25f);
        _garlicInRisotto = new FoodItemRecipe(_garlic, _risotto, 0.01f);
        _risottoRiceInRisotto = new FoodItemRecipe(_risottoRice, _risotto, 1f);
        _champignonInRisotto = new FoodItemRecipe(_champignon, _risotto, 0.75f);
        _bouillionInRisotto = new FoodItemRecipe(_vegetableBouillion, _risotto, 0.025f);
        _parmesanCheeseInRisotto = new FoodItemRecipe(_parmesanCheese, _risotto, 0.125f);
        _eggsInPastaCarbonara = new FoodItemRecipe(_egg, _pastaCarbonara, 0.8f);
        _cheeseInPastaCarbonara = new FoodItemRecipe(_parmesanCheese, _pastaCarbonara, 0.2f);
        _baconInPastaCarbonara = new FoodItemRecipe(_bacon, _pastaCarbonara, 0.25f);
        _pastaInPastaCarbonara = new FoodItemRecipe(_pasta, _pastaCarbonara, 1f);
        _pepperInPastaCarbonara = new FoodItemRecipe(_pepper, _pastaCarbonara, 0.025f);
        _greekYougurtInButterChicken = new FoodItemRecipe(_greekYogurt, _butterChicken, 0.25f);
        _chickenInButterChicken = new FoodItemRecipe(_chickenBreastFilet, _butterChicken, 1.25f);
        _cuminInButterChicken = new FoodItemRecipe(_cumin, _butterChicken, 0.01f);
        _butterInButterChicken = new FoodItemRecipe(_butter, _butterChicken, 0.125f);
        _creamInButterChicken = new FoodItemRecipe(_cream, _butterChicken, 0.125f);
        _slicedTomatoesInButterChicken = new FoodItemRecipe(_slicedTomatoes, _butterChicken, 1f);
        _gingerInButterChicken = new FoodItemRecipe(_ginger, _butterChicken, 0.025f);
        _riceInButterChicken = new FoodItemRecipe(_basmatiRice, _butterChicken, 1f);
        _chiliInCCC = new FoodItemRecipe(_chili, _chiliConCarne, 0.05f);
        _mincedMeatInCCC = new FoodItemRecipe(_mincedBeef, _chiliConCarne, 1f);
        _slicedTomatoesInCCC = new FoodItemRecipe(_slicedTomatoes, _chiliConCarne, 1f);
        _bakedBeansInCCC = new FoodItemRecipe(_bakedBeans, _chiliConCarne, 1.2f);
        _kidneyBeansInCCC = new FoodItemRecipe(_kidneyBeans, _chiliConCarne, 1.2f);
        _garlicInCCC = new FoodItemRecipe(_garlic, _chiliConCarne, 0.025f);
        _onionInCCC = new FoodItemRecipe(_onion, _chiliConCarne, 0.5f);
        _oliveOilInCCC = new FoodItemRecipe(_oliveOil, _chiliConCarne, 0.03f);
        _mincedVealAndPorkInMBC = new FoodItemRecipe(_mincedVealAndPork, _meatballsCurry, 1f);
        _flourInMBC = new FoodItemRecipe(_flour, _meatballsCurry, 0.08f);
        _onionInMBC = new FoodItemRecipe(_onion, _meatballsCurry, 0.25f);
        _milkIMBC = new FoodItemRecipe(_milk, _meatballsCurry, 0.625f);
        _eggInMBC = new FoodItemRecipe(_egg, _meatballsCurry, 0.2f);
        _butterInMBC = new FoodItemRecipe(_butter, _meatballsCurry, 0.19f);
        _curryInMBC = new FoodItemRecipe(_curry, _meatballsCurry, 0.03f);
        _riceInMBC = new FoodItemRecipe(_basmatiRice, _meatballsCurry, 0.75f);
        _steakInBeefBernaise = new FoodItemRecipe(_steak, _beefBernaise, 2f);
        _potatoesInBeefBernaise = new FoodItemRecipe(_potatoes, _beefBernaise, 1.5f);
        _greenBeansInBeefBernaise = new FoodItemRecipe(_greenBeans, _beefBernaise, 0.5f);
        _bernaiseInBeefBernaise = new FoodItemRecipe(_bernaiseSauce, _beefBernaise, 0.4f);
        _pastaInPastaPuttanesca = new FoodItemRecipe(_pasta, _pastaPuttanesca, 1f);
        _slicedTomatoesInPastaPuttanesca = new FoodItemRecipe(_slicedTomatoes, _pastaPuttanesca, 1f);
        _olivesInPastaPuttanesca = new FoodItemRecipe(_olives, _pastaPuttanesca, 0.3f);
        _anchoviesInPastaPuttanesca = new FoodItemRecipe(_anchovies, _pastaPuttanesca, 0.2f);
        _garlicInPastaPuttanesca = new FoodItemRecipe(_garlic, _pastaPuttanesca, 0.025f);
        _capersInPastaPuttanesca = new FoodItemRecipe(_capers, _pastaPuttanesca, 0.1f);
        _pastaInPastaTuna = new FoodItemRecipe(_pasta, _pastaTuna, 1f);
        _tunaInPastaTuna = new FoodItemRecipe(_tuna, _pastaTuna, 1f);
        _capersInPastaTuna = new FoodItemRecipe(_capers, _pastaTuna, 0.1f);
        _lemonInPastaTuna = new FoodItemRecipe(_lemon, _pastaTuna, 0.075f);
        _steakInSteakWithPepperSauce = new FoodItemRecipe(_steak, _steakWithPepperSauce, 2f);
        _potatoesInSteakWithPepperSauce = new FoodItemRecipe(_potatoes, _steakWithPepperSauce, 1.5f);
        _greenBeansInSteakWithPepperSauce = new FoodItemRecipe(_greenBeans, _steakWithPepperSauce, 0.5f);
        _pepperSauceInSteakWithPepperSauce = new FoodItemRecipe(_pepperSauce, _steakWithPepperSauce, 0.4f);
        _steakInSteakWithWhiskeySauce = new FoodItemRecipe(_steak, _steakWithWhiskeySauce, 2f);
        _potatoesInSteakWithWhiskeySauce = new FoodItemRecipe(_potatoes, _steakWithWhiskeySauce, 1.5f);
        _greenBeansInSteakWithWhiskeySauce = new FoodItemRecipe(_greenBeans, _steakWithWhiskeySauce, 0.5f);
        _whiskeySauceInSteakWithWhiskeySauce = new FoodItemRecipe(_whiskeySauce, _steakWithWhiskeySauce, 0.4f);
        _tomatosInTacos = new FoodItemRecipe(_tomato, _tacos, 1.25f);
        _onionInTacos = new FoodItemRecipe(_onion, _tacos, 0.6f);
        _avocadoInTacos = new FoodItemRecipe(_avocado, _tacos, 0.75f);
        _mincedBeefInTacos = new FoodItemRecipe(_mincedBeef, _tacos, 1f);
        _garlicInTacos = new FoodItemRecipe(_garlic, _tacos, 0.1f);
        _kidneyBeansInTacos = new FoodItemRecipe(_kidneyBeans, _tacos, 1f);
        _limeInTacos = new FoodItemRecipe(_lime, _tacos, 0.175f);
        _corianderInTacos = new FoodItemRecipe(_coriander, _tacos, 0.05f);
        _paprikaInTacos = new FoodItemRecipe(_paprika, _tacos, 0.05f);
        _oreganoInTacos = new FoodItemRecipe(_oregano, _tacos, 0.02f);
        _tacosInTacos = new FoodItemRecipe(_tacosItem, _tacos, 0.75f);
        _porkInHoneyGarlicFriedPorkChops = new FoodItemRecipe(_porkChops, _honeyGarlicFriedPorkChops, 2.5f);
        _honeyInHoneyGarlicFriedPorkChops = new FoodItemRecipe(_honey, _honeyGarlicFriedPorkChops, 0.15f);
        _garlicInHoneyGarlicFriedPorkChops = new FoodItemRecipe(_garlic, _honeyGarlicFriedPorkChops, 0.05f);
        _potatoesInHoneyGarlicFriedPorkChops = new FoodItemRecipe(_potatoes, _honeyGarlicFriedPorkChops, 1.5f);
        _broccoliInHoneyGarlicFriedPorkChops = new FoodItemRecipe(_broccoli, _honeyGarlicFriedPorkChops, 0.5f);
        _butterInHoneyGarlicFriedPorkChops = new FoodItemRecipe(_butter, _honeyGarlicFriedPorkChops, 0.2f);
        _vinegarInHoneyCarlicFriedPorkChops = new FoodItemRecipe(_vinegar, _honeyGarlicFriedPorkChops, 0.1f);
        _chickenInChickenFriedRice = new FoodItemRecipe(_chickenBreastFilet, _chickenFriedRice, 1.4f);
        _riceInChickenFriedRice = new FoodItemRecipe(_basmatiRice, _chickenFriedRice, 1f);
        _onionInChickenFriedRice = new FoodItemRecipe(_onion, _chickenFriedRice, 0.8f);
        _carrotInChickenFriedRice = new FoodItemRecipe(_carrot, _chickenFriedRice, 0.43f);
        _garlicInChickenFriedRice = new FoodItemRecipe(_garlic,_chickenFriedRice , 0.05f);
        _oilInChickenFriedRice = new FoodItemRecipe(_oliveOil,_chickenFriedRice, 0.05f);
        _eggInChickenFriedRice = new FoodItemRecipe(_egg,_chickenFriedRice , 0.5f);
        _saltInChickenFriedRice = new FoodItemRecipe(_salt, _chickenFriedRice, 0.01f);
        _pepperInChickenFriedRice = new FoodItemRecipe(_pepper,_chickenFriedRice, 0.01f);
        _soysauceInChickenFriedRice = new FoodItemRecipe(_soySauce, _chickenFriedRice, 0.15f);
        _shrimpsInShrimpStirFry = new FoodItemRecipe(_shrimp,_shrimpStirFry, 1f);
        _oilInShrimpStirFry = new FoodItemRecipe(_oliveOil,_shrimpStirFry, 0.1f);
        _garlicInShrimpStirFry = new FoodItemRecipe(_garlic,_shrimpStirFry, 0.05f);
        _gingerInShrimpStirFry = new FoodItemRecipe(_ginger,_shrimpStirFry, 0.03f);
        _lemonInShrimpStirFry = new FoodItemRecipe(_lemon,_shrimpStirFry, 0.1f);
        _onionInShrimpStirFry = new FoodItemRecipe(_onion,_shrimpStirFry, 0.5f);
        _saltInShrimpStirFry = new FoodItemRecipe(_salt,_shrimpStirFry, 0.005f);
        _pepperInShrimpStirFry = new FoodItemRecipe(_pepper,_shrimpStirFry, 0.005f);
        _corianderInShrimpStirFry = new FoodItemRecipe(_coriander, _springRolls, 0.2f);
        _paprikaInShrimpStirFry = new FoodItemRecipe(_paprika, _shrimpStirFry, 0.05f);
        _oreganoInShrimpStirFry = new FoodItemRecipe(_oregano, _shrimpStirFry, 0.025f);
        _riceInShrimpStirFry = new FoodItemRecipe(_basmatiRice, _shrimpStirFry, 1f);
        _oilInSpringRolls = new FoodItemRecipe(_oliveOil, _springRolls, 0.1f);
        _mincedPorkInSpringRolls = new FoodItemRecipe(_mincedPork,_springRolls,0.5f);
        _garlicInSpringRolls = new FoodItemRecipe(_ginger, _springRolls, 0.05f);
        _gingerInSpringRolls = new FoodItemRecipe(_ginger,_springRolls, 0.05f);
        _springOnionsInSpringRolls = new FoodItemRecipe(_springOnion, _springRolls, 0.2f);
        _scallionsInSpringRolls = new FoodItemRecipe(_scallion, _springRolls, 0.2f);
        _vinegarInSpringRolls = new FoodItemRecipe(_vinegar, _springRolls, 0.1f);
        _phylloDoughInSpringRolls = new FoodItemRecipe(_flour, _springRolls, 0.2f);
        _limeInSpringRolls = new FoodItemRecipe(_lemon, _springRolls, 0.1f);
        _corianderInSpringRolls = new FoodItemRecipe(_coriander,_springRolls,0.2f);
        _soysauceInSpringRolls = new FoodItemRecipe(_soySauce, _springRolls, 0.1f);
        _lemonInBakedSalmonAndAsparagus = new FoodItemRecipe(_lemon, _bakedSalmonAndAsparagus, 0.3f);
        _salmonInBakedSalmonAndAsparagus = new FoodItemRecipe(_salmon, _bakedSalmonAndAsparagus, 2.5f);
        _asparagusInBakedSalmonAndAsparagus = new FoodItemRecipe(_asparagus, _bakedSalmonAndAsparagus, 1f);
        _butterInBakedSalmonAndAsparagus = new FoodItemRecipe(_butter, _bakedSalmonAndAsparagus, 0.1f);
        _garlicInBakedSalmonAndAsparagus = new FoodItemRecipe(_garlic, _bakedSalmonAndAsparagus, 0.05f);
        _saltInBakedSalmonAndAsparagus = new FoodItemRecipe(_salt, _bakedSalmonAndAsparagus, 0.02f);
        _crackersInCWP = new FoodItemRecipe(_crackers, _crackersWithPesti, 0.5f);
        _pestiInCWP = new FoodItemRecipe(_pesto, _crackersWithPesti, 0.2f);
        _salamiInSSOAC = new FoodItemRecipe(_salami, _salamiSticksOlivesAndCheese, 0.5f);
        _oliveInSSOAC = new FoodItemRecipe(_olives, _salamiSticksOlivesAndCheese, 0.5f);
        _cheeseInSSOAC = new FoodItemRecipe(_slicedCheese, _salamiSticksOlivesAndCheese, 0.5f);
        _salamiInSS = new FoodItemRecipe(_salami, _salamiSticks, 0.5f);
        _appleinACOS = new FoodItemRecipe(_apple, _appleCarrotOrangeSmoothie, 1f);
        _orangeinACOS = new FoodItemRecipe(_orange, _appleCarrotOrangeSmoothie, 1f);
        _carrotinACOS = new FoodItemRecipe(_carrot, _appleCarrotOrangeSmoothie,1f);
        _milkinACOS = new FoodItemRecipe(_milk,_appleCarrotOrangeSmoothie,1f);
        _blueberriesinBS = new FoodItemRecipe(_blueberry,_blueberrySmoothie,1f);
        _milkInBS = new FoodItemRecipe(_milk,_blueberrySmoothie,1f);
        _mangoInMS = new FoodItemRecipe(_mango, _mangoSmoothie, 1.5f);
        _milkInMS = new FoodItemRecipe(_milk, _mangoSmoothie, 1f);
        _bananaInSBS = new FoodItemRecipe(_banana,_strawberryBananaSmoothie, 1f);
        _strawberryInSBS = new FoodItemRecipe(_strawberry,_strawberryBananaSmoothie, 1f);
        _milkInSBS = new FoodItemRecipe(_milk,_strawberryBananaSmoothie, 1f);
        _blackberryInBSFM = new FoodItemRecipe(_blackberry,_blackberryStrawberryfruitMix,0.5f);
        _strawberryInBSFM = new FoodItemRecipe(_strawberry,_blackberryStrawberryfruitMix,0.5f);
        _raspberryInRBFM = new FoodItemRecipe(_raspberry,_raspberryBlueberryfruitMix,0.6f);
        _blueberryInRBFM = new FoodItemRecipe(_blueberry, _raspberryBlueberryfruitMix,0.3f);
        _appleInAOPFM = new FoodItemRecipe(_apple,_appleOrangePearfruitMix, 0.2f);
        _orangeInAOPFM = new FoodItemRecipe(_orange,_appleOrangePearfruitMix, 0.2f);
        _pearInAOPFM = new FoodItemRecipe(_pear,_appleOrangePearfruitMix, 0.2f);
        _pIneappleInPOFM = new FoodItemRecipe(_pineapple,_pineappleOrangefruitMix,0.3f); 
        _orangeInPOFM = new FoodItemRecipe(_orange,_pineappleOrangefruitMix,0.3f);
        _raisinInARM = new FoodItemRecipe(_raisin,_almondRaisinMix,0.3f);
        _almondInARM = new FoodItemRecipe(_almond,_almondRaisinMix,0.3f);
        _pistachioinPNM = new FoodItemRecipe(_pistachionut,_pistachioNutMix,0.6f);
        _peanutInCPM = new FoodItemRecipe(_peanut,_cashewPeanutNutMix,0.3f);
        _cashewInCPM = new FoodItemRecipe(_cashewnut,_cashewPeanutNutMix,0.3f);
        _mincedVealAndPorkInFWR = new FoodItemRecipe(_mincedVealAndPork,_frikadellerWithRice,1f);
        _eggInFWR = new FoodItemRecipe(_egg,_frikadellerWithRice,0.35f);
        _flourInFWR = new FoodItemRecipe(_flour,_frikadellerWithRice, 0.10f);
        _riceInFWR = new FoodItemRecipe(_basmatiRice,_frikadellerWithRice ,0.5f);
        _onionInFWR = new FoodItemRecipe(_onion,_frikadellerWithRice,0.2f);
        _oliveOilInFWR = new FoodItemRecipe(_oliveOil,_frikadellerWithRice, 0.1f);
        _saltInFWR = new FoodItemRecipe(_salt,_frikadellerWithRice, 0.005f);
        _pepperInFWR = new FoodItemRecipe(_pepper,_frikadellerWithRice,0.005f);
        _cuminInFWR = new FoodItemRecipe(_cumin, _frikadellerWithRice, 0.05f);
        _tortillasInTortillas = new FoodItemRecipe(_tortilla,_tortillas,1f);
        _mincedMeatInTortillas = new FoodItemRecipe(_mincedBeef,_tortillas, 0.6f);
        _bellPepperInTortillas = new FoodItemRecipe(_bellPepper,_tortillas, 0.5f);
        _cremeFraicheInTortillas = new FoodItemRecipe(_cremeFraiche,_tortillas,0.15f);
        _avocadoInTortillas = new FoodItemRecipe(_avocado,_tortillas,1f);
        _lemonInTortillas = new FoodItemRecipe(_lemon,_tortillas,0.1f);
        _garlicInTortillas = new FoodItemRecipe(_garlic,_tortillas,0.1f);
        _cucumberInTortillas = new FoodItemRecipe(_cucumber,_tortillas, 0.5f);
        _tomatoInTortillas = new FoodItemRecipe(_tomato,_tortillas,0.5f);
        _lambInLWP = new FoodItemRecipe(_lambChop,_lambChopWithPotatoes,1.5f);
        _potatoesInLWP = new FoodItemRecipe(_potatoes,_lambChopWithPotatoes, 2.5f);
        _oliveOilInLWP = new FoodItemRecipe(_oliveOil,_lambChopWithPotatoes, 0.01f);
        _saltInLWP = new FoodItemRecipe(_salt,_lambChopWithPotatoes, 0.005f);
        _pepperInLWP = new FoodItemRecipe(_pepper,_lambChopWithPotatoes, 0.005f);
        _garlicInLWP = new FoodItemRecipe(_garlic,_lambChopWithPotatoes, 0.1f);
        _pastaInVL = new FoodItemRecipe(_pasta, _veganLasagna, 1f);
        _carrotInVL = new FoodItemRecipe(_carrot, _veganLasagna, 0.25f);
        _bellPepperInVL = new FoodItemRecipe(_bellPepper, _veganLasagna, 0.25f);
        _garlicInVL = new FoodItemRecipe(_garlic,_veganLasagna, 0.08f);
        _saltInVL = new FoodItemRecipe(_salt,_veganLasagna, 0.00175f);
        _slicedTomatoesInVL = new FoodItemRecipe(_slicedTomatoes, _veganLasagna,0.25f);
        _mozzarellaInVL = new FoodItemRecipe(_mozzarella,_meatLasagna,0.25f);
        _oliveOilInVL = new FoodItemRecipe(_oliveOil, _veganLasagna, 0.1f);
        _mincedMeatInML = new FoodItemRecipe(_mincedBeef,_meatLasagna, 1f);
        _pastaInML = new FoodItemRecipe(_pasta, _meatLasagna, 1f);
        _carrotInML = new FoodItemRecipe(_carrot, _meatLasagna, 0.25f);
        _bellPepperInML = new FoodItemRecipe(_bellPepper, _meatLasagna, 0.25f);
        _garlicInML = new FoodItemRecipe(_garlic,_meatLasagna, 0.08f);
        _saltInML = new FoodItemRecipe(_salt,_meatLasagna, 0.00175f);
        _slicedTomatoesInML = new FoodItemRecipe(_slicedTomatoes, _meatLasagna,0.25f);
        _mozzarellaInML = new FoodItemRecipe(_mozzarella,_meatLasagna,0.25f);
        _oliveOilInML = new FoodItemRecipe(_oliveOil, _meatLasagna, 0.1f);
        _peanutbutterInPeanutbutterSandwich = new FoodItemRecipe(_Peanutbutter, _peanutbutterSandwich, 0.5f);
        _toastInPeanutbutterSandwich = new FoodItemRecipe(_toast, _peanutbutterSandwich, 1f);
        _context.FoodItemRecipes.Add(_eggsInEggsAndBacon);
        _context.FoodItemRecipes.Add(_baconInEggsAndBacon);
        _context.FoodItemRecipes.Add(_bunsInBunsWithStrawberryJam);
        _context.FoodItemRecipes.Add(_strawberryJamInBunsWithStrawberryJam);
        _context.FoodItemRecipes.Add(_bunsInBunsWithRaspberryJam);
        _context.FoodItemRecipes.Add(_raspberryJamInBunsWithRaspberryJam);
        _context.FoodItemRecipes.Add(_bunsInBunsWithBlackberryJam);
        _context.FoodItemRecipes.Add(_blackberryJamInBunsWithBlackberryJam);
        _context.FoodItemRecipes.Add(_bunsInBunsWithRhubarbJam);
        _context.FoodItemRecipes.Add(_rhubarbJamInBunsWithRhubarbJam);
        _context.FoodItemRecipes.Add(_bunsInBunsWithCheese);
        _context.FoodItemRecipes.Add(_cheeseInBunsWithCheese);
        _context.FoodItemRecipes.Add(_bunsInBunsWithPeanutbutter);
        _context.FoodItemRecipes.Add(_peanutbutterInBunsWithPeanutbutter);
        _context.FoodItemRecipes.Add(_bunsInBunsWithmapleSyrup);
        _context.FoodItemRecipes.Add(_mapleSyrupInBunsWithmapleSyrup);
        _context.FoodItemRecipes.Add(_bunsInBunsWithSpreadChocolate);
        _context.FoodItemRecipes.Add(_spreadChocolateInBunsWithSpreadChocolate);
        _context.FoodItemRecipes.Add(_quinoaInQuinoaBowl);
        _context.FoodItemRecipes.Add(_tomatoInQuinoaBowl);
        _context.FoodItemRecipes.Add(_champignonInQuinoaBowl);
        _context.FoodItemRecipes.Add(_eggsInQuinoaBowl);
        _context.FoodItemRecipes.Add(_lemonInQuinoaBowl);
        _context.FoodItemRecipes.Add(_oatsInOatmealBowl);
        _context.FoodItemRecipes.Add(_milkInOatmealBowl);
        _context.FoodItemRecipes.Add(_blueberryInOatmealBowl);
        _context.FoodItemRecipes.Add(_raspberryInOatmealBowl);
        _context.FoodItemRecipes.Add(_oatsInOatmealBoiled);
        _context.FoodItemRecipes.Add(_butterInOatmealBoiled);
        _context.FoodItemRecipes.Add(_breadInBreadAndSalmon);
        _context.FoodItemRecipes.Add(_salmonInBreadAndSalmon);
        _context.FoodItemRecipes.Add(_butterInBreadAndSalmon);
        _context.FoodItemRecipes.Add(_breadInBreadAndHam);
        _context.FoodItemRecipes.Add(_hamInBreadAndHam);
        _context.FoodItemRecipes.Add(_butterInBreadAndHam);
        _context.FoodItemRecipes.Add(_eggsInEggsBaconAndSausages);
        _context.FoodItemRecipes.Add(_baconInEggsBaconAndSausages);
        _context.FoodItemRecipes.Add(_sausagesInEggsBaconAndSausages);
        _context.FoodItemRecipes.Add(_appleInFruitBowl);
        _context.FoodItemRecipes.Add(_orangeInFruitBowl);
        _context.FoodItemRecipes.Add(_pineapleInFruitBowl);
        _context.FoodItemRecipes.Add(_cornflakesInCereal);
        _context.FoodItemRecipes.Add(_milkInCereal);
        _context.FoodItemRecipes.Add(_flourInPancakes);
        _context.FoodItemRecipes.Add(_milkInPancakes);
        _context.FoodItemRecipes.Add(_eggsInPancakes);
        _context.FoodItemRecipes.Add(_sugarInPancakes);
        _context.FoodItemRecipes.Add(_butterInPancakes);
        _context.FoodItemRecipes.Add(_durumWrapInDurumKebab);
        _context.FoodItemRecipes.Add(_kebabInDurumKebab);
        _context.FoodItemRecipes.Add(_dressingInDurumKebab);
        _context.FoodItemRecipes.Add(_icebergInDurumKebab);
        _context.FoodItemRecipes.Add(_onionInDurumKebab);
        _context.FoodItemRecipes.Add(_tomatoInDurumKebab);
        _context.FoodItemRecipes.Add(_cucumberInDurumKebab);
        _context.FoodItemRecipes.Add(_durumWrapInDurumChicken);
        _context.FoodItemRecipes.Add(_chickenInDurumChicken);
        _context.FoodItemRecipes.Add(_dressingInDurumChicken);
        _context.FoodItemRecipes.Add(_icebergInDurumChicken);
        _context.FoodItemRecipes.Add(_onionInDurumChicken);
        _context.FoodItemRecipes.Add(_tomatoInDurumChicken);
        _context.FoodItemRecipes.Add(_cucumberInDurumChicken);
        _context.FoodItemRecipes.Add(_durumWrapInDurumMix);
        _context.FoodItemRecipes.Add(_kebabInDurumMix);
        _context.FoodItemRecipes.Add(_chickenInDurumMix);
        _context.FoodItemRecipes.Add(_dressingInDurumMix);
        _context.FoodItemRecipes.Add(_icebergInDurumMix);
        _context.FoodItemRecipes.Add(_onionInDurumMix);
        _context.FoodItemRecipes.Add(_tomatoInDurumMix);
        _context.FoodItemRecipes.Add(_cucumberInDurumMix);
        _context.FoodItemRecipes.Add(_frenchFriesInKebabBox);
        _context.FoodItemRecipes.Add(_kebabInKebabBox);
        _context.FoodItemRecipes.Add(_dressingInKebabBox);
        _context.FoodItemRecipes.Add(_icebergInKebabBox);
        _context.FoodItemRecipes.Add(_onionInKebabBox);
        _context.FoodItemRecipes.Add(_tomatoInKebabBox);
        _context.FoodItemRecipes.Add(_cucumberInKebabBox);
        _context.FoodItemRecipes.Add(_frenchFriesInChickenBox);
        _context.FoodItemRecipes.Add(_chickenInChickenBox);
        _context.FoodItemRecipes.Add(_dressingInChickenBox);
        _context.FoodItemRecipes.Add(_icebergInChickenBox);
        _context.FoodItemRecipes.Add(_onionInChickenBox);
        _context.FoodItemRecipes.Add(_tomatoInChickenBox);
        _context.FoodItemRecipes.Add(_cucumberInChickenBox);
        _context.FoodItemRecipes.Add(_frenchFriesInMixBox);
        _context.FoodItemRecipes.Add(_kebabInMixBox);
        _context.FoodItemRecipes.Add(_chickenInMixBox);
        _context.FoodItemRecipes.Add(_dressingInMixBox);
        _context.FoodItemRecipes.Add(_icebergInMixBox);
        _context.FoodItemRecipes.Add(_onionInMixBox);
        _context.FoodItemRecipes.Add(_tomatoInMixBox);
        _context.FoodItemRecipes.Add(_cucumberInMixBox);
        _context.FoodItemRecipes.Add(_fishFilletInFishFillet);
        _context.FoodItemRecipes.Add(_ryebreadInFishFillet);
        _context.FoodItemRecipes.Add(_remouladeInFishFillet);
        _context.FoodItemRecipes.Add(_butterInFishFillet);
        _context.FoodItemRecipes.Add(_ryebreadInRyebreadWithSalmon);
        _context.FoodItemRecipes.Add(_salmonInRyebreadWithSalmon);
        _context.FoodItemRecipes.Add(_butterInRyebreadWithSalmon);
        _context.FoodItemRecipes.Add(_ryebreadInRyebreadWithAvocado);
        _context.FoodItemRecipes.Add(_avocadoInRyebreadWithAvocado);
        _context.FoodItemRecipes.Add(_butterInRyebreadWithAvocado);
        _context.FoodItemRecipes.Add(_ryebreadInRyebreadPlatte);
        _context.FoodItemRecipes.Add(_butterInRyebreadPlatte);
        _context.FoodItemRecipes.Add(_avocadoInRyebreadPlatte);
        _context.FoodItemRecipes.Add(_salmonInRyebreadPlatte);
        _context.FoodItemRecipes.Add(_liverPateInRyebreadPlatte);
        _context.FoodItemRecipes.Add(_tunaInGrilledToastWithTunaAndPesto);
        _context.FoodItemRecipes.Add(_pestoInGrilledToastWithTunaAndPesto);
        _context.FoodItemRecipes.Add(_butterInGrilledToastWithTunaAndPesto);
        _context.FoodItemRecipes.Add(_toastInGrilledToastWithTunaAndPesto);
        _context.FoodItemRecipes.Add(_toastInGrilledToastWithHamAndCheese);
        _context.FoodItemRecipes.Add(_butterInGrilledToastWithHamAndCheese);
        _context.FoodItemRecipes.Add(_hamInGrilledToastWithHamAndCheese);
        _context.FoodItemRecipes.Add(_cheeseInGrilledToastWithHamAndCheese);
        _context.FoodItemRecipes.Add(_breadInPaninoWithTomatoMozzarellaAndSalami);
        _context.FoodItemRecipes.Add(_mayoInPaninoWithTomatoMozzarellaAndSalami);
        _context.FoodItemRecipes.Add(_tomatoInPaninoWithTomatoMozzarellaAndSalami);
        _context.FoodItemRecipes.Add(_mozzarellaInPaninoWithTomatoMozzarellaAndSalami);
        _context.FoodItemRecipes.Add(_salamiInPaninoWithTomatoMozzarellaAndSalami);
        _context.FoodItemRecipes.Add(_breadInHamAndCheeseSandwich);
        _context.FoodItemRecipes.Add(_mayoInHamAndCheeseSandwich);
        _context.FoodItemRecipes.Add(_hamInHamAndCheeseSandwich);
        _context.FoodItemRecipes.Add(_cheeseInHamAndCheeseSandwich);
        _context.FoodItemRecipes.Add(_onionInRisotto);
        _context.FoodItemRecipes.Add(_garlicInRisotto);
        _context.FoodItemRecipes.Add(_risottoRiceInRisotto);
        _context.FoodItemRecipes.Add(_champignonInRisotto);
        _context.FoodItemRecipes.Add(_bouillionInRisotto);
        _context.FoodItemRecipes.Add(_parmesanCheeseInRisotto);
        _context.FoodItemRecipes.Add(_eggsInPastaCarbonara);
        _context.FoodItemRecipes.Add(_cheeseInPastaCarbonara);
        _context.FoodItemRecipes.Add(_baconInPastaCarbonara);
        _context.FoodItemRecipes.Add(_pastaInPastaCarbonara);
        _context.FoodItemRecipes.Add(_pepperInPastaCarbonara);
        _context.FoodItemRecipes.Add(_greekYougurtInButterChicken);
        _context.FoodItemRecipes.Add(_chickenInButterChicken);
        _context.FoodItemRecipes.Add(_cuminInButterChicken);
        _context.FoodItemRecipes.Add(_butterInButterChicken);
        _context.FoodItemRecipes.Add(_creamInButterChicken);
        _context.FoodItemRecipes.Add(_slicedTomatoesInButterChicken);
        _context.FoodItemRecipes.Add(_gingerInButterChicken);
        _context.FoodItemRecipes.Add(_riceInButterChicken);
        _context.FoodItemRecipes.Add(_chiliInCCC);
        _context.FoodItemRecipes.Add(_mincedMeatInCCC);
        _context.FoodItemRecipes.Add(_slicedTomatoesInCCC);
        _context.FoodItemRecipes.Add(_bakedBeansInCCC);
        _context.FoodItemRecipes.Add(_kidneyBeansInCCC);
        _context.FoodItemRecipes.Add(_garlicInCCC);
        _context.FoodItemRecipes.Add(_onionInCCC);
        _context.FoodItemRecipes.Add(_oliveOilInCCC);
        _context.FoodItemRecipes.Add(_mincedVealAndPorkInMBC);
        _context.FoodItemRecipes.Add(_flourInMBC);
        _context.FoodItemRecipes.Add(_onionInMBC);
        _context.FoodItemRecipes.Add(_milkIMBC);
        _context.FoodItemRecipes.Add(_eggInMBC);
        _context.FoodItemRecipes.Add(_butterInMBC);
        _context.FoodItemRecipes.Add(_curryInMBC);
        _context.FoodItemRecipes.Add(_riceInMBC);
        _context.FoodItemRecipes.Add(_steakInBeefBernaise);
        _context.FoodItemRecipes.Add(_potatoesInBeefBernaise);
        _context.FoodItemRecipes.Add(_greenBeansInBeefBernaise);
        _context.FoodItemRecipes.Add(_bernaiseInBeefBernaise);
        _context.FoodItemRecipes.Add(_pastaInPastaPuttanesca);
        _context.FoodItemRecipes.Add(_slicedTomatoesInPastaPuttanesca);
        _context.FoodItemRecipes.Add(_olivesInPastaPuttanesca);
        _context.FoodItemRecipes.Add(_anchoviesInPastaPuttanesca);
        _context.FoodItemRecipes.Add(_garlicInPastaPuttanesca);
        _context.FoodItemRecipes.Add(_capersInPastaPuttanesca);
        _context.FoodItemRecipes.Add(_pastaInPastaTuna);
        _context.FoodItemRecipes.Add(_tunaInPastaTuna);
        _context.FoodItemRecipes.Add(_capersInPastaTuna);
        _context.FoodItemRecipes.Add(_lemonInPastaTuna);
        _context.FoodItemRecipes.Add(_steakInSteakWithPepperSauce);
        _context.FoodItemRecipes.Add(_potatoesInSteakWithPepperSauce);
        _context.FoodItemRecipes.Add(_greenBeansInSteakWithPepperSauce);
        _context.FoodItemRecipes.Add(_pepperSauceInSteakWithPepperSauce);
        _context.FoodItemRecipes.Add(_steakInSteakWithWhiskeySauce);
        _context.FoodItemRecipes.Add(_potatoesInSteakWithWhiskeySauce);
        _context.FoodItemRecipes.Add(_greenBeansInSteakWithWhiskeySauce);
        _context.FoodItemRecipes.Add(_whiskeySauceInSteakWithWhiskeySauce);
        _context.FoodItemRecipes.Add(_tomatosInTacos);
        _context.FoodItemRecipes.Add(_onionInTacos);
        _context.FoodItemRecipes.Add(_avocadoInTacos);
        _context.FoodItemRecipes.Add(_mincedBeefInTacos);
        _context.FoodItemRecipes.Add(_garlicInTacos);
        _context.FoodItemRecipes.Add(_kidneyBeansInTacos);
        _context.FoodItemRecipes.Add(_limeInTacos);
        _context.FoodItemRecipes.Add(_corianderInTacos);
        _context.FoodItemRecipes.Add(_paprikaInTacos);
        _context.FoodItemRecipes.Add(_oreganoInTacos);
        _context.FoodItemRecipes.Add(_tacosInTacos);
        _context.FoodItemRecipes.Add(_porkInHoneyGarlicFriedPorkChops);
        _context.FoodItemRecipes.Add(_honeyInHoneyGarlicFriedPorkChops);
        _context.FoodItemRecipes.Add(_garlicInHoneyGarlicFriedPorkChops);
        _context.FoodItemRecipes.Add(_potatoesInHoneyGarlicFriedPorkChops);
        _context.FoodItemRecipes.Add(_broccoliInHoneyGarlicFriedPorkChops);
        _context.FoodItemRecipes.Add(_butterInHoneyGarlicFriedPorkChops);
        _context.FoodItemRecipes.Add(_vinegarInHoneyCarlicFriedPorkChops);
        _context.FoodItemRecipes.Add(_chickenInChickenFriedRice);
        _context.FoodItemRecipes.Add(_riceInChickenFriedRice);
        _context.FoodItemRecipes.Add(_onionInChickenFriedRice);
        _context.FoodItemRecipes.Add(_carrotInChickenFriedRice);
        _context.FoodItemRecipes.Add(_garlicInChickenFriedRice);
        _context.FoodItemRecipes.Add(_oilInChickenFriedRice);
        _context.FoodItemRecipes.Add(_eggInChickenFriedRice);
        _context.FoodItemRecipes.Add(_saltInChickenFriedRice);
        _context.FoodItemRecipes.Add(_pepperInChickenFriedRice);
        _context.FoodItemRecipes.Add(_shrimpsInShrimpStirFry);
        _context.FoodItemRecipes.Add(_oilInShrimpStirFry);
        _context.FoodItemRecipes.Add(_garlicInShrimpStirFry);
        _context.FoodItemRecipes.Add(_gingerInShrimpStirFry);
        _context.FoodItemRecipes.Add(_lemonInShrimpStirFry);
        _context.FoodItemRecipes.Add(_onionInShrimpStirFry);
        _context.FoodItemRecipes.Add(_saltInShrimpStirFry);
        _context.FoodItemRecipes.Add(_pepperInShrimpStirFry);
        _context.FoodItemRecipes.Add(_corianderInShrimpStirFry);
        _context.FoodItemRecipes.Add(_riceInShrimpStirFry);
        _context.FoodItemRecipes.Add(_oilInSpringRolls);
        _context.FoodItemRecipes.Add(_mincedPorkInSpringRolls);
        _context.FoodItemRecipes.Add(_garlicInSpringRolls);
        _context.FoodItemRecipes.Add(_gingerInSpringRolls);
        _context.FoodItemRecipes.Add(_springOnionsInSpringRolls);
        _context.FoodItemRecipes.Add(_scallionsInSpringRolls);
        _context.FoodItemRecipes.Add(_vinegarInSpringRolls);
        _context.FoodItemRecipes.Add(_phylloDoughInSpringRolls);
        _context.FoodItemRecipes.Add(_limeInSpringRolls);
        _context.FoodItemRecipes.Add(_corianderInSpringRolls);
        _context.FoodItemRecipes.Add(_lemonInBakedSalmonAndAsparagus);
        _context.FoodItemRecipes.Add(_salmonInBakedSalmonAndAsparagus);
        _context.FoodItemRecipes.Add(_asparagusInBakedSalmonAndAsparagus);
        _context.FoodItemRecipes.Add(_butterInBakedSalmonAndAsparagus);
        _context.FoodItemRecipes.Add(_garlicInBakedSalmonAndAsparagus);
        _context.FoodItemRecipes.Add(_crackersInCWP);
        _context.FoodItemRecipes.Add(_pestiInCWP);
        _context.FoodItemRecipes.Add(_salamiInSSOAC);
        _context.FoodItemRecipes.Add(_oliveInSSOAC);
        _context.FoodItemRecipes.Add(_cheeseInSSOAC);
        _context.FoodItemRecipes.Add(_salamiInSS);
        _context.FoodItemRecipes.Add(_peanutbutterInPeanutbutterSandwich);
        _context.FoodItemRecipes.Add(_toastInPeanutbutterSandwich);
        _context.FoodItemRecipes.Add(_appleinACOS);
        _context.FoodItemRecipes.Add(_orangeinACOS);
        _context.FoodItemRecipes.Add(_carrotinACOS);
        _context.FoodItemRecipes.Add(_milkinACOS);
        _context.FoodItemRecipes.Add(_blueberriesinBS);
        _context.FoodItemRecipes.Add(_milkInBS);
        _context.FoodItemRecipes.Add(_mangoInMS);
        _context.FoodItemRecipes.Add(_milkInMS);
        _context.FoodItemRecipes.Add(_bananaInSBS);
        _context.FoodItemRecipes.Add(_strawberryInSBS);
        _context.FoodItemRecipes.Add(_milkInSBS);
        _context.FoodItemRecipes.Add(_blackberryInBSFM);
        _context.FoodItemRecipes.Add(_strawberryInBSFM);
        _context.FoodItemRecipes.Add(_raspberryInRBFM);
        _context.FoodItemRecipes.Add(_blueberryInRBFM);
        _context.FoodItemRecipes.Add(_appleInAOPFM);
        _context.FoodItemRecipes.Add(_orangeInAOPFM);
        _context.FoodItemRecipes.Add(_pearInAOPFM);
        _context.FoodItemRecipes.Add(_pIneappleInPOFM);
        _context.FoodItemRecipes.Add(_orangeInPOFM);
        _context.FoodItemRecipes.Add(_raisinInARM);
        _context.FoodItemRecipes.Add(_almondInARM);
        _context.FoodItemRecipes.Add(_pistachioinPNM);
        _context.FoodItemRecipes.Add(_peanutInCPM);
        _context.FoodItemRecipes.Add(_cashewInCPM);
        _context.FoodItemRecipes.Add(_mincedVealAndPorkInFWR);
        _context.FoodItemRecipes.Add(_eggInFWR);
        _context.FoodItemRecipes.Add(_flourInFWR);
        _context.FoodItemRecipes.Add(_riceInFWR);
        _context.FoodItemRecipes.Add(_onionInFWR);
        _context.FoodItemRecipes.Add(_oliveOilInFWR);
        _context.FoodItemRecipes.Add(_saltInFWR);
        _context.FoodItemRecipes.Add(_pepperInFWR);
        _context.FoodItemRecipes.Add(_tortillasInTortillas);
        _context.FoodItemRecipes.Add(_mincedMeatInTortillas);
        _context.FoodItemRecipes.Add(_bellPepperInTortillas);
        _context.FoodItemRecipes.Add(_cremeFraicheInTortillas);
        _context.FoodItemRecipes.Add(_avocadoInTortillas);
        _context.FoodItemRecipes.Add(_lemonInTortillas);
        _context.FoodItemRecipes.Add(_garlicInTortillas);
        _context.FoodItemRecipes.Add(_cucumberInTortillas);
        _context.FoodItemRecipes.Add(_tomatoInTortillas);
        _context.FoodItemRecipes.Add(_lambInLWP);
        _context.FoodItemRecipes.Add(_potatoesInLWP);
        _context.FoodItemRecipes.Add(_oliveOilInLWP);
        _context.FoodItemRecipes.Add(_saltInLWP);
        _context.FoodItemRecipes.Add(_pepperInLWP);
        _context.FoodItemRecipes.Add(_garlicInLWP);
        _context.FoodItemRecipes.Add(_pastaInVL);
        _context.FoodItemRecipes.Add(_carrotInVL);
        _context.FoodItemRecipes.Add(_bellPepperInVL);
        _context.FoodItemRecipes.Add(_garlicInVL);
        _context.FoodItemRecipes.Add(_saltInVL);
        _context.FoodItemRecipes.Add(_slicedTomatoesInVL);
        _context.FoodItemRecipes.Add(_mozzarellaInVL);
        _context.FoodItemRecipes.Add(_mincedMeatInML);
        _context.FoodItemRecipes.Add(_pastaInML);
        _context.FoodItemRecipes.Add(_carrotInML);
        _context.FoodItemRecipes.Add(_bellPepperInML);
        _context.FoodItemRecipes.Add(_garlicInML);
        _context.FoodItemRecipes.Add(_saltInML);
        _context.FoodItemRecipes.Add(_slicedTomatoesInML);
        _context.FoodItemRecipes.Add(_mozzarellaInML);
        _context.SaveChanges();
    }
}