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
public class ParentController(IParentService parentService, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetParentsAsync([FromQuery] PaginationRequest paginationRequest, CancellationToken cancellationToken)
    {
        var parents = await parentService.GetParentsAsync(paginationRequest, cancellationToken);
        return Ok(mapper.Map<PaginationResponse<ParentViewModel>>(parents));
    }
       
    [HttpGet("{studentId:int}")]
    public async Task<IActionResult> GetParentsByStudentIdAsync(int studentId, CancellationToken cancellationToken)
    {
        var parents = await parentService.GetParentsByStudentIdAsync(studentId, cancellationToken);
        return Ok(mapper.Map<List<ParentViewModel>>(parents));
    }

    [HttpPost]
    public async Task<IActionResult> CreateParent([FromBody] ParentCreateModel parentCreateModel, CancellationToken cancellationToken)
    {
        var parent = await parentService.CreateParentAsync(mapper.Map<ParentModel>(parentCreateModel), cancellationToken);
        return Ok(mapper.Map<ParentViewModel>(parent));
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateParent([FromBody] ParentCreateModel parentCreateModel, CancellationToken cancellationToken)
    {
        var parent = await parentService.UpdateParentAsync(mapper.Map<ParentModel>(parentCreateModel), cancellationToken);
        return Ok(mapper.Map<ParentViewModel>(parent));
    }
    
    [HttpDelete("{parentId:int}")]
    public async Task<IActionResult> DeleteParent(int parentId, CancellationToken cancellationToken)
    {
        await parentService.DeleteParentAsync(parentId, cancellationToken);
        return Ok();
    }
    
}