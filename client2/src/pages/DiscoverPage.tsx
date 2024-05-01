import { useEffect } from 'react';
import HorizontalFoodRoll from '../components/HorizontalFoodRoll';
import Logo from '../components/Logo';
import SearchBar from '../components/SearchBar';
import '../styles/DiscoverPage.scss';
import authService from '../services/authService';
import { useNavigate } from 'react-router-dom';

function DiscoverPage() {
    const navigate = useNavigate()

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
            
            <HorizontalFoodRoll />
            <HorizontalFoodRoll />
            <HorizontalFoodRoll />
        </div>
    );
}

export default DiscoverPage;