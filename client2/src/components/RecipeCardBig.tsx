import { ThumbnailRecipe } from "../types/recipe";
import { User } from "../types/user";
import '../styles/RecipeCard.scss';
import { useNavigate } from "react-router-dom";
import NNButton from "./NNButton";
import global from "../global";

export interface RecipeCardProps {
    recipe: ThumbnailRecipe;
    author: User;
}

function RecipeCardBig(props: RecipeCardProps) {
    const navigate = useNavigate()

    const hasImage = props.recipe.picture !== undefined && props.recipe.picture !== "";
    const footerTransparent = hasImage ? "transparent" : "not-transparent";

    const isYours = false;

    const buttonText = () => {
        if (isYours) {
            if (props.recipe.accessiblity === "public") {
                return "Public"
            } else  if (props.recipe.accessiblity === "private"){
                return "Private"
            } else {
                return "Friends"
            }
        }
        if (true) { //If you have saved the recipe
            return "Save"
        } else {
            return "Saved"
        }
    }

    const buttonColor = () => {
        return buttonText() === "Save" ? global.accentColor : global.secondaryColor;
    }

    const descriptionSize = props.recipe.title.length > 38 ? "medium" : "big";

    return (
        <div className="recipe-card-big">
            <div className="card-body">
                {hasImage && <img className="card-image" src={props.recipe.picture} alt="" />} 
                {!hasImage && <div className={`description ${descriptionSize}`}>{props.recipe.description}</div>}
            </div>
            <div className={`card-footer-big ${footerTransparent}`}>
                <div className="recipe-name">{props.recipe.title}</div>
                <div className="footer-footer">
                    <NNButton 
                        onClick={function (): void {
                        } } 
                        text={buttonText()} 
                        color={buttonColor()} 
                        textColor={global.textColor} 
                        sizePX={14}/>
                    <div className="card-profile">
                        <img className="card-profile-picture" src={props.author.profilePicture} alt="" onClick={
                            () => navigate('/profile')
                        }/>
                        <div className="author-name">{props.author.nickname}</div>
                    </div>
                </div>
                
            </div>
        </div>
    );
}

export default RecipeCardBig;