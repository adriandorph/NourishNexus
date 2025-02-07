import HorizontalFoodRoll from '../components/HorizontalFoodRoll';
import Logo from '../components/Logo';
import SearchBar from '../components/SearchBar';
import Footer from '../components/Footer';
import '../styles/DiscoverPage.scss';
import { useNavigate } from 'react-router-dom';
import { ThumbnailRecipe } from '../types/recipe';
import { User } from '../types/user';

function DiscoverPage() {
    const navigate = useNavigate()

    interface Discover {
        recipes: ThumbnailRecipe[]
        users: User[]
    }

    const discover: Discover = {
        users: [{
            id: 1,
            nickname: "Adrian Bay Dorph",
            profilePicture: "pb.jpeg",
            recipeIds: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13]
        }],
        recipes: [
            {
                id: 1,
                authorId: 1,
                title: "Pasta",
                description: "A simple pasta recipe",
                picture: "vodka-pasta.jpg.webp",
                accessiblity: "public"
            },
            {
                id: 2,
                authorId: 1,
                title: "Pizza",
                description: "A simple pizza recipe",
                picture: "vodka-pasta.jpg.webp",
                accessiblity: "public"
            },
            {
                id: 3,
                authorId: 1,
                title: "Vodka Pasta",
                description: "A very great and nice pasta recipe with vodka and pasta and tomato sauce and parmesan cheese and some green stuff on top",
                picture: "vodka-pasta.jpg.webp",
                accessiblity: "public"
            },
            {
                id: 4,
                authorId: 1,
                title: "Vodka Pasta",
                description: "A very great and nice pasta recipe with vodka and pasta and tomato sauce and parmesan cheese and some green stuff on top",
                picture: "vodka-pasta.jpg.webp",
                accessiblity: "public"
            },
            {
                id: 5,
                authorId: 1,
                title: "Vodka Pasta",
                description: "A very great and nice pasta recipe with vodka and pasta and tomato sauce and parmesan cheese and some green stuff on top",
                picture: "vodka-pasta.jpg.webp",
                accessiblity: "public"
            },
            {
                id: 6,
                authorId: 1,
                title: "Vodka Pasta",
                description: "A very great and nice pasta recipe with vodka and pasta and tomato sauce and parmesan cheese and some green stuff on top",
                picture: "vodka-pasta.jpg.webp",
                accessiblity: "public"
            },
            {
                id: 7,
                authorId: 1,
                title: "Vodka Pasta",
                description: "A very great and nice pasta recipe with vodka and pasta and tomato sauce and parmesan cheese and some green stuff on top",
                picture: "vodka-pasta.jpg.webp",
                accessiblity: "public"
            },
            {
                id: 8,
                authorId: 1,
                title: "Vodka Pasta",
                description: "A very great and nice pasta recipe with vodka and pasta and tomato sauce and parmesan cheese and some green stuff on top",
                picture: "vodka-pasta.jpg.webp",
                accessiblity: "public"
            },
            {
                id: 9,
                authorId: 1,
                title: "Vodka Pasta",
                description: "A very great and nice pasta recipe with vodka and pasta and tomato sauce and parmesan cheese and some green stuff on top",
                picture: "vodka-pasta.jpg.webp",
                accessiblity: "public"
            },
            {
                id: 10,
                authorId: 1,
                title: "Vodka Pasta",
                description: "A very great and nice pasta recipe with vodka and pasta and tomato sauce and parmesan cheese and some green stuff on top",
                picture: "vodka-pasta.jpg.webp",
                accessiblity: "public"
            },
            {
                id: 11,
                authorId: 1,
                title: "Vodka Pasta",
                description: "A very great and nice pasta recipe with vodka and pasta and tomato sauce and parmesan cheese and some green stuff on top",
                picture: "vodka-pasta.jpg.webp",
                accessiblity: "public"
            },
            {
                id: 12,
                authorId: 1,
                title: "Vodka Pasta",
                description: "A very great and nice pasta recipe with vodka and pasta and tomato sauce and parmesan cheese and some green stuff on top",
                picture: "vodka-pasta.jpg.webp",
                accessiblity: "public"
            },
            {
                id: 13,
                authorId: 1,
                title: "Vodka Pasta",
                description: "A very great and nice pasta recipe with vodka and pasta and tomato sauce and parmesan cheese and some green stuff on top",
                picture: "vodka-pasta.jpg.webp",
                accessiblity: "public"
            }
        ]
    }
    
    return (
        <>
        <div className='centered-container'>
            <Logo size='small' />
            <div className="search-bar-container">
                <SearchBar placeholder='Search...' onChange={() => {}}/>
            </div>
            
            <HorizontalFoodRoll id='discover' recipes={discover.recipes} authors={discover.users} title="Discover" autoRoll={true}/>
            <HorizontalFoodRoll id='follows' recipes={discover.recipes} authors={discover.users} title="From cooks you follow"/>
            <HorizontalFoodRoll id='saved' recipes={discover.recipes} authors={discover.users} title="Saved recipes"/>
        </div>
        <Footer />
        </>
    );
}

export default DiscoverPage;