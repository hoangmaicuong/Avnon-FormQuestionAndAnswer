using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FormQuestionAndAnswer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        public QuestionService questionService;
        public QuestionController(QuestionService _questionService)
        {
            questionService = _questionService;
        }
        [HttpGet]
        public object Get()
        {
            //return new string[] { "value1", "value2" };
            return questionService.Get();
        }
        [HttpPost]
        public object Post(QuestionDTO questionDTO)
        {
            return questionService.Post(questionDTO);
        }
    }
}
