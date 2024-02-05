using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webapi;
using webapi.Data_Access_Layer;
using webapi.Exceptions;
using webapi.Models;

namespace MyPlanner_wepapi_Test
{
    public class ProjectRespositoryTests : BaseTest, IDisposable
    {
        public BaseRepository Repository;
        public ProjectRespositoryTests() : base()
        {
            Repository = new BaseRepository(Context);
        }

        [Fact]
        public async Task GettingEntityById_Successful()
        {
            var projectToGet = Context.Projects
                .Include(p => p.Assignees)
                .Include(p => p.Branches)
                .First(p => p.Assignees.Count != 0);

            var projectToGetWithRepository =
                await Repository.GetById<Project>(projectToGet.Id,
                p => p.Branches,
                p => p.Assignees);

            Assert.Equal(projectToGet, projectToGetWithRepository);
            Assert.Equal(projectToGetWithRepository.Assignees, projectToGet.Assignees);
            Assert.Equal(projectToGetWithRepository.Branches, projectToGet.Branches);
        }
        [Fact]
        public async Task GettingEntityById_Failed_NonExistentId()
        {
            // The last Id + 1
            int nonExistentId = Context.Projects.ToList().Last().Id + 10;
            await Assert.ThrowsAsync<EntityNotFoundException>(async () => await Repository.GetById<Project>(nonExistentId));
        }

        [Fact]
        public async Task AddingToDatabase_Successful()
        {
            Project testProjectToAdd = new Project()
            {
                Name = "successful test",
                Description = "test description",
            };
            Assert.DoesNotContain(testProjectToAdd, Context.Projects);

            await Repository.Add<Project>(testProjectToAdd);

            Assert.Contains(testProjectToAdd, Context.Projects);
        }
        [Fact]
        public async Task AddingToDatabase_Failed_MissingValues()
        {
            Project testProjectToAdd = new Project();
            var exception = await Assert.ThrowsAsync<ValidationException>(async () => await Repository.Add(testProjectToAdd));
            Assert.Equal("Name is required", exception.Message);
            Assert.DoesNotContain(testProjectToAdd, Context.Projects);
        }
        // This cannot be tested with In Memory Database because it does not enforce the unique constraint of indexes.
        // Works with MSSQL.
        [Fact]
        public async Task AddingToDatabase_Failed_NotUniqueName()
        {
            Project testProjectToAdd1 = new Project()
            {
                Name = "sameName"
            };
            Project testProjectToAdd2 = new Project()
            {
                Name = "sameName"
            };
            await Repository.Add(testProjectToAdd1);
            var exception = await Assert.ThrowsAsync<PropertyNotUniqueException>(async () => await Repository.Add(testProjectToAdd2));
            Assert.Equal("The given Name of Project is not unique", exception.Message);
            Assert.DoesNotContain(testProjectToAdd2, Context.Projects);
        }

        [Fact]
        public async Task UpdatingDatabase_Successful()
        {
            var projectToChange = Context.Projects.First();
            string originalName = $"{projectToChange.Name}";
            var updatedProject = new Project()
            {
                Branches = projectToChange.Branches,
                Assignees = projectToChange.Assignees,
                Name = "newName",
                Id = projectToChange.Id,
                Description = projectToChange.Description
            };

            await Repository.Update(updatedProject);
            Assert.NotEqual(originalName, Context.Projects.First(p => p.Id == projectToChange.Id).Name);
            Assert.Equal("newName", Context.Projects.First(p => p.Id == projectToChange.Id).Name);

        }
        [Fact]
        public async Task UpdatingDatabase_Failed_MissingName()
        {
            var projectToChange = Context.Projects.First();
            string originalName = $"{projectToChange.Name}";
            var updatedProject = new Project()
            {
                Branches = projectToChange.Branches,
                Assignees = projectToChange.Assignees,
                Id = projectToChange.Id,
                Description = projectToChange.Description,
                Name = null
            };
            var exception = 
                await Assert.ThrowsAsync<ValidationException>(async () => await Repository.Update(updatedProject));
            Assert.Equal("Name is required", exception.Message);
            Assert.Equal(originalName, Context.Projects.First().Name);
        }
        [Fact]
        public async Task UpdatingDatabase_Failed_NonExistentId()
        {
            var projectToChange = Context.Projects.First();
            string originalName = $"{projectToChange.Name}";
            int nonExistentId = Context.Projects.Select(p => p.Id).Max() + 10;
            var updatedProject = new Project()
            {
                Branches = projectToChange.Branches,
                Assignees = projectToChange.Assignees,
                Id = nonExistentId,
                Description = projectToChange.Description,
                Name = "newName"
            };
            var exception =
                await Assert.ThrowsAsync<EntityNotFoundException>(async () => await Repository.Update(updatedProject));
            Assert.Equal("Project not found", exception.Message);
            Assert.Equal(originalName, Context.Projects.First().Name);
        }
        // In memory database does not enforce the unique constraint. With MSSQL it works.
        [Fact]
        public async Task UpdatingDatabse_Failed_NotUniqueName()
        {
            var projectToChange = Context.Projects.Find(1);
            var repeatedProjectName = Context.Projects.Find(2)?.Name;

            var updatedProject = new Project()
            {
                Branches = projectToChange.Branches,
                Assignees = projectToChange.Assignees,
                Id = projectToChange.Id,
                Description = projectToChange.Description,
                Name = repeatedProjectName
            };
            await Assert.ThrowsAsync<PropertyNotUniqueException>(async () => await Repository.Update(updatedProject));
        }
        [Fact]
        public async Task DeleteProject_Failed_NonExistentId()
        {
            int nonExistentId = Context.Projects.Select(p => p.Id).Max() + 10;
            var exception = 
                await Assert.ThrowsAsync<EntityNotFoundException>(async () => 
                await Repository.Delete<Project>(nonExistentId));
            Assert.Equal("Project not found", exception.Message);
        }
        // The connected Branches and Subtasks should be deleted but not the Assignees.
        [Fact]
        public async Task DeleteProject_Successful()
        {
            var connectedAssignee = Context.Assignees
                .Include(a => a.Projects).ThenInclude(p => p.Branches).ThenInclude(b => b.Subtasks)
                .Where(a => a.Projects.Count != 0).First();

            var projectToDelete = connectedAssignee.Projects.First();

            int connectedAssigneeId = connectedAssignee.Id;
            int connectedBranchId = projectToDelete.Branches.First().Id;
            int connectedSubTaskId = projectToDelete.Branches.First().Subtasks.First().Id;

            await Repository.Delete<Project>(projectToDelete.Id);

            Assert.Null(await Context.Projects.FindAsync(projectToDelete.Id));
            Assert.Null(await Context.Branches.FindAsync(connectedBranchId));
            Assert.Null(await Context.SubTasks.FindAsync(connectedSubTaskId));
            Assert.NotNull(await Context.Assignees.FindAsync(connectedAssigneeId));
        }
    }
}
