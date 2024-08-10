namespace PSchool.DAL.Repositories.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task InsertDataAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteDataAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task UpdateDataAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task GetById(TEntity entity, CancellationToken cancellationToken = default);
}