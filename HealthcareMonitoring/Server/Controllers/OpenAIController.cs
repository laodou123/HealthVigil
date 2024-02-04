using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace HealthcareMonitoring.Server.Controllers;

//[Authorize]
[ApiController]
[Route("api/[controller]")]
public class OpenAIController : ControllerBase
{
    private IAzureOpenAIService _openAIService;

    public OpenAIController(IAzureOpenAIService service)
    {
        _openAIService = service;
    }

    [HttpPost]
    public async Task<string> Post([FromBody] string val)
    {
        var result = new StringBuilder();
        await _openAIService.CreateNewTopic();
        await foreach (var chatMessage in _openAIService.GetChatCompletionsStreamingAsync(val))
        {
            result.Append(chatMessage.Content);
        }
        return result.ToString();
    }
}