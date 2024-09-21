using FormQuestionAndAnswer.Models;

namespace FormQuestionAndAnswer.Controllers
{
    public class QuestionTitleDTO
    {
        public QuestionTitleDTO()
        {
            questions = new List<QuestionDTO>();
        }
        public int? questionTitleId { set; get; }
        public string? questionTitleContent { set; get; }
        public List<QuestionDTO> questions { set; get; }
    }
    public class QuestionDTO
    {
        public QuestionDTO()
        {
            answerOptionDTOs = new List<AnswerOptionDTO>();
        }
        public int questionId { get; set; }

        public string? questionContent { get; set; }
        public bool? isRequired { set; get; }

        public int? answerTypeId { get; set; }
        public string answerTypeCode { get; set; }

        public string? answerContent { get; set; }
        public List<AnswerOptionDTO> answerOptionDTOs { get; set; }
    }
    public class AnswerOptionDTO
    {
        public int id { get; set; }
        public int? questionId { get; set; }
        public string? optionAnswerContent { get; set; }
    }
}
