import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthorService } from '../../services/author/author.service';
import { Observable } from 'rxjs';
import { Author } from './author.model';

@Injectable({
    providedIn: 'root'
})
export class AuthorResolver implements Resolve<Author> {
    constructor(private authorService: AuthorService) {
    }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<Author> {
        return this.authorService.findById(route.params['id']);
    }
}