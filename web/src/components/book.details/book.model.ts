import { BookInterface } from "./book.interface";

export class Book implements BookInterface {
    id: string;
    title: string;
    author: string;
    price: number;
    rating: number;
    releaseDate: Date;
    genre: string;
    topSeller: boolean;
    imageUrl: string;
}