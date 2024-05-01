import { Recipe } from "../types/recipe";
import RecipeCardSmall from "./RecipeCardSmall";

export interface HorizontalFoodRollProps {
    recipes: Recipe[];
    title: string;
}

function HorizontalFoodRoll({recipes, title}: HorizontalFoodRollProps) {

    const recipeCards = recipes.map((recipe) => {
        return <RecipeCardSmall recipe={recipe}/>
    });

    return (// Display a list of RecipeCardSmall horizontally
        <div className="container">
            <h2>{title}</h2>
            <div className="roll">
                {recipeCards}
            </div>
        </div>
    );
}

export default HorizontalFoodRoll;