import { ThumbnailRecipe } from "../types/recipe";
import "../styles/RecipeCard.scss";
import { User } from "../types/user";
import { useNavigate } from "react-router-dom";

export interface RecipeCardSmallProps {
    recipe: ThumbnailRecipe;
    author: User;
}

function RecipeCardSmall(props: RecipeCardSmallProps) {
    const navigate = useNavigate()

    const hasImage = props.recipe.picture !== undefined || props.recipe.picture !== "";
    const footerTransparent = hasImage ? "transparent" : "not-transparent";
    
    return (
        <div className="recipe-card-small">
            <div className="card-body">
                {hasImage && <img className="card-image" src={props.recipe.picture} alt="" />} 
                {!hasImage && <div className="description">{props.recipe.description}</div>}
            </div>
            <div className={`card-footer ${footerTransparent}`}>
                <p>{props.recipe.title}</p>
                <img className="card-profile-picture" src={props.author.profilePicture} alt="" onClick={
                    () => navigate('/profile')
                }/>
            </div>
        </div>
    );
}

export default RecipeCardSmall;