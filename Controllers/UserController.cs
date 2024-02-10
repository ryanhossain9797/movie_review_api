using Microsoft.AspNetCore.Mvc;
using imdb.Repository;
using imdb.Models;
using imdb.Services;
using Microsoft.EntityFrameworkCore;

namespace imdb.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : Controller {

    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await _userService.GetUserById(id);
        
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        if(user is null)
            return NotFound();

        return Ok(user);
    }
}