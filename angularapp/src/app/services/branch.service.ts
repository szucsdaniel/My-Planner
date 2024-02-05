import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { BranchDetail, BranchPostDTO, BranchPutDTO } from './branch-dtos';

@Injectable({
  providedIn: 'root'
})
export class BranchService {

  constructor(private http: HttpClient){
  }

  getBranchById(id: number): Observable<BranchDetail>{
    return this.http.get<BranchDetail>('/api/Branch/' + id)
      .pipe(
        catchError((error: HttpErrorResponse) => {
          return throwError(error.message);
        }
      ));
  }
  addBranch(branch: BranchPostDTO): Observable<string>{
    return this.http.post<string>('/api/Branch', branch)
    .pipe(
      catchError((error: HttpErrorResponse) => {
        return throwError(error.message);
      }
    ));
  }
  changeBranch(branch: BranchPutDTO): Observable<string>{
    return this.http.put<string>('api/Branch', branch)
    .pipe(
      catchError((error: HttpErrorResponse) => {
        return throwError(error.message);
      }
    ));
  }
  deleteBranchById(id: number): Observable<string>{
    return this.http.delete<string>('api/Branch/'+ id)
    .pipe(
      catchError((error: HttpErrorResponse) => {
        return throwError(error.message);
      }
    ));
  }
}
