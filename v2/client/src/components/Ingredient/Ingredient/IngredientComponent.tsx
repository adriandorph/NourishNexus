import { Ingredient } from "../../../types/ingredient";
import { Nutrient } from "../../../types/nutrient";
import './IngredientComponent.scss';

interface IngredientProps {
    ingredient: Ingredient;
}

const findNutrientAmount = (nutrientType: string, nutrients: Nutrient[]): string => {
    for (let i = 0; i < nutrients.length; i++) {
        if (nutrients[i].nutrientType === nutrientType) {
            return nutrients[i].amount.toString();
        }
    }
    return '?';
}

const IngredientComponent = ({ ingredient }:IngredientProps) => {
    return (
        <div className='ingredient-container'>
            <div className='ingredient-name-amount'>{ingredient.name} • {ingredient.quantity}{ingredient.unit}</div>
            {ingredient.hasNutrition && <div className='ingredient-macros'>
                <div className='macro'>
                    <div className="macro-amount">{findNutrientAmount('energy', ingredient.nutrients)} kcal</div>
                    <div className="macro-name">Energy</div>
                </div>
                <div className='macro-separator'/>
                <div className='macro'>
                    <div className="macro-amount">{findNutrientAmount('protein', ingredient.nutrients)}g</div>
                    <div className="macro-name">Protein</div>
                </div>
                <div className='macro-separator'/>
                <div className='macro'>
                    <div className="macro-amount">{findNutrientAmount('carbs', ingredient.nutrients)}g</div>
                    <div className="macro-name">Carbs</div>
                </div>
                <div className='macro-separator'/>
                <div className='macro'>
                    <div className="macro-amount">{findNutrientAmount('fat', ingredient.nutrients)}g</div>
                    <div className="macro-name">Fat</div>
                </div>
            </div>}
        </div>
    )
}

export default IngredientComponent;