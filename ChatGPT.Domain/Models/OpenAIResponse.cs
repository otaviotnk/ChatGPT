namespace ChatGPT.Domain.Models
{
    public sealed class OpenAIResponse
    {
        public OpenAIResponse()
        {
            Choices = new List<Choice>();
            Usage = new Usage();
        }
        public string? Id { get; set; }
        public string? Data { get; set; }
        public int Created { get; set; }
        public string? Model { get; set; }
        public List<Choice> Choices { get; set; }
        public Usage Usage { get; set; }

    }
}
