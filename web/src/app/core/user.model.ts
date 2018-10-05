import * as firebase from "firebase";

export class FirebaseUserModel {
  image: string;
  name: string;
  provider: string;
  fName: string;
  lName: string;
  email: string;
  id: string;
  billingAddress: string;
  shippingAddress: string;
  nickname: string;

  constructor(){
    this.image = "";
    this.name = "";
    this.provider = "";
    this.fName = '';
    this.lName = '';
    this.email = '';
    this.nickname = '';
    firebase.auth().onAuthStateChanged((user) => {
      if (user) {
        this.id = user.uid;
      } else {
        this.id = '';
      }
    });
    
  }
}
