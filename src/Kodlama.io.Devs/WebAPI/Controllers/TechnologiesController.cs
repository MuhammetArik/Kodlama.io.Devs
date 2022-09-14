using Application.Features.Technologies.Commands.CreateTechnology;
using Application.Features.Technologies.Commands.DeleteTechnology;
using Application.Features.Technologies.Commands.UpdateTechnology;
using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Models;
using Application.Features.Technologies.Queries.GetByIdTechnology;
using Application.Features.Technologies.Queries.GetListTechnology;
using Application.Features.Technologies.Queries.GetListTechnologyByDynamic;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechnologiesController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListTechnologyQuery getListTechnologyQuery = new GetListTechnologyQuery { PageRequest = pageRequest };
            TechnologyListModel result = await Mediator.Send(getListTechnologyQuery);
            return Ok(result);
        }

        [HttpPost("GetList/ByDynamic")]
        public async Task<IActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest, [FromBody] Dynamic dynamic)
        {
            GetListTechnologyByDynamicQuery getListByDynamicTechnologyQuery = new GetListTechnologyByDynamicQuery { PageRequest = pageRequest, Dynamic = dynamic };
            TechnologyListModel result = await Mediator.Send(getListByDynamicTechnologyQuery);
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdTechnologyQuery getByIdTechnologyQuery)
        {
            TechnologyGetByIdDto technologyGetByIdDto = await Mediator.Send(getByIdTechnologyQuery);
            return Ok(technologyGetByIdDto);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateTechnologyCommand createTechnologyCommand)
        {
            CreatedTechnologyDto createdTechnologyDto = await Mediator.Send(createTechnologyCommand);
            return Created("", createdTechnologyDto);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteTechnologyCommand deleteTechnologyCommand)
        {
            DeletedTechnologyDto deletedTechnologyDto = await Mediator.Send(deleteTechnologyCommand);
            return Ok(deletedTechnologyDto);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateTechnologyCommand updateTechnologyCommand)
        {
            UpdatedTechnologyDto updatedTechnologyDto = await Mediator.Send(updateTechnologyCommand);
            return Ok(updatedTechnologyDto);
        }
    }
}
