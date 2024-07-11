using Hangfire;

namespace HangfireDemo;

public class JobProcessor
{
  private readonly IHttpClientFactory _httpClientFactory;

  public JobProcessor(IHttpClientFactory httpClientFactory)
  {
    _httpClientFactory = httpClientFactory;
  }

  [AutomaticRetry(Attempts = 0)]
  public async Task ProcessJobAsync(string jobId)
  {
    // Simulate job processing
    await Task.Delay(5000); // Simulating work by delaying for 5 seconds

    var result = new JobResult
    {
      JobId = jobId,
      Status = "Completed",
      // Additional result data
    };

    // Notify BFF service
    var client = _httpClientFactory.CreateClient();
    await client.PostAsJsonAsync("https://localhost:7065/api/job/notifyJobCompletion", result);
  }
}

public class JobResult
{
  public string JobId { get; set; }
  public string Status { get; set; }
  // Additional result data
}
