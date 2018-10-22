import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { rootRouterConfig } from './app.routes';
import { AngularFireModule } from '@angular/fire';
import { AngularFirestoreModule } from '@angular/fire/firestore';
import { AngularFireAuthModule } from '@angular/fire/auth';
import { ReactiveFormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { LoginComponent } from '../components/login/login.component';
import { UserComponent } from '../components/user/user.component';
import { RegisterComponent } from '../components/register/register.component';
import { BookComponent } from '../components/book/book.component';
import { environment } from '../environments/environment';
import { AuthService } from '../components/core/auth.service';
import { UserService } from '../components/core/user.service';
import { UserResolver } from '../components/user/user.resolver';
import { AuthGuard } from '../components/core/auth.guard';
import { FormsModule } from '@angular/forms';
import { CartComponent } from '../components/cart/cart.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    UserComponent,
    RegisterComponent,
    BookComponent,
    CartComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    RouterModule.forRoot(rootRouterConfig, { useHash: false }),
    AngularFireModule.initializeApp(environment.firebase),
    AngularFirestoreModule, // imports firebase/firestore, only needed for database features
    AngularFireAuthModule, // imports firebase/auth, only needed for auth features
  ],
  providers: [AuthService, UserService, UserResolver, AuthGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
