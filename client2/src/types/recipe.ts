export type Recipe = {
    id: number;
    authorId: number;
    authorName: string;
    title: string;
    isPublic: boolean;
    description: string;
    method: string;
    categoryIDs: number[];
    isBreakfast: boolean;
    isLunch: boolean;
    isDinner: boolean;
    isSnack: boolean;
}