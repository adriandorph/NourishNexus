import { useState } from 'react';
import { Ingredient } from '../../../types/ingredient';
import IngredientComponent from '../../Ingredient/Ingredient/IngredientComponent';
import './IngredientsMethod.scss';
import NutritionDetails from '../../NutritionDetails/NutritionDetails';
import { Nutrient } from '../../../types/nutrient';
import NNTextArea from '../../NNTextArea/NNTextArea';
import NNButton from '../../NNButton';
import global from '../../../global';
import { useNavigate } from 'react-router';
import removeSVG from '../../../assets/remove.svg';
import IconButton from '../../IconButton/IconButton';

interface IngredientsMethodProps {
    ingredients: Ingredient[];
    method: string;
    isEditing: boolean;
    onChangeMethod: (method: string) => void;
    onRemoveIngredient: (ingredient: Ingredient) => void;
}

const IngredientsMethod = ({ ingredients, method, isEditing, onChangeMethod, onRemoveIngredient }: IngredientsMethodProps) => {
    const [selectedTab, setSelectedTab] = useState('ingredients');

    const navigate = useNavigate();

    const tabSelectClass = (tab: string): string => {
        return tab === selectedTab ? 'tab-selected selected' : 'tab-selected';
    }

    const totalNutrients = (ingredients: Ingredient[]): Nutrient[] => {
        const sumNutrients = new Map<string, Nutrient>();
        ingredients.forEach((ingredient) => {
            ingredient.nutrients.forEach((nutrient) => {
                const newNutrient = sumNutrients.get(nutrient.nutrientType) 
                || {nutrientType: nutrient.nutrientType, amount: 0, unit: nutrient.unit};
                newNutrient.amount += nutrient.amount;
                sumNutrients.set(nutrient.nutrientType, newNutrient);
            });
        });
        return Array.from(sumNutrients.entries()).map(([_, value]) => value);
    }

    const handleAddIngredient = () => {
        //TODO
        //Save edited recipe to sessionStorage
        //Redirect to ingredients page
        navigate('/ingredients');
    }

    return (
        <div className='ingredients-method-container'>
            <div className='ingredients-method-navigator'>
                <div className='navigator-tab' onClick={() => setSelectedTab("ingredients")}>
                    <div className='tab-label'>Ingredients</div>
                    <div className={tabSelectClass('ingredients')}></div>
                </div>
                <div className='navigator-tab' onClick={() => setSelectedTab("method")}>
                    <div className='tab-label'>Method</div>
                    <div className={tabSelectClass('method')}></div>
                </div>
            </div>
            <div className='ingredients-method-text'>
                {selectedTab !== 'ingredients' && isEditing !== true && method}
                {selectedTab !== 'ingredients' && isEditing && <NNTextArea value={method} onChange={onChangeMethod} />}
                {selectedTab === 'ingredients' && 
                    <div className='ingredients'>
                        <div className='ingredients-ingredients'>
                            {ingredients.map((ingredient, index) => 
                            <div className='ingredient-row' key={index}>
                                {isEditing && <IconButton src={removeSVG} alt={'remove'} onClick={() => {onRemoveIngredient(ingredient)}} />}
                                <IngredientComponent key={index} ingredient={ingredient} />
                            </div>
                            )}
                        </div>
                        {isEditing &&
                            <div className='add-ingredient-button'>
                                <NNButton 
                                    text={'+ Add Ingredient'} 
                                    color={global.secondaryColor} 
                                    textColor={global.textColor} 
                                    sizePX={12}
                                    onClick={handleAddIngredient} />
                            </div> 
                        }
                        <div className='total-nutrition-details'>
                            <div className='total-nutrition-details-label'>Total Nutrition</div>
                            <NutritionDetails nutrients={totalNutrients(ingredients)} enableHideNutrients={true}/>
                        </div>
                    </div>
                }
            </div>
        </div>
    )
}

export default IngredientsMethod;