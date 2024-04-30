import HorizontalFoodRoll from '../components/HorizontalFoodRoll';
import Logo from '../components/Logo';
import SearchBar from '../components/SearchBar';
import '../styles/DiscoverPage.scss';

function DiscoverPage() {
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