using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace h2oAPI.Models;

public class Score
{
    public int ScoreID { get; set; }
    public int UserID { get; set; }
    public int QuestionID { get; set; }
    public int TeamID { get; set; }
    public string? Competition { get; set; } = null!;
    public int ScoreValue { get; set; }
}
