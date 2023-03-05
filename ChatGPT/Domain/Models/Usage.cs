namespace ChatGPT.Domain.Models
{
    public sealed class Usage
    {
        public int PromptTokens { get; set; }
        public int CompletionTolkens { get; set; }
        public int TotalTokens { get; set; }
    }
}
