using Microsoft.AspNetCore.Mvc;
using ProjectManagement_MOM.Extensions;
using ProjectManagement_MOM.Services;
using Shared.Entities.Projects.AProjects;
using Shared.Enums;

namespace ProjectManagement_MOM.Controllers;

[ApiController]
[Route("projects")]
public class ProjectController(ILogger<ProjectController> logger, RabbitMqService rabbitMqService) : ControllerBase{
    
    
    [HttpPost("approvement/init")]
    public async Task<ActionResult<AProjectCreated>> ApproveProject([FromBody] CreateAProjectDto projectDto, CancellationToken ct) {
        
        if (projectDto.State != EProjectState.Created) return BadRequest();
        
        logger.LogInformation("Project Approved; Creating Project...");
        
        var dto = projectDto.Map();

        rabbitMqService.PublishProject(dto);
        
        return Ok(dto);
    }
    
}