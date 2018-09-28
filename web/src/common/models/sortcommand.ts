import { SortDirection } from "../enums/sortdirection";
import { HttpParams } from "@angular/common/http";

export class SortCommand {
    public key: string;
    public sortBy: SortDirection

    public constructor() {
        this.sortBy = SortDirection.Asc;
    }
}