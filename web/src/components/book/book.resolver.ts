import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { BookService } from '../../services/book/book.service';
import { Observable } from 'rxjs';
import { BookInterface } from '../book.details/book.interface';
import { SortCommand } from '../../common/models/sortcommand';
import { SortDirection } from '../../common/enums/sortdirection';

@Injectable({
    providedIn: 'root'
})
export class BookResolver implements Resolve<BookInterface[]> {
    private readonly sortKey: string;

    constructor(private bookService: BookService) {
    }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<BookInterface[]> {
        let sortCommand = new SortCommand();
        sortCommand.key = this.sortKey;
        sortCommand.sortBy = SortDirection.Asc;

        return this.bookService.findAll(sortCommand, null, null);
    }
}