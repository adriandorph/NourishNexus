import IngredientsMethod from '../components/recipe/IngredientsMethod/IngredientsMethod'
import '../styles/RecipePage.scss'
import { Recipe, exampleRecipe } from '../types/recipe'


function RecipePage() {

    const recipe: Recipe = exampleRecipe;

    return (
    <div className='centered-container'>
        <div className='recipe-container'>
            <div className='image-title'>
                {recipe.picture && <img className='image' src="vodka-pasta.jpg.webp" alt="" />}
                {!recipe.picture && <div className='image image-replacement'/>}
                <div className='title-container'>
                    <div className='title'>{recipe.title}</div>
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
                <div className='description text'>{recipe.description}</div>
            </div>
            <IngredientsMethod ingredients={recipe.ingredietns} method={recipe.method} />
        </div>
    </div>
    )
}

export default RecipePage