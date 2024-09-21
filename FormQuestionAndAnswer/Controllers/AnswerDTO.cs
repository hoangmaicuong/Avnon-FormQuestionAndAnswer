namespace FormQuestionAndAnswer.Controllers
{
    public class AnswerDTO
    {
        public AnswerDTO() 
        {
            questions = new List<AnswerQuestionDTO>();
        }
        public int? questionTitleId { get; set; }
        public string questionTitleContent { get; set; } = null;
        public List<AnswerQuestionDTO> questions { get; set; }
    }
    public class AnswerQuestionDTO
    {
        public AnswerQuestionDTO()
        {
            answerOptionChosed = new List<AnswerOptionChosed>();
        }
        public int? questionId { get; set; }
        public string questionContent { get; set; }
        public bool isRequired { get; set; }
        public int? answerTypeId { get; set; }
        public string? answerTypeCode { get; set; }
        public string? answerContent { get; set; }
        public List<AnswerOptionChosed> answerOptionChosed { get; set; }
    }
    public class AnswerOptionChosed
    {
        public int? answerId { get; set; }
        public int? answerOptionId { get; set; }
        public string optionAnswerContent { get; set; }
    }
}
