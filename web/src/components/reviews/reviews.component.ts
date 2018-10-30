import { Component, OnInit, Input} from '@angular/core';
import { ReviewService } from '../../services/review/review.service';
import { Review } from './review.model';
import { UserService } from '../core/user.service';
import { FirebaseUserModel } from '../core/user.model';
import { StringUtils } from '../../common/utils/stringutils';

@Component({
  selector: 'app-reviews',
  templateUrl: './reviews.component.html',
  styleUrls: ['./reviews.component.css']
})
export class ReviewsComponent implements OnInit {
  private static readonly anonymousUser = 'Hidden';

  @Input() public bookId: string;
  public reviews: Review[];
  public reviewComment: string;
  public useNickname: boolean;
  public isAnonymous: boolean;

  private canSubmitReview: boolean;

  public constructor(private reviewService: ReviewService, private userService: UserService) {
    this.reviews = [];
  }
  
  public ngOnInit() {
    this.reviewComment = '';
    this.useNickname = true;
    this.isAnonymous = false;
    this.canSubmitReview = true;

    this.loadData();
  }

  /**
   * Loads all the reviews.
   */
  private loadData() {
    if (this.bookId) {
      this.reviewService.findAllByBookId(this.bookId)
        .subscribe((result: Review[]) => {
          this.reviews = result;

          for (let review of this.reviews) {
            let userId = review.userId;

            this.userService.getUser(userId)
              .subscribe((user: FirebaseUserModel) => {
                review.username = review.useNickname ? user.nickname : `${user.firstName} ${user.lastName}`;
                review.username = review.anonymous ? ReviewsComponent.anonymousUser : review.username;
              });
          }
        });
    }
  }

  /**
   * Creates a new review for the given book.
   */
  public async submitReview() {
    if (this.canSubmitReview) {
      this.canSubmitReview = false;

      let review         = new Review();
      review.bookId      = this.bookId;
      review.userId      = await this.getUserId();
      review.date        = new Date();
      review.comment     = this.reviewComment;
      review.useNickname = this.useNickname;
      review.anonymous   = this.isAnonymous;

      if (this.isReviewValid(review)) {
        this.reviewService.create(review)
          .subscribe(() => {
            window.location.reload();
          });
      }

      else {
        window.location.reload();
        console.error("Error: Failed to create review.");
      }
    }
  }

  /**
   * Retrives the ID of the current user.
   */
  private getUserId() {
    return this.userService.getCurrentUser()
      .then((user: FirebaseUserModel) => {
        return user.id;
      }, () => {
        return null;
      })
  }

  /**
   * Checks if the given review is valid for submission.
   * @param review The review to be validated.
   */
  private isReviewValid(review: Review) {
    let isBookIdValid = !StringUtils.isNullOrEmpty(review.bookId);
    let isUserIdValid = !StringUtils.isNullOrEmpty(review.userId);
    let isCommentValid = !StringUtils.isNullOrEmpty(review.comment);

    return isBookIdValid && isUserIdValid && isCommentValid;
  }
}
