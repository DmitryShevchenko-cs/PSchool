using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PSchool.BLL.Models;
using PSchool.BLL.Services.Interfaces;
using PSchool.Web.Models;

namespace PSchool.Web.Controllers;

[AllowAnonymous]
[Route("api/[controller]")]
[ApiController]
public class StudentController( IStudentService studentService, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetParentsAsync([FromQuery]string parentName, [FromQuery] PaginationRequest paginationRequest, CancellationToken cancellationToken)
    {
        var parents = await studentService.GetStudentsAsync(paginationRequest, parentName, cancellationToken);
        return Ok(mapper.Map<PaginationResponse<ParentViewModel>>(parents));
    }
       
    [HttpGet("{studentId:int}")]
    public async Task<IActionResult> GetParentsByStudentIdAsync(int studentId, CancellationToken cancellationToken)
    {
        var parents = await studentService.GetStudentByIdAsync(studentId, cancellationToken);
        return Ok(mapper.Map<List<ParentViewModel>>(parents));
    }

    [HttpPost]
    public async Task<IActionResult> CreateParent([FromBody] StudentCreateModel parentCreateModel, CancellationToken cancellationToken)
    {
        var parent = await studentService.CreateStudentAsync(mapper.Map<StudentModel>(parentCreateModel), cancellationToken);
        return Ok(mapper.Map<ParentViewModel>(parent));
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateParent([FromBody] StudentCreateModel parentCreateModel, CancellationToken cancellationToken)
    {
        var parent = await studentService.UpdateStudentAsync(mapper.Map<StudentModel>(parentCreateModel), cancellationToken);
        return Ok(mapper.Map<ParentViewModel>(parent));
    }
    
    [HttpDelete("{studentId:int}")]
    public async Task<IActionResult> DeleteParent(int studentId, CancellationToken cancellationToken)
    {
        await studentService.DeleteStudentAsync(studentId, cancellationToken);
        return Ok();
    }   
}