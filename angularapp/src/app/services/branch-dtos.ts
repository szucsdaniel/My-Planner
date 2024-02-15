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
    status: Status,
    assignees: Assignee[] | null
}
export enum Status{
    WAITING, IN_PROGRESS, FINISHED
}
export const StatusColors = {
    [Status.WAITING]: 'red',
    [Status.IN_PROGRESS]: 'blue',
    [Status.FINISHED]: 'grey'
};
export interface BranchPostDTO{
    id: number,
    name: string,
    projectId: number
}
export interface BranchPutDTO{
    id: number,
    name: string,
}
export interface Assignee{
    id: number,
    name: string
}