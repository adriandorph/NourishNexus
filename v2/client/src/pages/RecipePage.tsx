import '../styles/RecipePage.scss'


function RecipePage() {

    const recipe = {
        id: 1,
        authorId: 1,
        title: "A very great and nice pasta recipe with vodka and pasta and tomato sauce and parmesan cheese and some green stuff on top. This is a very traditional very authentic pasta with some vodka. It is very Italian and it has a lot of nice italian flavor buildt into it but sadly it is not very healthy. It is very good though and I hope you enjoy it. It is very easy to make and it",
        description: "A simple pasta recipe",
        method: "Take the vodka and splash it into the tomato sauce and katapult it into the pasta.",
        picture: "vodka-pasta.jpg.webp",
        accessiblity: "public"
    }

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
            <div className='first-row'></div>
            <div className='second-row'></div>
            <div className='description-container'>
                <div className='description-label'>Description</div>
                <div className='description-divider'></div>
                <div className='description'></div>
            </div>
            <div className='ingredients-method-container'>

            </div>
        </div>
    </div>
    )
}

export default RecipePage