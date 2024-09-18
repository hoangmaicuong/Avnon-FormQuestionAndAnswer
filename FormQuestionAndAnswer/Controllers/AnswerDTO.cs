namespace FormQuestionAndAnswer.Controllers
{
    public class AnswerDTO
    {
        public AnswerDTO() 
        {
            answerOptionChosed = new List<AnswerOptionChosed>();
        }
        public int? question_id { get; set; }
        public string answer_content { get; set; } = null;
        public List<AnswerOptionChosed> answerOptionChosed { get; set; }
    }
    public class AnswerOptionChosed
    {
        public int? answer_option_id { get; set; }
        public string option_answer_content { get; set; }
    }
}
