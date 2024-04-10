using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace h2oAPI.Models;

public class ScoreRequest
{
    public int UserID { get; set; }

    public int QuestionID { get; set; }
    public int TeamID { get; set; }
    public string? Competition { get; set; }
    public int ScoreValue { get; set; }
}
