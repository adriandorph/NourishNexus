import { Nutrient, exampleNutrient, exampleNutrientEnergy, exampleNutrientProtein, exampleNutrientCarbs, exampleNutrientFat} from './nutrient';

export type Ingredient = {
    name: string;
    quantity: string;
    unit: string;
    hasNutrition: boolean;
    Nutrients: Nutrient[];
}

export const exampleIngredient: Ingredient = {
    name: 'Example Ingredient',
    quantity: '400',
    unit: 'g',
    hasNutrition: true,
    Nutrients: [exampleNutrient, exampleNutrientEnergy, exampleNutrientProtein, exampleNutrientCarbs, exampleNutrientFat]
};