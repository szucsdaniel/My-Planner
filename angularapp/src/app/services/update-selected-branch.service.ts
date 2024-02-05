import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { BranchList } from './project-dtos';

@Injectable({
  providedIn: 'root'
})
export class UpdateSelectedBranchService {
  private selectedBranchSubject = new BehaviorSubject<BranchList | null>(null);
  selectedBranch$ = this.selectedBranchSubject.asObservable();

  setSelectedBranch(branch: BranchList | null): void {
    this.selectedBranchSubject.next(branch);
  }
  constructor() { }
}
