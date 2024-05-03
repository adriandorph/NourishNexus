import { Recipe } from "../types/recipe";
import "../styles/RecipeCardSmall.scss";

export interface RecipeCardSmallProps {
    recipe: Recipe;
}

function RecipeCardSmall(props: RecipeCardSmallProps) {

    const content = (recipe: Recipe) => {
        if (false){ //if recipe has an image
            return (
                <>
                <div className="card-body">
                    <img className="card-image" src="vodka-pasta.jpg.webp" alt="" />
                </div>
                <div className="card-footer transparent">
                    <p>{recipe.title}</p>
                    <img className="card-profile-picture" src="pb.jpeg" alt="" />
                </div>
                </>
            );
        } else {
            return (
                <>
                <div className="card-body">
                    <div className="description">
                        {props.recipe.description}
                    </div>
                </div>
                <div className="card-footer not-transparent">
                    <p>{recipe.title}</p>
                    <img className="card-profile-picture" src="pb.jpeg" alt="" />
                </div>
                </>
            );
        }
    }
    return (
        <div className="recipe-card-small">
            {content(props.recipe)}
        </div>
    );
}

export default RecipeCardSmall;