using FormQuestionAndAnswer.Models;

namespace FormQuestionAndAnswer.Controllers
{
    public class QuestionDTO
    {
        public QuestionDTO() 
        {
            answerOptionDTOs = new List<AnswerOptionDTO>();
        }
        public int id { get; set; }

        public string? questionContent { get; set; }

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
