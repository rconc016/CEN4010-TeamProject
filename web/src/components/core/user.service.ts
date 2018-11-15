import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import { AngularFirestore } from '@angular/fire/firestore';
import { AngularFireAuth } from '@angular/fire/auth';
import * as firebase from 'firebase/app';
import { FirebaseUserModel } from './user.model';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json',
  })
};

@Injectable()
export class UserService {

  private readonly url = `${environment.apiUrl}/user`

  constructor(
   public db: AngularFirestore,
   public afAuth: AngularFireAuth,
   private http: HttpClient
 ) {
 }

  getUser(id : string):Observable<FirebaseUserModel> {
    return this.http.get<FirebaseUserModel>(`${this.url}/${id}`);
  }

  updateUser(user : FirebaseUserModel):Observable<FirebaseUserModel> {
    return this.http.put<FirebaseUserModel>(`${this.url}/${user.id}`, user, httpOptions);
  }

  getCurrentUser() {
    return new Promise<any>((resolve, reject) => {
      var user = firebase.auth().onAuthStateChanged(function(user){
        if (user) {
          resolve(user);
        } else {
          reject('No user logged in');
        }
      });
    });
  }

  updateCurrentUser(value){
    return new Promise((resolve, reject) => {
      var user = firebase.auth().currentUser;
      user.updateProfile({
        displayName: value.name,
        photoURL: user.photoURL
      }).then((res: any) => {
        resolve(res);
      }, err => reject(err));
    });
  }

  isPasswordValid(password: string) {
    let config = {
      params: {
        password: password
      }
    };

    return this.http.get<boolean>(`${this.url}/validate/password`, config);
  }
}
