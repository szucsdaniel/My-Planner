using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using webapi.Data_Access_Layer;
using webapi.Exceptions;
using webapi.Mapping.GetDTOs;
using webapi.Mapping.PostDTOs;
using webapi.Mapping.PutDTOs;
using webapi.Models;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AssigneeController : ControllerBase
    {
        private BaseRepository _repository;
        private IMapper _mapper;

        public AssigneeController(MyPlannerDbContext context, IMapper mapper)
        {
            _repository = new BaseRepository(context);
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<List<AssigneeGetListDTO>>> GetAssignees()
        {
            var assignees = await _repository.GetAll<Assignee>();
            return Ok(assignees.Select(a => _mapper.Map<AssigneeGetListDTO>(a)));
        }

        [HttpPost]
        public async Task<ActionResult> AddAssignee([FromBody] AssigneePostDTO assignee)
        {
            try
            {
                await _repository.Add(_mapper.Map<Assignee>(assignee));
                return Ok("Assignee successfully added");
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAssignee([FromBody] AssigneePutDTO assignee)
        {
            try
            {
                await _repository.Update(_mapper.Map<Assignee>(assignee));
                return Ok("Assignee successfully updated");
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAssignee(int id)
        {
            try
            {
                await _repository.Delete<Assignee>(id);
                return Ok("Assignee successfully deleted");
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
