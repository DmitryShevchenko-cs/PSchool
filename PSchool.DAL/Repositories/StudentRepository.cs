using PSchool.DAL.Entities;
using PSchool.DAL.Repositories.Interfaces;

namespace PSchool.DAL.Repositories;

public class StudentRepository : IStudentRepository
{
    public Task InsertDataAsync(Student entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteDataAsync(Student entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateDataAsync(Student entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task GetById(Student entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}