using Microsoft.EntityFrameworkCore;
using webapi.Exceptions;
using webapi.Models;

namespace webapi.Data_Access_Layer
{
    public class SubTaskRepository : BaseRepository
    {
        public SubTaskRepository(MyPlannerDbContext context) : base(context) { }

        public async Task AssignPeople(SubTask updatedSubtask)
        {
            try
            {
                var subtaskToUpdate = await GetById<SubTask>(updatedSubtask.Id, p => p.Assignees);
                var assignees = new List<Assignee>();
                foreach (var assignee in updatedSubtask.Assignees)
                {
                    assignees.Add(await GetById<Assignee>(assignee.Id));
                }
                subtaskToUpdate.Assignees = assignees;
                _context.Entry(subtaskToUpdate).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (EntityNotFoundException) { throw; }
        }
    }
}

