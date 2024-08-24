import { FoodItem } from "../../../types/foodItem";
import { Nutrient } from "../../../types/nutrient";
import IngredientComponent from "../Ingredient/IngredientComponent";
import './IngredientCard.scss';

export interface IngredientCardProps {
    foodItem: FoodItem;
}


function IngredientCard({ foodItem }: IngredientCardProps) {

    const nutrients100g: Nutrient[] = foodItem.nutrients.map((nutrient) => {
        return {
            nutrientType: nutrient.nutrientType,
            amount: nutrient.amount * 100,
            unit: nutrient.unit
        }
    })

    return (
        <div className="ingredient-card">
            <IngredientComponent ingredient={{
                    name: foodItem.name,
                    quantity: '100',
                    unit: 'g',
                    hasNutrition: foodItem.hasNutrition,
                    nutrients: nutrients100g
                }} />
        </div>
    );
}
export default IngredientCard;