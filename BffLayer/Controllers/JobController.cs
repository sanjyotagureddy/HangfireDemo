using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace BffLayer.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class JobController : ControllerBase
  {
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IHubContext<JobHub> _hubContext;

    public JobController(IHttpClientFactory httpClientFactory, IHubContext<JobHub> hubContext)
    {
      _httpClientFactory = httpClientFactory;
      _hubContext = hubContext;
    }

    [HttpPost("startJob")]
    public async Task<IActionResult> StartJob()
    {
      var jobId = Guid.NewGuid().ToString();
      var client = _httpClientFactory.CreateClient();
      var response = await client.PostAsJsonAsync("https://localhost:7205/api/job/startJob", new { jobId });

      if (response.IsSuccessStatusCode)
      {
        return Ok(new { JobId = jobId });
      }

      return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
    }

    [HttpPost("notifyJobCompletion")]
    public async Task<IActionResult> NotifyJobCompletion([FromBody] User result)
    {
      await _hubContext.Clients.Group(result.Name).SendAsync("JobCompleted", result);
      return Ok(result);
    }
  }

  public class JobResult
  {
    public string JobId { get; set; }

    public string Status { get; set; }
    // Additional result data
  }
}