using System.Net.Http.Headers;
using System.Text;

namespace CombineMakerAI.Clients
{
    public class OpenAIClient
    {
        private readonly HttpClient _httpClient;
        private readonly string apiKey = "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";  // API açarını buraya daxil edin

        public OpenAIClient()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        }

        public async Task<string> SendRequestAsync(string prompt)
        {
            var requestBody = new
            {
                model = "text-davinci-003",
                prompt = prompt,
                max_tokens = 100
            };

            var content = new StringContent(
                Newtonsoft.Json.JsonConvert.SerializeObject(requestBody),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                return responseData;
            }

            return $"Error: {response.StatusCode}";
        }
    }
}
