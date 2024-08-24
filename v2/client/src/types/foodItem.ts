import { exampleNutrient, exampleNutrientCarbs, exampleNutrientEnergy, exampleNutrientFat, exampleNutrientProtein, Nutrient } from "./nutrient";
import { UnitConversion } from "./unitConversion";

export type FoodItem = {
    id: number;
    name: string;
    verified: boolean;
    source: string;
    authorId: string;
    description: string;
    hasNutrition: boolean;
    nutrients: Nutrient[];
    unitConversions: UnitConversion[];
}

export const exampleFoodItem: FoodItem = {
    id: 1,
    name: 'Example Ingredient',
    verified: false,
    source: 'Example Source',
    authorId: 'author123123ID',
    description: 'This is the description of the example food item',
    hasNutrition: true,
    nutrients: [exampleNutrient, exampleNutrientEnergy, exampleNutrientProtein, exampleNutrientCarbs, exampleNutrientFat],
    unitConversions: [
        // Conversion from grams to other units
        {
            unit: 'g',
            multiplier: 1
        },
        {
            unit: 'kg',
            multiplier: 1000
        },
        {
            unit: 'lb',
            multiplier: 220.462
        }
    ]
};