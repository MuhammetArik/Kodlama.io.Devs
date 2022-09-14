using Application.Features.GitHubProfiles.Commands.CreateGitHubProfile;
using Application.Features.GitHubProfiles.Commands.DeleteGitHubProfile;
using Application.Features.GitHubProfiles.Commands.UpdateGitHubProfile;
using Application.Features.GitHubProfiles.Dtos;
using Application.Features.GitHubProfiles.Models;
using Application.Features.GitHubProfiles.Queries.GetListGitHubProfile;
using Core.Application.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GitHubProfilesController : BaseController
    {
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] CreateGitHubProfileCommand createGitHubProfileCommand)
        {
            CreatedGitHubProfileDto createdGitHubProfileDto = await Mediator.Send(createGitHubProfileCommand);
            return Created("", createdGitHubProfileDto);
        }

        [HttpDelete("Delete/{Id}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteGitHubProfileCommand deleteGitHubProfileCommand)
        {
            DeletedGitHubProfileDto deletedGitHubProfileDto = await Mediator.Send(deleteGitHubProfileCommand);
            return Ok(deletedGitHubProfileDto);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateGitHubProfileCommand updateGitHubProfileCommand)
        {
            UpdatedGitHubProfileDto updatedGitHubProfileDto = await Mediator.Send(updateGitHubProfileCommand);
            return Ok(updatedGitHubProfileDto);
        }

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListGitHubProfileQuery getListGitHubProfileQuery = new GetListGitHubProfileQuery { PageRequest = pageRequest };
            GitHubProfileListModel result = await Mediator.Send(getListGitHubProfileQuery);
            return Ok(result);
        }
    }
}
