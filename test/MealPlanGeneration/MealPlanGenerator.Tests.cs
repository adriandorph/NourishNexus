using server.Services.MealPlan;

namespace test;
#pragma warning disable CS8618

public class MealPlanGeneratorTests
{
    NourishNexusContext _context;
    //Repos
    MealPlanGenerator _mealPlanGenerator;
    IFoodItemRepository _foodRepo;
    IMealRepository _mealRepo;
    IUserRepository _userRepo;
    IRecipeRepository _recipeRepo;
    ICategoryRepository _categoryRepo;


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


    public MealPlanGeneratorTests()
    {
        var connection = new SqliteConnection("Data Source=:memory:");
        connection.Open();

        var builder = new DbContextOptionsBuilder<NourishNexusContext>();
        builder.UseSqlite(connection);
        builder.EnableSensitiveDataLogging();

        var context = new NourishNexusContext(builder.Options);
        context.Database.EnsureCreated();

        _foodRepo = new FoodItemRepository(context);
        _userRepo = new UserRepository(context);
        _mealRepo = new MealRepository(context);
        _categoryRepo = new CategoryRepository(context);
        _recipeRepo = new RecipeRepository(context);
        _mealPlanGenerator = new MealPlanGenerator(_foodRepo, _mealRepo, _recipeRepo, _userRepo, _categoryRepo);
        
        _context = context;

        _context.Categories.Add(_fruit);
        _context.Categories.Add(_meat);
        _context.Categories.Add(_vegetarian);
        _context.SaveChanges();
        InitializeUser();
        InitializeFoodItems();
        InitializeRecipes();
        AddFoodItemsToRecipes();
    }

