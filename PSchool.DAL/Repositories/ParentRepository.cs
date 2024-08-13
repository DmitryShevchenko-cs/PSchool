using Microsoft.EntityFrameworkCore;
using PSchool.DAL.Entities;
using PSchool.DAL.Repositories.Interfaces;

namespace PSchool.DAL.Repositories;

public class ParentRepository(PSchoolDbContext pSchoolDbContext) : IParentRepository
{
    public async Task<Parent> InsertDataAsync(Parent entity, CancellationToken cancellationToken = default)
    {
        var entityEntry = await pSchoolDbContext.Parents.AddAsync(entity, cancellationToken);
        await pSchoolDbContext.SaveChangesAsync(cancellationToken);
        return entityEntry.Entity;
    }

    public async Task DeleteDataAsync(Parent entity, CancellationToken cancellationToken = default)
    {
        pSchoolDbContext.Parents.Remove(entity);
        await pSchoolDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Parent> UpdateDataAsync(Parent entity, CancellationToken cancellationToken = default)
    {
        var entityEntry = pSchoolDbContext.Parents.Update(entity);
        await pSchoolDbContext.SaveChangesAsync(cancellationToken);
        return entityEntry.Entity;
    }

    public async Task<Parent?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await pSchoolDbContext.Parents.Include(r => r.Children)
            .SingleOrDefaultAsync(i => i.Id == id, cancellationToken);
    }

    public IQueryable<Parent> GetAll()
    {
        return pSchoolDbContext.Parents.Include(r => r.Children).AsQueryable();
    }
}