export interface BookInterface {
    id: string;
    title: string;
    author: string;
    authorId: string;
    descriptionId: string;
    price: number;
    rating: number;
    releaseDate: Date;
    genre: string;
    topSeller: boolean;
    imageUrl: string;
}