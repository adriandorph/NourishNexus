import { Nutrient, exampleNutrient, exampleNutrientEnergy, exampleNutrientProtein, exampleNutrientCarbs, exampleNutrientFat} from './nutrient';

export type Ingredient = {
    name: string;
    quantity: string;
    unit: string;
    hasNutrition: boolean;
    nutrients: Nutrient[];
}

export const exampleIngredient: Ingredient = {
    name: 'Example Ingredient',
    quantity: '400',
    unit: 'g',
    hasNutrition: true,
    nutrients: [exampleNutrient, exampleNutrientEnergy, exampleNutrientProtein, exampleNutrientCarbs, exampleNutrientFat]
};