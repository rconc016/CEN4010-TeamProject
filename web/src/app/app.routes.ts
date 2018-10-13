import { Routes } from '@angular/router';
import { LoginComponent } from '../components/login/login.component';
import { AuthGuard } from '../components/core/auth.guard';
import { RegisterComponent } from '../components/register/register.component';
import { UserComponent } from '../components/user/user.component';
import { UserResolver } from '../components/user/user.resolver';
import { BookComponent } from '../components/book/book.component';


export const rootRouterConfig: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent, canActivate: [AuthGuard] },
  { path: 'register', component: RegisterComponent, canActivate: [AuthGuard] },
  { path: 'user', component: UserComponent,  resolve: { data: UserResolver} },
  { path: 'book', component: BookComponent }
];
