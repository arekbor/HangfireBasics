using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HangfireBasics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangfireController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() {
            return Ok("Hello hangfire");
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult Welcome() {
            var jobId = BackgroundJob.Enqueue(() => 
                SendWelcomeEmail("Welcome to our app"));
            return Ok($"Job ID: {jobId} - welcome email sent to user");
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult Discount()
        {
            var jobId = BackgroundJob.Schedule(() => 
                SendWelcomeEmail("Welcome to our app"),TimeSpan.FromSeconds(20));
            return Ok($"Job ID: {jobId} - Discount email will be sent in 30 seconds");
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult DatabaseUpdate() {
            RecurringJob.AddOrUpdate(() => Console.WriteLine("Database updated"),Cron.Minutely);

            return Ok($"database check job initiaded");
        }

        public void SendWelcomeEmail(string email) {
            Console.WriteLine(email);
        }
    }
}
