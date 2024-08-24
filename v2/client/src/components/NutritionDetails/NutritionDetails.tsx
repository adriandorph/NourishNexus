import { useState } from 'react';
import { Nutrient } from '../../types/nutrient';
import SearchBar from '../SearchBar';
import './NutritionDetails.scss'

export interface NutritionDetailsProps {
    nutrients: Nutrient[];
    enableHideNutrients?: boolean;
}

function NutritionDetails({ nutrients, enableHideNutrients }: NutritionDetailsProps) {
    const [nutrientSearchResults, setNutrientSearchResults] = useState<Nutrient[]>(nutrients);
    const [showAllNutrients, setShowAllNutrients] = useState<boolean>(enableHideNutrients !== true);

    const getNutrientAmount = (nutrientType: string): string => {
        return nutrients.find((nutrient) => nutrient.nutrientType === nutrientType)?.amount.toString() || '?';
    }

    const handleSearch = (query: string) => {
        const newSearchResults = nutrients.filter((nutrient) => nutrient.nutrientType.toLowerCase().includes(query.toLowerCase()));
        setNutrientSearchResults(newSearchResults);
    }

    const toggleShowAllNutrients = () => {
        setShowAllNutrients(!showAllNutrients);
    }

    return (
        <div className='nutrition-details-container'>
            <div className='nutrition-details-line'/>
            <div className='nutrition-details-pinned'>
                <div className='nutrition-details-pinned-item'>
                    <div className='nutrition-details-pinned-item-amount'>{getNutrientAmount('energy')} kcal</div>
                    <div className='nutrition-details-pinned-item-nutrient-type'>Energy</div>
                </div>
                <div className='nutrition-details-pinned-item'>
                    <div className='nutrition-details-pinned-item-amount'>{getNutrientAmount('protein')}g</div>
                    <div className='nutrition-details-pinned-item-nutrient-type'>Protein</div>
                </div>
                <div className='nutrition-details-pinned-item'>
                    <div className='nutrition-details-pinned-item-amount'>{getNutrientAmount('fat')}g</div>
                    <div className='nutrition-details-pinned-item-nutrient-type'>Fat</div>
                </div>
                <div className='nutrition-details-pinned-item'>
                    <div className='nutrition-details-pinned-item-amount'>{getNutrientAmount('carbs')}g</div>
                    <div className='nutrition-details-pinned-item-nutrient-type'>Carbs</div>
                </div>
            </div>
            <div className='nutrition-details-all-nutrients'>
                {enableHideNutrients && showAllNutrients && <div className='nutrition-details-show-hide' onClick={toggleShowAllNutrients}>hide</div>}
                {enableHideNutrients && !showAllNutrients && <div className='nutrition-details-show-hide' onClick={toggleShowAllNutrients}>show all nutrients</div>}
                {enableHideNutrients !== true && <div className='nutrition-details-space'/>}
                {showAllNutrients && <SearchBar placeholder='Search for nutrients...' onChange={handleSearch}/>}
                <div className='nutrition-details-nutrient-list'>
                    {showAllNutrients && nutrientSearchResults.map((nutrient) => (
                        <div className='nutrition-details-nutrient' key={nutrient.nutrientType}>
                            <div className='nutrition-details-nutrient-type'>{nutrient.nutrientType}</div>
                            <div className='nutrition-details-nutrient-amount'>{nutrient.amount}{nutrient.unit}</div>
                        </div>
                    ))}
                </div>
                
            </div>
        </div>
    );
}
export default NutritionDetails;