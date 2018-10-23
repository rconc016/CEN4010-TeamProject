import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { BookService } from '../../services/book/book.service';
import { Observable } from 'rxjs';
import { BookInterface } from '../book.details/book.interface';
import { SortCommand } from '../../common/models/sortcommand';
import { SortDirection } from '../../common/enums/sortdirection';
import { PageCommand } from '../../common/models/pagecommand';
import { BookComponent } from './book.component';

@Injectable({
    providedIn: 'root'
})
export class BookResolver implements Resolve<BookInterface[]> {
    private readonly sortKey = 'title';

    constructor(private bookService: BookService) {
    }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<BookInterface[]> {
        let sortCommand = new SortCommand();
        sortCommand.key = this.sortKey;
        sortCommand.sortBy = SortDirection.Asc;

        let pageCommand = new PageCommand();
        pageCommand.limit = BookComponent.numberOfBooksPerPage;

        return this.bookService.findAll(sortCommand, null, pageCommand);
    }
}