namespace WebApiDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WelkomController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> GetWelkomBericht()
        {
            return "Welkom bij Programming Advanced! Je eerste eigen endpoint werkt 🎉";
        }

        [HttpGet("gepersonaliseerd/{naam}")]
        public ActionResult<string> GetGepersonaliseerdBericht(string naam)
        {
            return $"Welkom bij Programming Advanced, {naam}! 🚀";
        }
    }
}
