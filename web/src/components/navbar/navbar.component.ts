import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserComponent } from '../user/user.component';
import * as firebase from 'firebase';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  constructor(private router: Router, private userComponent: UserComponent) { }

  ngOnInit() {
  }

  goHome() {
    this.router.navigate(['/book']);
  }

  goProfile() {
    this.router.navigate(['/user']);
  }

  goCart() {
    var user = firebase.auth().currentUser;

    if(user)
      this.router.navigate(['/cart']);
    else
      this.router.navigate(['/login']);
  }

  goLoginLogout() {
    var user = firebase.auth().currentUser;

    if (user)
      this.userComponent.logout();
    else
      this.router.navigate(['/login']);
  }
}

