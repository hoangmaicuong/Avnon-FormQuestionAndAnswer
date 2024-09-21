using FormQuestionAndAnswer.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FormQuestionAndAnswer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        public AnswerService answerService;
        public AnswerController(AnswerService _answerService) 
        {
            answerService = _answerService;
        }
        [HttpGet("{questionTitleId}")]
        public object Get(int questionTitleId)
        {
            return answerService.Get(questionTitleId);
        }
        [HttpPost]
        public object Post(AnswerDTO answerDTO)
        {
            return answerService.Post(answerDTO);
        }
    }
}
