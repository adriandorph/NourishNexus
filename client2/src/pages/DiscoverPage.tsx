import { useEffect } from 'react';
import HorizontalFoodRoll from '../components/HorizontalFoodRoll';
import Logo from '../components/Logo';
import SearchBar from '../components/SearchBar';
import '../styles/DiscoverPage.scss';
import authService from '../services/authService';
import { useNavigate } from 'react-router-dom';
import { Recipe } from '../types/recipe';

function DiscoverPage() {
    const navigate = useNavigate()

    const recipes: Recipe[] = [{
        id: 1,
        authorId: 1,
        authorName: "Adrian",
        title: "Pasta Vodka",
        isPublic: true,
        description: "A delicious pasta dish with a vodka sauce. It is so delicious and easy to make! Very good for a dinner party. Very affordable and easy to make! Very great for lunch as well!",
        method: "1. Boil water\n2. Add pasta\n3. Cook pasta\n4. Make sauce\n5. Combine pasta and sauce\n6. Serve",
        categoryIDs: [1, 2],
        isBreakfast: false,
        isLunch: true,
        isDinner: true,
        isSnack: false,
    }];

    useEffect(() => {
        if(!authService.handleAuthorization()) {
            navigate('/authenticate')
        }
    }, [])
    
    return (
        <div className='centered-container'>
            <Logo size='small' />
            <div className="search-bar-container">
                <SearchBar placeholder='Search...' onChange={() => {}}/>
            </div>
            
            <HorizontalFoodRoll recipes={recipes} title="the title"/>
        </div>
    );
}

export default DiscoverPage;