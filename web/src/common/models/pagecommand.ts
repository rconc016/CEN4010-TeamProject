import { HttpParams } from "@angular/common/http";
import { CommandService } from "../services/command/command.service";

export class PageCommand {
    public limit: number;
    public offset: number;
}