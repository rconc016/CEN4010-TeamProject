import { Component, Input } from '@angular/core';
import { AuthService } from '../core/auth.service'
import { Router, Params } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../core/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {

  private static readonly userUrlRoute = ['/user'];

  registerForm: FormGroup;
  errorMessage: string = '';
  successMessage: string = '';

  constructor(
    public authService: AuthService,
    private router: Router,
    private fb: FormBuilder,
    private userService: UserService
  ) {
    this.createForm();
  }

   createForm() {
     this.registerForm = this.fb.group({
       email: ['', Validators.required ],
       password: ['', Validators.required ]
     });
   }

  tryFacebookLogin() {
    this.authService.doFacebookLogin()
    .then(res =>{
      this.router.navigate(RegisterComponent.userUrlRoute);
    }, err => console.log(err)
    )
  }

  tryTwitterLogin() {
    this.authService.doTwitterLogin()
    .then(res =>{
      this.router.navigate(RegisterComponent.userUrlRoute);
    }, err => console.log(err)
    )
  }

  tryGoogleLogin() {
    this.authService.doGoogleLogin()
    .then(res =>{
      this.router.navigate(RegisterComponent.userUrlRoute);
    }, err => console.log(err)
    )
  }

  async tryRegister(value) {
    let passwordForm = this.registerForm.get('password');
    let result = await this.userService.isPasswordValid(passwordForm.value).toPromise();

    if (!passwordForm.valid || !result) {
      this.errorMessage = "Password not valid";
      this.successMessage = '';

      return;
    }

    this.authService.doRegister(value)
    .then(res => {
      console.log(res);
      this.errorMessage = '';
      this.successMessage = "Your account has been created";
      this.router.navigate(RegisterComponent.userUrlRoute);
    }, err => {
      console.log(err);
      this.errorMessage = err.message;
      this.successMessage = '';
    })
  }
}
