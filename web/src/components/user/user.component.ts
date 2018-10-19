import { Component, OnInit } from '@angular/core';
import { UserService } from '../core/user.service';
import { AuthService } from '../core/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FirebaseUserModel } from '../core/user.model';

@Component({
  selector: 'page-user',
  templateUrl: 'user.component.html',
  styleUrls: ['user.scss']
})
export class UserComponent implements OnInit {
  user: FirebaseUserModel;
  profileForm: FormGroup;

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
      name: [name, Validators.required ],
    });
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
          name: this.user.name
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
