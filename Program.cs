// See https://aka.ms/new-console-template for more information
using CountdownLetterGame.Repositories;
using CountdownLetterGame.Services;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
            .AddTransient<IWordDataRepository, WordDataRepository>()
            .AddTransient<ICountDownGameService, CountDownGameService>()
            .BuildServiceProvider();

var game = serviceProvider.GetService<ICountDownGameService>();

Console.WriteLine("Welcome to Countdown Letters Game!");
Console.WriteLine("You will play 4 rounds.");

for (int round = 1; round <= 4; round++)
{
    Console.WriteLine($"\nStarting Round {round}:");
    game?.CountDownLetterGame();
}

Console.WriteLine("\nThank you for playing!");
Console.WriteLine($"Final Score is {game?.FinalScore ?? 0}");
