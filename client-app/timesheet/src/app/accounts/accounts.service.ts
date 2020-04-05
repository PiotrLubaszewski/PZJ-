import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ApiResponse } from '../models/apiResponse.model';
import { CollectionResult } from '../models/collectionResult.model';
import { AccountModel } from '../models/account.model';
import { Observable } from 'rxjs';
import { RoleModel } from '../models/roleModel.model';
import { SalaryModel } from '../models/salaryModel.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AccountsService {

  origin = !environment.production ? 'https://apitimesheetzpi.azurewebsites.net' : '';

  constructor(private http: HttpClient) { }

  getAccounts(
    { pageSize = '', currentPage = '', sort = '' }:
    { pageSize?: string; currentPage?: string; sort?: string; } = {}):
    Observable<ApiResponse<CollectionResult<AccountModel>>> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
        'Authorization': 'Bearer ' + sessionStorage.getItem('user')
      })
    };
    return this.http.get<ApiResponse<CollectionResult<AccountModel>>>(
      this.origin +
    '/accounts/'
    // + pageSize ? '?pageSize=' + pageSize : ''
    // + currentPage ? '&currentPage=' + currentPage : ''
    // + sort ? '&sort=' + sort : ''
    , httpOptions);
  }

  postAccounts(account: AccountModel): Observable<ApiResponse<string>> {
    const requestBody: AccountModel = account;
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
        'Authorization': 'Bearer ' + sessionStorage.getItem('user')
      })
    };
    return this.http.post<ApiResponse<string>>(
      this.origin +
      '/accounts', requestBody, httpOptions);
  }

  authorize(user: {userName: string, password: string}): Observable<ApiResponse<string>> {
    const requestBody: {userName: string, password: string} = user;
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
        'Authorization': 'Bearer ' + sessionStorage.getItem('user')
      })
    };
    return this.http.post<ApiResponse<string>>(
      this.origin +
      '/accounts/authorize', requestBody);
  }

  getUser(userId: string): Observable<ApiResponse<AccountModel>> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
        'Authorization': 'Bearer ' + sessionStorage.getItem('user')
      })
    };
    return this.http.get<ApiResponse<AccountModel>>(
      this.origin +
      '/accounts/' + userId, httpOptions);
  }

  getUserRoles(
    userId: string,
    { pageSize = '', currentPage = '', sort = '' }:
    { pageSize?: string; currentPage?: string; sort?: string; } = {}):
  Observable<ApiResponse<RoleModel>> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
        'Authorization': 'Bearer ' + sessionStorage.getItem('user')
      })
    };
    return this.http.get<ApiResponse<RoleModel>>(
      this.origin +
      '/accounts/' + userId +
    '/roles?PageSize=' + pageSize +
    '&CurrentPage=' + currentPage +
    '&Sort=' + sort, httpOptions) ;
  }

  addUserRoles(userId: string, rolesIds: string[]): Observable<ApiResponse<string>> {
    const requestBody = rolesIds;
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
        'Authorization': 'Bearer ' + sessionStorage.getItem('user')
      })
    };
    return this.http.post<ApiResponse<string>>(
      this.origin +
      '/accounts/' + userId +
    '/roles', requestBody, httpOptions);
  }

  editUserRoles(userId: string, rolesIds: string[]): Observable<ApiResponse<string>> {
    const requestBody = rolesIds;
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
        'Authorization': 'Bearer ' + sessionStorage.getItem('user')
      })
    };
    return this.http.put<ApiResponse<string>>(
      this.origin +
      '/accounts/' + userId +
    '/roles', requestBody, httpOptions);
  }

  getRoles(
    { pageSize = '', currentPage = '', sort = '' }:
    { pageSize?: string; currentPage?: string; sort?: string; } = {}):
    Observable<ApiResponse<CollectionResult<RoleModel>>> {
      const httpOptions = {
        headers: new HttpHeaders({
          'Content-Type':  'application/json',
          'Authorization': 'Bearer ' + sessionStorage.getItem('user')
        })
      };
      return this.http.get<ApiResponse<CollectionResult<RoleModel>>>(
        this.origin +
        '/roles?PageSize=' + pageSize +
        '&CurrentPage=' + currentPage +
        '&Sort=' + sort, httpOptions);
  }

  getUserSalaries(
    userId: string,
    { pageSize = '', currentPage = '', sort = '' }:
    { pageSize?: string; currentPage?: string; sort?: string; } = {}):
    Observable<ApiResponse<CollectionResult<SalaryModel>>>  {
      const httpOptions = {
        headers: new HttpHeaders({
          'Content-Type':  'application/json',
          'Authorization': 'Bearer ' + sessionStorage.getItem('user')
        })
      };
      return this.http.get<ApiResponse<CollectionResult<SalaryModel>>>(
        this.origin +
        '/accounts/' + userId +'/salaries?PageSize=' + pageSize +
        '&CurrentPage=' + currentPage +
        '&Sort=' + sort, httpOptions);
  }

  addUserSalaries(userId: string, salary: SalaryModel): Observable<ApiResponse<string>> {
    const requestBody = salary;
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
        'Authorization': 'Bearer ' + sessionStorage.getItem('user')
      })
    };
    return this.http.post<ApiResponse<string>>(
      this.origin +
      '/accounts/' + userId + '/salaries', requestBody, httpOptions );
  }

  editUserSalaries(userId: string, salary: SalaryModel): Observable<ApiResponse<string>> {
    const requestBody = salary;
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
        'Authorization': 'Bearer ' + sessionStorage.getItem('user')
      })
    };
    return this.http.put<ApiResponse<string>>(
      this.origin +
      '/accounts/' + userId + '/salaries', requestBody, httpOptions );
  }

  getUserSalary(userId: string, salaryId: string): Observable<ApiResponse<SalaryModel>> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
        'Authorization': 'Bearer ' + sessionStorage.getItem('user')
      })
    };
    return this.http.get<ApiResponse<SalaryModel>>(
      this.origin +
      '/accounts/' + userId + '/salaries/' + salaryId, httpOptions);
  }

  deleteUserSalary(userId: string, salaryId: string): Observable<ApiResponse<string>> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
        'Authorization': 'Bearer ' + sessionStorage.getItem('user')
      })
    };
    return this.http.delete<ApiResponse<string>>(
      this.origin +
      '/accounts/' + userId + '/salaries/' + salaryId, httpOptions);
  }

  getUserCurrentSalary(userId: string): Observable<ApiResponse<SalaryModel>> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
        'Authorization': 'Bearer ' + sessionStorage.getItem('user')
      })
    };
    return this.http.get<ApiResponse<SalaryModel>>(
      this.origin +
      '/accounts/' + userId + '/salaries/current', httpOptions);
  }

  getUserLastSalary(userId: string): Observable<ApiResponse<SalaryModel>> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
        'Authorization': 'Bearer ' + sessionStorage.getItem('user')
      })
    };
    return this.http.get<ApiResponse<SalaryModel>>(
      this.origin +
      '/accounts/' + userId + '/salaries/last', httpOptions);
  }
}
