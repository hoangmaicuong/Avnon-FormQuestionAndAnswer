using System.Data.SqlClient;

namespace FormQuestionAndAnswer.Contexts
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string connectionString;
        public DapperContext(IConfiguration configuration) 
        {
            this._configuration = configuration;
            connectionString = _configuration.GetConnectionString("DapperConnection");
        }
        public SqlConnection CreateConnection()
        {
            return new SqlConnection("Data Source=MAICUONG-LAP;Initial Catalog=FormQuestionAndAnswer;User ID=sa;Password=123;TrustServerCertificate=True;");
        }
    }
}
