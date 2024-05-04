import { ThumbnailRecipe } from "../types/recipe";
import RecipeCardSmall from "./RecipeCardSmall";
import ArrowButton from "./ArrowButton";
import "../styles/HorizontalFoodRoll.scss";
import { useEffect, useRef, useState } from "react";
import { User } from "../types/user";

export interface HorizontalFoodRollProps {
    recipes: ThumbnailRecipe[];
    authors: User[];
    title: string;
    autoRoll?: boolean;
    id: string;
}

function HorizontalFoodRoll({recipes, title, autoRoll, id, authors}: HorizontalFoodRollProps) {
    const [enableScroll, setEnableScroll] = useState(true);
    const enableScrollRef = useRef(enableScroll);

    const [scrollDirection, setScrollDirection] = useState(1);
    const scrollDirectionRef = useRef(scrollDirection);

    useEffect(() => {
        enableScrollRef.current = enableScroll; // Update the ref value
    }, [enableScroll]);

    useEffect(() => {
        scrollDirectionRef.current = scrollDirection; // Update the ref value
    }, [scrollDirection]);

    useEffect(() => {
        if (autoRoll) {
            const interval = setInterval(() => {
                    scroll();
            }, 50);

            return () => {
                clearInterval(interval);
            }
        }
    }, []);

    function incrementScrollLeft(distance: number) {
        let roll = document.querySelector(`#${id}`) as HTMLElement;
        roll.scrollLeft -= distance;

        if (roll.scrollLeft <= 0) {
            setScrollDirection(1);
        }
    }

    function incrementScrollRight(distance: number) {
        let roll = document.querySelector(`#${id}`) as HTMLElement;
        roll.scrollLeft += distance;

        if (roll.scrollLeft + roll.clientWidth + 10 >= roll.scrollWidth) {
           setScrollDirection(-1);  
        }
    }

    function smoothScroll(px: number) {
        let roll = document.querySelector(`#${id}`) as HTMLElement;
        roll.scrollBy({
            left: px,
            behavior: 'smooth'
        });
    }

    function scroll() {
        if (enableScrollRef.current) {
            if (scrollDirectionRef.current === 1) {
                incrementScrollRight(1);
            } else {
                incrementScrollLeft(1);
            }
        }
    }

    const recipeCards = recipes.map((recipe) => {
        return <RecipeCardSmall key={recipe.id} recipe={recipe} author={
            authors.find((author) => author.id === recipe.authorId) as User
        }/> // Add key prop
    });

    function isTouchDevice() {
        // Check if the user agent contains keywords indicating a touch device
        return 'ontouchstart' in window || navigator.maxTouchPoints > 0 || navigator.maxTouchPoints > 0;
    }

    const arrowsShouldShow = () => {
        if (isTouchDevice()) {
            return false;
        }
        if (recipes.length < 5) {
            return false;
        }
        return true;
    }

    const hideArrows = arrowsShouldShow() ? "" : "hide";
    const showArrows = arrowsShouldShow() ? "show" : "";

    return (
        <div className="container">
            <h2>{title}</h2>
            <div className="container-arrows"
                onMouseEnter={() => {setEnableScroll(false)}} 
                onMouseLeave={() => {setEnableScroll(true);}}
                onTouchMove={() => {setEnableScroll(false)}}
            >
                <div className={hideArrows}
                    style={{
                        marginLeft: '20px',
                    }}>
                    <ArrowButton direction="left" onClick={() => smoothScroll(-235)}/>
                </div>
                <div className={`roll ${showArrows}`} id={id} >
                    {recipeCards}
                </div>
                <div className={hideArrows}
                    style={{
                        marginRight: '20px',
                    }}>
                    <ArrowButton direction="right" onClick={() => smoothScroll(235)}/>
                </div>
            </div>
            
        </div>
    );
}

export default HorizontalFoodRoll;