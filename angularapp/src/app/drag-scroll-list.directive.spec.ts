import { ElementRef, Renderer2, inject } from '@angular/core';
import { DragScrollListDirective } from './drag-scroll-list.directive';
import { UpdateSelectedProjectService } from './services/update-selected-project.service';
import { UpdateSelectedBranchService } from './services/update-selected-branch.service';
import { ProjectServiceService } from './services/project-service.service';

describe('DragScrollListDirective', () => {
  it('should create an instance', inject(() => (elemenRef: ElementRef, renderer: Renderer2
      , selectedProjectService: UpdateSelectedProjectService
      , selectedBranchService: UpdateSelectedBranchService
      , projectService: ProjectServiceService) => {
    const directive = new DragScrollListDirective(elemenRef, renderer, selectedProjectService, selectedBranchService, projectService);
    expect(directive).toBeTruthy();
  }));
});
