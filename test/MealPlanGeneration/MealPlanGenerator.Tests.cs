

public class MealPlanGeneratorTests
{
    NourishNexusContext _context;
    //Repos

    //Author
    User _author = new User("Author", "author@recipes.cook", new List<Recipe>());

    //Categories
    Category _fruit = new Category("Fruit");
    Category _italian = new Category("Italian");
    Category _meat = new Category("Meat");
    Category _Vegan = new Category("Vegan");
    Category _Vegetarian = new Category("Vegetarian");
    Category _lowFat = new Category("Low fat");
    Category _highProtein = new Category("High Protein");
    Category _fish = new Category("Fish");

    //Breakfast
    Recipe _eggsAndBacon;
    Recipe _bunsWithStrawberryJam;
    Recipe _bunsWithRaspberryJam;
    Recipe _bunswithBlackberryJam;
    Recipe _bunswithRhubarbJam;
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
    FoodItem _vanillaSugar;
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
    
    FoodItem _icebergSalad;
    FoodItem _tomato;
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
    FoodItem _tunaWater;
    FoodItem _mozzarella;
    FoodItem _salami;


    
    public MealPlanGeneratorTests()
    {
        var connection = new SqliteConnection("Data Source=:memory:");
        connection.Open();

        var builder = new DbContextOptionsBuilder<NourishNexusContext>();
        builder.UseSqlite(connection);
        builder.EnableSensitiveDataLogging();

        var context = new NourishNexusContext(builder.Options);
        context.Database.EnsureCreated();
        
        _context = context;

        InitializeFoodItems();
        InitializeRecipes();
        AddFoodItemsToRecipes();
    }

