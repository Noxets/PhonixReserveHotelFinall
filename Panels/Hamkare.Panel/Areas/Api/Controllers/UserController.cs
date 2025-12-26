using Hamkare.Common.Entities.Identity;
using Hamkare.Service.Services.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hamkare.Panel.Areas.Api.Controllers;

public class UserController(UserManager<User> userManager, UserService userService) : ApiController
{

    // GET
    [Authorize(Roles = "Administrator,System")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public virtual async Task<IActionResult> Get(int top, int skip, CancellationToken cancellationToken = default)
    {
        var items = await userService.GetAllAsync(top, skip, cancellationToken);
        return Ok(items);
    }

    [HttpPost]
    public virtual async Task<IActionResult> Get([FromRoute] long id, CancellationToken cancellationToken = default)
    {
        var item = await userService.FindAsync(id, cancellationToken);
        return Ok(item);
    }


    [Authorize(Roles = "Administrator,System")]
    [HttpPut]
    public virtual async Task<IActionResult> Create([FromBody] User item)
    {
        await userManager.CreateAsync(item);
        return Ok();
    }

    [Authorize(Roles = "Administrator,System")]
    [HttpPost]
    public virtual async Task<IActionResult> Update([FromBody] User item)
    {
        await userManager.UpdateAsync(item);
        var result = await userManager.FindByIdAsync(item.Id.ToString());
        return Ok(result);
    }

    [Authorize(Roles = "Administrator,System")]
    [HttpDelete]
    public virtual async Task<IActionResult> Delete(long id, CancellationToken cancellationToken = default)
    {
        var user = await userManager.FindByIdAsync(id.ToString());
        await userService.DeleteAsync(user.Id, cancellationToken);
        return Ok();
    }
}