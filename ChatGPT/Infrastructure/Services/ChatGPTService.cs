using ChatGPT.Domain.Models;
using ChatGPT.Infrastructure.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Globalization;
using System.Net.Http.Headers;

namespace ChatGPT.Infrastructure.Services
{
    public sealed class ChatGPTService : IChatGPTService
    {
        private readonly HttpClient _httpClient;
        private const string API_TOKEN = "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";
        private const int MAX_TOKENS = 250; //Maximum amount of tokens the API can use
        private const double TEMPERATURE = 0.7; //Controls randomness: Result close to 0, more predictable response.
        private const int TOP_P = 1; //Controls diversity of options, default 1
        private const int FREQUENCY_PENALTY = 0; //Higher number = less repetitive response 
        private const int PRESENCE_PENALTY = 0; //Higher number = more likely to start talking about a different topic
        private readonly JsonSerializerSettings _snakeCaseSerializerSettings = new()
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        };

        public ChatGPTService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://api.openai.com/v1/engines/text-davinci-002/completions");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", API_TOKEN);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }


        public async Task<string> CallOpenAI(string question)
        {
            var openIaResponse = new OpenAIResponse();

            try
            {
                var request = new HttpRequestMessage(new HttpMethod(HttpMethod.Post.Method), _httpClient.BaseAddress);

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", API_TOKEN);

                request.Content = new StringContent("{\n  \"prompt\": \"" + question + "\",\n  \"temperature\": " +
                                                    TEMPERATURE.ToString(CultureInfo.InvariantCulture) +
                                                    ",\n  \"max_tokens\": " + MAX_TOKENS +
                                                    ",\n  \"top_p\": " + TOP_P +
                                                    ",\n  \"frequency_penalty\": " + FREQUENCY_PENALTY +
                                                    ",\n  \"presence_penalty\": " + PRESENCE_PENALTY + "\n}");

                request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    openIaResponse = JsonConvert.DeserializeObject<OpenAIResponse>(response.Content.ReadAsStringAsync().Result, _snakeCaseSerializerSettings);
                }

                return openIaResponse?.Choices[0].Text ?? "Resposta não encontrada.";
            }
            catch (Exception)
            {
                return "Erro ao enviar mensagem.";
            }
        }
    }
}

