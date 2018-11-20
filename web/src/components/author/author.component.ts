import { Component, OnInit } from '@angular/core';
import { Author } from './author.model';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-author',
  templateUrl: './author.component.html',
  styleUrls: ['./author.component.css']
})
export class AuthorComponent implements OnInit {
  private author: Author;

  public constructor(private route: ActivatedRoute) { 
    this.author = this.route.snapshot.data.authorResolver as Author
  }

  public ngOnInit() {
  }

  /**
   * Puts the author's first and last names together
   * separated by a single space.
   */
  public getAuthorFullName() {
    return this.author ? `${this.author.firstName} ${this.author.lastName}` : null;
  }
}
