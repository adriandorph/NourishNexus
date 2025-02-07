import { useNavigate, useParams } from 'react-router-dom';
import IngredientsMethod from '../../components/recipe/IngredientsMethod/IngredientsMethod'
import { Recipe, exampleRecipe } from '../../types/recipe'
import { useEffect, useState } from 'react';
import NNButton from '../../components/NNButton';
import global from '../../global';
import NNTextArea from '../../components/NNTextArea/NNTextArea';
import { Ingredient } from '../../types/ingredient';
import hone from '../../assets/hone.svg';
import IconButton from '../../components/IconButton/IconButton';
import authService from '../../services/authService';
import recipeService from '../../services/recipeService';
import './RecipePage.scss'


function RecipePage() {
    //Use id to get recipe from api
    const { id } = useParams<{ id: string }>();
    const [recipe, setRecipe] = useState<Recipe | undefined>(undefined);
    //Check if user is the author of the recipe
    const [isAuthor, setIsAuthor] = useState<boolean>(false);
    const [isEditing, setIsEditing] = useState<boolean>(false);
    const [editedRecipe, setEditedRecipe] = useState<Recipe | undefined>(undefined);

    const navigate = useNavigate();

    const getRecipe: (id: string|undefined) => Promise<Recipe | undefined | 'new'> = async (id: string | undefined) => {
        if (id === 'new') {
            return 'new';
        } else if (id) {
            //Get recipe from api
            return await recipeService.getRecipe(id);
        } else 
            return undefined;
    }

    const handleSave = async () => {
        //Send edited recipe to api
        if (id === 'new' && editedRecipe) {
            setEditedRecipe(prevRecipe => {
                return {...prevRecipe, accessibility: "Public"} as Recipe
            });

            const createdRecipe = await recipeService.createRecipe(editedRecipe as Recipe);
            if (!createdRecipe) return;

            setRecipe(createdRecipe);
            navigate(`/recipe/${createdRecipe.id}`);

        } else if (editedRecipe && editedRecipe.id) {
            const updatedRecipe = await recipeService.updateRecipe(editedRecipe as Recipe);
            if (!updatedRecipe) return;
            setRecipe(updatedRecipe);
        }
        
        setIsEditing(false);
        setEditedRecipe(undefined);
        setEditedRecipeSessionStorage(undefined);
    }

    const handleCancel = () => {
        setIsEditing(false);
        setEditedRecipe(undefined);
        setEditedRecipeSessionStorage(undefined);
    }

    const handleDelete = () => {
        //Delete recipe from api
        if (recipe && recipe.id) {
            recipeService.deleteRecipe(recipe.id);
        }
        //Redirect to home
        navigate('/discover');
    }

    const onChangeDescription = (description: string) => {
        setEditedRecipe(prevRecipe => {
            if (prevRecipe) {
                const updatedRecipe = {...prevRecipe, description: description}
                setEditedRecipeSessionStorage(updatedRecipe)
                return updatedRecipe
            }
            return prevRecipe
        })
    }

    const onChangeMethod = (method: string) => {
        setEditedRecipe(prevRecipe => {
            if (prevRecipe) {
                const updatedRecipe = {...prevRecipe, method: method}
                setEditedRecipeSessionStorage(updatedRecipe)
                return updatedRecipe
            }
            return prevRecipe
        })
    }

    const onChangeTitle = (title: string) => {
        setEditedRecipe(prevRecipe => {
            if (prevRecipe) {
                const updatedRecipe = {...prevRecipe, title: title}
                setEditedRecipeSessionStorage(updatedRecipe)
                return updatedRecipe
            }
            return prevRecipe
        })
    }

    const onRemoveIngredient = (ingredient: Ingredient) => {
        setEditedRecipe(prevRecipe => {
            if (prevRecipe) {
                const updatedRecipe = {...prevRecipe, ingredients: prevRecipe.ingredients.filter(i => i !== ingredient)}
                setEditedRecipeSessionStorage(updatedRecipe);
                return updatedRecipe 
            }
            setEditedRecipeSessionStorage(prevRecipe);
            return prevRecipe
        })
    }

    const handleEdit = () => {
        setEditedRecipe(recipe);
        setIsEditing(true);
        setEditedRecipeSessionStorage(recipe);
    }

    const setEditedRecipeSessionStorage = (recipe: Recipe | undefined) => {
        if (recipe)
            sessionStorage.setItem('editedRecipe', JSON.stringify(recipe));
        else
            sessionStorage.removeItem('editedRecipe');
    }

    const getEditedRecipeFromSessionStorage = () => {
        const recipe = sessionStorage.getItem('editedRecipe');
        if (recipe)
            return JSON.parse(recipe) as Recipe;
        return undefined;
    }

    const handleAddIngredient = () => {
        //Take edited recipe and store in sessionStorage and redirect to ingredients page
        sessionStorage.setItem('editedRecipe', JSON.stringify(editedRecipe));
        //Redirect to ingredients page
        navigate(`/ingredients`);
    }

    const initialiseCreateRecipe = () => {
        const recipe = recipeService.initialRecipe();
        setRecipe(recipe);
        setEditedRecipe(recipe);
        setEditedRecipeSessionStorage(recipe);
        setIsEditing(true);
        setIsAuthor(true);
    }

    const initialiseRecipe = (recipe: Recipe) => {
        setRecipe(recipe);
        const user = authService.getAuthenticatedUser();
        if (user && user.nameidentifier === recipe.author.id) {
            setIsAuthor(true);
            const editedRecipe = getEditedRecipeFromSessionStorage();
            if (editedRecipe)
                initialiseEditing(editedRecipe);
        } else {
            //Read only
            setIsAuthor(false);
            setIsEditing(false);
            setEditedRecipe(undefined);
            setEditedRecipeSessionStorage(undefined);
        }
    }

    const initialiseEditing = (editedRecipe: Recipe) => {
        setEditedRecipe(editedRecipe);
        setEditedRecipeSessionStorage(editedRecipe);
        setIsEditing(true);
    }

    const initialiseRecipeNotFound = () => {
        setRecipe(undefined);
        setEditedRecipe(undefined);
        setEditedRecipeSessionStorage(undefined);
        setIsEditing(false);
        setIsAuthor(false);
    }

    useEffect(() => {
        //Fetch recipe from api
        const initialise = async () => {
            //Get recipe
            const recipe = await getRecipe(id);
            if (recipe === 'new') {
                initialiseCreateRecipe();
            } else if (recipe) {
                initialiseRecipe(recipe);
            } else {
                initialiseRecipe(exampleRecipe);
                //initialiseRecipeNotFound();
            }
        }
        initialise();
    }, [])

    const recipePage = recipe ? (
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
                {!recipe.picture && <img className='image' src="../recipe-placeholder.svg" alt="Recipe image"/>}
                <div className='title-container'>
                    {!isEditing && <div className='title'>{recipe.title}</div>}
                    {isAuthor && isEditing && <input className='title' value={editedRecipe?.title || ''} onChange={(e) => onChangeTitle(e.target.value)} placeholder='Title' />}
                </div>
                <div className='image-title-divider'></div>
            </div>
            <div className='details'>
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
                recipe={recipe}
                isEditing={(isAuthor && isEditing)}
                onChangeMethod={onChangeMethod}
                onRemoveIngredient={onRemoveIngredient}
                onAddIngredient={handleAddIngredient}
                />
            {isAuthor && isEditing && 
                <center>
                    <div className='recipe-save-cancel'>
                        <NNButton text='Save' onClick={handleSave} color={global.accentColor} textColor={global.textColor} sizePX={20} />
                        <NNButton text='Cancel' onClick={handleCancel} color={global.secondaryColor} textColor={global.textColor} sizePX={20} />
                    </div>
                </center>
            }
            <div className='delete'>
                {isAuthor && isEditing && recipe && recipe.id && //To check that it is not a new recipe. TODO add confirmation dialog
                    <NNButton 
                        text='Delete recipe' 
                        onClick={handleDelete} 
                        color={global.dangerColor} 
                        textColor={global.textColor} 
                        sizePX={20} 
                        />
                }
            </div>
        </div>
    </div>
    ) : undefined;

    const couldNotLoadRecipe = (
        <div>
            <div>Could not load recipe</div>
        </div>
    )

    return (
        <div>
            {recipe && recipePage}
            {!recipe && couldNotLoadRecipe}
        </div>
    )
}

export default RecipePage