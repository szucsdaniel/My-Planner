export interface ProjectList{
id: number,
name: string
}
export interface ProjectDetail{
id: number,
name: string,
description?: string,
branches: BranchList[],
assignees: AssigneeList[]
}
export interface BranchList{
id: number,
name: string
}
export interface AssigneeList{
id: number,
name: string
}
export interface ProjectPostDTO{
name: string,
description?: string
}
export interface ProjectPutDTO{
id: number,
name: string
description?: string
}
export interface ProjectAssignDTO{
projectId: number,
assignees: number[]
}