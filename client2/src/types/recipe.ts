export type Recipe = {
    id: number;
    authorId: number;
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