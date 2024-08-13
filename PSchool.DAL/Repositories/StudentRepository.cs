using Microsoft.EntityFrameworkCore;
using PSchool.DAL.Entities;
using PSchool.DAL.Repositories.Interfaces;

namespace PSchool.DAL.Repositories;

public class StudentRepository(PSchoolDbContext pSchoolDbContext) : IStudentRepository
{
    public async Task<Student> InsertDataAsync(Student entity, CancellationToken cancellationToken = default)
    {
        var entityEntry = await pSchoolDbContext.Students.AddAsync(entity, cancellationToken);
        await pSchoolDbContext.SaveChangesAsync(cancellationToken);
        return entityEntry.Entity;
    }

    public async Task DeleteDataAsync(Student entity, CancellationToken cancellationToken = default)
    {
        pSchoolDbContext.Students.Remove(entity);
        await pSchoolDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Student> UpdateDataAsync(Student entity, CancellationToken cancellationToken = default)
    {
        var entityEntry = pSchoolDbContext.Students.Update(entity);
        await pSchoolDbContext.SaveChangesAsync(cancellationToken);
        return entityEntry.Entity;
    }

    public async Task<Student?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await pSchoolDbContext.Students.Include(r => r.Parents)
            .SingleOrDefaultAsync(i => i.Id == id, cancellationToken);
    }

    public IQueryable<Student> GetAll()
    {
        return pSchoolDbContext.Students.Include(p => p.Parents).AsQueryable();
    }
}