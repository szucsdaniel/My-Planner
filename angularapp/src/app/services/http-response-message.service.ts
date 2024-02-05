import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HttpResponseMessageService {
  
  private httpResponseMessageSubject = new Subject<string>();
  responseMessage$ = this.httpResponseMessageSubject.asObservable();

  showResponseMessage(message: string): void {
    this.httpResponseMessageSubject.next(message);
  }
  constructor() { }
}
