using Dapper;
using FormQuestionAndAnswer.Contexts;
using FormQuestionAndAnswer.Models;
using Microsoft.EntityFrameworkCore;

namespace FormQuestionAndAnswer.Controllers
{
    public class QuestionService
    {
        private FormQuestionAndAnswerContext dbContext = new FormQuestionAndAnswerContext();
        private DapperContext dapperContext;
        public QuestionService(DapperContext _dapperContext)
        {
            dapperContext = _dapperContext;
        }
        public object Get()
        {
            var query = "EXEC GetQuestion";
            var parameters = new DynamicParameters();
            var result = new Dictionary<string, object>();
            using (var connec = dapperContext.CreateConnection())
            {
                var data = connec.QueryMultiple(query, parameters);
                var Questions = data.Read();
                var AnswerType = data.Read();

                result["Questions"] = Questions;
                result["AnswerType"] = AnswerType;

                return result;
            }
        }
        public object Post(QuestionDTO questionDTO)
        {
            Question question = new Question();
            int? aswerTypeID = dbContext.AnswerTypes.FirstOrDefault(x => x.Code == questionDTO.answerTypeCode)?.Id;

            question.QuestionContent = questionDTO.questionContent;
            question.AnswerTypeId = aswerTypeID;
            if(questionDTO.answerTypeCode == "ChooseAS" && aswerTypeID != null)
            {
                foreach(var item in questionDTO.answerOptionDTOs)
                {
                    var answerOption = new AnswerOption();
                    answerOption.OptionAnswerContent = item.optionAnswerContent;
                    question.AnswerOptions.Add(answerOption);
                }
            }
            dbContext.Questions.Add(question);
            dbContext.SaveChanges();
            return true;
        }
    }
}
