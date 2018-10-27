import { Component, OnInit} from '@angular/core';
import { Router } from "@angular/router";
import { ActivatedRoute, RouterStateSnapshot } from "@angular/router";
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { Book } from '../book.details/book.model';
import { BookInterface } from '../book.details/book.interface';
import { BookService } from '../../services/book/book.service';
import { BookSortingOption } from './booksortingoption.model';
import { SortCommand } from '../../common/models/sortcommand';
import { SortDirection } from '../../common/enums/sortdirection';
import { ScrollEvent } from 'ngx-scroll-event';
import { PageCommand } from '../../common/models/pagecommand';
import { TopSellerStatus } from '../../common/enums/topsellerstatus';
import { BookFilterCommand } from '../../common/models/bookfiltercommand';

@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.css']
})
export class BookComponent implements OnInit {
  public static readonly numberOfBooksPerPage = 8;
  public static readonly topSellerFilterBoth = "Both";
  public static readonly topSellerFilterTopSeller = "Top Seller";
  public static readonly topSellerFilterRegular = "Regular";

  private useTestData: boolean;
  private loading: boolean;
  private offset: number;

  public books: Book[];
  public bookSortingOptions: BookSortingOption[];
  public selectedSortingOption: BookSortingOption;
  public selectedSortingDirectionName: string;
  public selectedSortingDirectionValue: SortDirection;
  public topSellerFilterName: string;
  public topSellerFilterValue: TopSellerStatus;
  public titleFilterValue: string;
  public authorFilterValue: string;
  public genreFilterValue: string;
  public minPriceFilterValue: string;
  public maxPriceFilterValue: string;
  public minDateFilterValue: string;
  public maxDateFilterValue: string;
  public ratingFilterValue: number;

  public constructor(private route: ActivatedRoute, private router: Router, private fb: FormBuilder, private bookService: BookService) {
    this.useTestData = false;

    this.bookSortingOptions = this.createSortingOptions();
    this.selectedSortingOption = this.bookSortingOptions[0];
    this.selectedSortingDirectionName = 'Ascending';
    this.selectedSortingDirectionValue = SortDirection.Asc;
    this.books = this.useTestData ? this.getTestData() : this.getData();
  }
  
  public ngOnInit() {
    this.loading = false;
    this.offset = 0;
    this.topSellerFilterName = BookComponent.topSellerFilterBoth;
    this.topSellerFilterValue = TopSellerStatus.Both;
  }
    
  /**
   * Returns an array of books with test data.
   */
  private getTestData() {
    let books: Book[] = [
    {
      id: "2NdnZLuGrAQxDo09nK0A",
      title: "A Feast for Crows",
      author: "George R. R. Martin",
      genre: "Fantasy",
      price: 15.29,
      rating: 4.2,
      releaseDate: new Date(2005, 1, 11),
      topSeller: true,
      imageUrl: "https://images-na.ssl-images-amazon.com/images/I/51s9NYowTlL.jpg"
    },
    {
      id: "9f4h2cmXiisRyjHtfa87",
      title: "Harry Potter and the Deathly Hallows",
      author: "J.K. Rowling",
      genre: "Fantasy",
      price: 13.49,
      rating: 4.7,
      releaseDate: new Date(2009, 7, 7),
      topSeller: true,
      imageUrl: "https://images-na.ssl-images-amazon.com/images/I/91ninPjyGML.jpg"
    },
    {
      id: "EWA1cUQUvbxODLBhtRva",
      title: "Harry Potter and the Goblet of Fire",
      author: "J.K. Rowling",
      genre: "Fantasy",
      price: 11.58,
      rating: 4.8,
      releaseDate: new Date(2000, 30, 7),
      topSeller: false,
      imageUrl: "https://images-na.ssl-images-amazon.com/images/I/91qQ2aNaKAL.jpg"
    },
    {
      id: "Kb0SEgQom2fJWANelKtW",
      title: "The Return of the King",
      author: "J.R.R Tolkien",
      genre: "Fantasy",
      price: 11.16,
      rating: 4.7,
      releaseDate: new Date(1955, 20, 10),
      topSeller: true,
      imageUrl: "https://images-na.ssl-images-amazon.com/images/I/517fXi%2BOX2L.jpg"
    },
    {
      id: "M9jiYQXgbX9S1NwMTIGA",
      title: "A Game of Thrones",
      author: "George R. R. Martin",
      genre: "Fantasy",
      price: 10.89,
      rating: 4.6,
      releaseDate: new Date(1996, 1, 8),
      topSeller: false,
      imageUrl: "https://images-na.ssl-images-amazon.com/images/I/919-FLL37TL.jpg"
    }]
  
    return books;
  }

  /**
   * Returns the pre-loaded book data from the resolver.
   */
  private getData() {
    return this.route.snapshot.data.bookResolver as BookInterface[];
	} 
  
  /**
   * Redirects the browser to the selected book's details page.
   * @param book The book to load the details for.
   */
  public selectBook(book: Book) {
    this.router.navigate([`book/${book.id}`]);
  }

  /**
   * Creates the list of fields to be used as sorting keys.
   */
  private createSortingOptions() {
    let options: BookSortingOption[] = [
      this.createSortingOption('Title', 'title'),
      this.createSortingOption('Author', 'author'),
      this.createSortingOption('Price', 'price'),
      this.createSortingOption('Rating', 'rating'),
      this.createSortingOption('Release Date', 'releaseDate'),
      this.createSortingOption('Genre', 'genre')
    ];

    return options;
  }

  /**
   * Creates a new book sorting option with the given name and key.
   * @param name The name to display in user interface.
   * @param key The name of the key to sort on.
   */
  private createSortingOption(name: string, key: string) {
    let option = new BookSortingOption();
    option.name = name;
    option.key = key;

    return option;
  }

