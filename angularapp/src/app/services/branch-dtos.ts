export interface BranchDetail{
    id: number,
    name: string,
    projectId: number,
    subtasks: SubtaskList[]
}
export interface SubtaskList{
    id: number,
    name: string,
    deadline: Date,
    status: Status
}
export interface FormattedSubtaskList{
    id: number,
    name: string,
    deadline: string,
    status: Status
}
export enum Status{
    WAITING, IN_PROGRESS, FINISHED
}
export interface BranchPostDTO{
    id: number,
    name: string,
    projectId: number
}
export interface BranchPutDTO{
    id: number,
    name: string,
}