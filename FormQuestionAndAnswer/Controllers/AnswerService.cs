using Dapper;
using FormQuestionAndAnswer.Contexts;
using FormQuestionAndAnswer.Models;

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
            List<Answer> answers = new List<Answer>();
            var question = dbContext.Questions.FirstOrDefault(x => x.Id == answerDTO.question_id);
            if(question != null)
                question.AnswerContent = answerDTO.answer_content ?? null;
            if(answerDTO.answerOptionChosed.Count > 0)
            {
                var answersOld = dbContext.Answers.Where(x => x.QuestionId == answerDTO.question_id).ToList();
                dbContext.Answers.RemoveRange(answersOld);
                dbContext.SaveChanges();

                foreach (var item in answerDTO.answerOptionChosed)
                {
                    Answer answer = new Answer();
                    answer.QuestionId = answerDTO.question_id;
                    answer.AnswerOptionId = item.answer_option_id;
                    answers.Add(answer);
                }
            }
            dbContext.Answers.AddRange(answers);
            dbContext.SaveChanges();
            return true;
        }
    }
}
