using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using h2oAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace h2oAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    [HttpPost]
    public int Register(string username,string password)
    {
        return _userService.Register(username, password);
    }
}
