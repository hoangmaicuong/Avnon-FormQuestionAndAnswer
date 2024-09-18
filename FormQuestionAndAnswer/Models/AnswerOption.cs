using System;
using System.Collections.Generic;

namespace FormQuestionAndAnswer.Models;

public partial class AnswerOption
{
    public int Id { get; set; }

    public string? OptionAnswerContent { get; set; }

    public int? QuestionId { get; set; }

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual Question? Question { get; set; }
}
