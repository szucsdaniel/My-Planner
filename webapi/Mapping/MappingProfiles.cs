using AutoMapper;
using webapi.Mapping.GetDTOs;
using webapi.Mapping.PostDTOs;
using webapi.Mapping.PutDTOs;
using webapi.Models;

namespace webapi.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            // Configuring mapping for GET methods
            CreateMap<Project, ProjectListDTO>();
            CreateMap<Project, ProjectDetailDTO>();
            CreateMap<Branch, BranchGetListDTO>();
            CreateMap<Branch, BranchGetDetailDTO>();
            CreateMap<SubTask, SubTaskGetListDTO>();
            CreateMap<SubTask, SubTaskGetDetailDTO>();
            CreateMap<Assignee, AssigneeGetListDTO>();

            // Configuring mapping for POST methods
            CreateMap<ProjectPostDTO, Project>();
            CreateMap<BranchPostDTO, Branch>();
            CreateMap<SubTaskPostDTO, SubTask>();
            CreateMap<AssigneePostDTO, Assignee>();

            // Configuring mapping for PUT methods
            CreateMap<ProjectPutDTO, Project>();
            CreateMap<ProjectAssignPutDTO, Project>();
            CreateMap<BranchPutDTO, Branch>();
            CreateMap<SubTaskPutDTO, SubTask>();
            CreateMap<SubTaskAssignPutDTO, SubTask>();
            CreateMap<AssigneePutDTO, Assignee>();
            CreateMap<AssignPersonPutDTO, Assignee>();
        }
    }
}
