import { Recipe } from "../types/recipe";
import RecipeCardSmall from "./RecipeCardSmall";
import "../styles/HorizontalFoodRoll.scss";
import { useEffect, useRef, useState } from "react";

export interface HorizontalFoodRollProps {
    recipes: Recipe[];
    title: string;
    autoRoll?: boolean;
}

function HorizontalFoodRoll({recipes, title, autoRoll}: HorizontalFoodRollProps) {
    const [enableScroll, setEnableScroll] = useState(true);
    const enableScrollRef = useRef(enableScroll);

    useEffect(() => {
        enableScrollRef.current = enableScroll; // Update the ref value
    }, [enableScroll]);

    useEffect(() => {
        if (autoRoll) {
        let timeoutId: NodeJS.Timeout | undefined;

        const handleInfiniteScroll = async () => {
            try {
                timeoutId = await infiniteScroll(1);
            } catch (error) {
                console.error("Error occurred during infinite scroll:", error);
            }
        };

        handleInfiniteScroll();

        return () => {
            if (timeoutId) {
                clearTimeout(timeoutId); // Cleanup: Clear the timeout
            }
        };
    }
    }, []);

    async function incrementScrollLeft(distance: number) {
        let roll = document.querySelector('.roll') as HTMLElement;
        roll.scrollLeft -= distance;

        if (roll.scrollLeft <= 0) {
            return infiniteScroll(1);    
        } else {
            return infiniteScroll(-1);
        }
    }

    async function incrementScrollRight(distance: number) {
        let roll = document.querySelector('.roll') as HTMLElement;
        roll.scrollLeft += distance;

        if (roll.scrollLeft + roll.clientWidth + 10 >= roll.scrollWidth) {
            return infiniteScroll(-1);    
        } else {
            return infiniteScroll(1);
        }
    }

    async function infiniteScroll(scrollDirection: number = 1) {
        if (enableScrollRef.current) {
            return setTimeout(() => {
                if (scrollDirection === 1) {
                    incrementScrollRight(1);
                } else {
                    incrementScrollLeft(1);
                }
            }, 50);
        }
    }

    const recipeCards = recipes.map((recipe) => {
        return <RecipeCardSmall key={recipe.id} recipe={recipe}/> // Add key prop
    });

    return (
        <div className="container">
            <h2>{title}</h2>
            <div 
                className="roll" 
                onMouseEnter={() => {setEnableScroll(false)}} 
                onMouseLeave={() => {
                    setTimeout(() => {
                        setEnableScroll(true);
                    }, 2000);
                }}
                >
                {recipeCards}
            </div>
        </div>
    );
}

export default HorizontalFoodRoll;