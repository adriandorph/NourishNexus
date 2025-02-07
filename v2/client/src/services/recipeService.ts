import { ForkCreateDTO } from "../types/fork";
import { Recipe, RecipeCreateDTO, RecipeUpdateDTO } from "../types/recipe";
import apiClient from "./apiClient";
import authService from "./authService";

export interface RecipeService {
    initialRecipe: () => Recipe | undefined;
    getRecipe: (id: string) => Promise<Recipe | undefined>;
    createRecipe: (recipe: Recipe) => Promise<Recipe | undefined>;
    updateRecipe: (recipe: Recipe) => Promise<Recipe | undefined>;
    deleteRecipe: (id: string) => Promise<void>;
    recipeToCreateDTO: (recipe: Recipe) => RecipeCreateDTO | undefined;
}

async function getRecipe(id: string) {
    return await apiClient.get(`${import.meta.env.VITE_API_URL}/recipe/${id}`).then((res) => {
        if (res && res.status === 200) {
            return recipeDTOToRecipe(res.data);
        } else {
            return undefined;
        }
    });
}

async function createRecipe(recipe: Recipe) {
    const createRecipe = recipeToCreateDTO(recipe);
    return await apiClient.post(`${import.meta.env.VITE_API_URL}/recipe`, createRecipe).then((res) => {
        if (res && res.status === 200) {
            return recipeDTOToRecipe(res.data);
        } else {
            return undefined;
        }
    });
}

async function updateRecipe(recipe: Recipe) {
    const updateRecipe = recipeToUpdateDTO(recipe);
    if (!updateRecipe) return undefined;
    return await apiClient.put(`${import.meta.env.VITE_API_URL}/recipe`, updateRecipe).then((res) => {
        if (res && res.status === 200) {
            return recipeDTOToRecipe(res.data);
        } else {
            return undefined;
        }
    });
}

async function deleteRecipe(id: string) {
    await apiClient.delete(`${import.meta.env.VITE_API_URL}/recipe/${id}`);
}

function initialRecipe(): Recipe | undefined {
    const authUser = authService.getAuthenticatedUser();
    if (authUser && authUser.nameidentifier && authUser.name) {
        const recipe: Recipe = {
            id: undefined,
            title: '',
            author: {
                id: authUser.nameidentifier,
                name: authUser.name,
                picture: ''
            }, //Find author
            description: '',
            picture: '',
            method: '',
            accessibility: 'Public',
            ingredients: [],
            totalNutrients: []
        }
        return recipe;
    }
    return undefined;
}

function recipeToCreateDTO(recipe: Recipe, fork?: ForkCreateDTO): RecipeCreateDTO | undefined {
    const authorId = authService.getAuthenticatedUser()?.nameidentifier;
    if (!authorId) return undefined;
    
    return {
        title: recipe.title,
        fork: fork,
        authorId: authorId,
        description: recipe.description,
        imageBase64: recipe.picture,
        steps: recipe.method,
        accessibility: recipe.accessibility,
        ingredients: recipe.ingredients
    }
}

function recipeDTOToRecipe(recipe: any): Recipe {
    return {
        id: recipe.id,
        title: recipe.title,
        author: recipe.author,
        forkedFromAuthor: undefined,
        forkedFromRecipeId: undefined,
        description: recipe.description,
        picture: recipe.imageBase64,
        method: recipe.steps,
        accessibility: recipe.accessibility,
        ingredients: recipe.ingredients,
        totalNutrients: recipe.totalNutrients
    }
}

function recipeToUpdateDTO(recipe: Recipe): RecipeUpdateDTO | undefined {
    if (!recipe || !recipe.id) return undefined;
    return {
        id: recipe.id,
        title: recipe.title,
        description: recipe.description,
        steps: recipe.method,
        imageBase64: recipe.picture,
        accessibility: recipe.accessibility,
        ingredients: recipe.ingredients
    }
}


const recipeService: RecipeService = {
    getRecipe: getRecipe,
    createRecipe: createRecipe,
    updateRecipe: updateRecipe,
    deleteRecipe: deleteRecipe,
    initialRecipe: initialRecipe,
    recipeToCreateDTO: recipeToCreateDTO
}
export default recipeService;