using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PSchool.BLL.Models;
using PSchool.BLL.Services.Interfaces;
using PSchool.Web.Models;
using BaseModel = PSchool.Web.Models.BaseModel;
using ParentModel = PSchool.Web.Models.ParentModel;

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
        return Ok(mapper.Map<PaginationResponse<ParentModel>>(parents));
    }
       
    [HttpGet("{parentId:int}")]
    public async Task<IActionResult> GetByIdAsync(int parentId, CancellationToken cancellationToken)
    {
        var parent = await parentService.GetParentsByIdAsync(parentId, cancellationToken);
        return Ok(mapper.Map<ParentModel>(parent));
    }

    [HttpPost]
    public async Task<IActionResult> CreateParent([FromBody] ParentCreateModel parentCreateModel, CancellationToken cancellationToken)
    {
        var parent = await parentService.CreateParentAsync(mapper.Map<BLL.Models.ParentModel>(parentCreateModel), cancellationToken);
        return Ok(mapper.Map<ParentModel>(parent));
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateParent([FromBody] ParentUpdateModel parentCreateModel, CancellationToken cancellationToken)
    {
        var parent = await parentService.UpdateParentAsync(mapper.Map<BLL.Models.ParentModel>(parentCreateModel), cancellationToken);
        return Ok(mapper.Map<ParentModel>(parent));
    }
    
    [HttpDelete("{parentId:int}")]
    public async Task<IActionResult> DeleteParent(int parentId, CancellationToken cancellationToken)
    {
        await parentService.DeleteParentAsync(parentId, cancellationToken);
        return Ok();
    }
    
}