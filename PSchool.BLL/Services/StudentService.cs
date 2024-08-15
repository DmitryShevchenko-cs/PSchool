using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PSchool.BLL.Exceptions;
using PSchool.BLL.Extensions;
using PSchool.BLL.Helpers;
using PSchool.BLL.Models;
using PSchool.BLL.Services.Interfaces;
using PSchool.DAL.Entities;
using PSchool.DAL.Repositories.Interfaces;

namespace PSchool.BLL.Services;

public class StudentService(IStudentRepository studentRepository, IParentRepository parentRepository, IMapper mapper) : IStudentService
{
    public async Task<StudentModel> CreateStudentAsync(StudentModel student, CancellationToken cancellationToken = default)
    {
        var studentDb = await studentRepository.GetAll()
            .SingleOrDefaultAsync(r => r.Email == student.Email, cancellationToken);
        if (studentDb is not null)
            throw new EmailExistsException($"Account with this email is already exists");
        
        studentDb = await studentRepository.GetAll()
            .SingleOrDefaultAsync(r => r.PhoneNumber == student.PhoneNumber, cancellationToken);
        if (studentDb is not null)
            throw new EmailExistsException($"Account with this phone number is already exists");
        
        studentDb = await studentRepository.InsertDataAsync(mapper.Map<Student>(student), cancellationToken);
        return mapper.Map<StudentModel>(studentDb);
    }

    public async Task DeleteStudentAsync(int studentId, CancellationToken cancellationToken = default)
    {
        var studentDb = await studentRepository.GetByIdAsync(studentId, cancellationToken);
        if (studentDb is null)
            throw new StudentNotFoundException($"Student with id-{studentId} not found");
        await studentRepository.DeleteDataAsync(studentDb, cancellationToken);
    }

    public async Task<StudentModel> RemoveParent(int studentId, int parentId, CancellationToken cancellationToken = default)
    {
        var studentDb = await studentRepository.GetByIdAsync(studentId, cancellationToken);
        if (studentDb is null)
            throw new StudentNotFoundException($"Student with id-{parentId} not found");
        
        var parentDb = await parentRepository.GetByIdAsync(parentId, cancellationToken);
        if (parentDb is null)
            throw new ParentNotFoundException($"Parent with id-{parentId} not found");

        studentDb.Parents.Remove(parentDb);

        var student = await studentRepository.UpdateDataAsync(studentDb, cancellationToken);

        return mapper.Map<StudentModel>(student);
    }
    
    public async Task<StudentModel> UpdateStudentAsync(StudentModel student, CancellationToken cancellationToken = default)
    {
        var studentDb = await studentRepository.GetByIdAsync(student.Id, cancellationToken);
        if (studentDb is null)
            throw new StudentNotFoundException($"Student with id-{student.Id} not found");

        foreach (var propertyMap in ReflectionHelper.WidgetUtil<StudentModel, Student>.PropertyMap)
        {
            var studentProperty = propertyMap.Item1;
            var studentDbProperty = propertyMap.Item2;

            var studentSourceValue = studentProperty.GetValue(student);
            var studentTargetValue = studentDbProperty.GetValue(studentDb);

            if (studentSourceValue != null && studentDbProperty.Name != "Email" && studentDbProperty.Name != "PhoneNumber" && studentDbProperty.Name != "Parents" &&
                !ReferenceEquals(studentSourceValue, "") && !studentSourceValue.Equals(studentTargetValue))
            {
                studentDbProperty.SetValue(studentDb, studentSourceValue);
            }
        }

        studentDb = await studentRepository.UpdateDataAsync(studentDb, cancellationToken);
        return mapper.Map<StudentModel>(studentDb);
    }
    
    public async Task<PaginationResponse<StudentModel>> GetStudentsAsync(PaginationRequest paginationRequest,
        string parentFullName = null!, CancellationToken cancellationToken = default)
    {
        var query = studentRepository.GetAll();
        
        if (!string.IsNullOrEmpty(parentFullName))
        {
            var parts = parentFullName.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 1)
            {
                var namePart = parts[0];
                query = query.Where(s => s.Parents.Any(p => 
                    p.FirstName.ToLower().StartsWith(namePart) || 
                    p.SecondName.ToLower().StartsWith(namePart)));
            }
            else if (parts.Length == 2)
            {
                var firstNamePart = parts[0];
                var lastNamePart = parts[1];
                query = query.Where(s => s.Parents.Any(p => 
                    (p.FirstName.ToLower().StartsWith(firstNamePart) && p.SecondName.ToLower().StartsWith(lastNamePart)) ||
                    (p.FirstName.ToLower().StartsWith(lastNamePart) && p.SecondName.ToLower().StartsWith(firstNamePart))));
            }
        }
        
        var students = await query.Pagination(paginationRequest.CurrentPage, paginationRequest.PageSize).ToListAsync(cancellationToken);
        
        return new PaginationResponse<StudentModel>(
            mapper.Map<List<StudentModel>>(students),
            students.Count,
            paginationRequest.CurrentPage,
            paginationRequest.PageSize);
    }

    public async Task<StudentModel> AddParentAsync(int studentId, int parentId, CancellationToken cancellationToken = default)
    {
        var studentDb = await studentRepository.GetByIdAsync(studentId, cancellationToken);
        if (studentDb is null)
            throw new StudentNotFoundException($"Student with id-{studentId} not found");

        var parentDb = await parentRepository.GetByIdAsync(parentId, cancellationToken);
        if (studentDb is null)
            throw new ParentNotFoundException($"Parent with id-{studentId} not found");
        
        studentDb.Parents.Add(parentDb!);
        var student = await studentRepository.UpdateDataAsync(studentDb, cancellationToken);
        return mapper.Map<StudentModel>(student);
    }

    public async Task<StudentModel> GetStudentByIdAsync(int studentId, CancellationToken cancellationToken = default)
    {
        var studentDb = await studentRepository.GetByIdAsync(studentId, cancellationToken);
        if (studentDb is null)
            throw new StudentNotFoundException($"Student with id-{studentId} not found");

        return mapper.Map<StudentModel>(studentDb);
    }
}