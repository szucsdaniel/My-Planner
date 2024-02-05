using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.IdentityModel.Tokens;
using webapi;
using webapi.Models;

namespace MyPlanner_wepapi_Test
{
    public class BaseTest : IDisposable 
    {
        public readonly MyPlannerDbContext Context;
        // The constructor creates the DbContext and the database
        public BaseTest()
        {
            var options = new DbContextOptionsBuilder<MyPlannerDbContext>()
            .UseInMemoryDatabase("TestMyPlannerDb")
            .Options;

            Context = new MyPlannerDbContext(options);

            Context.Database.EnsureCreated();
            InitializeDatabase();
        }
        // This method initializes the database with mock data
        public void InitializeDatabase()
        {
            var project1 = new Project() { Name = "My planner" };
            var branch1 = new Branch() { Name = "Backend" };
            var branch2 = new Branch() { Name = "Frontend" };
            var subTask = new SubTask() {  Name = "Creating tests", Deadline = DateTime.UtcNow, Status = Status.IN_PROGRESS };
            var assignee = new Assignee() { Name = "John Doe" };

            Context.Projects.Add(project1);
            Context.SaveChanges();

            Context.Entry<Project>(project1).Entity.Branches.Add(branch1);
            Context.Entry<Project>(project1).Entity.Branches.Add(branch2);
            Context.SaveChanges();

            Context.Entry<Branch>(branch1).Entity.Subtasks.Add(subTask);
            Context.SaveChanges();

            Context.Entry<Project>(project1).Entity.Assignees.Add(assignee);
            Context.Entry<SubTask>(subTask).Entity.Assignees.Add(assignee);
            Context.SaveChanges();

        }
        // This method is automatically called after all the tests are finished
        public void Dispose()
        {
            Context.Dispose();
        }
    }
}