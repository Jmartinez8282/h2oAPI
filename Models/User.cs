using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace h2oAPI.Models;

public class User
{
    public int UserID { get; set; }
    public string? Name { get; set;} = null!;
    public string? Email { get; set;} = null!;
    public string? Phone { get; set; } = null!;
    public string? UserType { get; set; } = null!;
    public string? Competition { get; set; } = null!;
    public string? Division { get; set; } = null!;
    public string? Password { get; set; } = null!;
    public string? IsDeleted { get; set; } = null!;
    public string? UserRole { get; set; } = null!;
}
