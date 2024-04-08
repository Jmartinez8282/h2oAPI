using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace h2oAPI.Models;

public class Question
{
    public int QuestionID { get; set; } 
    public int SortOrder { get; set; }
    public string? QuestionText { get; set; } = null!;
    public string? Competition { get; set; } = null!;
    public bool IsHidden { get; set; }

}
