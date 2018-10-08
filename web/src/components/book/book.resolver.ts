import { Injectable } from "@angular/core";
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { BookService } from "../../services/book/book.service";
import { Observable } from "rxjs";
import { BookInterface } from "./book.interface";

@Injectable({
    providedIn: 'root'
})
export class BookResolver implements Resolve<BookInterface[]> {
    constructor(private bookService: BookService) {
    }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<BookInterface[]> {
        return this.bookService.findAll(null, null, null);
    }
}