import { Component, OnInit, Inject} from '@angular/core';
import {Router} from "@angular/router";
import {ActivatedRoute, RouterStateSnapshot} from "@angular/router";
import { Book } from './book.model';
import { BookInterface } from './book.interface';
import { BookDescription } from './book.description.model';
import { BookService } from '../../services/book/book.service';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from "@angular/material";

export interface DialogData {
	dialogImgUrl: string;
}

@Component({
  selector: 'app-book',
  templateUrl: './book.details.component.html',
  styleUrls: ['./book.details.component.css']
})
export class BookDetailsComponent implements OnInit {
  public id: string;
  public book: Book;
  public bookDescription: BookDescription;

  public constructor(private route: ActivatedRoute, private router: Router, private fb: FormBuilder, private descriptionService: BookService, public dialog: MatDialog) { 
    this.book = this.route.snapshot.data.bookResolver as BookInterface;
    this.bookDescription = new BookDescription();
    this.descriptionService.findDescriptionById(this.book.descriptionId)
      .subscribe((response: BookDescription) => { 
        this.bookDescription = response; 
      });
  }

  public navigateToBrowserPage() {
    this.router.navigate(['book']);
  }

  public ngOnInit() {
  }
  
  /**
   * Opens dialog with enlarged book image
   */
  public openDialog(selectedBookImgUrl){
	this.dialog.open(DetailsDataDialog, {
		data: { dialogImgUrl: selectedBookImgUrl }
	});
  }
}

@Component({
  selector: 'dialog-data',
  templateUrl: 'dialog-data.html',
})
export class DetailsDataDialog {
  constructor(@Inject(MAT_DIALOG_DATA) public data: DialogData) {}
}
