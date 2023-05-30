using API.Contexts;
using API.Contracts;
using API.Models;
using System.Data;

namespace API.Repositories;

public class GeneralRepository<TEntity> : IGeneralRepository<TEntity> where TEntity : class
{

    protected readonly BookingManagementDbContext _context;

    public GeneralRepository(BookingManagementDbContext context)
    {
        _context = context;
    }

    /*
     * <summary>
     * Create Entity
     * </summary>
     * <param name="param">Entity object</param>
     * <returns>Entity object</returns>
     */
    public TEntity? Create(TEntity entity)
    {
        try
        {
            typeof(TEntity).GetProperty("CreatedDate")!.SetValue(entity, DateTime.Now);
            typeof(TEntity).GetProperty("ModifiedDate")!.SetValue(entity, DateTime.Now);

            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
            return entity;
        }
        catch
        {
            return null;
        }
    }

    /*
    * <summary>
    * Update Entity
    * </summary>
    * <param name="param">Entity object</param>
    * <returns>true if data updated</returns>
    * <returns>false if data not updated</returns>
    */
    public bool Update(TEntity entity)
    {
        try
        {
            var guid = (Guid)typeof(TEntity).GetProperty("Guid")!.GetValue(entity)!;

            var oldEntity = GetByGuid(guid);
            if (oldEntity == null)
            {
                return false;
            }

            var getCreatedDate = typeof(TEntity).GetProperty("CreatedDate")!.GetValue(oldEntity)!;

            typeof(TEntity).GetProperty("CreatedDate")!.SetValue(entity, getCreatedDate);
            typeof(TEntity).GetProperty("ModifiedDate")!.SetValue(entity, DateTime.Now);
            _context.Set<TEntity>().Update(entity);
            _context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }


    /*
    * <summary>
    * Delete Entity
    * </summary>
    * <param name="guid">Entity guid</param>
    * <returns>true if data deleted</returns>
    * <returns>false if data not deleted</returns>
    */
    public bool Delete(Guid guid)
    {
        try
        {
            var entity = GetByGuid(guid);
            if (entity == null)
            {
                return false;
            }

            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    /*
     * <summary>
     * Get all Entity
     * </summary>
     * <returns>List of entities</returns>
     * <returns>Empty list if no data found</returns>
     */
    public IEnumerable<TEntity> GetAll()
    {
        return _context.Set<TEntity>().ToList();
    }

    /*
     * <summary>
     * Get an entity by guid
     * </summary>
     * <param name="guid">Entity guid</param>
     * <returns>Entity object</returns>
     * <returns>null if no data found</returns>
     */
    public TEntity? GetByGuid(Guid guid)
    {
        var entity = _context.Set<TEntity>().Find(guid);
        _context.ChangeTracker.Clear();
        return entity;
    }

}
