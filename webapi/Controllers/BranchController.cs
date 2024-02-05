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
    public class BranchController : ControllerBase
    {
        private BaseRepository _repository;
        private IMapper _mapper;
        public BranchController(MyPlannerDbContext context, IMapper mapper)
        {
            _repository = new BaseRepository(context);
            _mapper = mapper;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<BranchGetDetailDTO>> GetBranchById(int id)
        {
            try
            {
                var branch = await _repository.GetById<Branch>(id,
                    b => b.Subtasks);

                return Ok(_mapper.Map<BranchGetDetailDTO>(branch));
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> AddBranch([FromBody] BranchPostDTO branch)
        {
            try
            {
                await _repository.Add(_mapper.Map<Branch>(branch));
                return Ok("Branch successfully added");
            }
            catch(ValidationException e)
            {
                return BadRequest(e.Message);
            }
            catch(PropertyNotUniqueException)
            {
                return Conflict("Name of Branch is not unique");
            }
            catch (NonExcistentForeignKeyException)
            {
                return BadRequest("The given Project ID does not exist");
            }
        }
        [HttpPut]
        public async Task<ActionResult> UpdateBranch([FromBody] BranchPutDTO branch)
        {
            try
            {
                var branchToUpdate = await _repository.GetById<Branch>(branch.Id);
                var updatedBranch = _mapper.Map<Branch>(branch);
                updatedBranch.ProjectId = branchToUpdate.ProjectId;

                await _repository.Update(updatedBranch);
                return Ok("Branch successfully updated");
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (PropertyNotUniqueException)
            {
                return Conflict("Name of Branch is not unique");
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBranch(int id)
        {
            try
            {
                await _repository.Delete<Branch>(id);
                return Ok("Branch successfully deleted");
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
        [HttpPost("nameUnique")]
        public ActionResult<bool> IsNameUnique([FromBody]string name)
        {
            if (_repository.IsNameUnique<Branch>(name))
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }
    }
}
