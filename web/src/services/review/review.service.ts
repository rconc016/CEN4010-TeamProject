import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Review } from '../../components/reviews/review.model';

@Injectable({
  providedIn: 'root'
})
export class ReviewService {
  public readonly url = `${environment.apiUrl}/review`;

  public constructor(private http: HttpClient) { 
  }

  /**
   * Retrieves all the reviews associated with the given book ID.
   * @param bookId The ID of the book to look for.
   */
  public findAllByBookId(bookId: string) {
    return this.http.get<Review[]>(`${this.url}/${bookId}`);
  }

  /**
   * Creates a new review.
   * @param review The review data to store.
   */
  public create(review: Review) {
    return this.http.post(this.url, review);
  }
}
