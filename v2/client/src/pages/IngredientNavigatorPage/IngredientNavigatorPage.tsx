import { useState } from 'react';
import IngredientCard from '../../components/Ingredient/IngredientCard/IngredientCard';
import SearchBar from '../../components/SearchBar';
import { FoodItem } from '../../types/foodItem';
import foodItemSearchService from '../../services/foodItemSearchService';
import { useNavigate } from 'react-router-dom';
import './IngredientNavigatorPage.scss'

function IngredientNavigatorPage() {
    const [searchResults, setSearchResults] = useState<FoodItem[]>([]);
    const navigate = useNavigate();


    const handleSearch = async (query: string) => {
        const newSearchResults = await foodItemSearchService.search(query);
        setSearchResults(newSearchResults);
        console.log("Searching for: '", query, "' returned: ", newSearchResults);
    }

    const handleSelect = (foodItem: FoodItem) => {
        console.log('Navigate to ingredient page with id: ', foodItem.id);
        navigate('/ingredient/' + foodItem.id);
    }

    return (
        <div className='centered'>
            <div className='ingredient-navigator-container'>
                <div className='ingredient-navigator-title'>Add Ingredient</div>
                <SearchBar placeholder='Search for ingredients...' onChange={handleSearch} />
                <div className='ingredient-navigator-search-results'>
                    {searchResults.map((foodItem) => (
                        <div className='ingredient-navigator-option' onClick={() => handleSelect(foodItem)} key={foodItem.id}>
                            <IngredientCard foodItem={foodItem} />
                        </div>
                    ))}
                </div>
            </div>
        </div>
    );
}
export default IngredientNavigatorPage;