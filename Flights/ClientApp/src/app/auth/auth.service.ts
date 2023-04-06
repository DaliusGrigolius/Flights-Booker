import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor() { }

  currentUser?: User;

  loginUser(user: User) {
    this.currentUser = user
  }
}

interface User {
  email?: null | string
}
