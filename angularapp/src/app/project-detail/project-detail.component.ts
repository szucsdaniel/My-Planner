import { Component, ElementRef } from '@angular/core';
import { BranchList, ProjectDetail, ProjectList } from '../services/project-dtos';
import { ProjectServiceService } from '../services/project-service.service';
import { UpdateSelectedProjectService } from '../services/update-selected-project.service';
import { UpdateSelectedBranchService } from '../services/update-selected-branch.service';

@Component({
  selector: 'app-project-detail',
  templateUrl: './project-detail.component.html',
  styleUrls: ['./project-detail.component.css']
})
export class ProjectDetailComponent {
  selectedProject: ProjectDetail | null = null;
  selectedBranch: BranchList | null = null;

  constructor(private projectService: ProjectServiceService, 
      private selectedProjectService: UpdateSelectedProjectService,
      private selectedBranchService: UpdateSelectedBranchService,
      private el: ElementRef){
  }
  ngOnInit(): void {
    this.selectedProjectService.selectedProject$.subscribe((selectedProject)  => {
      if(selectedProject)
      this.projectService.getProjectById(selectedProject.id).subscribe(
        (data : ProjectDetail) =>{
          this.selectedProject = data;
        }
      );
    });
    this.selectedBranchService.selectedBranch$.subscribe((selectedBranch) => {
      if(selectedBranch){
        this.selectedBranch = selectedBranch;
      }
      else{
        this.selectedBranch = null;
      }
    })
  }
  getAssigneeNames(project:ProjectDetail): string{
    let names  = "";
    project.assignees?.forEach((assignee)=> names += `${assignee.name}, `);
    names = names.substring(0, names.length-2);

    return names;
  }
}
