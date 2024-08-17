export type Nutrient = {
    nutrientType: string;
    amount: number;
    unit: string;
}

export const exampleNutrient: Nutrient = {
    nutrientType: 'example nutrient',
    amount: 1,
    unit: 'g'
};

export const exampleNutrientEnergy: Nutrient = {
    nutrientType: 'energy',
    amount: 100,
    unit: 'kcal'
};

export const exampleNutrientProtein: Nutrient = {
    nutrientType: 'protein',
    amount: 10,
    unit: 'g'
};

export const exampleNutrientCarbs: Nutrient = {
    nutrientType: 'carbs',
    amount: 20,
    unit: 'g'
};

export const exampleNutrientFat: Nutrient = {
    nutrientType: 'fat',
    amount: 5,
    unit: 'g'
};