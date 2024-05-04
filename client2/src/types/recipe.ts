export type Recipe = {
    id: number;
    authorId: number;
    authorName: string;
    title: string;
    isPublic: boolean;
    description: string;
    method: string;
    categoryIDs: number[];
    foodItemIDs: number[];
    isBreakfast: boolean;
    isLunch: boolean;
    isDinner: boolean;
    isSnack: boolean;
}

export type ThumbnailRecipe = {
    id: number;
    authorId: number;
    title: string;
    description: string;
    picture: string;
    accessiblity: "public" | "private" | "onlyBuds";
}