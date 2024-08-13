using System.Threading;
using System.Threading.Tasks;
using PSchool.BLL.Models;

namespace PSchool.BLL.Services.Interfaces;

public interface IParentService
{
    Task<ParentModel> CreateParentAsync(ParentModel parent, CancellationToken cancellationToken = default);
    Task DeleteParentAsync(int parentId, CancellationToken cancellationToken = default);
    Task<ParentModel> UpdateParentAsync(ParentModel parent, CancellationToken cancellationToken = default);

    Task<PaginationResponse<ParentModel>> GetParentsAsync(PaginationRequest paginationRequest,
        CancellationToken cancellationToken = default);
    
    Task<PaginationResponse<ParentModel>> GetParentsByStudentIdAsync(int studentId, PaginationRequest paginationRequest,
        CancellationToken cancellationToken = default);
}