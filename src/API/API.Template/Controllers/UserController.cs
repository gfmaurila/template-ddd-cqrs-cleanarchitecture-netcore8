using API.Template.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Feature.Users.Commands.Create;
using Template.Application.Feature.Users.Queries.GetById;

namespace API.Template.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(ISender sender, ILogger<UserController> logger) : BaseController(logger)
{
    private readonly ISender _sender = sender;

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _sender.Send(new GetUserByIdQuery(id));
        return result.IsSuccess ? Ok(result.ToSerializableObject()) : HandleError(result.Error);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateUserCommand command)
    {
        var result = await _sender.Send(command);
        return result.IsSuccess
            ? CreatedAtAction(nameof(GetById), new { id = result.Value }, result.ToSerializableObject())
            : HandleError(result.Error);
    }
}
