import { Component, OnInit } from '@angular/core';
import { BookService } from '../../services/book/book.service';

@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.css']
})
export class BookComponent implements OnInit {

  constructor(private bookService: BookService) { }

  ngOnInit() {
  }
}
