import { Observable } from "rxjs";

export interface IValidate {
    validateName(nameToValidate: string): Observable<boolean>;
  }