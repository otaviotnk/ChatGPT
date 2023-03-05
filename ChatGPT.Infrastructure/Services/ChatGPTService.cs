using ChatGPT.Domain.Models;
using ChatGPT.Infrastructure.Interfaces;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ChatGPT.Infrastructure.Services
{
    public sealed class ChatGPTService : IChatGPTService
    {
        private readonly HttpClient _httpClient;
        private const string API_TOKEN = "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";
        private const int MAX_TOKENS = 250;
        private const double TEMPERATURE = 0.7;
        private const int TOP_P = 1;        
        private const int FREQUENCY_PENALTY = 0;
        private const int PRESENCE_PENALTY = 0;
        
        public ChatGPTService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://api.openai.com/v1/engines/");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", API_TOKEN);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }


        public async Task<string> CallOpenAI(string question)
        {
            var openIaResponse = new OpenAIResponse();            

            try
            {
                var content = new StringContent("{\n  \"prompt\": \"" + question + "\",\n  \"temperature\": " +
                                                  TEMPERATURE.ToString(CultureInfo.InvariantCulture) +
                                                  ",\n  \"max_tokens\": " + MAX_TOKENS +
                                                  ",\n  \"top_p\": " + TOP_P +
                                                  ",\n  \"frequency_penalty\": " + FREQUENCY_PENALTY +
                                                  ",\n  \"presence_penalty\": " + PRESENCE_PENALTY + "\n}"); ;

                var response = await _httpClient.PostAsync("text-davinci-002/completions", content);

                if (response.IsSuccessStatusCode)
                {
                    openIaResponse = JsonSerializer.Deserialize<OpenAIResponse>(response.Content.ReadAsStringAsync().Result);
                }

                return openIaResponse.Choices[0].Text;
            }
            catch (Exception)
            {
                return "error to send message";
            }
        }
    }
}

