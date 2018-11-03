import { Component, OnInit } from '@angular/core';
import { Book } from '../book.details/book.model';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';
import { BookService } from '../../services/book/book.service';
import { UserService } from '../core/user.service';
import { FirebaseUserModel } from '../core/user.model';

@Component({
  selector: 'app-book-rating',
  templateUrl: './book-rating.component.html',
  styleUrls: ['./book-rating.component.css']
})
export class BookRatingComponent implements OnInit {

  public book: Book;
  public bookRating: number;

  public constructor(private bookService: BookService, private route: ActivatedRoute, private location: Location,
    private userService: UserService) { 
    this.book = this.route.snapshot.data.bookResolver as Book;
  }

  public ngOnInit() {
    this.bookRating = 5;
  }

  /**
   * Returns to the previous page.
   */
  public return() {
    this.location.back();
  }

  /**
   * Submits the new rating score to the API.
   */
  public async submit() {
    let userId = await this.getUserId();

    if (!userId) {
      return;
    }

    this.bookService.updateRating(this.book.id, this.bookRating)
      .subscribe(() => {
        this.return();
      });
  }

  private getUserId() {
    return this.userService.getCurrentUser()
      .then((user: FirebaseUserModel) => {
        return user.id;
      }, () => {
        return null;
      })
  }
}
