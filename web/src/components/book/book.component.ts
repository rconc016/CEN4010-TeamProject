import { Component, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import {ActivatedRoute, RouterStateSnapshot} from "@angular/router";
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { Book } from '../book.details/book.model';
import { BookInterface } from '../book.details/book.interface';
import { BookService } from '../../services/book/book.service';
import { BookSortingOption } from './booksortingoption.model';
import { SortCommand } from '../../common/models/sortcommand';
import { SortDirection } from '../../common/enums/sortdirection';

@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.css']
})
export class BookComponent implements OnInit {
  private useTestData = false;
    
  public searchForm: FormGroup;
  public hasSearched = false;
  public query = '';
  public filterIndex = 999;    
  public filteredBooks: Book[];  
  public books: Book[];
  public bookSortingOptions: BookSortingOption[];
  public selectedSortingOption: BookSortingOption;
  public selectedSortingDirection: string;

  public constructor(private route: ActivatedRoute, private router: Router, private fb: FormBuilder, private bookService: BookService) {
    this.bookSortingOptions = this.createSortingOptions();
    this.selectedSortingOption = this.bookSortingOptions[0];
    this.selectedSortingDirection = 'Ascending';
    this.books = this.useTestData ? this.getTestData() : this.getData();
    this.createForm();
  }
  
  public createForm() {
    this.searchForm = this.fb.group({
      bookQuery: ['', Validators.required ]
    });
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
  
  public saveQuery(event: KeyboardEvent) {
    this.query = (<HTMLInputElement>event.target).value;
    this.hasSearched = true;
  }
  
  public filterBooks(query) {
	  return this.filteredBooks = this.books.filter(books => books.title.toLowerCase().indexOf(query.toLowerCase()) > -1);
  }
  
  public saveFilterIndex(ind: number) {
    this.filterIndex = ind;
    return this.filterIndex;
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
    this.reloadBooks(this.selectedSortingOption.key, SortDirection.Asc);
  }

  /**
   * Reloads the list of books in ascending order.
   */
  public setAscSortingDirection() {
    this.selectedSortingDirection = 'Ascending';
    this.reloadBooks(this.selectedSortingOption.key, SortDirection.Asc);
  }

  /**
   * Reloads the list of books in descending order.
   */
  public setDescSortingDirection() {
    this.selectedSortingDirection = 'Descending';
    this.reloadBooks(this.selectedSortingOption.key, SortDirection.Desc);
  }

  /**
   * Reloads the list of books with the given sorting key and direction.
   * @param sortKey The key of the field to sort on.
   * @param sortDirection The order to sort in.
   */
  private reloadBooks(sortKey: string, sortDirection: SortDirection) {
    let sortCommand = new SortCommand();
    sortCommand.key = sortKey;
    sortCommand.sortBy = sortDirection;

    if (this.useTestData === false) {
      this.bookService.findAll(sortCommand, null, null)
        .subscribe((result: BookInterface[]) => {
          this.books = result;
        });
    }
  }

  public ngOnInit() {
  }
}
