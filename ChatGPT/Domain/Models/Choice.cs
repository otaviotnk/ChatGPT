namespace ChatGPT.Domain.Models
{
    public sealed class Choice
    {
        public string? Text { get; set; }
        public int Index { get; set; }
        public object? LogProbs { get; set; }
        public string? FinishReason { get; set; }
    }
}
