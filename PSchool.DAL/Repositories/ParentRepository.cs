using PSchool.DAL.Entities;
using PSchool.DAL.Repositories.Interfaces;

namespace PSchool.DAL.Repositories;

public class ParentRepository : IParentRepository
{
    public Task InsertDataAsync(Parent entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteDataAsync(Parent entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateDataAsync(Parent entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task GetById(Parent entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}