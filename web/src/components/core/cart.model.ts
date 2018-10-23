import * as firebase from "firebase";
import { Book } from '../book.details/book.model';

export class FirebaseCartModel {
  id: string;
  products: Book[];

  constructor() {
    firebase.auth().onAuthStateChanged((user) => {
      if (user) {

        this.id = user.uid;
      } else {
        this.id = '';
      }
    });

    this.products = [];
  }
}
