import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { ProjectList } from './project-dtos';

@Injectable({
  providedIn: 'root'
})
export class UpdateSelectedProjectService {
  
  private selectedProjectSubject = new BehaviorSubject<ProjectList | null>(null);
  selectedProject$ = this.selectedProjectSubject.asObservable();

  setSelectedProject(project: ProjectList): void {
    this.selectedProjectSubject.next(project);
  }
  constructor() { }
}
