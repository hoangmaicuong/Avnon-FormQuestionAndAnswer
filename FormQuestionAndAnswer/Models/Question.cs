using System;
using System.Collections.Generic;

namespace FormQuestionAndAnswer.Models;

public partial class Question
{
    public int Id { get; set; }

    public string? QuestionContent { get; set; }

    public int? AnswerTypeId { get; set; }

    public string? AnswerContent { get; set; }

    public virtual ICollection<AnswerOption> AnswerOptions { get; set; } = new List<AnswerOption>();

    public virtual AnswerType? AnswerType { get; set; }

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();
}
