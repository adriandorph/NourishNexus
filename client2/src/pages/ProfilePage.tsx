import NNButton from '../components/NNButton';
import RecipeCardBig from '../components/RecipeCardBig';
import '../styles/ProfilePage.scss'
import { ThumbnailRecipe } from '../types/recipe';
import { User } from '../types/user';
import global from '../global';

function ProfilePage() {

    interface Profile {
        recipes: ThumbnailRecipe[]
        user: User
    }

    const profile : Profile = {
        user: {
            id: 1,
            nickname: "Adrian Bay Dorph",
            profilePicture: "pb.jpeg",
            recipeIds: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13]
        },
        recipes: [
            {
                id: 1,
                authorId: 1,
                title: "A very great and nice pasta recipe with vodka and pasta and tomato sauce and parmesan cheese and some green stuff on top. This is a very traditional very authentic pasta with some vodka. It is very Italian and it has a lot of nice italian flavor buildt into it but sadly it is not very healthy. It is very good though and I hope you enjoy it. It is very easy to make and it",
                description: "A simple pasta recipe",
                picture: "vodka-pasta.jpg.webp",
                accessiblity: "public"
            },
            {
                id: 2,
                authorId: 1,
                title: "Pizza",
                description: "A very great and nice pasta recipe with vodka and pasta and tomato sauce and parmesan cheese and some green stuff on top. This is a very traditional very authentic pasta with some vodka. It is very Italian and it has a lot of nice italian flavor buildt into it but sadly it is not very healthy. It is very good though and I hope you enjoy it. It is very easy to make and it",
                picture: "",
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
                title: "A very great and nice pasta recipe with",
                description: "A very great and nice pasta recipe with vodka and pasta and tomato sauce and parmesan cheese and some green stuff on top. This is a very traditional very authentic pasta with some vodka. It is very Italian and it has a lot of nice italian flavor buildt into it but sadly it is not very healthy. It is very good though and I hope you enjoy it. It is very easy to make and it",
                picture: "",
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

    const isYou = false;

    function appostrophize(word: string) {
        return word[word.length - 1] === 's' ? `${word}'` : `${word}'s`;
    }

    const recipesLabelText = isYou ? "Your recipes" : `${appostrophize(profile.user.nickname)} recipes`;

    const hideNoRecipes = profile.recipes.length === 0 ? "hide" : "";

    const dividerButtonsYou = (
        <>
            <a href="">Buds</a>
            <a href="">Following</a>
        </>
    );

    const following = false;
    const buds = false;

    const dividerButtonsNotYou = () => (
        <>
            {!following && <NNButton text="Follow" color={global.accentColor} textColor={global.textColor} sizePX={14} onClick={() => {}} />}
            {following && <NNButton text="Following" color={global.secondaryColor} textColor={global.textColor} sizePX={14} onClick={() => {}} />}

            {!buds && <NNButton text="Add bud" color={global.accentColor} textColor={global.textColor} sizePX={14} onClick={() => {}} />}
            {buds && <NNButton text="Taste buds" color={global.secondaryColor} textColor={global.textColor} sizePX={14} onClick={() => {}} />}
        </>
    );

    return (
        <div className='centered-container'>
            <div className='top-controls-line'>
                <div className='top-controls'></div>
                <div className='top-controls'></div>
            </div>
            <div className='profile'>
                <div className='picture'>
                    <img className='profile-picture' src={profile.user.profilePicture} alt="" />
                    <div className='name'>
                        {profile.user.nickname}
                    </div>
                </div>
                <div className='divider'></div>
                <div className='divider-buttons'>
                    {isYou ? dividerButtonsYou : dividerButtonsNotYou()}
                </div>
                <div className='bio'>
                    <p>Hi, I'm Adrian! I love to cook and not bake, and I'm always looking for new recipes to try out. I hope you enjoy my recipes!</p>
                </div>
            </div>
            <div className={`recipes ${hideNoRecipes}`}>
                <div className='recipes-label'>
                    <h4>{recipesLabelText}</h4>
                </div>
                <div className='recipes-container'>
                    {profile.recipes.map((recipe) => {
                        return (
                            <RecipeCardBig key={recipe.id} recipe={recipe} author={profile.user}/>
                        );
                    })}
                </div>
                
            </div>
        </div>
    );
}

export default ProfilePage;