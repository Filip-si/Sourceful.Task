using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sourceful.Application.Models;
using Sourceful.Task.Functions.User;
using Sourceful.Task.Functions.User.Commands;
using System;
using System.Threading.Tasks;

namespace Sourceful.Task.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetUserById(Guid userId) 
        {
            var response = await _mediator.Send(new GetUserById.Query(userId));
            return response == null ? NotFound() : Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateUser(CreateUser.Command command) => Ok(await _mediator.Send(command));

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateUser([FromBody] UserUpdateRequest updateRequest)
        {
            UpdateUser.Command command = new UpdateUser.Command(updateRequest);
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<string>> DeleteUser(Guid userId)
        {
            return await _mediator.Send(new DeleteUser.Command(userId));
        }
    }
}
