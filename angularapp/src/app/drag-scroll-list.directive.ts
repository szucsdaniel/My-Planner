import { Directive, ElementRef, HostListener, Input, Renderer2 } from '@angular/core';
import { UpdateSelectedProjectService } from './services/update-selected-project.service';
import { UpdateSelectedBranchService } from './services/update-selected-branch.service';
import { BranchList, ProjectDetail } from './services/project-dtos';
import { ProjectServiceService } from './services/project-service.service';

@Directive({
  selector: '[appDragScrollList]'
})
export class DragScrollListDirective{
  private selectedBranchPosition = 0;
  private branches: BranchList[] = [];
  
  private isDragging = false;
  private initialX = 0;
  private initialScrollLeft = 0;

  private transitionDuration = 100;

  private scrollInterval: any;

  @Input() gapInPx: number = 0;

  constructor(private el:ElementRef, private renderer: Renderer2
      , private selectedProjectService: UpdateSelectedProjectService
      , private selectedBranchService: UpdateSelectedBranchService
      , private projectService: ProjectServiceService){

    this.selectedProjectService.selectedProject$.subscribe((selectedProject)  => {
      if(selectedProject){
        this.el.nativeElement.scrollLeft = 0;
        this.selectedBranchPosition = 0;
        this.projectService.getProjectById(selectedProject.id).subscribe(
          (data : ProjectDetail) =>{
            this.branches = data.branches;
            this.selectBranchIdFromList();
          }
        );
      }
    });
  }

  @HostListener('mousedown', ['$event'])
  onMouseDown(event: MouseEvent){
    this.isDragging = true;
    this.initialScrollLeft = this.el.nativeElement.scrollLeft;
    this.initialX = event.clientX;
    
    //this.renderer.addClass(this.el.nativeElement, 'grabbing');
  }

  @HostListener('document:mousemove', ['$event'])
  onMouseMove(event: MouseEvent){
    if (this.isDragging) {
      const deltaX = (event.clientX - this.initialX) * -1 + this.initialScrollLeft;
      this.el.nativeElement.scrollLeft = deltaX;
    }
  }

  @HostListener('document:mouseup', ['$event'])
  onMouseUp(event: MouseEvent){
    if(this.isDragging){
      this.isDragging = false;
      
      if(this.el.nativeElement.firstElementChild){
        const targetScrollLeft = this.calculateTargetScroll();
        this.snap(targetScrollLeft);
      }

      this.initialScrollLeft = this.el.nativeElement.scrollLeft;
    }
    //this.renderer.removeClass(this.el.nativeElement, 'grabbing');
  }

  @HostListener('window:resize', ['$event'])
  onResize() {
    this.el.nativeElement.scrollLeft =
      this.selectedBranchPosition * (this.el.nativeElement.firstElementChild.clientWidth + this.gapInPx);
  }

  // css scroll-snapping can not be used because scrollLeft value isn't updated until it reaches a threshold and then it is snapped
  // it looks too blocky
  snap(targetScrollLeft: number){
    const startScrollLeft = this.el.nativeElement.scrollLeft;
    const interval = 10; //10ms interval
    const steps = Math.ceil(this.transitionDuration/interval);

    let currentScrollLeft = startScrollLeft;

    if(startScrollLeft < targetScrollLeft){
      this.scrollInterval = setInterval(()=>{
        if (currentScrollLeft < targetScrollLeft) {
          currentScrollLeft += Math.ceil((targetScrollLeft - currentScrollLeft) / steps);
          this.renderer.setProperty(this.el.nativeElement, 'scrollLeft', currentScrollLeft);
        } 
        else {
          this.el.nativeElement.scrollLeft = targetScrollLeft;
          clearInterval(this.scrollInterval);
        }
      }, interval);
    }
    else{
      this.scrollInterval = setInterval(()=>{
        if (currentScrollLeft > targetScrollLeft) {
          currentScrollLeft -= Math.ceil((currentScrollLeft - targetScrollLeft) / steps);
          this.renderer.setProperty(this.el.nativeElement, 'scrollLeft', currentScrollLeft);
        } 
        else {
          this.el.nativeElement.scrollLeft = targetScrollLeft;
          clearInterval(this.scrollInterval);
        }
      }, interval);
    }
  }
  
  calculateTargetScroll(): number{
    const oldBranchPosition = this.selectedBranchPosition;

    let parent = this.el.nativeElement;
    let child = parent.firstElementChild;

    const closestElement = Math.round((parent.scrollLeft - this.initialScrollLeft)/child.clientWidth);

    // calculating the scrollLeft value
    let targetScrollLeft = (closestElement + this.selectedBranchPosition) * (child.clientWidth + this.gapInPx);

    // the last elements position
    const lastElementScrollLeft = (parent.children.length - 1) * (child.clientWidth + this.gapInPx);

    // If the scrolling there's a branch on the we update the position number. Otherwise it remains the same.
    if(targetScrollLeft > lastElementScrollLeft){
      targetScrollLeft = lastElementScrollLeft;
    }
    else{
      this.selectedBranchPosition =  oldBranchPosition + closestElement;
    }
    // If the position changed a new branch is selected.
    if(oldBranchPosition != this.selectedBranchPosition)
    {
      this.selectBranchIdFromList();
    }

    return targetScrollLeft;
  }

  // This function selects the branch.
  selectBranchIdFromList(){
    if(this.branches){
      this.selectedBranchService.setSelectedBranch(this.branches[this.selectedBranchPosition]);
    }
    else{
      this.selectedBranchService.setSelectedBranch(null);
    }
  }
}
