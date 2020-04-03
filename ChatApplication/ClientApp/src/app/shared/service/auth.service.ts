import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable()
export class AuthService {
  constructor() {}

  public isAuthenticated(): boolean {
    var jwtHelper = new JwtHelperService()
    const token = localStorage.getItem('BearerToken');

    // Check whether the token is expired and return true or false
    return !jwtHelper.isTokenExpired(token);
  }
}