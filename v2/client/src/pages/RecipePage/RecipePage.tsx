import { useParams } from 'react-router-dom';
import IngredientsMethod from '../../components/recipe/IngredientsMethod/IngredientsMethod'
import { Recipe, exampleRecipe } from '../../types/recipe'
import { useState } from 'react';
import NNButton from '../../components/NNButton';
import global from '../../global';
import NNTextArea from '../../components/NNTextArea/NNTextArea';
import { Ingredient } from '../../types/ingredient';
import hone from '../../assets/hone.svg';
import './RecipePage.scss'
import IconButton from '../../components/IconButton/IconButton';


function RecipePage() {
    const { id } = useParams<{ id: string }>();
    //Use id to get recipe from api
    const recipe: Recipe = exampleRecipe;
    //Check if user is the author of the recipe
    const isAuthor = true;
    const [isEditing, setIsEditing] = useState<boolean>(true); //TODO Check sessionStorage for editing status
    const [editedRecipe, setEditedRecipe] = useState<Recipe | undefined>(recipe); //TODO Check sessionStorage for edited recipe

    const handleSave = () => {
        //Send edited recipe to api
        setIsEditing(false);
        setEditedRecipe(undefined);
    }

    const handleCancel = () => {
        setIsEditing(false);
        setEditedRecipe(undefined);
    }

    const onChangeDescription = (description: string) => {
        setEditedRecipe(prevRecipe => {
            if (prevRecipe) {
                return {...prevRecipe, description: description}
            }
            return prevRecipe
        })
    }

    const onChangeMethod = (method: string) => {
        setEditedRecipe(prevRecipe => {
            if (prevRecipe) {
                return {...prevRecipe, method: method}
            }
            return prevRecipe
        })
    }

    const onRemoveIngredient = (ingredient: Ingredient) => {
        setEditedRecipe(prevRecipe => {
            if (prevRecipe) {
                return {...prevRecipe, ingredients: prevRecipe.ingredients.filter(i => i !== ingredient)}
            }
            return prevRecipe
        })
    }

    const handleEdit = () => {
        setEditedRecipe(recipe);
        setIsEditing(true);
    }

    return (
    <div className='centered-container'>
        <div className='recipe-container'>
            <div className='image-title'>
                <div className='image-top-buttons-container'>
                    <div className='image-top-buttons'>
                        <div className='image-top-buttons-cluster'>
                        </div>
                        <div className='image-top-buttons-cluster'>
                            {isAuthor && !isEditing && 
                                <IconButton 
                                    onClick={handleEdit}
                                    src={hone}
                                    alt='edit'
                                    size='30px'
                                    />
                            }
                        </div>
                    </div>
                </div>
                {recipe.picture && <img className='image' src="../vodka-pasta.jpg.webp" alt="" />}
                {!recipe.picture && <div className='image image-replacement'/>}
                <div className='title-container'>
                    <div className='title'>{recipe.title}</div>
                </div>
                <div className='image-title-divider'></div>
            </div>
            <div className='details'>
                {isAuthor && isEditing && 
                    <div className='recipe-save-cancel'>
                        <NNButton text='Save' onClick={handleSave} color={global.accentColor} textColor={global.textColor} sizePX={20} />
                        <NNButton text='Cancel' onClick={handleCancel} color={global.secondaryColor} textColor={global.textColor} sizePX={20} />
                    </div>
                }
                <div className='first-row row'></div>
                <div className='second-row row'></div>
            </div>
            <div className='description-container'>
                <div className='description-label'>Description</div>
                <div className='description-divider'></div>
                {isAuthor && isEditing && <div className='description text'>
                        <NNTextArea value={editedRecipe?.description || ''} onChange={onChangeDescription} />
                    </div>}
                {isAuthor !== true || isEditing !== true && <div className='description text'>{recipe.description}</div>}
            </div>
            <IngredientsMethod 
                ingredients={recipe.ingredients} 
                method={recipe.method} 
                isEditing={(isAuthor && isEditing)} 
                onChangeMethod={onChangeMethod}
                onRemoveIngredient={onRemoveIngredient}
                />
        </div>
    </div>
    )
}

export default RecipePage