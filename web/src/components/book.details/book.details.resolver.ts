import { Injectable } from "@angular/core";
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { BookInterface } from "./book.interface";
import { BookService } from "../../services/book/book.service";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class BookDetailsResolver implements Resolve<BookInterface> {
    constructor(private bookService: BookService) {
    }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<BookInterface> {
        return this.bookService.findById(route.params['id']);
    }
}