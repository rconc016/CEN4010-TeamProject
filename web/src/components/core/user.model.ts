import * as firebase from "firebase";

export class FirebaseUserModel {
  image: string;
  name: string;
  provider: string;
  firstName: string;
  lastName: string;
  email: string;
  id: string;
  billingAddress: string;
  shippingAddress: string[];
  nickname: string;
  creditCards: string[];

  constructor(){
    this.image = '';
    this.name = '';
    this.provider = '';
    this.firstName = '';
    this.lastName = '';
    this.email = '';
    this.nickname = '';
    this.shippingAddress = [];
    this.creditCards = [];
    firebase.auth().onAuthStateChanged((user) => {
      if (user) {

        this.id = user.uid;
        this.email = user.email;
      } else {
        this.id = '';
      }
    });
    
  }
}
