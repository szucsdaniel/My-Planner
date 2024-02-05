using Microsoft.AspNetCore.Components.Web;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Text.Json.Serialization.Metadata;
using webapi.Exceptions;
using webapi.Models;

namespace webapi.Data_Access_Layer
{
    // This class contains the basic CRUD operations that are non-specific to any given model.
    public class BaseRepository
    {
        protected MyPlannerDbContext _context;
        public BaseRepository(MyPlannerDbContext context)
        {
            _context = context;
        }

        // Get all records of a table. Includes optional parameter is specifies which 
        public async Task<List<T>> GetAll<T>(params Expression<Func<T, object?>>[] includes) where T : class{
            var query = _context.Set<T>().AsQueryable();

            foreach (var include in includes)
            {
                query.Include(include);
            }
            return await query.ToListAsync();
        }
        // Get any Entity from database based on id
        public async Task<T> GetById<T>(int id, 
             params Expression<Func<T, object?>>[] includes) where T : ModelBase

        {
            var query = _context.Set<T>().AsQueryable();

            var entity = await query.FirstOrDefaultAsync(e => e.Id == id);

            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(T).ToString());
            }

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstAsync(e => e.Id == id);
        }
        // If Entity isn't in the database 
        public async Task Add<T>(T entityToAdd) where T : ModelBase
        {
            try
            {
                entityToAdd.Validate();
                await _context.Set<T>().AddAsync(entityToAdd);
                await _context.SaveChangesAsync();

            }
            // If a Property that is supposed to be unique isn't we handle it here. 
            catch (DbUpdateException e)
            {
                if (e.InnerException is SqlException)
                {
                    var entry = e.Entries.FirstOrDefault();
                    string entityType = typeof(T).ToString();

                    SqlException exception = (SqlException)e.InnerException;
                    switch (exception.Number)
                    {
                        case 2601:
                            throw new PropertyNotUniqueException(entry, entityType);
                        case 547:
                            throw new NonExcistentForeignKeyException(entityType);
                        default:
                            throw;
                    }
                }
                else
                {
                    throw;
                }
            }
            catch(ValidationException){ throw; }

        }
        // Updates an Entity with the values of updatedEntity.
        public async Task Update<T>(T updatedEntity) where T : ModelBase
        {
            try
            {
                updatedEntity.Validate();
                var entityToUpdate = await GetById<T>(updatedEntity.Id);

                _context.Entry(entityToUpdate).CurrentValues.SetValues(updatedEntity);
                await _context.SaveChangesAsync();

            }
            catch (EntityNotFoundException) { throw; }
            // If a Property that is supposed to be unique isn't we handle it here. 
            catch (DbUpdateException e)
            {
                if (e.InnerException is SqlException)
                {
                    var entry = e.Entries.FirstOrDefault();
                    string entityType = typeof(T).ToString();

                    SqlException exception = (SqlException)e.InnerException;
                    switch (exception.Number)
                    {
                        case 2601:
                            throw new PropertyNotUniqueException(entry, entityType);
                        default:
                            throw;
                    }
                }
                else
                {
                    throw;
                }
            }
            catch (ValidationException) { throw; }
        }

        // Delete a record from any table by id
        public async Task Delete<T>(int id) where T : ModelBase
        {
            try
            {
                var entity = await GetById<T>(id);
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();

            }
            catch (EntityNotFoundException){ throw; }
        }
        public bool IsNameUnique<T>(string name) where T : ModelBase
        {
            return _context.Set<T>().Select(x => x.Name).Contains(name);
        }
    }
}
