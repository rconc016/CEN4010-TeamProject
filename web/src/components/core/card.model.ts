import * as firebase from "firebase";

export class Card {
  cardNumber: string;
  expirationDate: string;
  cvc: string;
  cardName: string;
  

  constructor() {
    this.cardNumber = '';
    this.expirationDate = '';
    this.cvc = '';
    this.cardName = '';
  }  
}
