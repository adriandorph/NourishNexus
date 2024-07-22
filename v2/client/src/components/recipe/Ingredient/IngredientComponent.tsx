import { Ingredient } from "../../../types/ingredient";
import './IngredientComponent.scss';

interface IngredientProps {
    ingredient: Ingredient;
}

const IngredientComponent = ({ ingredient }:IngredientProps) => {
    //TODO: Make
    return (
        <div className='ingredient-container'>
            {ingredient.name}
        </div>
    )
}

export default IngredientComponent;