    //SETUP
    private void InitializeUser()
    {
        _author = new User("Author", "author@recipes.cook", new List<Recipe>());

        _author.BreakfastCalories = 500;
        _author.LunchCalories = 650;
        _author.DinnerCalories = 800;
        _author.SnackCalories = 700;
        //Total = 2650
        //100E% fedt = 2650 / 9 = 294.4444 g
        //100E% kulhydrater og protein = 2650 / 4 = 662.5g 
        
        _author.ProteinLB = 62.5f; //10 E%
        _author.ProteinII = 99.375f; //15 E%
        _author.ProteinUB = 132.5f; //20 E%

        _author.CarbohydratesLB = 298.125f;//45 E%
        _author.CarbohydratesII = 347.8125f;//52.5 E%
        _author.CarbohydratesUB = 397.5f;//60 E%

        //Added sugars
        _author.SugarsLB = 0f;
        _author.SugarsII = 0f;
        _author.SugarsUB = 62.5f; //10E%

        _author.FibresLB = 25f;
        _author.FibresII = 33.285f; //3g/MJ = 0.01256018421g/kcal
        _author.FibresUB = 70f; //ChatGPT

        _author.TotalFatLB = 73.61f; //25 E%
        _author.TotalFatII = 95.694443f; //32.5 E%
        _author.TotalFatUB = 117.777776f; //40 E%

        _author.SaturatedFatLB = 0f; //0 E%
        _author.SaturatedFatII = 0f; //0 E%
        _author.SaturatedFatUB = 29.444f; //10 E% 

        _author.MonounsaturatedFatLB = 29.444f; //10 E%
        _author.MonounsaturatedFatII = 44.1666f; //15 E%
        _author.MonounsaturatedFatUB = 58.888888f; //20 E% 

        _author.PolyunsaturatedFatLB = 14.72222f; //5 E%
        _author.PolyunsaturatedFatII = 22.083333f; //7.5 E%
        _author.PolyunsaturatedFatUB = 29.444f; //10 E%

        _author.TransFatLB = 0f; //0 E%
        _author.TransFatII = 0f; //0 E%
        _author.TransFatUB = 14.72222f; //5 E% ?

        _author.VitaminALB = 500f; //400 for women, 500 for men
        _author.VitaminAII = 900f; //700 for women, 900 for men
        _author.VitaminAUB = 1500f; //Post menupausal for women

        _author.VitaminB6LB = 1.0f; //0.8 for women, 1 for men
        _author.VitaminB6II = 1.5f; // 1.2 for women, 1.5 for men
        _author.VitaminB6UB = 25f;

        _author.VitaminB12LB = 1f;
        _author.VitaminB12II = 2f;
        _author.VitaminB12UB = float.PositiveInfinity; //Not established

        _author.VitaminCLB = 10f;
        _author.VitaminCII = 75f;
        _author.VitaminCUB = float.PositiveInfinity; //No health effects

        _author.VitaminDLB = 2.5f;
        _author.VitaminDII = 10f;
        _author.VitaminDUB = 100f;

        _author.VitaminELB = 3.5f; //3 for women, 4 for men
        _author.VitaminEII = 9f; //8 for women, 10 for men
        _author.VitaminEUB = 300f;

        _author.ThiaminLB = 0.6f; //0.5 for women, 0.6 for men
        _author.ThiaminII = 1.4f; //1.1 for women, 1.4 for men
        _author.ThiaminUB = float.PositiveInfinity; //Not established

        _author.RiboflavinLB = 0.8f;
        _author.RiboflavinII = 1.7f; //1.3 for women, 1.7 for men
        _author.RiboflavinUB = float.PositiveInfinity; //Not established

        _author.NiacinLB = 12f; //9 for women, 12 for men
        _author.NiacinII = 18f; //15 for women, 18 for men
        _author.NiacinUB = 35f;

        _author.FolateLB = 100f;
        _author.FolateII = 300f; //400 for women in resproductive age
        _author.FolateUB = float.PositiveInfinity; //No health effects of high intake

        _author.SaltLB = 0f;//?
        _author.SaltII = 6f;
        _author.SaltUB = 12f;//?

        //Potasium is in mg/d in frida
        _author.PotassiumLB = 1600f;
        _author.PotassiumII = 3500f; //3.1 for women, 3.5 for men g/d
        _author.PotassiumUB = float.PositiveInfinity; //Not established

        _author.MagnesiumLB = 0f; //Not specified
        _author.MagnesiumII = 350f; //280 for women, 350 for men
        _author.MagnesiumUB = float.PositiveInfinity; //Not specified

        _author.IronLB = 7f; //5 for women, 7 for men
        _author.IronII = 9f; // 15 for women, 9 for women post menupause, 9 for men
        _author.IronUB = 60f;

        _author.ZincLB = 5f; //4 for women, 5 for men
        _author.ZincII = 9f; //7 for women, 9 for men
        _author.ZincUB = float.PositiveInfinity; //Not specified

        _author.PhosphorusLB = 300f;
        _author.PhosphorusII = 600f;
        _author.PhosphorusUB = 3000f;

        _author.CopperLB = 0.4f;
        _author.CopperII = 0.9f;
        _author.CopperUB = 5f;

        _author.IodineLB = 70f;
        _author.IodineII = 150f;
        _author.IodineUB = 600f;

        _author.SeleniumLB = 20f;
        _author.SeleniumII = 60f; //50 for women, 60 for men
        _author.SeleniumUB = 300f;

        _author.CalciumLB = 400f;
        _author.CalciumII = 800f;
        _author.CalciumUB = 2500f;
    
        _context.Users.Add(_author);
        _context.SaveChanges();
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
        _eggsAndBacon = new Recipe("Eggs and Bacon", true, "", "", 1, new List<Category>{_meat}, true, false, false, false);
        _bunsWithStrawberryJam = new Recipe("Buns with Strawberry Jam", true, "", "", 1, new List<Category>{_vegetarian}, true, false, false, true);
        _bunsWithRaspberryJam = new Recipe("Buns with Raspberry Jam", true, "", "", 1, new List<Category>{_vegetarian}, true, false, false, true);
        _bunsWithBlackberryJam = new Recipe("Buns with Blackberry Jam", true, "", "", 1, new List<Category>{_vegetarian}, true, false, false, true);
        _bunsWithRhubarbJam = new Recipe("Buns with Rhubarb Jam", true, "", "", 1, new List<Category>{_vegetarian}, true, false, false, true);
        _bunsWithCheese = new Recipe("Buns with Cheese", true, "", "", 1, new List<Category>{_vegetarian}, true, false, false, true);
        _bunsWithPeanutbutter = new Recipe("Buns with Peanutbutter", true, "", "", 1, new List<Category>{_vegetarian}, true, false, false, true);
        _bunsWithMapleSyrup = new Recipe("Buns with Maple Syrup", true, "", "", 1, new List<Category>{_vegetarian}, true, false, false, true);
        _bunswithSpreadChocolate = new Recipe("Buns with Chocolate Spread", true, "", "", 1, new List<Category>{_vegetarian}, true, false, false, true);
        _QuinoaBowl = new Recipe("Quinoa Bowl", true, "", "", 1, new List<Category>{_vegetarian}, true, false, false, true);
        _OatMealBowl = new Recipe("Oatmeal Bowl", true, "", "", 1, new List<Category>{_vegetarian}, true, false, false, true);
        _OatMealBoiled = new Recipe("Oatmeal Boiled", true, "", "", 1, new List<Category>{_vegetarian}, true, false, false, true);
        _BreadAndSalmon = new Recipe("Bread and Salmon", true, "", "", 1, new List<Category>{_meat}, true, false, false, true);
        _BreadAndHam = new Recipe("Bread and Ham", true, "", "", 1, new List<Category>{_meat}, true, false, false, true);
        _EggsBaconAndSausages = new Recipe("Eggs with Bacon and Sausages", true, "", "", 1, new List<Category>{_meat}, true, false, false, false);
        _FruitBowl = new Recipe("Fruit bowl", true, "", "", 1, new List<Category>{_fruit, _vegetarian}, true, false, false, true);
        _Cereal = new Recipe("Cornflakes", true, "", "", 1, new List<Category>{_vegetarian}, true, false, false, false);
        _pancakes = new Recipe("Pancakes", true, "", "", 1, new List<Category>{_vegetarian}, true, false, false, true);
        
        //Lunch 
        _durumKebab = new Recipe("Durum with Kebab", true, "", "", 1, new List<Category>{_meat}, false, true, true, false);
        _durumChicken = new Recipe("Durum with Chicken", true, "", "", 1, new List<Category>{_meat}, false, true, true, false);
        _durumMix = new Recipe("Durum with Mix", true, "", "", 1, new List<Category>{_meat}, false, true, true, false);
        _mixBox = new Recipe("Mix Box", true, "", "", 1, new List<Category>{_meat}, false, true, true, false);
        _kebabBox = new Recipe("Kebab Box", true, "", "", 1, new List<Category>{_meat}, false, true, true, false);
        _chickenBox = new Recipe("Chicken Box", true, "", "", 1, new List<Category>{_meat}, false, true, true, false);
        _fishFillet = new Recipe("Fish fillets", true, "", "", 1, new List<Category>{_meat}, false, true, false, false);
        _ryebreadWithSalmon = new Recipe("Ryebread with Salmon", true, "", "", 1, new List<Category>{_meat}, false, true, false, true);
        _ryebreadWithAvocado = new Recipe("Ryebread with Avocado", true, "", "", 1, new List<Category>{_vegetarian}, false, true, false, true);
        _ryebreadPlatte = new Recipe("Ryebread Platte", true, "", "", 1, new List<Category>{_meat}, false, true, false, true);
        _grilledToastWithTunaAndPesto = new Recipe("Grilled Toast with Tuna and Pesto", true, "", "", 1, new List<Category>{_meat}, false, true, false, true);
        _grilledToastWithHamAndCheese = new Recipe("Grilled Toast with Ham and Cheese", true, "", "", 1, new List<Category>{_meat}, false, true, false, true);
        _paninoWithTomatoMozzarellaAndSalami = new Recipe("Panino with Tomato Mozzarella and Salami", true, "", "", 1, new List<Category>{_meat}, false, true, false, true);
        _hamAndCheeseSandwich = new Recipe("Ham and Cheese Sandwich", true, "", "", 1, new List<Category>{_meat}, false, true, false, true);
        
        //Dinner
        _risotto = new Recipe("Risotto", true, "Risotto", "Risotto",1, new List<Category>{_vegetarian},false, false, true, false);
        _pastaCarbonara = new Recipe("Pasta Carbonara", true, "Pasta Carbonara", "Pasta Carbonara", 1, new List<Category>{_meat}, false, false, true, false);
        _butterChicken = new Recipe("Butter Chicken", true, "Butter Chicken", "Butter Chicken", 1, new List<Category>{_meat}, false, false, true, false);
        _chiliConCarne = new Recipe("Chili Con Carne", true, "Chili Con Carne", "Chili Con Carne", 1, new List<Category>{_meat}, false, false, true, false);
        _beefBernaise = new Recipe("Beef Bernaise", true, "Beef Bernaise", "Beef Bernaise", 1, new List<Category>{_meat},false, false, true, false);
        _pastaPuttanesca = new Recipe("Pasta Putanesca", true, "Pasta Putanesca", "Pasta Putanesca", 1, new List<Category>{_meat}, false, false, true, false);
        _pastaTuna = new Recipe("Pasta Tuna", true, "Pasta Tuna", "Pasta Tuna", 1, new List<Category>{_meat}, false, false, true, false);
        _tortillas = new Recipe("Tortillas", true, "Tortillas", "Tortillas", 1, new List<Category>{_meat}, false, false, true, false);
        _meatballsCurry = new Recipe("Meatballs Curry", true, "Meatballs Curry", "Meatballs Curry", 1, new List<Category>{_meat}, false, false, true, false);
        _steakWithPepperSauce = new Recipe("Steak with Pepper Sauce", true, "Steak with Pepper Sauce", "", 1, new List<Category>{_meat}, false, false, true, false);
        _steakWithWhiskeySauce = new Recipe("Steak with Whiskey Sauce", true, "Steak with Whiskey Sauce", "", 1, new List<Category>{_meat}, false, false, true, false);
        _tacos = new Recipe("Tacos", true, "Tacos", "Tacos", 1, new List<Category>{}, false, false, true, false);
        _honeyGarlicFriedPorkChops = new Recipe("Honey-garlic fried pork chops", true,"Honey-garlic fried pork chops", "Honey-garlic fried pork chops", 1, new List<Category>{_meat},false, false, true, false);
        _chickenFriedRice = new Recipe("Chicken Fried Rice", true, "Chicken stir fry", "Chicken stir fry", 1, new List<Category>{_meat}, false, false, true, false);
        _shrimpStirFry = new Recipe("Shrimp stir fry", true, "Shrimp stir fry", "Shrimp stir fry", 1, new List<Category>{_meat},false, false, true, false);
        _springRolls = new Recipe("Spring rolls", true, "Spring rolls", "Spring rolls",1,new List<Category>{_vegetarian},false, false, true, false);
        _bakedSalmonAndAsparagus = new Recipe("Baked salmon and asparagus", true,"Baked salmon and asparagus", "Baked salmon and asparagus",1, new List<Category>{_meat}, false, false, true, false);
        _meatLasagna = new Recipe("Meat lasagna", true, "Meat lasagna", "Meat lasagna", 1, new List<Category>{_meat}, false, false, true, false);
        _veganLasagna = new Recipe("Vegan lasagna", true, "Vegan lasagna", "Vegan lasagna", 1, new List<Category>{_vegetarian}, false, false, true, false);
        _lambChopWithPotatoes = new Recipe("Lamb chop with potatoes", true, "Lamb chop with potatoes", "Lamb chop with potatoes", 1, new List<Category>{_meat}, false, false, true, false);
        _frikadellerWithRice = new Recipe ("Frikadeller with rice", true, "Frikadeller with rice", "Frikadeller with rice", 1, new List<Category>{_meat}, false, false, true, false);

        _cashewPeanutNutMix = new Recipe("Cashew Peanut Mix", true, "", "", 1, new List<Category>{_vegetarian}, false, false, false, true);
        _pistachioNutMix = new Recipe("Pistachio Mix", true, "", "", 1, new List<Category>{_vegetarian}, false, false, false, true);
        _almondRaisinMix = new Recipe("Alomnd Raisin Mix", true, "", "", 1, new List<Category>{_vegetarian}, false, false, false, true);
        _pineappleOrangefruitMix = new Recipe("Pineapple Orange Mix", true, "", "", 1, new List<Category>{_fruit, _vegetarian}, false, false, false, true);
        _appleOrangePearfruitMix = new Recipe("Apple Orange Pear Mix", true, "", "", 1, new List<Category>{_fruit, _vegetarian}, false, false, false, true);
        _raspberryBlueberryfruitMix = new Recipe("Raspberry Blueberry Mix", true, "", "", 1, new List<Category>{_fruit, _vegetarian}, false, false, false, true);
        _blackberryStrawberryfruitMix = new Recipe("Blackberry Strawberry Mix", true, "", "", 1, new List<Category>{_fruit, _vegetarian}, false, false, false, true);
        _strawberryBananaSmoothie = new Recipe("Strawberry Banana Smoothie", true, "", "", 1, new List<Category>{_fruit, _vegetarian}, false, false, false, true);
        _mangoSmoothie = new Recipe("Mango smoothie", true, "", "", 1, new List<Category>{_fruit, _vegetarian}, false, false, false, true);
        _blueberrySmoothie = new Recipe("Blueberry smoothie", true, "", "", 1, new List<Category>{_fruit, _vegetarian}, false, false, false, true);
        _appleCarrotOrangeSmoothie = new Recipe("Apple Carrot Orange smoothie", true, "", "", 1, new List<Category>{_fruit, _vegetarian}, false, false, false, true);
        _peanutbutterSandwich = new Recipe("Peanutbutter sandwich", true, "", "", 1, new List<Category>{_vegetarian}, false, false, false, true);
        _salamiSticks = new Recipe("Salami sticks", true, "", "", 1, new List<Category>{_meat}, false, false, false, true);
        _salamiSticksOlivesAndCheese = new Recipe("Salami sticks olives and cheese", true, "", "", 1, new List<Category>{_meat}, false, false, false, true);
        _crackersWithPesti = new Recipe("Crackers with pesti", true, "", "", 1, new List<Category>{_vegetarian}, false, false, false, true);
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


    //TESTS
    [Fact]
    public async void Generate7DayMealPlan()
    {
        //Arrange
        var date = new DateTime(2023, 1,1);
        //Act
        var r = await _mealPlanGenerator.Generate7DayMealPlan(_author.Id, date);
        
        //Assert
        var meals = await _mealRepo.ReadAllByDateRangeAndUser(_author.Id, date, date.AddDays(6));

        Console.WriteLine();
        Console.WriteLine($"Meals created: {meals.Count()}");
        foreach(var meal in meals)
        {
            var recipes = await _recipeRepo.ReadAllByMealId(meal.Id);
            var recipe = recipes.FirstOrDefault();
            if (recipe == null) throw new Exception();
            Console.Write(recipe.Recipe.Title + " ");
            Console.WriteLine(recipe.Amount);
        }

        Console.WriteLine();
        if (r.LB == null) throw new Exception();
        if (r.II == null) throw new Exception();
        if (r.UB == null) throw new Exception();
        if (r.PlannedIntake == null) throw new Exception();

        Console.WriteLine($"Protein LB: {r.LB.Protein}");
        Console.WriteLine($"Protein II: {r.II.Protein}");
        Console.WriteLine($"Protein UB: {r.UB.Protein}");
        Console.WriteLine($"Protein:Planned {r.PlannedIntake.Protein}");
        if(r.PlannedIntake.Protein <= r.UB.Protein && r.PlannedIntake.Protein >= r.LB.Protein) Console.WriteLine("PASSED");
        else Console.WriteLine("FAILED");
        Console.WriteLine();
        Console.WriteLine($"Carbohydrates LB: {r.LB.Carbohydrates}");
        Console.WriteLine($"Carbohydrates II: {r.II.Carbohydrates}");
        Console.WriteLine($"Carbohydrates UB: {r.UB.Carbohydrates}");
        Console.WriteLine($"Carbohydrates:Planned {r.PlannedIntake.Carbohydrates}");
        if(r.PlannedIntake.Carbohydrates <= r.UB.Carbohydrates && r.PlannedIntake.Carbohydrates >= r.LB.Carbohydrates) Console.WriteLine("PASSED");
        else Console.WriteLine("FAILED");
        Console.WriteLine();
        Console.WriteLine($"Sugars LB: {r.LB.Sugars}");
        Console.WriteLine($"Sugars II: {r.II.Sugars}");
        Console.WriteLine($"Sugars UB: {r.UB.Sugars}");
        Console.WriteLine($"Sugars:Planned {r.PlannedIntake.Sugars}");
        if(r.PlannedIntake.Sugars <= r.UB.Sugars && r.PlannedIntake.Sugars >= r.LB.Sugars) Console.WriteLine("PASSED");
        else Console.WriteLine("FAILED");
        Console.WriteLine();
        Console.WriteLine($"Fibres LB: {r.LB.Fibres}");
        Console.WriteLine($"Fibres II: {r.II.Fibres}");
        Console.WriteLine($"Fibres UB: {r.UB.Fibres}");
        Console.WriteLine($"Fibres:Planned {r.PlannedIntake.Fibres}");
        if(r.PlannedIntake.Fibres <= r.UB.Fibres && r.PlannedIntake.Fibres >= r.LB.Fibres) Console.WriteLine("PASSED");
        else Console.WriteLine("FAILED");
        Console.WriteLine();
        Console.WriteLine($"TotalFat LB: {r.LB.TotalFat}");
        Console.WriteLine($"TotalFat II: {r.II.TotalFat}");
        Console.WriteLine($"TotalFat UB: {r.UB.TotalFat}");
        Console.WriteLine($"TotalFat:Planned {r.PlannedIntake.TotalFat}");
        if(r.PlannedIntake.TotalFat <= r.UB.TotalFat && r.PlannedIntake.TotalFat >= r.LB.TotalFat) Console.WriteLine("PASSED");
        else Console.WriteLine("FAILED");
        Console.WriteLine();
        Console.WriteLine($"SaturatedFat LB: {r.LB.SaturatedFat}");
        Console.WriteLine($"SaturatedFat II: {r.II.SaturatedFat}");
        Console.WriteLine($"SaturatedFat UB: {r.UB.SaturatedFat}");
        Console.WriteLine($"SaturatedFat:Planned {r.PlannedIntake.SaturatedFat}");
        if(r.PlannedIntake.SaturatedFat <= r.UB.SaturatedFat && r.PlannedIntake.SaturatedFat >= r.LB.SaturatedFat) Console.WriteLine("PASSED");
        else Console.WriteLine("FAILED");
        Console.WriteLine();
        Console.WriteLine($"MonounsaturatedFat LB: {r.LB.MonounsaturatedFat}");
        Console.WriteLine($"MonounsaturatedFat II: {r.II.MonounsaturatedFat}");
        Console.WriteLine($"MonounsaturatedFat UB: {r.UB.MonounsaturatedFat}");
        Console.WriteLine($"MonounsaturatedFat:Planned {r.PlannedIntake.MonounsaturatedFat}");
        if(r.PlannedIntake.MonounsaturatedFat <= r.UB.MonounsaturatedFat && r.PlannedIntake.MonounsaturatedFat >= r.LB.MonounsaturatedFat) Console.WriteLine("PASSED");
        else Console.WriteLine("FAILED");
        Console.WriteLine();
        Console.WriteLine($"PolyunsaturatedFat LB: {r.LB.PolyunsaturatedFat}");
        Console.WriteLine($"PolyunsaturatedFat II: {r.II.PolyunsaturatedFat}");
        Console.WriteLine($"PolyunsaturatedFat UB: {r.UB.PolyunsaturatedFat}");
        Console.WriteLine($"PolyunsaturatedFat:Planned {r.PlannedIntake.PolyunsaturatedFat}");
        if(r.PlannedIntake.PolyunsaturatedFat <= r.UB.PolyunsaturatedFat && r.PlannedIntake.PolyunsaturatedFat >= r.LB.PolyunsaturatedFat) Console.WriteLine("PASSED");
        else Console.WriteLine("FAILED");
        Console.WriteLine();
        Console.WriteLine($"TransFat LB: {r.LB.TransFat}");
        Console.WriteLine($"TransFat II: {r.II.TransFat}");
        Console.WriteLine($"TransFat UB: {r.UB.TransFat}");
        Console.WriteLine($"TransFat:Planned {r.PlannedIntake.TransFat}");
        if(r.PlannedIntake.TransFat <= r.UB.TransFat && r.PlannedIntake.TransFat >= r.LB.TransFat) Console.WriteLine("PASSED");
        else Console.WriteLine("FAILED");
        Console.WriteLine();
        Console.WriteLine($"VitaminA LB: {r.LB.VitaminA}");
        Console.WriteLine($"VitaminA II: {r.II.VitaminA}");
        Console.WriteLine($"VitaminA UB: {r.UB.VitaminA}");
        Console.WriteLine($"VitaminA:Planned {r.PlannedIntake.VitaminA}");
        if(r.PlannedIntake.VitaminA <= r.UB.VitaminA && r.PlannedIntake.VitaminA >= r.LB.VitaminA) Console.WriteLine("PASSED");
        else Console.WriteLine("FAILED");
        Console.WriteLine();
        Console.WriteLine($"VitaminB6 LB: {r.LB.VitaminB6}");
        Console.WriteLine($"VitaminB6 II: {r.II.VitaminB6}");
        Console.WriteLine($"VitaminB6 UB: {r.UB.VitaminB6}");
        Console.WriteLine($"VitaminB6:Planned {r.PlannedIntake.VitaminB6}");
        if(r.PlannedIntake.VitaminB6 <= r.UB.VitaminB6 && r.PlannedIntake.VitaminB6 >= r.LB.VitaminB6) Console.WriteLine("PASSED");
        else Console.WriteLine("FAILED");
        Console.WriteLine();
        Console.WriteLine($"VitaminB12 LB: {r.LB.VitaminB12}");
        Console.WriteLine($"VitaminB12 II: {r.II.VitaminB12}");
        Console.WriteLine($"VitaminB12 UB: {r.UB.VitaminB12}");
        Console.WriteLine($"VitaminB12:Planned {r.PlannedIntake.VitaminB12}");
        if(r.PlannedIntake.VitaminB12 <= r.UB.VitaminB12 && r.PlannedIntake.VitaminB12 >= r.LB.VitaminB12) Console.WriteLine("PASSED");
        else Console.WriteLine("FAILED");
        Console.WriteLine();
        Console.WriteLine($"VitaminC LB: {r.LB.VitaminC}");
        Console.WriteLine($"VitaminC II: {r.II.VitaminC}");
        Console.WriteLine($"VitaminC UB: {r.UB.VitaminC}");
        Console.WriteLine($"VitaminC:Planned {r.PlannedIntake.VitaminC}");
        if(r.PlannedIntake.VitaminC <= r.UB.VitaminC && r.PlannedIntake.VitaminC >= r.LB.VitaminC) Console.WriteLine("PASSED");
        else Console.WriteLine("FAILED");
        Console.WriteLine();
        Console.WriteLine($"VitaminD LB: {r.LB.VitaminD}");
        Console.WriteLine($"VitaminD II: {r.II.VitaminD}");
        Console.WriteLine($"VitaminD UB: {r.UB.VitaminD}");
        Console.WriteLine($"VitaminD:Planned {r.PlannedIntake.VitaminD}");
        if(r.PlannedIntake.VitaminD <= r.UB.VitaminD && r.PlannedIntake.VitaminD >= r.LB.VitaminD) Console.WriteLine("PASSED");
        else Console.WriteLine("FAILED");
        Console.WriteLine();
        Console.WriteLine($"VitaminE LB: {r.LB.VitaminE}");
        Console.WriteLine($"VitaminE II: {r.II.VitaminE}");
        Console.WriteLine($"VitaminE UB: {r.UB.VitaminE}");
        Console.WriteLine($"VitaminE:Planned {r.PlannedIntake.VitaminE}");
        if(r.PlannedIntake.VitaminE <= r.UB.VitaminE && r.PlannedIntake.VitaminE >= r.LB.VitaminE) Console.WriteLine("PASSED");
        else Console.WriteLine("FAILED");
        Console.WriteLine();
        Console.WriteLine($"Thiamin LB: {r.LB.Thiamin}");
        Console.WriteLine($"Thiamin II: {r.II.Thiamin}");
        Console.WriteLine($"Thiamin UB: {r.UB.Thiamin}");
        Console.WriteLine($"Thiamin:Planned {r.PlannedIntake.Thiamin}");
        if(r.PlannedIntake.Thiamin <= r.UB.Thiamin && r.PlannedIntake.Thiamin >= r.LB.Thiamin) Console.WriteLine("PASSED");
        else Console.WriteLine("FAILED");
        Console.WriteLine();
        Console.WriteLine($"Riboflavin LB: {r.LB.Riboflavin}");
        Console.WriteLine($"Riboflavin II: {r.II.Riboflavin}");
        Console.WriteLine($"Riboflavin UB: {r.UB.Riboflavin}");
        Console.WriteLine($"Riboflavin:Planned {r.PlannedIntake.Riboflavin}");
        if(r.PlannedIntake.Riboflavin <= r.UB.Riboflavin && r.PlannedIntake.Riboflavin >= r.LB.Riboflavin) Console.WriteLine("PASSED");
        else Console.WriteLine("FAILED");
        Console.WriteLine();
        Console.WriteLine($"Niacin LB: {r.LB.Niacin}");
        Console.WriteLine($"Niacin II: {r.II.Niacin}");
        Console.WriteLine($"Niacin UB: {r.UB.Niacin}");
        Console.WriteLine($"Niacin:Planned {r.PlannedIntake.Niacin}");
        if(r.PlannedIntake.Niacin <= r.UB.Niacin && r.PlannedIntake.Niacin >= r.LB.Niacin) Console.WriteLine("PASSED");
        else Console.WriteLine("FAILED");
        Console.WriteLine();
        Console.WriteLine($"Folate LB: {r.LB.Folate}");
        Console.WriteLine($"Folate II: {r.II.Folate}");
        Console.WriteLine($"Folate UB: {r.UB.Folate}");
        Console.WriteLine($"Folate:Planned {r.PlannedIntake.Folate}");
        if(r.PlannedIntake.Folate <= r.UB.Folate && r.PlannedIntake.Folate >= r.LB.Folate) Console.WriteLine("PASSED");
        else Console.WriteLine("FAILED");
        Console.WriteLine();
        Console.WriteLine($"Salt LB: {r.LB.Salt}");
        Console.WriteLine($"Salt II: {r.II.Salt}");
        Console.WriteLine($"Salt UB: {r.UB.Salt}");
        Console.WriteLine($"Salt:Planned {r.PlannedIntake.Salt}");
        if(r.PlannedIntake.Salt <= r.UB.Salt && r.PlannedIntake.Salt >= r.LB.Salt) Console.WriteLine("PASSED");
        else Console.WriteLine("FAILED");
        Console.WriteLine();
        Console.WriteLine($"Potassium LB: {r.LB.Potassium}");
        Console.WriteLine($"Potassium II: {r.II.Potassium}");
        Console.WriteLine($"Potassium UB: {r.UB.Potassium}");
        Console.WriteLine($"Potassium:Planned {r.PlannedIntake.Potassium}");
        if(r.PlannedIntake.Potassium <= r.UB.Potassium && r.PlannedIntake.Potassium >= r.LB.Potassium) Console.WriteLine("PASSED");
        else Console.WriteLine("FAILED");
        Console.WriteLine();
        Console.WriteLine($"Magnesium LB: {r.LB.Magnesium}");
        Console.WriteLine($"Magnesium II: {r.II.Magnesium}");
        Console.WriteLine($"Magnesium UB: {r.UB.Magnesium}");
        Console.WriteLine($"Magnesium:Planned {r.PlannedIntake.Magnesium}");
        if(r.PlannedIntake.Magnesium <= r.UB.Magnesium && r.PlannedIntake.Magnesium >= r.LB.Magnesium) Console.WriteLine("PASSED");
        else Console.WriteLine("FAILED");
        Console.WriteLine();
        Console.WriteLine($"Iron LB: {r.LB.Iron}");
        Console.WriteLine($"Iron II: {r.II.Iron}");
        Console.WriteLine($"Iron UB: {r.UB.Iron}");
        Console.WriteLine($"Iron:Planned {r.PlannedIntake.Iron}");
        if(r.PlannedIntake.Iron <= r.UB.Iron && r.PlannedIntake.Iron >= r.LB.Iron) Console.WriteLine("PASSED");
        else Console.WriteLine("FAILED");
        Console.WriteLine();
        Console.WriteLine($"Zinc LB: {r.LB.Zinc}");
        Console.WriteLine($"Zinc II: {r.II.Zinc}");
        Console.WriteLine($"Zinc UB: {r.UB.Zinc}");
        Console.WriteLine($"Zinc:Planned {r.PlannedIntake.Zinc}");
        if(r.PlannedIntake.Zinc <= r.UB.Zinc && r.PlannedIntake.Zinc >= r.LB.Zinc) Console.WriteLine("PASSED");
        else Console.WriteLine("FAILED");
        Console.WriteLine();
        Console.WriteLine($"Phosphorus LB: {r.LB.Phosphorus}");
        Console.WriteLine($"Phosphorus II: {r.II.Phosphorus}");
        Console.WriteLine($"Phosphorus UB: {r.UB.Phosphorus}");
        Console.WriteLine($"Phosphorus:Planned {r.PlannedIntake.Phosphorus}");
        if(r.PlannedIntake.Phosphorus <= r.UB.Phosphorus && r.PlannedIntake.Phosphorus >= r.LB.Phosphorus) Console.WriteLine("PASSED");
        else Console.WriteLine("FAILED");
        Console.WriteLine();
        Console.WriteLine($"Copper LB: {r.LB.Copper}");
        Console.WriteLine($"Copper II: {r.II.Copper}");
        Console.WriteLine($"Copper UB: {r.UB.Copper}");
        Console.WriteLine($"Copper:Planned {r.PlannedIntake.Copper}");
        if(r.PlannedIntake.Copper <= r.UB.Copper && r.PlannedIntake.Copper >= r.LB.Copper) Console.WriteLine("PASSED");
        else Console.WriteLine("FAILED");
        Console.WriteLine();
        Console.WriteLine($"Iodine LB: {r.LB.Iodine}");
        Console.WriteLine($"Iodine II: {r.II.Iodine}");
        Console.WriteLine($"Iodine UB: {r.UB.Iodine}");
        Console.WriteLine($"Iodine:Planned {r.PlannedIntake.Iodine}");
        if(r.PlannedIntake.Iodine <= r.UB.Iodine && r.PlannedIntake.Iodine >= r.LB.Iodine) Console.WriteLine("PASSED");
        else Console.WriteLine("FAILED");
        Console.WriteLine();
        Console.WriteLine($"Selenium LB: {r.LB.Selenium}");
        Console.WriteLine($"Selenium II: {r.II.Selenium}");
        Console.WriteLine($"Selenium UB: {r.UB.Selenium}");
        Console.WriteLine($"Selenium:Planned {r.PlannedIntake.Selenium}");
        if(r.PlannedIntake.Selenium <= r.UB.Selenium && r.PlannedIntake.Selenium >= r.LB.Selenium) Console.WriteLine("PASSED");
        else Console.WriteLine("FAILED");
        Console.WriteLine();
        Console.WriteLine($"Calcium LB: {r.LB.Calcium}");
        Console.WriteLine($"Calcium II: {r.II.Calcium}");
        Console.WriteLine($"Calcium UB: {r.UB.Calcium}");
        Console.WriteLine($"Calcium:Planned {r.PlannedIntake.Calcium}");
        if(r.PlannedIntake.Calcium <= r.UB.Calcium && r.PlannedIntake.Calcium >= r.LB.Calcium) Console.WriteLine("PASSED");
        else Console.WriteLine("FAILED");
        Console.WriteLine();

        Assert.Equal(server.Services.MealPlan.Response.Success, r.Response);
    }
}