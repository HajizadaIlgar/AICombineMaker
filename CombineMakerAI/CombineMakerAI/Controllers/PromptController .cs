using CombineMakerAI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace CombineMakerAI.Controllers
{
    public class PromptController : Controller
    {
        private readonly HttpClient _httpClient;

        public PromptController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new PromptModel());
        }
        [HttpPost("generate")]
        public async Task<IActionResult> Generate([FromBody] PromptModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Prompt))
            {
                return BadRequest("Prompt boş ola bilməz.");
            }

            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                prompt = model.Prompt,
                max_tokens = 100
            };

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GetApiKey());

            try
            {
                var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);

                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode, "AI serverindən cavab alınmadı.");
                }

                var responseBody = await response.Content.ReadAsStringAsync();
                return Ok(responseBody);  // JSON formatında cavab qaytarir js ucun
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Xəta baş verdi: {ex.Message}");
            }
        }

        // API açarını mühit dəyişəni və ya konfiqurasiya faylından almaq
        private string GetApiKey()
        {
            return Environment.GetEnvironmentVariable("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") ?? "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";
        }
    }
}
