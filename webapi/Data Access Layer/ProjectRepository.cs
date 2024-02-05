using Microsoft.EntityFrameworkCore;
using webapi.Exceptions;
using webapi.Models;

namespace webapi.Data_Access_Layer
{
    public class ProjectRepository : BaseRepository
    {
        public ProjectRepository(MyPlannerDbContext context): base(context){}

        public async Task AssignPeople(Project updatedProject)
        {
            try
            {
                var projectToUpdate = await GetById<Project>(updatedProject.Id, p => p.Assignees);
                var assignees = new List<Assignee>();
                foreach(var assignee in updatedProject.Assignees)
                {
                    assignees.Add(await GetById<Assignee>(assignee.Id));
                }
                projectToUpdate.Assignees = assignees;
                _context.Entry(projectToUpdate).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (EntityNotFoundException) { throw; }
        }
    }
}
