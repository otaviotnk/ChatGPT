#pragma warning disable CS8604
#pragma warning disable CS8602

using ChatGPT;
using ChatGPT.Common;
using ChatGPT.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;

IServiceCollection services = new ServiceCollection();
Startup startup = new();
startup.ConfigureServices(services);
IServiceProvider serviceProvider = services.BuildServiceProvider();

var chatGPTService = serviceProvider.GetService<IChatGPTService>();
string? keyPressed;

do
{
    CommonMethods.AskQuestion();
    var question = Console.ReadLine();
    await GetAnswerAsync(question);
    keyPressed = Console.ReadLine();
    Console.Clear();
}
while (string.IsNullOrEmpty(keyPressed));
{
    Console.Clear();
    CommonMethods.AskQuestion();
    var question = Console.ReadLine();
    await GetAnswerAsync(question);
    Console.ReadLine();    
}

async Task GetAnswerAsync(string question)
{
    CommonMethods.Typewriter();
    var answer = await chatGPTService.CallOpenAI(question);
    CommonMethods.Typewriter();
    Console.WriteLine();
    Console.WriteLine(answer);
}
