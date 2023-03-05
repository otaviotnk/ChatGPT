namespace ChatGPT.Infrastructure.Interfaces
{
    public interface IChatGPTService
    {
        Task<string> CallOpenAI(string question);
    }
}
