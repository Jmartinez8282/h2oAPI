using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace h2oAPI.Models;

public class LoginRequest
{
    public int Id { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }

    
}
