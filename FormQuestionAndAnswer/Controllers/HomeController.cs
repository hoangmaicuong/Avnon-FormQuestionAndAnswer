using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FormQuestionAndAnswer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        // GET: api/<HomeController>
        public HomeService homeService;
        public HomeController(HomeService _homeService)
        {
            homeService = _homeService;
        }
        [HttpGet]
        public object Get()
        {
            return homeService.Get();
        }
    }
}
