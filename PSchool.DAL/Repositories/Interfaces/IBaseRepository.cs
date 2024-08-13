using PSchool.DAL.Entities;

namespace PSchool.DAL.Repositories.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<TEntity> InsertDataAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteDataAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity> UpdateDataAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    IQueryable<TEntity> GetAll();

}