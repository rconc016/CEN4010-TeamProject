import { Component, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import {ActivatedRoute, RouterStateSnapshot} from "@angular/router";
import { Book } from './book.model';
import { BookInterface } from './book.interface';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';

@Component({
  selector: 'app-book',
  templateUrl: './book.details.component.html',
  styleUrls: ['./book.details.component.css']
})
export class BookDetailsComponent implements OnInit {
  public id: string;
  public book: Book;

  public constructor(private route: ActivatedRoute, private router: Router, private fb: FormBuilder) { 
    this.book = this.route.snapshot.data.bookResolver as BookInterface;
  }

  public navigateToBrowserPage() {
    this.router.navigate(['book']);
  }

  public ngOnInit() {
  }
}
