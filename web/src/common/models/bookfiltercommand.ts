import { HttpParams } from "@angular/common/http";
import { CommandService } from "../services/command/command.service";

export class BookFilterCommand {
    public title: string;
    public author: string;
    public minPrice: number;
    public maxPrice: number;
    public rating: number;
    public minReleaseDate: Date;
    public maxReleaseDate: Date;
    public genre: string;
    public topSeller: boolean
}