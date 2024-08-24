import { useParams } from 'react-router-dom';

import './IngredientPage.scss'
import NutritionDetails from '../../components/NutritionDetails/NutritionDetails';
import { exampleFoodItem } from '../../types/foodItem';

function IngredientPage() {
    const { id } = useParams<{ id: string }>();
    console.log('Ingredient Page for id: ', id);

    /*
    Check if there is a recipe object in sessionStorage, if so add an add to recipe button
    */


    return (
        <div className='centered'>
            <div className='ingredient-page-container'>
                <div className='ingredient-page-name'>{exampleFoodItem.name}</div>
                <div className='ingredient-page-nutrition-details'>
                    <div className='ingredient-page-control-row'>
                        <div className='ingredient-page-source'>{exampleFoodItem.source}</div>
                        <div className='ingredient-page-amount-controls'>
                            controls
                        </div>
                    </div>
                    <NutritionDetails nutrients={exampleFoodItem.nutrients}/>
                </div>
            </div>
        </div>
    );
}
export default IngredientPage;