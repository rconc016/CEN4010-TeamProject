import { Component, OnInit } from '@angular/core';
import { UserService } from '../core/user.service';
import { AuthService } from '../core/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';
import { FirebaseUserModel } from '../core/user.model';
import * as firebase from 'firebase';

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
  shippingAddresses: FormArray
  creditCards: FormArray

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
      shippingAddresses: this.fb.array([this.initShippingAddresses()]),
      creditCard: this.fb.array([this.initCreditCards()])
    });
  }

  initShippingAddresses() {
    return this.fb.group({
      shippingAddress: ['']
    })
  }

  initCreditCards() {
    return this.fb.group({
      creditCard: ['']
    })
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

    user.updatePassword(newPassword).then(res => {
      this.successMessagePassword = "Password was successfully updated";
      this.errorMessagePassword = "";
    }).catch(err => {
      this.successMessagePassword = "";
      this.errorMessagePassword = "This operation is sensitive and requires recent authentication.Log in again before retrying this request.";
    });
      
  }

  addShippingAddress() {
    this.shippingAddresses = this.profileForm.get('shippingAddresses') as FormArray;
    this.shippingAddresses.push(this.initShippingAddresses());
    for (let i = 0; i < this.shippingAddresses.length; i++) {
      this.user.shippingAddress.push(this.shippingAddresses[i]);
    }
  }

  addCreditCard() {
    this.creditCards = this.profileForm.get('creditCards') as FormArray;
    this.creditCards.push(this.initCreditCards());
    for (let i = 0; i < this.creditCards.length; i++) {
      this.user.creditCards.push(this.creditCards[i]);
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
