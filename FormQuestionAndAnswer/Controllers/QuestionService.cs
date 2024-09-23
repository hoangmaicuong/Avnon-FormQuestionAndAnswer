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
        public object Post(QuestionTitleDTO questionDTO)
        {
            QuestionTitle questionTitle = new QuestionTitle();
            Question question;
            AnswerOption answerOption;
            var answerTypesAll = dbContext.AnswerTypes.ToList();
            

            questionTitle.QuestionTitleContent = questionDTO.questionTitleContent;
            foreach(var itemQuestion in questionDTO.questions)
            {
                question = new Question();
                int? aswerTypeID = answerTypesAll.FirstOrDefault(x => x.Code?.Trim() == itemQuestion.answerTypeCode?.Trim())?.Id;

                question.QuestionContent = itemQuestion.questionContent;
                question.AnswerTypeId = aswerTypeID;
                question.IsRequired = itemQuestion.isRequired;
                if (aswerTypeID != null && itemQuestion.answerTypeCode?.Trim() == "ChooseAS")
                {
                    foreach (var itemAnswerOption in itemQuestion.answerOptionDTOs)
                    {
                        answerOption = new AnswerOption();
                        answerOption.OptionAnswerContent = itemAnswerOption.optionAnswerContent;
                        question.AnswerOptions.Add(answerOption);
                    }
                }
                questionTitle.Questions.Add(question);
            }
            dbContext.QuestionTitles.Add(questionTitle);
            dbContext.SaveChanges();
            return true;
        }
    }
}
