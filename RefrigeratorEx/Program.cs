using System;

namespace RefrigeratorEx
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                RefrigeratorGame game = new RefrigeratorGame();
                game.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}