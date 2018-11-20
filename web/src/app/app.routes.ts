import { Routes } from '@angular/router';
import { LoginComponent } from '../components/login/login.component';
import { AuthGuard } from '../components/core/auth.guard';
import { RegisterComponent } from '../components/register/register.component';
import { UserComponent } from '../components/user/user.component';
import { UserResolver } from '../components/user/user.resolver';
import { BookDetailsComponent } from '../components/book.details/book.details.component';
import { BookDetailsResolver } from '../components/book.details/book.details.resolver';
import { BookComponent } from '../components/book/book.component';
import { BookResolver } from '../components/book/book.resolver';
import { AuthorComponent } from '../components/author/author.component';
import { AuthorResolver } from '../components/author/author.resolver';

export const rootRouterConfig: Routes = [
  { path: '', redirectTo: 'book', pathMatch: 'full' },
  { path: 'login', component: LoginComponent, canActivate: [AuthGuard] },
  { path: 'register', component: RegisterComponent, canActivate: [AuthGuard] },
  { path: 'user', component: UserComponent,  resolve: { data: UserResolver} },
  { path: 'book', component: BookComponent, resolve: { bookResolver: BookResolver } },
  { path: 'book/:id', component: BookDetailsComponent, resolve: { bookResolver: BookDetailsResolver } },
  { path: 'author/:id', component: AuthorComponent, resolve: { authorResolver: AuthorResolver }},
];
