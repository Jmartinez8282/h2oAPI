using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace h2oAPI.Models;

public class Team
{
    public int TeamID { get; set; } 
    public string? Name { get; set; } = null!;
    public string? Competition { get; set; } = null!;
    public string? Division { get; set; } = null!;
    public string? Coach { get; set; } = null!;
    
}
