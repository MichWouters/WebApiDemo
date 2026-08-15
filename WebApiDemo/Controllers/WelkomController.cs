using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WelkomController : ControllerBase
    {
        // Dit endpoint reageert op: GET /welkom
        [HttpGet]
        public string GetWelkomBericht()
        {
            return "Welkom bij Programming Advanced! Je eerste eigen endpoint werkt 🎉";
        }

        // Dit endpoint reageert op: GET /welkom/gepersonaliseerd/Anna
        [HttpGet("gepersonaliseerd/{naam}")]
        public string GetGepersonaliseerdBericht(string naam)
        {
            return $"Welkom bij Programming Advanced, {naam}! 🚀";
        }
    }
}