  /**
   * Reloads the list of books with the given sorting option.
   * @param option The book sorting option to use.
   */
  public setSelectedSortingOption(option: BookSortingOption) {
    this.selectedSortingOption = option;
    this.offset = 0;
    this.reloadBooksWithDefault(true);
  }

  /**
   * Reloads the list of books in ascending order.
   */
  public setAscSortingDirection() {
    this.selectedSortingDirectionName = 'Ascending';
    this.selectedSortingDirectionValue = SortDirection.Asc;
    this.offset = 0;
    this.reloadBooksWithDefault(true);
  }

  /**
   * Reloads the list of books in descending order.
   */
  public setDescSortingDirection() {
    this.selectedSortingDirectionName = 'Descending';
    this.selectedSortingDirectionValue = SortDirection.Desc;
    this.offset = 0;
    this.reloadBooksWithDefault(true);
  }

  /**
   * Constructs a new sort command with the given values.
   * @param sortKey The key of the field to sort on.
   * @param sortDirection The order to sort in.
   */
  private buildSortCommand(sortKey: string, sortDirection: SortDirection) {
    let sortCommand = new SortCommand();
    sortCommand.key = sortKey;
    sortCommand.sortBy = sortDirection;

    return sortCommand;
  }

/**
 * Constructs a new filter command with the given values.
 * @param title The title filter.
 * @param author The author filter.
 * @param genre The genre filter.
 * @param minPrice The minimum price range filter.
 * @param maxPrice The maximum price range filter.
 * @param minDate The minimum release date range filter.
 * @param maxDate The maximum release date range filter.
 * @param rating The minimum rating filter.
 * @param topSeller The top seller status filter.
 */
  private buildFilterCommand(title: string, author: string, genre: string, minPrice: string, maxPrice: string, 
    minDate: string, maxDate: string, rating: number, topSeller: TopSellerStatus) {
    let filterCommand = new BookFilterCommand();
    filterCommand.title = title;
    filterCommand.author = author;
    filterCommand.genre = genre;
    filterCommand.minPrice = +(minPrice);
    filterCommand.maxPrice = +(maxPrice);
    filterCommand.minReleaseDate = new Date(minDate);
    filterCommand.maxReleaseDate = new Date(maxDate);
    filterCommand.rating = +(rating);

    if (this.topSellerFilterValue !== TopSellerStatus.Both) {
      filterCommand.topSeller = topSeller === TopSellerStatus.TopSeller;
    }

    return filterCommand;
  }

  /**
   * Constructs a new paging command.
   * @param offset The number of the current page offset.
   */
  private buildPageCommand(offset: number) {
    let pageCommand = new PageCommand();
    pageCommand.limit = BookComponent.numberOfBooksPerPage;
    pageCommand.offset = BookComponent.numberOfBooksPerPage * offset;

    return pageCommand;
  }

  /**
   * Reloads the list of books with the default values.
   * @param clean If true, the current list of books will be replaced by the result, 
   *              otherwise the result will be appended.
   */
  private reloadBooksWithDefault(clean: boolean) {
    let sortCommand = this.buildSortCommand(this.selectedSortingOption.key, this.selectedSortingDirectionValue);
    let filterCommand = this.buildFilterCommand(this.titleFilterValue, this.authorFilterValue, this.genreFilterValue,
      this.minPriceFilterValue, this.maxPriceFilterValue, this.minDateFilterValue, this.maxDateFilterValue,
      this.ratingFilterValue, this.topSellerFilterValue);
    let pageCommand = this.buildPageCommand(this.offset);

    this.reloadBooks(sortCommand, filterCommand, pageCommand, clean);
  }

  /**
   * Reloads the list of books with the given sorting key and direction.
   * @param clean If true, the current list of books will be replaced by the result, 
   *              otherwise the result will be appended.
   */
  private reloadBooks(sortCommand: SortCommand, filterCommand: BookFilterCommand, pageCommand: PageCommand, clean: boolean) {
    if (this.useTestData === false) {
      this.loading = true;

      this.bookService.findAll(sortCommand, filterCommand, pageCommand)
        .subscribe((result: BookInterface[]) => {
          this.loading = false;
          this.books = clean ? result : this.books.concat(result);
        });
    }
  }

  /**
   * Loads the next page of books.
   * @param event The scrolling event triggered.
   */
  public loadAdditionalBooks(event: ScrollEvent) {
    if (event.isReachingBottom && !this.loading) {
      this.loading = true;
      this.offset += 1;
      this.reloadBooksWithDefault(false);
    }
  }

  /**
   * Sets the top seller filter values.
   * @param status Top seller status value.
   */
  public setTopSellerFilter(status: number) {
    if (status == TopSellerStatus.Both) {
      this.topSellerFilterName = BookComponent.topSellerFilterBoth;
      this.topSellerFilterValue = TopSellerStatus.Both;
    }

    else if (status == TopSellerStatus.TopSeller) {
      this.topSellerFilterName = BookComponent.topSellerFilterTopSeller;
      this.topSellerFilterValue = TopSellerStatus.TopSeller;
    }

    else if (status == TopSellerStatus.Regular) {
      this.topSellerFilterName = BookComponent.topSellerFilterRegular;
      this.topSellerFilterValue = TopSellerStatus.Regular;
    }
  }

  /**
   * Sets the rating filter value.
   * @param rating The minumum rating to filter with.
   */
  public setRatingFilter(rating: number) {
    this.ratingFilterValue = rating;
  }

  /**
   * Reloads the books with the default values.
   */
  public applyFilters() {
    this.offset = 0;
    this.reloadBooksWithDefault(true);
  }
}
