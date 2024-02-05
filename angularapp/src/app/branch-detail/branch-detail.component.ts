import { Component } from '@angular/core';
import { UpdateSelectedBranchService } from '../services/update-selected-branch.service';
import { BranchList } from '../services/project-dtos';
import { BranchService } from '../services/branch.service';
import { HttpResponseMessageService } from '../services/http-response-message.service';
import { BranchDetail, FormattedSubtaskList, SubtaskList } from '../services/branch-dtos';
import { catchError } from 'rxjs';

@Component({
  selector: 'app-branch-detail',
  templateUrl: './branch-detail.component.html',
  styleUrls: ['./branch-detail.component.css']
})
export class BranchDetailComponent {
  branch: BranchDetail | null = null;
  subtasks: FormattedSubtaskList[] | null = null;

  constructor(private branchselectedService: UpdateSelectedBranchService, private branchService: BranchService
    , private httpResponseService: HttpResponseMessageService ){}

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
              status: subtask.status
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
}
