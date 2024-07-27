using AdoptHub.Crm.Domain.Services.Features.Instances.Commands.CreateInstance;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AdoptHub.Crm.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstancesController
    {
        private readonly IMediator _mediator;
        //POST /api/Instance/ModifyInstance
        //POST /api/Instance/CreateInstance
        //POST /api/Instance/CreateInstancePreview
        //POST /api/Instance/GetInstancesByUser
        //POST /api/Instance/RunInstance
        //POST /api/Instance/StopInstance
        //DELETE /api/Instance/DeleteInstance
        //PUT /api/Instance/UpdateInstance

        public InstancesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("ModifyInstance")]
        public IActionResult ModifyInstance()
        {
            return new OkResult();
        }

        [HttpPost("CreateInstance")]
        public Task CreateInstance()
        {
            return _mediator.Send(new CreateInstanceCommand("testAbcd xdd"));
        }

        [HttpPost("CreateInstancePreview")]
        public IActionResult CreateInstancePreview()
        {
            return new OkResult();
        }

        [HttpPost("GetInstancesByUser")]
        public IActionResult GetInstancesByUser()
        {
            return new OkResult();
        }

        [HttpPost("RunInstance")]
        public IActionResult RunInstance()
        {
            return new OkResult();
        }

        [HttpPost("StopInstance")]
        public IActionResult StopInstance()
        {
            return new OkResult();
        }

        [HttpDelete("DeleteInstance")]
        public IActionResult DeleteInstance()
        {
            return new OkResult();
        }

        [HttpPut("UpdateInstance")]
        public IActionResult UpdateInstance()
        {
            return new OkResult();
        }
    }
}
