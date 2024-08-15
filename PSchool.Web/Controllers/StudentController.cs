using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PSchool.BLL.Models;
using PSchool.BLL.Services.Interfaces;
using PSchool.Web.Models;
using ParentModel = PSchool.Web.Models.ParentModel;
using StudentModel = PSchool.BLL.Models.StudentModel;

namespace PSchool.Web.Controllers;

[AllowAnonymous]
[Route("api/[controller]")]
[ApiController]
public class StudentController(IStudentService studentService, IParentService parentService, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetParentsAsync([FromQuery] PaginationRequest paginationRequest,
        CancellationToken cancellationToken, [FromQuery] string? parentName = null)
    {
        var parents = await studentService.GetStudentsAsync(paginationRequest, parentName, cancellationToken);
        return Ok(mapper.Map<PaginationResponse<Models.StudentModel>>(parents));
    }
       
    [HttpGet("{studentId:int}")]
    public async Task<IActionResult> GetStudentByIdAsync(int studentId, CancellationToken cancellationToken)
    {
        var parents = await studentService.GetStudentByIdAsync(studentId, cancellationToken);
        return Ok(mapper.Map<Models.StudentModel>(parents));
    }
    
    [HttpGet("parents/{studentId:int}")]
    public async Task<IActionResult> GetParentsByStudentIdAsync(int studentId, CancellationToken cancellationToken)
    {
        var parents = await parentService.GetParentsByStudentIdAsync(studentId, cancellationToken);
        return Ok(mapper.Map<List<ParentModel>>(parents));
    }

    [HttpPost]
    public async Task<IActionResult> CreateParent([FromBody] StudentCreateModel parentCreateModel, CancellationToken cancellationToken)
    {
        var parent = await studentService.CreateStudentAsync(mapper.Map<StudentModel>(parentCreateModel), cancellationToken);
        return Ok(mapper.Map<Models.StudentModel>(parent));
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateParent([FromBody] StudentUpdateModel parentCreateModel, CancellationToken cancellationToken)
    {
        var parent = await studentService.UpdateStudentAsync(mapper.Map<StudentModel>(parentCreateModel), cancellationToken);
        return Ok(mapper.Map<Models.StudentModel>(parent));
    }
    
    [HttpDelete("parent")]
    public async Task<IActionResult> UpdateParent([FromQuery]int studentId, [FromQuery]int parentId, CancellationToken cancellationToken)
    {
        var parent = await studentService.RemoveParent(studentId, parentId, cancellationToken);
        return Ok(mapper.Map<Models.StudentModel>(parent));
    }
    
    [HttpDelete("{studentId:int}")]
    public async Task<IActionResult> DeleteParent(int studentId, CancellationToken cancellationToken)
    {
        await studentService.DeleteStudentAsync(studentId, cancellationToken);
        return Ok();
    }   
}