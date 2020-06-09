import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthService } from '../shared/auth.service';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/apiResponse.model';
import { CollectionResult } from '../models/collectionResult.model';
import { ProjectModel } from '../models/project.model';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  origin = "https://apitimesheetzpi.azurewebsites.net";

  constructor(private http: HttpClient, private authService: AuthService) {}

  getProjects({
    pageSize = "",
    currentPage = "",
    sort = "",
  }: {
    pageSize?: string;
    currentPage?: string;
    sort?: string;
  } = {}): Observable<ApiResponse<CollectionResult<ProjectModel>>> {
    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + this.authService.getAccessToken(),
      }),
    };
    return this.http.get<ApiResponse<CollectionResult<ProjectModel>>>(
      this.origin + "/projects/",
      // + pageSize ? '?pageSize=' + pageSize : ''
      // + currentPage ? '&currentPage=' + currentPage : ''
      // + sort ? '&sort=' + sort : ''
      httpOptions
    );
  }

  postProjects(project: any): Observable<ApiResponse<string>> {
    const requestBody = project;
    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + this.authService.getAccessToken(),
      }),
    };
    return this.http.post<ApiResponse<string>>(
      this.origin + "/projects",
      requestBody,
      httpOptions
    );
  }

  deleteProjects(id: any): Observable<ApiResponse<string>> {
    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + this.authService.getAccessToken(),
      }),
    };
    return this.http.delete<ApiResponse<string>>(
      this.origin + "/projects/" + id,
      httpOptions
    );
  }

  putProjects(project: any): Observable<ApiResponse<string>> {
    const requestBody = project;
    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + this.authService.getAccessToken(),
      }),
    };
    return this.http.put<ApiResponse<string>>(
      this.origin + "/projects",
      requestBody,
      httpOptions
    );
  }

}
