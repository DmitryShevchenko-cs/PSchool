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
       
    [HttpGet("{parentId:int}")]
    public async Task<IActionResult> GetByIdAsync(int parentId, CancellationToken cancellationToken)
    {
        var parent = await parentService.GetByIdAsync(parentId, cancellationToken);
        return Ok(mapper.Map<ParentViewModel>(parent));
    }

    [HttpPost]
    public async Task<IActionResult> CreateParent([FromBody] ParentCreateViewModel parentCreateViewModel, CancellationToken cancellationToken)
    {
        var parent = await parentService.CreateParentAsync(mapper.Map<ParentModel>(parentCreateViewModel), cancellationToken);
        return Ok(mapper.Map<ParentViewModel>(parent));
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateParent([FromBody] ParentUpdateViewModel parentCreateViewModel, CancellationToken cancellationToken)
    {
        var parent = await parentService.UpdateParentAsync(mapper.Map<ParentModel>(parentCreateViewModel), cancellationToken);
        return Ok(mapper.Map<ParentViewModel>(parent));
    }
    
    [HttpDelete("{parentId:int}")]
    public async Task<IActionResult> DeleteParent(int parentId, CancellationToken cancellationToken)
    {
        await parentService.DeleteParentAsync(parentId, cancellationToken);
        return Ok();
    }
    
}