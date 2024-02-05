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
    [Route("api/[controller]")]
    public class SubTaskController: ControllerBase
    {
        private SubTaskRepository _repository;
        private IMapper _mapper;

        public SubTaskController(MyPlannerDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _repository = new SubTaskRepository(context);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SubTaskGetDetailDTO>> GetSubTaskById(int id)
        {
            try
            {
                var subtask = await _repository.GetById<SubTask>(id,
                    b => b.Assignees);

                return Ok(_mapper.Map<SubTaskGetDetailDTO>(subtask));
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddSubTask([FromBody] SubTaskPostDTO subtask)
        {
            try
            {
                await _repository.Add(_mapper.Map<SubTask>(subtask));
                return Ok("SubTask successfully added");
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
            catch (NonExcistentForeignKeyException)
            {
                return BadRequest("The given Branch ID does not exist");
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateSubTask([FromBody] SubTaskPutDTO subtask)
        {
            try
            {

                var subtaskToUpdate = await _repository.GetById<SubTask>(subtask.Id);
                var updatedSubtask = _mapper.Map<SubTask>(subtask);
                updatedSubtask.BranchId = subtaskToUpdate.BranchId;

                await _repository.Update(updatedSubtask);
                return Ok("SubTask successfully updated");
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

        [HttpPut("assign")]
        public async Task<ActionResult> AssignPerson([FromBody] SubTaskAssignPutDTO subtask)
        {
            try
            {
                await _repository.AssignPeople(_mapper.Map<SubTask>(subtask));
                return Ok("Assignees successfully changed");
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSubTask(int id)
        {
            try
            {
                await _repository.Delete<SubTask>(id);
                return Ok("SubTask successfully deleted");
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
