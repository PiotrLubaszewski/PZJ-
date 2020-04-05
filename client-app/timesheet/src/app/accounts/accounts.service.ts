import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
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

  origin = !environment.production ? 'http://apitimesheetzpi.azurewebsites.net' : '';

  constructor(private http: HttpClient) { }

  getAccounts(
    { pageSize = '', currentPage = '', sort = '' }:
    { pageSize?: string; currentPage?: string; sort?: string; } = {}):
    Observable<ApiResponse<CollectionResult<AccountModel>>> {
    return this.http.get<ApiResponse<CollectionResult<AccountModel>>>(
      this.origin +
    '/accounts/?PageSize=' + pageSize +
    '&CurrentPage=' + currentPage +
    '&Sort=' + sort);
  }

  postAccounts(account: AccountModel): Observable<ApiResponse<string>> {
    const requestBody: AccountModel = account;
    return this.http.post<ApiResponse<string>>(
      this.origin +
      '/accounts', requestBody);
  }

  authorize(user: {userName: string, password: string}): Observable<ApiResponse<string>> {
    const requestBody: {userName: string, password: string} = user;
    return this.http.post<ApiResponse<string>>(
      this.origin +
      '/accounts/authorize', requestBody);
  }

  getUser(userId: string): Observable<ApiResponse<AccountModel>> {
    return this.http.get<ApiResponse<AccountModel>>(
      this.origin +
      '/accounts/' + userId);
  }

  getUserRoles(
    userId: string,
    { pageSize = '', currentPage = '', sort = '' }:
    { pageSize?: string; currentPage?: string; sort?: string; } = {}):
  Observable<ApiResponse<RoleModel>> {
    return this.http.get<ApiResponse<RoleModel>>(
      this.origin +
      '/accounts/' + userId +
    '/roles?PageSize=' + pageSize +
    '&CurrentPage=' + currentPage +
    '&Sort=' + sort) ;
  }

  addUserRoles(userId: string, rolesIds: string[]): Observable<ApiResponse<string>> {
    const requestBody = rolesIds;
    return this.http.post<ApiResponse<string>>(
      this.origin +
      '/accounts/' + userId +
    '/roles', requestBody);
  }

  editUserRoles(userId: string, rolesIds: string[]): Observable<ApiResponse<string>> {
    const requestBody = rolesIds;
    return this.http.put<ApiResponse<string>>(
      this.origin +
      '/accounts/' + userId +
    '/roles', requestBody);
  }

  getRoles(
    { pageSize = '', currentPage = '', sort = '' }:
    { pageSize?: string; currentPage?: string; sort?: string; } = {}):
    Observable<ApiResponse<CollectionResult<RoleModel>>> {
      return this.http.get<ApiResponse<CollectionResult<RoleModel>>>(
        this.origin +
        '/roles?PageSize=' + pageSize +
        '&CurrentPage=' + currentPage +
        '&Sort=' + sort);
  }

  getUserSalaries(
    userId: string,
    { pageSize = '', currentPage = '', sort = '' }:
    { pageSize?: string; currentPage?: string; sort?: string; } = {}):
    Observable<ApiResponse<CollectionResult<SalaryModel>>>  {
      return this.http.get<ApiResponse<CollectionResult<SalaryModel>>>(
        this.origin +
        '/accounts/' + userId +'/salaries?PageSize=' + pageSize +
        '&CurrentPage=' + currentPage +
        '&Sort=' + sort);
  }

  addUserSalaries(userId: string, salary: SalaryModel): Observable<ApiResponse<string>> {
    const requestBody = salary;
    return this.http.post<ApiResponse<string>>(
      this.origin +
      '/accounts/' + userId + '/salaries', requestBody );
  }

  editUserSalaries(userId: string, salary: SalaryModel): Observable<ApiResponse<string>> {
    const requestBody = salary;
    return this.http.put<ApiResponse<string>>(
      this.origin +
      '/accounts/' + userId + '/salaries', requestBody );
  }

  getUserSalary(userId: string, salaryId: string): Observable<ApiResponse<SalaryModel>> {
    return this.http.get<ApiResponse<SalaryModel>>(
      this.origin +
      '/accounts/' + userId + '/salaries/' + salaryId);
  }

  deleteUserSalary(userId: string, salaryId: string): Observable<ApiResponse<string>> {
    return this.http.delete<ApiResponse<string>>(
      this.origin +
      '/accounts/' + userId + '/salaries/' + salaryId);
  }

  getUserCurrentSalary(userId: string): Observable<ApiResponse<SalaryModel>> {
    return this.http.get<ApiResponse<SalaryModel>>(
      this.origin +
      '/accounts/' + userId + '/salaries/current');
  }

  getUserLastSalary(userId: string): Observable<ApiResponse<SalaryModel>> {
    return this.http.get<ApiResponse<SalaryModel>>(
      this.origin +
      '/accounts/' + userId + '/salaries/last');
  }
}