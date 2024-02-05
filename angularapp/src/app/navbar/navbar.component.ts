import { Component, HostListener } from '@angular/core';
import { HttpResponseMessageService } from '../services/http-response-message.service';
import { ProjectList } from '../services/project-dtos';
import { ProjectServiceService } from '../services/project-service.service';
import { UpdateSelectedProjectService } from '../services/update-selected-project.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {
  isHovered = false;

  projectList: ProjectList[] = [];
  selectedProject?: ProjectList = undefined;
  constructor(private httpResponseMessageService: HttpResponseMessageService, 
    private projectService: ProjectServiceService,
    private updateSelectedProjectService: UpdateSelectedProjectService) { }

  ngOnInit(){
    this.getProjectList();
  }
  private getProjectList(){
    this.projectService.getProjects(
    ).subscribe(
      (data)=> {
        this.projectList = data;
        if(this.projectList.length != 0){
          this.selectedProject = this.projectList[0];
          this.selectProject(this.selectedProject);
        }
      },
      (error: string) => {
        this.httpResponseMessageService.showResponseMessage(error);
      });
  }
  selectProject(project : ProjectList){
    this.selectedProject = project;
    this.updateSelectedProjectService.setSelectedProject(project);
  }

  @HostListener('mouseenter') onMouseEnter() {
    this.isHovered = true;
  }
  @HostListener('mouseleave') onMouseLeave() {
    this.isHovered = false;
  }
}
