import { Component, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import {ActivatedRoute, RouterStateSnapshot} from "@angular/router";
import { Book } from './book.model';
import { BookInterface } from './book.interface';
import { BookDescription } from './book.description.model';
import { BookService } from '../../services/book/book.service';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { UserService } from '../core/user.service';
import { CartComponent } from '../cart/cart.component';
import { FirebaseUserModel } from '../core/user.model';

@Component({
  selector: 'app-book',
  templateUrl: './book.details.component.html',
  styleUrls: ['./book.details.component.css']
})
export class BookDetailsComponent implements OnInit {
  public id: string;
  public book: Book;
  public user: FirebaseUserModel;
  public bookDescription: BookDescription;

  public constructor(private route: ActivatedRoute, private router: Router, private fb: FormBuilder,
    private userService : UserService, private cartComponent : CartComponent) { 
    this.book = this.useTestData ? this.getTestData() : this.getData();
    this.user = new FirebaseUserModel();
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
    this.userService.getUser(this.user.id)
        .subscribe((data: FirebaseUserModel) => this.user = { 
          billingAddress: data['billingAddress'],
          email: data['email'],
          firstName: data['firstName'],
          id: data['id'],
          lastName: data['lastName'],
          nickname: data['nickname'],
          shippingAddress : data['shippingAddress'],
          provider: data['provider'],
          image: data['image'],
          name: data['name'],
          creditCards: data['creditCards']
        } );
    if (this.user.id == "") {
      this.router.navigate(['login']);
    }
    this.cartComponent.addProduct(product, this.user.id);
    
  }

  public ngOnInit() {
    
  }
}
