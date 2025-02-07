import { Author, exampleAuthor } from "./author";
import { ForkCreateDTO } from "./fork";
import { Ingredient, exampleIngredient } from "./ingredient";
import { Nutrient, exampleNutrient, exampleNutrientCarbs, exampleNutrientEnergy, exampleNutrientFat, exampleNutrientProtein } from "./nutrient";

export type Recipe = {
    id: string | undefined;
    title: string;
    author: Author;
    forkedFromAuthor?: Author;
    forkedFromRecipeId?: string;
    description: string;
    picture: string;
    method: string;
    accessibility: "Public" | "Private" | "Friends" | "Followers" | "Restricted";
    ingredients: Ingredient[];
    totalNutrients: Nutrient[];
}

export type RecipeCreateDTO = {
    title: string;
    fork?: ForkCreateDTO;
    authorId: string;
    description: string;
    imageBase64: string;
    steps: string;
    accessibility: "Public" | "Private" | "Friends" | "Followers" | "Restricted"
    ingredients: Ingredient[];
}

export type RecipeUpdateDTO = {
    id: string;
    title: string;
    description: string;
    steps: string;
    imageBase64: string | undefined;
    accessibility: "Public" | "Private" | "Friends" | "Followers" | "Restricted"
    ingredients: Ingredient[];
}


export type ThumbnailRecipe = {
    id: number;
    authorId: number;
    title: string;
    description: string;
    picture: string;
    accessiblity: "public" | "private" | "onlyBuds";
}

export const exampleRecipe: Recipe = {
    id: "1",
    author: exampleAuthor,
    forkedFromAuthor: exampleAuthor,
    forkedFromRecipeId: "1",
    title: "Vodka Pasta",
    description: "A very great and nice pasta recipe with vodka and pasta and tomato sauce and parmesan cheese and some green stuff on top. This is a very traditional very authentic pasta with some vodka. It is very Italian and it has a lot of nice italian flavor buildt into it but sadly it is not very healthy. It is very good though and I hope you enjoy it. It is very easy to make and it",
    method: "Take the vodka and splash it into the tomato sauce and katapult it into the pasta.",
    picture: "vodka-pasta.jpg.webp",
    accessibility: "Public",
    ingredients: [exampleIngredient, exampleIngredient, exampleIngredient],
    totalNutrients: [exampleNutrient, exampleNutrientEnergy, exampleNutrientProtein, exampleNutrientCarbs, exampleNutrientFat]
}