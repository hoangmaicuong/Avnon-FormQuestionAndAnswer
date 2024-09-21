using Dapper;
using FormQuestionAndAnswer.Contexts;
using FormQuestionAndAnswer.Models;
using Microsoft.EntityFrameworkCore;

namespace FormQuestionAndAnswer.Controllers
{
    public class AnswerService
    {
        private FormQuestionAndAnswerContext dbContext = new FormQuestionAndAnswerContext();
        private DapperContext dapperContext;
        public AnswerService(DapperContext _dapperContext)
        {
            dapperContext = _dapperContext;
        }
        public object Get(int questionTitleId)
        {
            var query = $"EXEC GetQuestionTitle @ID = {questionTitleId}";
            var parameters = new DynamicParameters();
            var result = new Dictionary<string, object>();
            using (var connec = dapperContext.CreateConnection())
            {
                var data = connec.QueryMultiple(query, parameters);
                var View = data.Read();
                var Answer = data.Read();

                result["View"] = View;
                result["Answer"] = Answer;

                return result;
            }
        }
        public object Post(AnswerDTO answerDTO)
        {
            List<Answer> answers;
            List<Answer> answerRemoves;
            foreach (var item in answerDTO.questions)
            {
                answers = new List<Answer>();
                answerRemoves = new List<Answer>();
                var question = dbContext.Questions.FirstOrDefault(x => x.Id == item.questionId);
                if (question != null)
                {
                    question.AnswerContent = item.answerContent ?? null;
                    dbContext.Entry(question).State = EntityState.Modified;
                }
                if (item.answerOptionChosed.Count > 0)
                {
                    //var answersOld = dbContext.Answers.Where(x => x.QuestionId == item.questionId).ToList();
                    //dbContext.Answers.RemoveRange(answersOld);
                    //dbContext.SaveChanges();
                    var answerAll = dbContext.Answers.Where(x => x.QuestionId == item.questionId).ToList();
                    foreach (var itemOption in item.answerOptionChosed)
                    {
                        Answer answer = new Answer();
                        if(itemOption.answerId > 0)
                        {
                            //Sữa
                            var answerFilter = answerAll.FirstOrDefault(x => x.Id == itemOption.answerId);
                            answerFilter.AnswerOptionId = itemOption.answerOptionId;
                            dbContext.Entry(answerFilter).State = EntityState.Modified;
                        }
                        else
                        {
                            // Thêm
                            answer.QuestionId = item.questionId;
                            answer.AnswerOptionId = itemOption.answerOptionId;
                            answers.Add(answer);
                        }
                    }
                    //remove dữ liệu cũ bỏ chọn
                    foreach(var itemAnswer in answerAll)
                    {
                        var isChose = item.answerOptionChosed.Any(x => x.answerId == itemAnswer.Id);
                        if(!isChose) answerRemoves.Add(itemAnswer);
                    }
                }
                dbContext.Answers.RemoveRange(answerRemoves);
                dbContext.Answers.AddRange(answers);
            }
            dbContext.SaveChanges();
            return true;
        }
    }
}
