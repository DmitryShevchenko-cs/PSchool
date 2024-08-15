using PSchool.BLL.Models;

namespace PSchool.BLL.Services.Interfaces;

public interface IStudentService
{
    Task<StudentModel> CreateStudentAsync(StudentModel student, CancellationToken cancellationToken = default);
    
    Task<StudentModel> RemoveParent(int studentId, int parentId, CancellationToken cancellationToken = default);
    
    Task DeleteStudentAsync(int studentId, CancellationToken cancellationToken = default);
    Task<StudentModel> UpdateStudentAsync(StudentModel student, CancellationToken cancellationToken = default);
    Task<PaginationResponse<StudentModel>> GetStudentsAsync(PaginationRequest paginationRequest, string parentFullName = null!, CancellationToken cancellationToken = default);

    Task<StudentModel> AddParentAsync(int studentId, int parentId, CancellationToken cancellationToken = default);

    Task<StudentModel> GetStudentByIdAsync(int studentId, CancellationToken cancellationToken = default);

}