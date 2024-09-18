using System;
using System.Collections.Generic;

namespace FormQuestionAndAnswer.Models;

public partial class AnswerType
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
