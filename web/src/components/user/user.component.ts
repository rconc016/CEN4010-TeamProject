import { Component, OnInit, Injectable } from '@angular/core';
import { UserService } from '../core/user.service';
import { AuthService } from '../core/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';
import { FirebaseUserModel } from '../core/user.model';
import * as firebase from 'firebase';
import { CreditCardValidator } from 'ngx-credit-cards';
import * as Payment from 'payment';

@Component({
  selector: 'page-user',
  templateUrl: 'user.component.html',
  styleUrls: ['user.scss']
})

export class UserComponent implements OnInit {
  user: FirebaseUserModel;
  profileForm: FormGroup;
  successMessageEmail: string = '';
  errorMessageEmail: string = '';
  successMessagePassword: string = '';
  errorMessagePassword: string = '';

  constructor(
    private userService: UserService,
    public authService: AuthService,
    private route: ActivatedRoute,
    private router: Router,
    private location : Location,
    private fb: FormBuilder
  ) {
  }

  ngOnInit(): void {
    this.route.data.subscribe(routeData => {
      let data = routeData['data'];
      if (data) {
        this.user = data;
        this.createForm(this.user.name);
      }
    })
    
    this.getUser();
  }

  createForm(name) {
    this.profileForm = this.fb.group({
      name: [name, Validators.required],
      cardNumber: ['', CreditCardValidator.validateCardNumber],
      cardExpDate: ['', CreditCardValidator.validateCardExpiry],
      cardCvv: ['', CreditCardValidator.validateCardCvc],
      cardName: ['', Validators.compose([Validators.required, Validators.minLength(2)])]
    });
  }

  customTrackBy(index: number, obj: any): any {
    return index;
  }

  updateEmail(email:string) {
    var user = firebase.auth().currentUser;

    user.updateEmail(email).then(res => {
      this.successMessageEmail = "Email was successfully updated";
      this.errorMessageEmail = "";
    }).catch(err => {
      this.successMessageEmail = "";
      this.errorMessageEmail = "This operation is sensitive and requires recent authentication.Log in again before retrying this request.";
    });
  }

  updatePassword(password:string) {
    var user = firebase.auth().currentUser;
    var newPassword = password;

    if (!newPassword.match(/^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{4,8}$/)) {
      this.successMessagePassword = "";
      this.errorMessagePassword = "Not a valid Password";
      return;
    }

    user.updatePassword(newPassword).then(res => {
      this.successMessagePassword = "Password was successfully updated";
      this.errorMessagePassword = "";
    }).catch(err => {
        this.successMessagePassword = "";
        this.errorMessagePassword = "This operation is sensitive and requires recent authentication.Log in again before retrying this request.";
    });
      
  }

  addShippingAddress() {
    this.user.shippingAddress.push('');
  };

  removeShippingAddress(shippingAddress: string) {
    for (let i = 0; i < this.user.shippingAddress.length; i++) {
      if (this.user.shippingAddress[i] === shippingAddress) {
        this.user.shippingAddress.splice(i, 1);
        break;

      }
    }
  }

  addCreditCard() {
    this.user.creditCards.push({
      cardNumber: '', expirationDate: '', cvc: '',
      cardName: ''
    });
  }

  removeCreditCard(creditCard: string) {
    for (let i = 0; i < this.user.creditCards.length; i++) {
      if (this.user.creditCards[i].cardNumber === creditCard) {
        this.user.creditCards.splice(i, 1);
        break;

      }
    }
  }

  logout(){
    this.authService.doLogout()
    .then((res) => {
      this.router.navigate(['']);
    }, (error) => {
      console.log("Logout error", error);
    });
  }

  getUser() {
    console.log(this.user.id);
    this.userService.getUser(this.user.id)
        .subscribe((data: FirebaseUserModel) => this.user = { 
          billingAddress: data['billingAddress'],
          email: data['email'],
          firstName: data['firstName'],
          id: data['id'],
          lastName: data['lastName'],
          nickname: data['nickname'],
          shippingAddress : data['shippingAddress'],
          provider: this.user.provider,
          image: this.user.image,
          name: this.user.name,
          creditCards: data['creditCards']
        });
  }

  updateUser() {
    this.user.name = `${this.user.firstName} ${this.user.lastName}`;
    this.userService.updateUser(this.user)
      .subscribe(res => {
        this.router.navigate(['/book']);
    })
  }
  
}
