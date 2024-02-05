using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using webapi.Data_Access_Layer;
using webapi.Exceptions;
using webapi.Mapping.GetDTOs;
using webapi.Mapping.PostDTOs;
using webapi.Mapping.PutDTOs;
using webapi.Models;
using Microsoft.EntityFrameworkCore;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private ProjectRepository _repository;
        private IMapper _mapper;
        public ProjectController(MyPlannerDbContext context, IMapper mapper) 
        {
            _repository = new ProjectRepository(context);
            _mapper = mapper;
        }
        /// <summary>
        /// Retrieves all projects without the specific details of the entities connected to it
        /// </summary>
        /// <returns>The list of existing projects</returns>
        [HttpGet]
        public async Task<ActionResult<List<ProjectListDTO>>> GetProjects()
        {
            var projects = await _repository.GetAll<Project>();
            return Ok(projects.Select(p => _mapper.Map<ProjectListDTO>(p)));
        }
        /// <summary>
        /// Retrieves a specific project with all the records connected to it by id
        /// </summary>
        /// <param name="id">The ID of the project</param>
        /// <returns>The project with the matching ID and all the Assignees, Branches and Subtasks connected to it.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDetailDTO>> GetProjectById(int id)
        {
            try
            {
                var project = await _repository.GetById<Project>(id,
                    p => p.Assignees,
                    b => b.Branches);
                return Ok(_mapper.Map<ProjectDetailDTO>(project));
            }
            catch(EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
        /// <summary>
        /// Adds a new project to the database
        /// </summary>
        /// <param name="project">The properties of the project are given here</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> AddProject([FromBody] ProjectPostDTO project)
        {
            try
            {
                await _repository.Add(_mapper.Map<Project>(project));
                return Ok("Project successfully added");
            }
            catch(PropertyNotUniqueException)
            {
                return Conflict("Name of Project is not unique");
            }
            catch(ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut]
        public async Task<ActionResult> UpdateProject([FromBody] ProjectPutDTO project)
        {
            try
            {
                await _repository.Update(_mapper.Map<Project>(project));
                return Ok("Project successfully updated");
            }
            catch(EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (PropertyNotUniqueException)
            {
                return Conflict("Name of Project is not unique");
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("assign")]
        public async Task<ActionResult> AssignPerson([FromBody] ProjectAssignPutDTO project)
        {
            try
            {
                await _repository.AssignPeople(_mapper.Map<Project>(project));
                return Ok("Assignees successfully changed");
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProject(int id)
        {
            try
            {
                await _repository.Delete<Project>(id);
                return Ok("Project successfully deleted");
            }
            catch(EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost("nameUnique")]
        public ActionResult<bool> IsNameUnique([FromBody] string name)
        {
            if (_repository.IsNameUnique<Project>(name))
            {
                return Ok(true);
            }
            else
            {
                return BadRequest(false);
            }
        }
    }
}
