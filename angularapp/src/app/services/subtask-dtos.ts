export interface SubtaskDetail{
    id: number
    assignees: Assignee[] | null
}
export interface Assignee{
    id: number,
    name: string
}