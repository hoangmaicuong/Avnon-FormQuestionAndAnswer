using System;
using System.Collections.Generic;

namespace FormQuestionAndAnswer.Models;

public partial class QuestionTitle
{
    public int Id { get; set; }

    public string? QuestionTitleContent { get; set; }

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
