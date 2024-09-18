using Dapper;
using FormQuestionAndAnswer.Contexts;
using FormQuestionAndAnswer.Models;

namespace FormQuestionAndAnswer.Controllers
{
    public class HomeService
    {
        private FormQuestionAndAnswerContext dbContext = new FormQuestionAndAnswerContext();
        private DapperContext dapperContext;
        public HomeService(DapperContext _dapperContext)
        {
            dapperContext = _dapperContext;
        }
        public object Get()
        {
            var query = "EXEC GetAllQuestion";
            var parameters = new DynamicParameters();
            var result = new Dictionary<string, object>();
            using (var connec = dapperContext.CreateConnection())
            {
                var data = connec.QueryMultiple(query, parameters);
                var Questions = data.Read();

                result["Questions"] = Questions;

                return result;
            }
        }
    }
}
