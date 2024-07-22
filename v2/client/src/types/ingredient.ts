import { Nutrient, exampleNutrient } from './nutrient';

export type Ingredient = {
    name: string;
    quantity: string;
    unit: string;
    hasNutrition: boolean;
    Nutrients: Nutrient[];
}

export const exampleIngredient: Ingredient = {
    name: 'example ingredient',
    quantity: '1',
    unit: 'unit',
    hasNutrition: true,
    Nutrients: [exampleNutrient]
};