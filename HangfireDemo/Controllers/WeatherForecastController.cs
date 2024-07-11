using Hangfire;
using Hangfire.Storage;
using Hangfire.Storage.Monitoring;

using Microsoft.AspNetCore.Mvc;

namespace HangfireDemo.Controllers
{
  [ApiController]
  [Route("api/job")]
  public class WeatherForecastController : ControllerBase
  {
    private readonly IMonitoringApi _monitoringApi = JobStorage.Current.GetMonitoringApi();


    private static readonly string[] Summaries = new[]
    {
      "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
      _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public string Get()
    {
      DateTime now = DateTime.Now;
      var addSeconds = now.AddSeconds(10);
      var summary = BackgroundJob.Schedule(() => GetSummary(), addSeconds);
     

      return summary;

    }
    [HttpPost("startJob")]
    public IActionResult StartJob()
    {
      var jobId = Guid.NewGuid().ToString();
      BackgroundJob.Enqueue<JobProcessor>(job => job.ProcessJobAsync(jobId));
      return Ok(new { JobId = jobId });
    }

    [HttpPost(Name = "/jobstatus")]
    [ProducesDefaultResponseType]
    public string Post([FromQuery]string jobId)
    {
      
      
      var details = _monitoringApi.JobDetails(jobId);
      var stat = details.History.First().StateName;
      return stat.Equals("Succeeded") ? details.History.First().Data["Result"] : "waiting";
    }


    [NonAction]
    public string GetSummary()
    {
      Task.Delay(TimeSpan.FromSeconds(10));
      return Summaries[Random.Shared.Next(Summaries.Length)];
    }
  }
}
