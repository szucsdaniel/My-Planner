import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { SubtaskDetail } from './subtask-dtos';

@Injectable({
  providedIn: 'root'
})
export class SubtaskService {

  constructor(private http: HttpClient) { }

  
  getSubtaskById(id: number): Observable<SubtaskDetail>{
    return this.http.get<SubtaskDetail>('/api/SubTask/' + id)
      .pipe(
        catchError((error: HttpErrorResponse) => {
          return throwError(error.message);
        }
      ));
  }
}
