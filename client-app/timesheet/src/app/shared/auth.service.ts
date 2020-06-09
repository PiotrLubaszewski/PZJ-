import { AuthenticatedUser } from './../models/authenticated-user.model';
import * as jwtDecode from 'jwt-decode';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private token: string;
  private authenticated = false;
  private tokenExpireDate: Date;
  private user: AuthenticatedUser;

  public isAuthenticated(): boolean {
    return this.authenticated && this.token != null && this.token !== undefined && this.token !== '';
  }

  public isAdmin(): boolean {
    return this.user != null && this.user.roles != null && (this.user.roles === 'Admin' || this.user.roles.includes('Admin'));
  }

  public isManager(): boolean {
    return this.user != null && this.user.roles != null && (this.user.roles === 'Manager' || this.user.roles.includes('Manager'));
  }


  public getAccessToken(): string {
    return this.token;
  }

  public getUser(): AuthenticatedUser {
    return this.user;
  }

  public authenticate(token: string) {
    this.token = token;
    this.authenticated = true;
    this.decodeToken();
    this.setTokenToStorage();
  }

  public deauthenticate() {
    this.token = null;
    this.authenticated = false;
    this.removeTokenFromStorage();
  }

  public authenticateFromStorage() {
    const token = this.getTokenFromStorage();
    if (token == null || token === undefined || token === '') {
      return;
    }

    this.authenticate(token);
  }

  private decodeToken() {
    const decodedToken = jwtDecode(this.token);
    const date = new Date(0);
    date.setUTCSeconds(decodedToken.exp);
    this.tokenExpireDate = date;

    this.user = new AuthenticatedUser(decodedToken.userId, decodedToken.userName, decodedToken.email, decodedToken.firstName, decodedToken.lastName);
    const role = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
    if (role != null && role !== undefined && role !== '') {
      this.user.roles = role;
    }

    console.log(decodedToken);
    console.log(role);
    console.log(this.user);
  }

  private setTokenToStorage() {
    localStorage.setItem('token', this.token);
  }

  private getTokenFromStorage() {
    return localStorage.getItem('token');
  }

  private removeTokenFromStorage() {
    return localStorage.removeItem('token');
  }
}