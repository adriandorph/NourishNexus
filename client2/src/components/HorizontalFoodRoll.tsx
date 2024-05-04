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

    const [scrollDirection, setScrollDirection] = useState(1);
    const scrollDirectionRef = useRef(scrollDirection);

    useEffect(() => {
        enableScrollRef.current = enableScroll; // Update the ref value
    }, [enableScroll]);

    useEffect(() => {
        scrollDirectionRef.current = scrollDirection; // Update the ref value
    }, [scrollDirection]);

    useEffect(() => {
        setInterval(() => {
            if (autoRoll) {
                scroll();
            }
        }, 50);
    }, []);

    async function incrementScrollLeft(distance: number) {
        let roll = document.querySelector('.roll') as HTMLElement;
        roll.scrollLeft -= distance;

        if (roll.scrollLeft <= 0) {
            setScrollDirection(1);
        }
    }

    async function incrementScrollRight(distance: number) {
        let roll = document.querySelector('.roll') as HTMLElement;
        roll.scrollLeft += distance;

        if (roll.scrollLeft + roll.clientWidth + 10 >= roll.scrollWidth) {
           setScrollDirection(-1);  
        }
    }

    async function scroll() {
        if (enableScrollRef.current) {
            if (scrollDirectionRef.current === 1) {
                incrementScrollRight(1);
            } else {
                incrementScrollLeft(1);
            }
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
                    setEnableScroll(true);
                }}
                >
                {recipeCards}
            </div>
        </div>
    );
}

export default HorizontalFoodRoll;