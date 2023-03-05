namespace ChatGPT.Common
{
    public static class CommonMethods
    {
        public static void AskQuestion()
        {
            Console.WriteLine("---------------------------Console Chat GPT---------------------------------");
            Console.WriteLine("Olá!");
            Console.WriteLine("Me faça uma pergunta!");
            Console.WriteLine("----------------------------------------------------------------------------");
            Console.Write("Pergunta: ");
        }

        public static void Typewriter()
        {
            Console.Write('.');
            Thread.Sleep(500);
            Console.Write('.');
            Thread.Sleep(500);
            Console.Write('.');
            Thread.Sleep(500);
        }
    }
}
