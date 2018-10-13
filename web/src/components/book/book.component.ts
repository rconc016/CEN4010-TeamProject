import { Component, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import {ActivatedRoute, RouterStateSnapshot} from "@angular/router";
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { Book } from '../book.details/book.model';
import { BookInterface } from '../book.details/book.interface';

@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.css']
})
export class BookComponent implements OnInit {
  private readonly useTestData = false;
    
  public searchForm: FormGroup;
  public hasSearched = false;
  public query = '';
  public filterIndex = 999;    
  public filteredBooks: Book[];  
  public books: Book[];

  public constructor(private route: ActivatedRoute, private router: Router, private fb: FormBuilder) {
    this.books = this.useTestData ? this.getTestData() : this.getData();
    this.createForm();
  }
  
  public createForm() {
    this.searchForm = this.fb.group({
      bookQuery: ['', Validators.required ]
    });
  }
    
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

  private getData() {
    return this.route.snapshot.data.bookResolver as BookInterface[];
	} 
	
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

  public ngOnInit() {
  }
}
