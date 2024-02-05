import { Injectable } from '@angular/core';
import { IValidate } from './IValidate';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import { ProjectAssignDTO, ProjectDetail, ProjectList, ProjectPostDTO, ProjectPutDTO } from './project-dtos';

@Injectable({
  providedIn: 'root'
})
export class ProjectServiceService implements IValidate{

  constructor(private http : HttpClient) { }

  validateName(nameToValidate: string ): Observable<boolean>
  {
    return this.http.post<boolean>('api/Project/validate', nameToValidate);
  }

  getProjects(): Observable<ProjectList[]>{
    return this.http.get<ProjectList[]>('/api/Project');
  }
  getProjectById(id: number): Observable<ProjectDetail>{
    return this.http.get<ProjectDetail>('/api/Project/' + id)
      .pipe(
        catchError((error: HttpErrorResponse) => {
          return throwError(error.message);
        }
      ));
  }
  addProject(project: ProjectPostDTO): Observable<string>{
    return this.http.post<string>('/api/Project', project)
    .pipe(
      catchError((error: HttpErrorResponse) => {
        return throwError(error.message);
      }
    ));
  }
  changeProject(project: ProjectPutDTO): Observable<string>{
    return this.http.put<string>('api/Project', project)
    .pipe(
      catchError((error: HttpErrorResponse) => {
        return throwError(error.message);
      }
    ));
  }
  deleteProjectById(id: number): Observable<string>{
    return this.http.delete<string>('api/Project/'+ id)
    .pipe(
      catchError((error: HttpErrorResponse) => {
        return throwError(error.message);
      }
    ));
  }
  assignPeople(assignedProject: ProjectAssignDTO): Observable<string>{
    return this.http.put<string>('api/Project/assign', assignedProject)
    .pipe(
      catchError((error: HttpErrorResponse) => {
        return throwError(error.message);
      }
    ));
  }
}