    //SETUP
    private void InitializeFoodItems()
    {
        _egg = new FoodItem("Eggs", 139f, 12.3f, 1.3f, 0f, 0f, 7.74f, 2.6f, 3.42f, 1.61f, 0.02f, 80.5f, 0.11f, 1.57f, 0.2f, 1.77f, 3.71f, 0f, 0.083f, 0.46f, 0.08f, 0f, 0.3f, 131.4f, 11.6f, 1.73f, 1.14f, 176f, 0.054f, 24.5f, 0f, 24.44f, 46.9f);
        _bacon = new FoodItem("Bacon", 430f, 13f, 0f, 0f, 0f, 39.06f, 17.09f, 18.9f, 3.07f, 0f, 0f, 0.15f, 0.65f, 0.8f, 0.43f, 0.3f, 0f, 0.48f, 0.16f, 3f, 2f, 2.85f, 185f, 15f, 0.51f, 1.98f, 220f, 0.063f, 0.7f, 0.8f, 8.3f, 5.1f);
        _buns = new FoodItem("Buns", 294f, 9.7f, 51.7f, 0.35f, 3.5f, 2.59f, 0.68f, 0.63f, 1.29f, 0f, 0f, 0.08f, 0f, 0f, 0f, 0.5f, 0f, 0.157f, 0.09f, 1.2f, 62.1f, 1.1f, 153.1f, 25.6f, 1.32f, 0.79f, 116f, 0.143f, 2.5f, 23.6f, 3.1f, 66.7f);
        _butter = new FoodItem("Butter", 741f, 0.7f, 0.6f, 0.58f, 0f, 77.02f, 53.54f, 16.98f, 2.2f, 3.52f, 749.3f, 0f, 0.17f, 0f, 0.41f, 1.76f, 0f, 0.007f, 0.04f, 0.1f, 3f, 0.9f, 29.1f, 1.6f, 0.01f, 0.04f, 21f, 0.022f, 2.7f, 0.6f, 1.56f, 16.8f);
        _strawberryJam = new FoodItem("Strawberry Jam", 226f, 0.5f, 54.3f, 58f, 1.2f, 0.4f, 0.04f, 0.07f, 0.29f, 0f, 4.2f, 0.03f, 0f, 14f, 0f, 0.2f, 0f, 0f, 0.04f, 0.2f, 5f, 0f, 44f, 4f, 0.5f, 0.03f, 13f, 0.12f, 1.4f, 10f, 0.3f, 25f);
        _raspberryJam = new FoodItem("Raspberry Jam", 203f, 0.6f, 48.1f, 46f, 1.2f, 0.48f, 0.05f, 0.05f, 0.39f, 0f, 4.2f, 0.03f, 0f, 5f, 0f, 0.15f, 0f, 0f, 0.04f, 0.2f, 5f, 0f, 44f, 4f, 0.5f, 0.03f, 13f, 0.12f, 1.4f, 10f, 0.3f, 15f);
        _slicedCheese = new FoodItem("Sliced Cheese", 325f, 23.7f, 1.6f, 0.18f, 0f, 21.54f, 14.54f, 5.14f, 0.53f, 0.99f, 212f, 0f, 0f, 0f, 0.35f, 0.5f, 0f, 0f, 0f, 0f, 0f, 1.6f, 60.5f, 27.2f, 0.09f, 3.37f, 446f, 0.049f, 14.1f, 1f, 14.17f, 682.7f);
        _Peanutbutter = new FoodItem("Peanut butter", 637f, 22.6f, 12.2f, 6.38f, 7.6f, 51.55f, 10.69f, 27.35f, 13.51f, 0f, 0f, 0.5f, 0f, 0f, 0f, 4.7f, 0f, 0.17f, 0.1f, 15f, 53f, 0.9f, 700f, 180f, 2.1f, 3f, 330f, 0.7f, 0.5f, 0f, 6.9f, 37f);
        _mapleSyrup = new FoodItem("Maple Syrup", 308f, 0.3f, 76.7f, 76.7f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0.01f, 0.1f, 0f, 0.2f, 220f, 34f, 2.5f, 0.13f, 3f, 0.24f, 5f, 30f, 1f, 75f);
        _spreadChocolate = new FoodItem("Chocolate Spread", 900f, 0f, 0f, 0f, 0f, 95.6f, 59.45f, 33.22f, 2.94f, 0f, 0f, 0f, 0f, 0f, 0f, 1.8f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f);
        _quinoa = new FoodItem("Quinoa", 347f, 13.7f, 42.3f, 3.2f, 13.9f, 4.4f, 0.51f, 1.18f, 2.71f, 0f, 0f, 0.14f, 0f, 0f, 0f, 2.33f, 3.1f, 0.064f, 0.29f, 0.73f, 195.5f, 0f, 870f, 175f, 3.75f, 2.8f, 415f, 0.515f, 0f, 60f, 4.6f, 46.5f);
        _oats = new FoodItem("Oat", 366f, 12.9f, 58.9f, 1.03f, 9.9f, 6.47f, 1.19f, 2.38f, 2.81f, 0f, 0f, 0.14f, 0f, 0f, 0f, 0.75f, 0f, 0.416f, 0.11f, 0.8f, 50.4f, 0.00405f, 386.3f, 154.5f, 3.86f, 2.99f, 440f, 0.39f, 0.5f, 121.1f, 5f, 115.3f);
        _milk = new FoodItem("Milk", 37f, 3.5f, 4.8f, 4.75f, 0f, 0.47f, 0.35f, 0.11f, 0.01f, 0f, 4.4f, 0.05f, 0.48f, 1.3f, 0.08f, 0.01f, 0f, 0.041f, 0.17f, 0.09f, 5.8f, 0.1f, 157.2f, 12.1f, 0.03f, 0.41f, 97f, 0.01f, 23.3f, 0.8f, 1.64f, 123.6f);
        _flute = new FoodItem("Flute", 259f, 8.1f, 47.9f, 0f, 4f, 2.33f, 0.94f, 0.73f, 0.66f, 0f, 0f, 0.1f, 0.07f, 0f, 0f, 0.5f, 0f, 0.192f, 0.07f, 1.57f, 39.3f, 1.1498611f, 161.5f, 28f, 1.16f, 0.82f, 112f, 0.134f, 23f, 3.5f, 2.06f, 35.2f);
        _ham = new FoodItem("Ham", 109f, 17.9f, 0.3f, 0f, 0f, 3.4f, 1.34f, 1.62f, 0.44f, 0f, 0f, 0.32f, 0.54f, 29f, 0.09f, 0.33f, 0f, 0.9f, 0.16f, 5.5f, 1.3f, 0f, 327f, 21f, 0.69f, 1.69f, 302f, 0.08f, 0f, 2f, 7.5f, 6.3f);
        _brunchSausages = new FoodItem("Brunch Sausages", 292f, 14f, 4.3f, 2.83f, 0.2f, 23.14f, 9.01f, 11.45f, 2.68f, 0f, 0f, 0.15f, 1.1f, 0f, 0.3f, 0.1f, 0f, 0.358f, 0.1f, 1.8f, 4f, 2.7f, 208.7f, 18.7f, 0.79f, 1.31f, 184f, 0.067f, 14f, 19f, 7.2f, 21.9f);
        _cornflakes = new FoodItem("Corn Flakes", 374f, 7.5f, 82f, 7.06f, 3.1f, 1.26f, 0.21f, 0.39f, 0.66f, 0f, 0f, 0.12f, 0f, 0f, 0f, 0.02f, 0f, 0.659f, 0.57f, 1.1f, 26.7f, 1.5195689f, 102.9f, 15.2f, 2.89f, 0.27f, 57f, 0.067f, 0.9f, 7.2f, 2.6f, 4.3f);
        _flour = new FoodItem("Flour", 343f, 9.7f, 56.5f, 1.13f, 6f, 1.24f, 0.38f, 0f, 0.85f, 0f, 0f, 0.07f, 0f, 0f, 0f, 0.43f, 0f, 0.164f, 0.03f, 0.37f, 30.6f, 0f, 154.7f, 25.4f, 1.17f, 0.87f, 115f, 0.134f, 1.4f, 3.7f, 4.23f, 17.6f);
        _lemon = new FoodItem("Lemon", 44f, 0.5f, 3.1f, 3.11f, 1.2f, 0.88f, 0.24f, 0.07f, 0.57f, 0f, 1.7f, 0.06f, 0f, 49f, 0f, 0.8f, 0f, 0.045f, 0.03f, 0.2f, 32f, 0.0075f, 175f, 9f, 0.08f, 0.13f, 21f, 0.053f, 0.3f, 6.6f, 0.12f, 35.1f);
        _blueberry = new FoodItem("Blueberry", 52f, 0.7f, 10.4f, 9.5f, 1.5f, 0.4f, 0.03f, 0.09f, 0.52f, 0f, 1.1f, 0.06f, 0f, 44f, 0f, 0f, 0f, 0.03f, 0.03f, 0.4f, 6f, 0.0075f, 103f, 7f, 0.8f, 0.1f, 9f, 0.11f, 1.2f, 0f, 0f, 15f);
        _blackberry = new FoodItem("Blackberry", 37f, 1.4f, 4.5f, 4.5f, 4.3f, 0.8f, 0.01f, 0.02f, 0.3f, 0f, 16.7f, 0.05f, 0f, 15f, 0f, 5.5f, 0f, 0.017f, 0.05f, 0.5f, 34f, 0.005f, 266f, 23f, 0.55f, 0.53f, 37f, 0.12f, 0.4f, 0f, 0.1f, 27f);
        _strawberry = new FoodItem("Strawberry", 38f, 0.7f, 6.1f, 6.07f, 1.5f, 0.48f, 0.05f, 0.08f, 0.35f, 0f, 3.3f, 0.05f, 0f, 66.9f, 0f, 0.41f, 20f, 0.015f, 0.01f, 0.43f, 117f, 0.0012695f, 178.6f, 12.5f, 0.25f, 0.1f, 24f, 0.038f, 0.1f, 3.5f, 0.18f, 18.5f);
        _raspberry = new FoodItem("Raspberry", 51f, 1.4f, 4.1f, 4.05f, 4.4f, 1.08f, 0.1f, 0.1f, 0.87f, 0f, 3.5f, 0.09f, 0f, 24f, 0f, 1.4f, 0f, 0.03f, 0.05f, 0.5f, 44f, 0.005f, 228f, 17f, 0.55f, 0.34f, 38f, 0.105f, 0.4f, 17.9f, 0.19f, 19.7f);
        _almond = new FoodItem("Almond", 606f, 21.2f, 6.6f, 6.55f, 10.6f, 46.91f, 4.05f, 31.4f, 11.36f, 0f, 0f, 0.13f, 0f, 0.8f, 0f, 23.35f, 0f, 0.137f, 0.94f, 1.88f, 76.1f, 0.00175f, 730f, 263.8f, 3.4f, 3.3f, 489f, 1f, 0.2f, 76f, 2.15f, 256.5f);
        _raisin = new FoodItem("Raisin", 333f, 3.2f, 69f, 68.85f, 3.6f, 1.06f, 0.52f, 0.06f, 0.47f, 0f, 2.3f, 0.11f, 0f, 3.3f, 0f, 0f, 0f, 0.085f, 0.03f, 0.5f, 4f, 0f, 785f, 35f, 2.4f, 0.3f, 107f, 0.32f, 2f, 17.8f, 0.4f, 45.1f);
        _cashewnut = new FoodItem("Cashew", 603f, 15.3f, 26.9f, 0f, 3f, 44.31f, 8.81f, 27.58f, 7.92f, 0f, 0f, 0.26f, 0f, 0f, 0f, 0.84f, 0f, 0.2f, 0.2f, 1.4f, 69f, 0.04f, 565f, 260f, 6f, 5.6f, 490f, 2.22f, 0f, 0f, 14.63f, 45f);
        _peanut = new FoodItem("Peanut", 596f, 25.8f, 8.1f, 3.1f, 7.7f, 46.78f, 6.83f, 24.42f, 15.55f, 0f, 0f, 0.35f, 0f, 0f, 0f, 8.2f, 0f, 0.91f, 0.1f, 20f, 106f, 0.005f, 703f, 170f, 1.9f, 3.1f, 409f, 0.86f, 0.5f, 60f, 6.9f, 55.6f);
        _mango = new FoodItem("Mango", 67f, 0.5f, 14.2f, 10.19f, 1.9f, 0.38f, 0.11f, 0.18f, 0.09f, 0f, 46.1f, 0.13f, 0f, 40.1f, 0f, 1.1f, 0f, 0.058f, 0.06f, 0.58f, 71.1f, 0.0051875f, 105.3f, 8.8f, 0.24f, 0.08f, 16f, 0.11f, 0.3f, 0f, 0.6f, 13.9f);
        _banana = new FoodItem("Banana", 93f, 1.1f, 19.7f, 15.37f, 1.6f, 0.15f, 0.08f, 0.04f, 0.03f, 0f, 4.4f, 0.31f, 0f, 11.2f, 0f, 0.27f, 0.5f, 0.04f, 0.01f, 0.6f, 38f, 0f, 348.1f, 28.1f, 0.25f, 0.17f, 26f, 0.106f, 0f, 4.1f, 0.35f, 6.6f);
        _apple = new FoodItem("Apple", 55f, 0.3f, 11.3f, 10.89f, 2.2f, 0.18f, 0.04f, 0.01f, 0.13f, 0f, 2.1f, 0.05f, 0f, 8.3f, 0f, 0.25f, 3f, 0.013f, 0.01f, 0.12f, 9f, 0.001503f, 117.9f, 4.5f, 0.12f, 0.02f, 10f, 0.031f, 0.1f, 0.5f, 0.01f, 4.1f);
        _orange = new FoodItem("Orange", 49f, 0.9f, 8.2f, 8.2f, 2f, 0.09f, 0.02f, 0.02f, 0.05f, 0f, 4f, 0.08f, 0f, 54.4f, 0f, 0.31f, 0f, 0.1f, 0.03f, 0.39f, 46.2f, 0.002846667f, 157.8f, 10.3f, 0.12f, 0.06f, 21f, 0.04f, 0.1f, 2.3f, 0.05f, 29.6f);
        _carrot = new FoodItem("Carrot", 29f, 0.3f, 4.4f, 4.4f, 2.5f, 0.04f, 0.01f, 0f, 0.03f, 0f, 524f, 0.07f, 0f, 2.5f, 0f, 0.33f, 2.2f, 0.008f, 0.03f, 0.28f, 11f, 0.095f, 233.3f, 9.1f, 0.21f, 0.24f, 19f, 0.05f, 0f, 3.4f, 0f, 29.5f);
        _pistachionut = new FoodItem("Pistachio", 606f, 21.6f, 8.1f, 7.2f, 8.8f, 45.53f, 5.81f, 25.33f, 14.28f, 0f, 0f, 0.9f, 0f, 5f, 0f, 1.64f, 26.2f, 0.424f, 0.24f, 1.1f, 68.4f, 0f, 1000f, 110f, 2.8f, 2.45f, 470f, 1.3f, 0f, 65.5f, 6.75f, 91.5f);
        _pineapple = new FoodItem("Pineapple", 55f, 0.5f, 10.8f, 10.77f, 1.4f, 0.32f, 0.01f, 0.01f, 0.02f, 0f, 5f, 0.09f, 0f, 25f, 0f, 0.1f, 0f, 0.08f, 0.02f, 0.2f, 12.1f, 0.01f, 174f, 14f, 0.2f, 0.08f, 14f, 0.09f, 1.4f, 50f, 0.6f, 18.9f);
        _bakedBeans= new FoodItem("Backed Beans", 88f, 5.1f, 9.6f, 5f, 6.6f, 0.38f, 0.08f, 0.05f, 0.25f, 0f, 0f, 0.12f, 0f, 0f, 0f, 0.6f, 0f, 0.07f, 0.05f, 0.5f, 29f, 1.2f, 300f, 31f, 1.4f, 0.7f, 91f, 0.21f, 0.6f, 0f, 3f, 45f);
        _kidneyBeans = new FoodItem("Kidney Beans", 312f, 18.9f, 45.6f, 3.2f, 17.8f, 1.6f, 0.2f, 0.1f, 0.9f, 0f, 1.1f, 0.42f, 0f, 0f, 0f, 0.34f, 170f, 0.35f, 0.14f, 2f, 140f, 0.02f, 1327f, 131f, 5f, 2f, 477f, 0.5f, 1.9f, 273.1f, 8.8f, 77.3f);
        _slicedTomatoes = new FoodItem("Sliced Tomatoes, canned", 21f, 1.2f, 3f, 2.3f, 0.9f, 0.24f, 0.06f, 0.04f, 0.15f, 0f, 29.3f, 0.11f, 0f, 11.3f, 0f, 0.74f, 0f, 0.045f, 0.06f, 0.58f, 24f, 0.3575f, 188f, 11f, 0.97f, 0.3f, 19f, 0.11f, 0f, 0.6f, 0.1f, 31f);
        _mincedBeef = new FoodItem("Minced Beef", 163f, 19.5f, 0f, 0f, 0f, 7.97f, 3.6f, 3.65f, 0.22f, 0.21f, 6.9f, 0.27f, 2.32f, 0f, 0.51f, 0.48f, 0f, 0.047f, 0.17f, 4.32f, 9.6f, 0.2f, 312f, 19.7f, 2.26f, 4.34f, 177f, 0.073f, 0.8f, 0f, 6.8f, 7.1f);
        _basmatiRice = new FoodItem("Basmati Rice", 354f, 8.4f, 78.1f, 0f, 0.7f, 0.98f, 0.27f, 0.29f, 0.42f, 0f, 0f, 0.11f, 0f, 0f, 0f, 0.05f, 0f, 0.07f, 0.04f, 1.4f, 31f, 0f, 150f, 35f, 1.2f, 1.7f, 130f, 0.2f, 2.2f, 35.6f, 6f, 52.7f);
        _cremeFraiche = new FoodItem("Creme Fraiche", 192f, 2.8f, 2.8f, 2.84f, 0f, 17.56f, 12.43f, 3.73f, 0.47f, 0.73f, 125.8f, 0.03f, 0.2f, 0.1f, 0.22f, 0.26f, 0f, 0.032f, 0.18f, 0.07f, 10f, 0.075f, 122.5f, 9.9f, 0.02f, 0.34f, 77f, 0.015f, 10.1f, 0.9f, 2.2f, 98.4f);
        _onion = new FoodItem("Onion", 43f, 1.2f, 5.4f, 5.36f, 1.9f, 0.07f, 0.02f, 0.01f, 0.04f, 0f, 2.5f, 0.17f, 0f, 8.1f, 0f, 0.06f, 0f, 0.038f, 0.01f, 0.19f, 36f, 0.006752632f, 186.1f, 9.2f, 0.28f, 0.19f, 31f, 0.037f, 0.2f, 2.9f, 0.13f, 23.3f);
        _garlic = new FoodItem("Garlic", 158f, 6.4f, 30.9f, 0f, 2.1f, 0.35f, 0.09f, 0.01f, 0.25f, 0f, 0f, 1.24f, 0f, 8.2f, 0f, 0.01f, 0f, 0.2f, 0.11f, 0.7f, 103f, 0.0425f, 401f, 25f, 1.7f, 1.16f, 160f, 0.299f, 0.2f, 0f, 2f, 20.6f);
        _risottoRice = new FoodItem("Risotto Rice", 362f, 6.8f, 76.3f, 0.2f, 2.4f, 2.24f, 0.54f, 0.97f, 0.96f, 0f, 0f, 0.51f, 0f, 0f, 0f, 0f, 0f, 0.413f, 0.04f, 0f, 20f, 0f, 268f, 143f, 1.8f, 2.02f, 307f, 0f, 2.1f, 0f, 0f, 12.1f);
        _chickenBreastFilet = new FoodItem("Chicken Breast Fillet", 149f, 21.5f, 0f, 0f, 0f, 6.31f, 1.96f, 2.88f, 1.48f, 0f, 24f, 0.53f, 0.34f, 1f, 1.5f, 0.5f, 0f, 0.06f, 0.09f, 9.9f, 4f, 0.2f, 220f, 25f, 0.9f, 0.8f, 198f, 0f, 0f, 0.3f, 10f, 11f);
        _champignon = new FoodItem("Champignon", 23f, 1.6f, 0.1f, 0.08f, 0.8f, 0.1f, 0.03f, 0f, 0.07f, 0f, 0f, 0.07f, 0f, 0f, 0f, 0f, 0f, 0.041f, 0.35f, 3.01f, 23.1f, 0f, 348.8f, 9.2f, 0.16f, 0.36f, 85f, 0.2f, 0f, 0.3f, 16.8f, 3.1f);
        _whiteWine = new FoodItem("White Wine", 83f, 0.1f, 2.6f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0.05f, 0f, 0f, 0f, 0f, 0f, 0.005f, 0.02f, 0.11f, 1f, 0.0125f, 71f, 10f, 0.27f, 0.12f, 18f, 0.004f, 0.3f, 0f, 0.1f, 9f);
        _eggplant = new FoodItem("Eggplant", 17f, 0.9f, 2.4f, 2.15f, 2.2f, 0f, 0f, 0f, 0f, 0f, 2.6f, 0.05f, 0f, 1.3f, 0f, 0f, 0.6f, 0.02f, 0.03f, 0.36f, 6.1f, 0.003333333f, 217.5f, 13.6f, 0.17f, 0.14f, 25f, 0.046f, 0f, 0.4f, 0f, 10.5f);
        _parmesanCheese = new FoodItem("Parmesan Cheese", 357f, 33.6f, 0.1f, 0.05f, 0f, 22.77f, 15.61f, 6.49f, 0.68f, 0f, 213.8f, 0.09f, 1.6f, 0f, 0.24f, 0.61f, 0f, 0.039f, 0.33f, 0.27f, 7f, 2.495f, 92f, 44f, 0.82f, 2.75f, 930f, 0.03f, 13.8f, 4f, 11f, 1180f);
        _vegetableBouillion = new FoodItem("Vegetable Bouillion", 4f, 0.3f, 0.3f, 0f, 0f, 0.21f, 0.05f, 0.08f, 0.07f, 0f, 0f, 0f, 0f, 0f, 0.01f, 0.01f, 0f, 0.002f, 0.01f, 0.04f, 1f, 0.7825f, 6f, 2f, 0.02f, 0.01f, 3f, 0.01f, 0.3f, 0f, 0.5f, 6f);
        _steak = new FoodItem("Steak", 265f, 18.2f, 0f, 0f, 0f, 20.02f, 8.99f, 10.17f, 0.86f, 0f, 25f, 0.4f, 1.4f, 0f, 0.8f, 0.66f, 0f, 0.039f, 0.16f, 5.3f, 5f, 0.1f, 293f, 19f, 1.9f, 3.9f, 170f, 0.062f, 1.1f, 0.7f, 6.5f, 4f);
        _potatoes = new FoodItem("Potatoes", 77f, 2f, 17.9f, 1.24f, 1.4f, 0.23f, 0.06f, 0.01f, 0.16f, 0f, 0.8f, 0.2f, 0f, 26.4f, 0f, 0.1f, 16f, 0.055f, 0.06f, 1.6f, 36f, 0.0175f, 413.5f, 20.4f, 1.04f, 0.3f, 55f, 0.052f, 1.2f, 5.8f, 0.27f, 6.8f);
        _salami = new FoodItem("Salami",  509f, 13.9f, 2.8f, 0f, 0f, 45.27f, 18.07f, 21.65f, 5.39f, 0f, 0f, 0.15f, 1.3f, 0f, 0.47f, 0.02f, 0f, 0.14f, 0.18f, 2.3f, 3f, 4.9689903f, 203.6f, 10f, 1.1f, 1.67f, 119f, 0.11f, 4.1f, 24f, 5f, 11.1f);
        _mozzarella = new FoodItem("Mozzarella", 326f, 24.1f, 0f, 0f, 0f, 23.72f, 16.25f, 6.76f, 0.71f, 0f, 222.2f, 0.09f, 1.38f, 0.5f, 0.25f, 0.64f, 0f, 0.05f, 0.33f, 0.1f, 60f, 1.395f, 65f, 27f, 0.11f, 3.2f, 390f, 0.086f, 10.4f, 1.4f, 8f, 720f);
        _tunaWater = new FoodItem("Canned tuna in water", 107f, 23.9f, 0f, 0f, 0f, 0.96f, 0.23f, 0.11f, 0.31f, 0f, 5.1f, 0.25f, 3.4f, 0f, 2.81f, 0.6f, 0f, 0f, 0.06f, 11.75f, 15f, 0.68625f, 224.5f, 27.1f, 1f, 0.58f, 164f, 0.047f, 15.6f, 0.8f, 82f, 8.9f);
        _toast = new FoodItem("Fine-grained toast bread", 255f, 7.9f, 47f, 0f, 3.3f, 2.55f, 0.79f, 0.96f, 0.8f, 0f, 0f, 0.06f, 0f, 0f, 0f, 0.5f, 0f, 0.161f, 0.05f, 1.18f, 25f, 1.1346428f, 110.6f, 17.8f, 0.96f, 0.58f, 80f, 0.102f, 18.6f, 3.2f, 1.57f, 46.2f);
        _gherkins = new FoodItem("Gherkins",76f, 0.5f, 17f, 12.5f, 0.5f, 0.4f, 0.03f, 0f, 0.04f, 0f, 0.6f, 0.01f, 0f, 4f, 0f, 0.1f, 0f, 0.01f, 0.02f, 0.04f, 8f, 0.7275f, 99f, 6.5f, 0.55f, 0.18f, 15f, 0.057f, 1f, 0.6f, 0.03f, 15.3f);
        _liverpate = new FoodItem("Liverpate",238f, 11.3f, 4.7f, 3.7f, 0.4f, 17.36f, 6.9f, 8.2f, 2.26f, 0f, 3950f, 0.21f, 9.9f, 29f, 0f, 0.26f, 10f, 0.13f, 1.02f, 4.4f, 170f, 1.8f, 170f, 12.5f, 5.57f, 2.5f, 164f, 0.41f, 3.1f, 1.2f, 19.2f, 26f);
        _mayonaise = new FoodItem("Mayonaise",730f, 1.1f, 0.1f, 0.1f, 0f, 76f, 6.88f, 43.88f, 25.23f, 0f, 60f, 0.1f, 1f, 0f, 1f, 7.6f, 75f, 0.008f, 0.03f, 0.1f, 14f, 1.5f, 34f, 7f, 0.3f, 0.4f, 59f, 0.03f, 6f, 0f, 1.6f, 8f);
        _mackarelInTomatoes = new FoodItem("Mackerel in tomatoes", 155f, 11.7f, 3.9f, 0f, 0f, 9.57f, 2.21f, 4.1f, 3f, 0f, 29.1f, 0.17f, 5.85f, 0f, 2.6f, 1.52f, 0f, 0.045f, 0.17f, 4.75f, 19f, 0.9f, 357f, 23.1f, 0.7f, 0.59f, 122f, 0.107f, 12.4f, 2.6f, 19.5f, 16f);
        _avocado = new FoodItem("Avocado", 155f, 1.6f, 1.2f, 1.14f, 5.5f, 12.53f, 2.77f, 7.69f, 1.74f, 0f, 5.6f, 0.14f, 0f, 2.9f, 0f, 1.94f, 13.7f, 0.044f, 0.11f, 1.22f, 99.3f, 0.006f, 433.8f, 28.9f, 0.47f, 0.54f, 45f, 0.253f, 0f, 59.7f, 0f, 14.6f);
        _smokedSalmon = new FoodItem("Smoked Salmon", 170f, 20.9f, 1.6f, 0f, 0f, 7.04f, 1.1f, 3.7f, 2.02f, 0f, 6.5f, 0.66f, 4.6f, 0f, 3.1f, 2.24f, 0f, 0.26f, 0.08f, 7.5f, 0f, 3f, 408f, 28.2f, 0.22f, 0.34f, 248f, 0.036f, 5.9f, 0.8f, 17.33f, 5.9f);
        _ryebread = new FoodItem("Ryebread", 170f, 20.9f, 1.6f, 0f, 0f, 7.04f, 1.1f, 3.7f, 2.02f, 0f, 6.5f, 0.66f, 4.6f, 0f, 3.1f, 2.24f, 0f, 0.26f, 0.08f, 7.5f, 0f, 3f, 408f, 28.2f, 0.22f, 0.34f, 248f, 0.036f, 5.9f, 0.8f, 17.33f, 5.9f);
        _fishFillets = new FoodItem("Fish Fillet", 283f, 12.7f, 22.9f, 0f, 0f, 14.1f, 3.04f, 7.4f, 3.59f, 0f, 0f, 0.08f, 0.74f, 0f, 1.65f, 3.8f, 0f, 0.13f, 0.07f, 1.53f, 0f, 1.1f, 185f, 21.5f, 0.58f, 0.42f, 134f, 0.067f, 15.6f, 3.4f, 20f, 48.5f);
        _frenchFries = new FoodItem("French Fries",311f, 3.7f, 39f, 0f, 3.2f, 14.07f, 3.65f, 7.49f, 2.88f, 0.05f, 0f, 0.18f, 0f, 23.3f, 0f, 0.1f, 0f, 0.128f, 0.06f, 1.2f, 10f, 0.9f, 587.4f, 32.7f, 0.79f, 0.51f, 120f, 0.171f, 0f, 41f, 2.3f, 13.2f);
        _kebab = new FoodItem("Durum with kebab, salad and dressing", 225f, 12.6f, 20.3f, 0f, 2.1f, 8.56f, 2.69f, 4f, 1.53f, 0.32f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0.098f, 0.1f, 0f, 0f, 1.3f, 286.3f, 20.6f, 1.25f, 2.6f, 134f, 0.08f, 0f, 0f, 0f, 19f);
        _cucumber = new FoodItem("Cucumber", 12f, 0.7f, 1.6f, 1.56f, 0.8f, 0.02f, 0.01f, 0f, 0.01f, 0f, 5.2f, 0.04f, 0f, 10.4f, 0f, 0.05f, 16.4f, 0.015f, 0.01f, 0.18f, 17.6f, 0.006015893f, 147.4f, 9.5f, 0.2f, 0.13f, 27f, 0.027f, 0.5f, 0.6f, 0.03f, 17.7f);
        _tomato = new FoodItem("Tomato",  20f, 0.7f, 3.2f, 3.18f, 1.9f, 0.24f, 0.06f, 0.04f, 0.15f, 0f, 82.7f, 0.09f, 0f, 15f, 0f, 1.1f, 7.9f, 0.043f, 0.02f, 0.73f, 31f, 0.01625f, 216f, 6.5f, 0.24f, 0.09f, 27f, 0.043f, 0f, 0.7f, 0.3f, 7.4f);
        _icebergSalad = new FoodItem("Iceberg Salad", 16f, 0.8f, 2.1f, 2.15f, 1.1f, 0.07f, 0.01f, 0f, 0.05f, 0f, 12.5f, 0.04f, 0f, 5.5f, 0f, 0.15f, 112f, 0.044f, 0.03f, 0.23f, 89f, 0.007075f, 186f, 7.3f, 0.27f, 0.16f, 22f, 0.012f, 1f, 0.3f, 0f, 15.5f);
        _lasagnaSheets = new FoodItem("Lasagna Sheets",128f, 5f, 26.9f, 0f, 2f, 0.28f, 0.06f, 0.04f, 0.18f, 0f, 0f, 0.01f, 0f, 0f, 0f, 0.07f, 0f, 0.04f, 0.01f, 0.3f, 2f, 0f, 78f, 24f, 0.6f, 0.7f, 50f, 0.2f, 1f, 0f, 1.8f, 13f);
        _lambChop = new FoodItem("Lamb chop", 128f, 19.6f, 0f, 0f, 0f, 4.38f, 2.21f, 1.65f, 0.15f, 0.21f, 45f, 0.2f, 1.2f, 0f, 0.4f, 0.7f, 0f, 0.18f, 0.31f, 4.3f, 1.4f, 0.195f, 350f, 27f, 2.2f, 3.3f, 210f, 0.122f, 0.7f, 2.1f, 6.05f, 12.8f);
        _tortilla = new FoodItem("Tortillas", 267f, 9.6f, 39.1f, 0f, 5.3f, 4.91f, 1.31f, 1.55f, 2.05f, 0f, 0f, 0.08f, 0.12f, 0f, 0f, 0.5f, 0f, 0.218f, 0.08f, 1.12f, 26f, 1.1f, 194.8f, 43.4f, 1.5f, 1.14f, 147f, 0.205f, 16.6f, 14.2f, 3.57f, 33.2f);
        _sugar = new FoodItem("Sugar", 399f, 0.5f, 99.3f, 99.3f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 89f, 15f, 0.9f, 0.1f, 20f, 0.06f, 0f, 0f, 0f, 53f);
        _pepper = new FoodItem("Pepper",304f, 11f, 44.5f, 0f, 26.5f, 3.12f, 1.39f, 0.74f, 1f, 0f, 9.5f, 0.34f, 0f, 21f, 0f, 0.72f, 0f, 0.109f, 0.24f, 1.14f, 10f, 0.11f, 1259f, 194f, 28.86f, 1.42f, 173f, 1.13f, 0f, 0f, 3.1f, 437f);
        _salt = new FoodItem("Salt", 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 97.125f, 0f, 290f, 0.2f, 0.08f, 8f, 0.16f, 1556.4f, 3f, 0.5f, 29f);
        _oliveOil = new FoodItem("Olive oil", 900f, 0f, 0f, 0f, 0f, 95.7f, 12.24f, 74.73f, 8.73f, 0f, 0f, 0f, 0f, 0f, 0f, 5.1f, 0f, 0f, 0f, 0f, 0f, 0f, 1f, 0f, 0.56f, 0f, 0f, 0f, 0f, 5.5f, 0f, 1f);
        _coriander = new FoodItem("Coriander",  346f, 12.4f, 13.1f, 0f, 41.9f, 16.31f, 0.94f, 13.62f, 1.75f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0.239f, 0.29f, 2.13f, 0f, 0.0875f, 1267f, 330f, 16.32f, 4.7f, 409f, 0f, 2.3f, 0f, 26.2f, 709f);
        _chili = new FoodItem("Chili", 44f, 2f, 7.7f, 0f, 1.8f, 0.14f, 0.02f, 0.01f, 0.11f, 0f, 365.8f, 0.28f, 0f, 166f, 0f, 2.9f, 0f, 0.09f, 0.09f, 0.9f, 23f, 0.0175f, 340f, 25f, 1.2f, 0.3f, 68f, 0.174f, 0f, 0f, 0.5f, 14f);
        _cumin = new FoodItem("Cumin", 428f, 17.8f, 30.6f, 2.25f, 10.5f, 0f, 0f, 0f, 0f, 0f, 63.5f, 0.44f, 0f, 7.7f, 0f, 3.33f, 5.4f, 0.628f, 0.33f, 4.58f, 10f, 0.4f, 1788f, 366f, 66.36f, 4.8f, 499f, 0.867f, 0f, 0f, 5.2f, 931f);
        _ginger = new FoodItem("Ginger", 83f, 1.8f, 16.7f, 0f, 1f, 0.53f, 0.21f, 0.15f, 0.16f, 0f, 0f, 0.16f, 0f, 5f, 0f, 0f, 0f, 0.025f, 0.03f, 0.75f, 11f, 0.0325f, 415f, 43f, 0.6f, 0.34f, 34f, 0.226f, 0.5f, 0f, 1f, 16f);
        _greekYogurt = new FoodItem("Greek yogurt", 117f, 4.9f, 6.2f, 3.75f, 0f, 7.12f, 4.66f, 1.61f, 0.14f, 0.56f, 58.5f, 0.05f, 0.33f, 0f, 0.13f, 0.39f, 0f, 0.032f, 0.16f, 0.09f, 0f, 0.2f, 150f, 10.4f, 0.03f, 0.44f, 109f, 0.005f, 14.4f, 0.5f, 2.16f, 113.5f);
        _pasta = new FoodItem("Pasta",128f, 5f, 26.9f, 0f, 2f, 0.28f, 0.06f, 0.04f, 0.18f, 0f, 0f, 0.01f, 0f, 0f, 0f, 0.07f, 0f, 0.04f, 0.01f, 0.3f, 2f, 0f, 78f, 24f, 0.6f, 0.7f, 50f, 0.2f, 1f, 0f, 1.8f, 13f);
        _cream = new FoodItem("Whipped Cream", 360f, 2.1f, 3f, 2.98f, 0f, 35.93f, 24.27f, 8.59f, 1.04f, 1.54f, 346.1f, 0.02f, 0.44f, 0.8f, 0.15f, 0.87f, 0f, 0.031f, 0.16f, 0.07f, 10.8f, 0.1f, 94.1f, 6.8f, 0.05f, 0.24f, 57f, 0.022f, 10.1f, 0.2f, 1.47f, 66.8f);
        
        var list = new List<FoodItem>();
        list.Add(_bacon);
        list.Add(_buns);
        list.Add(_butter);
        list.Add(_strawberryJam);
        list.Add(_raspberryJam);
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
        list.Add(_tunaWater);
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
        _context.FoodItems.AddRange(list);

    }
    private void InitializeRecipes()
    {
        _eggsAndBacon = new Recipe("Eggs and Bacon", true, "", "", 1, new List<Category>{_meat, _highProtein}, true, false, false, false);
        _bunsWithStrawberryJam = new Recipe("Buns with Strawberry Jam", true, "", "", 1, new List<Category>{}, true, false, false, true);
        _bunsWithRaspberryJam = new Recipe("Buns with Raspberry Jam", true, "", "", 1, new List<Category>{}, true, false, false, true);
        _bunsWithCheese = new Recipe("Buns with Cheese", true, "", "", 1, new List<Category>{}, true, false, false, true);
        _bunsWithPeanutbutter = new Recipe("Buns with Peanutbutter", true, "", "", 1, new List<Category>{}, true, false, false, true);
        _bunsWithMapleSyrup = new Recipe("Buns with Maple Syrup", true, "", "", 1, new List<Category>{}, true, false, false, true);
        _bunswithSpreadChocolate = new Recipe("Buns with Chocolate Spread", true, "", "", 1, new List<Category>{}, true, false, false, true);
        _QuinoaBowl = new Recipe("Quinoa Bowl", true, "", "", 1, new List<Category>{}, true, false, false, true);
        _OatMealBowl = new Recipe("Oatmeal Bowl", true, "", "", 1, new List<Category>{}, true, false, false, true);
        _OatMealBoiled = new Recipe("Oatmeal Boiled", true, "", "", 1, new List<Category>{}, true, false, false, true);
        _BreadAndSalmon = new Recipe("Bread and Salmon", true, "", "", 1, new List<Category>{}, true, false, false, true);
        _BreadAndHam = new Recipe("Bread and Ham", true, "", "", 1, new List<Category>{}, true, false, false, true);
        _EggsBaconAndSausages = new Recipe("Eggs with Bacon and Sausages", true, "", "", 1, new List<Category>{}, true, false, false, false);
        _FruitBowl = new Recipe("Fruit bowl", true, "", "", 1, new List<Category>{}, true, false, false, true);
        _Cereal = new Recipe("Cornflakes", true, "", "", 1, new List<Category>{}, true, false, false, false);
        _pancakes = new Recipe("Pancakes", true, "", "", 1, new List<Category>{}, true, false, false, true);
        
        //Lunch 
        _durumKebab = new Recipe("Durum with Kebab", true, "", "", 1, new List<Category>{}, false, true, true, false);
        _durumChicken = new Recipe("Durum with Chicken", true, "", "", 1, new List<Category>{}, false, true, true, false);
        _durumMix = new Recipe("Durum with Mix", true, "", "", 1, new List<Category>{}, false, true, true, false);
        _mixBox = new Recipe("Mix Box", true, "", "", 1, new List<Category>{}, false, true, true, false);
        _kebabBox = new Recipe("Kebab Box", true, "", "", 1, new List<Category>{}, false, true, true, false);
        _chickenBox = new Recipe("Chicken Box", true, "", "", 1, new List<Category>{}, false, true, true, false);
        _fishFillet = new Recipe("Fish fillets", true, "", "", 1, new List<Category>{}, false, true, false, false);
        _ryebreadWithSalmon = new Recipe("Ryebread with Salmon", true, "", "", 1, new List<Category>{}, false, true, false, true);
        _ryebreadWithAvocado = new Recipe("Ryebread with Avocado", true, "", "", 1, new List<Category>{}, false, true, false, true);
        _ryebreadPlatte = new Recipe("Ryebread Platte", true, "", "", 1, new List<Category>{}, false, true, false, true);
        _grilledToastWithTunaAndPesto = new Recipe("Grilled Toast with Tuna and Pesto", true, "", "", 1, new List<Category>{}, false, true, false, true);
        _grilledToastWithHamAndCheese = new Recipe("Grilled Toast with Ham and Cheese", true, "", "", 1, new List<Category>{}, false, true, false, true);
        _paninoWithTomatoMozzarellaAndSalami = new Recipe("Panino with Tomato Mozzarella and Salami", true, "", "", 1, new List<Category>{}, false, true, false, true);
        _hamAndCheeseSandwich = new Recipe("Ham and Cheese Sandwich", true, "", "", 1, new List<Category>{}, false, true, false, true);

        /*

        */
        
        //Dinner
        _risotto = new Recipe("Risotto", true, "Risotto", "Risotto",1, new List<Category>{},false, false, true, false);
        _pastaCarbonara = new Recipe("Pasta Carbonara", true, "Pasta Carbonara", "Pasta Carbonara", 1, new List<Category>{}, false, false, true, false);
        _butterChicken = new Recipe("Butter Chicken", true, "Butter Chicken", "Butter Chicken", 1, new List<Category>{}, false, false, true, false);
        _chiliConCarne = new Recipe("Chili Con Carne", true, "Chili Con Carne", "Chili Con Carne", 1, new List<Category>{}, false, false, true, false);
        _beefBernaise = new Recipe("Beef Bernaise", true, "Beef Bernaise", "Beef Bernaise", 1, new List<Category>{},false, false, true, false);
        _pastaPuttanesca = new Recipe("Pasta Putanesca", true, "Pasta Putanesca", "Pasta Putanesca", 1, new List<Category>{}, false, false, true, false);
        _pastaTuna = new Recipe("Pasta Tuna", true, "Pasta Tuna", "Pasta Tuna", 1, new List<Category>{}, false, false, true, false);
        _tortillas = new Recipe("Tortillas", true, "Tortillas", "Tortillas", 1, new List<Category>{}, false, false, true, false);
        _meatballsCurry = new Recipe("Meatballs Curry", true, "Meatballs Curry", "Meatballs Curry", 1, new List<Category>{}, false, false, true, false);
        _steakWithPepperSauce = new Recipe("Steak with Pepper Sauce", true, "Steak with Pepper Sauce", "", 1, new List<Category>{}, false, false, true, false);
        _steakWithWhiskeySauce = new Recipe("Steak with Whiskey Sauce", true, "Steak with Whiskey Sauce", "", 1, new List<Category>{}, false, false, true, false);
        _tacos = new Recipe("Tacos", true, "Tacos", "Tacos", 1, new List<Category>{}, false, false, true, false);
        _honeyGarlicFriedPorkChops = new Recipe("Honey-garlic fried pork chops", true,"Honey-garlic fried pork chops", "Honey-garlic fried pork chops", 1, new List<Category>{},false, false, true, false);
        _chickenFriedRice = new Recipe("Chicken Fried Rice", true, "Chicken stir fry", "Chicken stir fry", 1, new List<Category>{}, false, false, true, false);
        _shrimpStirFry = new Recipe("Shrimp stir fry", true, "Shrimp stir fry", "Shrimp stir fry", 1, new List<Category>{},false, false, true, false);
        _springRolls = new Recipe("Spring rolls", true, "Spring rolls", "Spring rolls",1,new List<Category>{},false, false, true, false);
        _bakedSalmonAndAsparagus = new Recipe("Baked salmon and asparagus", true,"Baked salmon and asparagus", "Baked salmon and asparagus",1, new List<Category>{}, false, false, true, false);
        _meatLasagna = new Recipe("Meat lasagna", true, "Meat lasagna", "Meat lasagna", 1, new List<Category>{}, false, false, true, false);
        _veganLasagna = new Recipe("Vegan lasagna", true, "Vegan lasagna", "Vegan lasagna", 1, new List<Category>{}, false, false, true, false);
        _lambChopWithPotatoes = new Recipe("Lamb chop with potatoes", true, "Lamb chop with potatoes", "Lamb chop with potatoes", 1, new List<Category>{}, false, false, true, false);
        _frikadellerWithRice = new Recipe ("Frikadeller with rice", true, "Frikadeller with rice", "Frikadeller with rice", 1, new List<Category>{}, false, false, true, false);
        

        _cashewPeanutNutMix = new Recipe("Cashew Peanut Mix", true, "", "", 1, new List<Category>{}, false, false, false, true);
        _pistachioNutMix = new Recipe("Pistachio Mix", true, "", "", 1, new List<Category>{}, false, false, false, true);
        _almondRaisinMix = new Recipe("Alomnd Raisin Mix", true, "", "", 1, new List<Category>{}, false, false, false, true);
        _pineappleOrangefruitMix = new Recipe("Pineapple Orange Mix", true, "", "", 1, new List<Category>{}, false, false, false, true);
        _appleOrangePearfruitMix = new Recipe("Apple Orange Pear Mix", true, "", "", 1, new List<Category>{}, false, false, false, true);
        _raspberryBlueberryfruitMix = new Recipe("Raspberry Blueberry Mix", true, "", "", 1, new List<Category>{}, false, false, false, true);
        _blackberryStrawberryfruitMix = new Recipe("Blackberry Strawberry Mix", true, "", "", 1, new List<Category>{}, false, false, false, true);
        _strawberryBananaSmoothie = new Recipe("Strawberry Banana Smoothie", true, "", "", 1, new List<Category>{}, false, false, false, true);
        _mangoSmoothie = new Recipe("Mango smoothie", true, "", "", 1, new List<Category>{}, false, false, false, true);
        _blueberrySmoothie = new Recipe("Blueberry smoothie", true, "", "", 1, new List<Category>{}, false, false, false, true);
        _appleCarrotOrangeSmoothie = new Recipe("Apple Carrot Orange smoothie", true, "", "", 1, new List<Category>{}, false, false, false, true);
        _peanutbutterSandwich = new Recipe("Peanutbutter sandwich", true, "", "", 1, new List<Category>{}, false, false, false, true);
        _salamiSticks = new Recipe("Salami sticks", true, "", "", 1, new List<Category>{}, false, false, false, true);
        _salamiSticksOlivesAndCheese = new Recipe("Salami sticks olives and cheese", true, "", "", 1, new List<Category>{}, false, false, false, true);
        _crackersWithPesti = new Recipe("Crackers with pesti", true, "", "", 1, new List<Category>{}, false, false, false, true);
    }

    private void AddFoodItemsToRecipes()
    {

    }


    //TESTS
}