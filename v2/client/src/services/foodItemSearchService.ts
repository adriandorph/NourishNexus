import { FoodItem, exampleFoodItem } from "../types/foodItem";
//import apiClient from "./apiClient";


export interface FoodItemSearchService {
    search(query: string): Promise<FoodItem[]>;
}

async function searchImpl (query: string): Promise<FoodItem[]> {
    if (query === '') {
        return [];
    }
    // Make a request to the backend to search for food items using apiClient
    if (exampleFoodItem.name.toLowerCase().includes(query.toLowerCase())) {
        return [exampleFoodItem, exampleFoodItem, exampleFoodItem, exampleFoodItem, exampleFoodItem];
    }

    return [];
}

const foodItemSearchService : FoodItemSearchService = {
    search: searchImpl
}
export default foodItemSearchService;