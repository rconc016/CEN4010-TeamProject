import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/toPromise';
import { AngularFirestore } from '@angular/fire/firestore';
import { AngularFireAuth } from '@angular/fire/auth';
import * as firebase from 'firebase/app';
import { User } from 'firebase/app';
import { FirebaseUserModel } from './user.model';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json',
  })
};

@Injectable()
export class UserService {

  url:string = 'http://localhost:63073/api/user/' 

  constructor(
   public db: AngularFirestore,
   public afAuth: AngularFireAuth,
   private http: HttpClient
 ) {
 }

  getUser(id : string):Observable<FirebaseUserModel>{
    return this.http.get<FirebaseUserModel>(this.url + id);
  }

  updateUser(user : FirebaseUserModel):Observable<FirebaseUserModel>
  {
    return this.http.put<FirebaseUserModel>(this.url + user.id, user, httpOptions);
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
}
