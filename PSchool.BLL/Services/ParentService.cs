using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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

public class ParentService(IParentRepository parentRepository, IStudentRepository studentRepository, IMapper mapper) : IParentService
{
    public async Task<ParentModel> CreateParentAsync(ParentModel parent, CancellationToken cancellationToken = default)
    {
        if (parent.Children.Count == 0)
            throw new StudentNotFoundException("Children are not registered as students");
        // get children by emails
        var childEmails = parent.Children.Select(i => i.Email).ToList();
        var children = await studentRepository.GetAll()
            .Where(r => childEmails.Contains(r.Email))
            .ToListAsync(cancellationToken);
    
        if (children.Count == 0)
            throw new StudentNotFoundException("Children are not registered as students");
        
        //try to find all ready exist to update children list
        var parentDb = await parentRepository.GetAll().Where(r =>
            r.FirstName == parent.FirstName &&
            r.SecondName == parent.SecondName &&
            r.Email == parent.Email &&
            r.PhoneNumber == parent.PhoneNumber).SingleOrDefaultAsync(cancellationToken);
        
        if (parentDb is null)
        {
            //if we don`t find all ready exist parent create it
            parentDb = await parentRepository.GetAll()
                .SingleOrDefaultAsync(r => r.Email == parent.Email, cancellationToken);
            if (parentDb is not null)
                throw new EmailExistsException($"Account with this email is already exists");
        
            parentDb = await parentRepository.GetAll()
                .SingleOrDefaultAsync(r => r.PhoneNumber == parent.PhoneNumber, cancellationToken);
            if (parentDb is not null)
                throw new EmailExistsException($"Account with this phone number is already exists");
            
            parent.Children = null!;
            parentDb = await parentRepository.InsertDataAsync(mapper.Map<Parent>(parent), cancellationToken);
        }
        
        //add children
        children.ForEach(child => parentDb.Children.Add(child));
        parentDb = await parentRepository.UpdateDataAsync(parentDb, cancellationToken);
        return mapper.Map<ParentModel>(parentDb);
    }
  
    public async Task DeleteParentAsync(int parentId, CancellationToken cancellationToken = default)
    {
        var parentDb = await parentRepository.GetByIdAsync(parentId, cancellationToken);
        if (parentDb is null)
            throw new ParentNotFoundException($"Parent with id-{parentId} not found");
        await parentRepository.DeleteDataAsync(parentDb, cancellationToken);
    }

    public async Task<ParentModel> UpdateParentAsync(ParentModel parent, CancellationToken cancellationToken = default)
    {
        var parentDb = await parentRepository.GetByIdAsync(parent.Id, cancellationToken);
        if (parentDb is null)
            throw new StudentNotFoundException($"Parent with id-{parent.Id} not found");

        foreach (var propertyMap in ReflectionHelper.WidgetUtil<ParentModel, Parent>.PropertyMap)
        {
            var studentProperty = propertyMap.Item1;
            var studentDbProperty = propertyMap.Item2;

            var studentSourceValue = studentProperty.GetValue(parent);
            var studentTargetValue = studentDbProperty.GetValue(parentDb);

            if (studentSourceValue != null && studentProperty.Name != "Email" && studentDbProperty.Name != "PhoneNumber" && studentDbProperty.Name != "Children" &&
                !ReferenceEquals(studentSourceValue, "") && !studentSourceValue.Equals(studentTargetValue))
            {
                studentDbProperty.SetValue(parentDb, studentSourceValue);
            }
        }

        parentDb = await parentRepository.UpdateDataAsync(parentDb, cancellationToken);

        return mapper.Map<ParentModel>(parentDb);
    }

    public async Task<PaginationResponse<ParentModel>> GetParentsAsync(PaginationRequest paginationRequest, CancellationToken cancellationToken = default)
    {
        var parents = await parentRepository.GetAll().Pagination(paginationRequest.CurrentPage, paginationRequest.PageSize)
            .ToListAsync(cancellationToken);
        
        return new PaginationResponse<ParentModel>(
            mapper.Map<List<ParentModel>>(parents),
            parentRepository.GetAll().Count(),
            paginationRequest.CurrentPage,
            paginationRequest.PageSize);
    }

    public async Task<List<ParentModel>> GetParentsByStudentIdAsync(int studentId, CancellationToken cancellationToken = default)
    {
        var parents = await parentRepository.GetAll().Where(r => r.Children.Any(i => i.Id == studentId))
            .ToListAsync(cancellationToken);

        return mapper.Map<List<ParentModel>>(parents);
    }

    public async Task<ParentModel> GetByIdAsync(int parentId, CancellationToken cancellationToken = default)
    {
        var parent = await parentRepository.GetByIdAsync(parentId, cancellationToken);

        return mapper.Map<ParentModel>(parent);
    }
}