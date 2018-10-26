import { Component, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import {ActivatedRoute, RouterStateSnapshot} from "@angular/router";
import { Book } from './book.model';
import { BookInterface } from './book.interface';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { UserService } from '../core/user.service';
import { CartComponent } from '../cart/cart.component';

@Component({
  selector: 'app-book',
  templateUrl: './book.details.component.html',
  styleUrls: ['./book.details.component.css']
})
export class BookDetailsComponent implements OnInit {
  private readonly useTestData = false;

  public id: string;
  public book: Book;

  public constructor(private route: ActivatedRoute, private router: Router, private fb: FormBuilder,
    private userService : UserService, private cartComponent : CartComponent) { 
    this.book = this.useTestData ? this.getTestData() : this.getData();
  }

  private getTestData() {
    this.book = {
			id: "testID",
			title: "The Adventures of Roelar",
			author: "Ron Con",
			genre: "Fantasy",
			price: 9.99,
			rating: 4,
			releaseDate: new Date(2018, 9, 10),
			topSeller: true,
			imageUrl: "https://images-na.ssl-images-amazon.com/images/I/919-FLL37TL.jpg"
		}	
    return this.book;
  }

  private getData() {
    return this.route.snapshot.data.bookResolver as BookInterface;
  } 

  public navigateToBrowserPage() {
    this.router.navigate(['book']);
  }

  public addToCart(product: Book) {
    let user = this.userService.getCurrentUser();
    if (user == null) {
      this.router.navigate(['login']);
    }

    this.cartComponent.addProduct(product);
    
  }

  public ngOnInit() {
  }
}
