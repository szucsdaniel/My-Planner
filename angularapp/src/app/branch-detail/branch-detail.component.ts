import { Component } from '@angular/core';
import { UpdateSelectedBranchService } from '../services/update-selected-branch.service';
import { BranchList } from '../services/project-dtos';
import { BranchService } from '../services/branch.service';
import { HttpResponseMessageService } from '../services/http-response-message.service';
import { BranchDetail, FormattedSubtaskList, Status, SubtaskList } from '../services/branch-dtos';
import { catchError } from 'rxjs';
import { SubtaskService } from '../services/subtask.service';

@Component({
  selector: 'app-branch-detail',
  templateUrl: './branch-detail.component.html',
  styleUrls: ['./branch-detail.component.css']
})
export class BranchDetailComponent {
  branch: BranchDetail | null = null;
  subtasks: FormattedSubtaskList[] | null = null;
  clickedSubtasks: FormattedSubtaskList[] = [];


  statusColor = {
    [Status.WAITING]: 'rgb(181, 106, 106)',
    [Status.IN_PROGRESS]: 'rgb(78, 130, 189)',
    [Status.FINISHED]: 'grey'
  }; 
  statusName = {
    [Status.WAITING]: 'Waiting',
    [Status.IN_PROGRESS]: 'In progress',
    [Status.FINISHED]: 'Finished'
  };

  constructor(private branchselectedService: UpdateSelectedBranchService, private branchService: BranchService
    , private httpResponseService: HttpResponseMessageService, private subtaskService: SubtaskService){}

  ngOnInit(){
    this.branchselectedService.selectedBranch$.subscribe((branch) =>{
      if(branch != null){
        this.branchService.getBranchById(branch.id).subscribe((data: BranchDetail)=>{
          this.branch = data;
          this.subtasks = [];
          data.subtasks.forEach((subtask: SubtaskList) => {
            let formattedSubtask: FormattedSubtaskList ={
              id: subtask.id,
              name : subtask.name,
              deadline: subtask.deadline.toString().split('T')[0],
              status: subtask.status,
              assignees: null
            }
            this.subtasks?.push(formattedSubtask);
          });
        })
      }
      else{
        this.subtasks = null;
      }
    });
  }
// if a subtask is clicked we try to load the list of assignees connected to it
  clickSubtask(clickedSubtask: FormattedSubtaskList){
    if(!this.clickedSubtasks?.includes(clickedSubtask)){
      if(clickedSubtask.assignees == null){
        this.subtaskService.getSubtaskById(clickedSubtask.id).subscribe((subtask)=>{
          clickedSubtask.assignees = subtask.assignees;
        });
      }
      this.clickedSubtasks?.push(clickedSubtask);
    }
    else{
      this.clickedSubtasks = this.clickedSubtasks?.filter((subtask) => subtask != clickedSubtask);
    }
  }

  getAssigneeNames(subtask:FormattedSubtaskList): string{
    let names  = "";
    subtask.assignees?.forEach((assignee)=> names += `${assignee.name}, `);
    names = names.substring(0, names.length-2);

    return names;
  }
}
