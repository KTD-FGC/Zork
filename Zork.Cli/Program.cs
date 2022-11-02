using System;
using System.IO;
using Newtonsoft.Json;
using Zork.Common;

namespace Zork.Cli
{
    class Program
    {


        static void Main(string[] args)
        {
            var output = new ConsoleOutputService();
            var input = new ConsoleInputService();


            Game game = JsonConvert.DeserializeObject<Game>(File.ReadAllText(@"Content\Game.json"));
            Console.WriteLine("Welcome to Zork");
            game.Run(input, output);
            while (game.IsRunning)
            {
                if (game.IsRunning)
                {
                    game.Output.WriteLine(game.Player.CurrentRoom);
                    if (game.PreviousRoom != game.Player.CurrentRoom)
                    {
                        game.Output.WriteLine(game.Player.CurrentRoom.Description);
                        foreach (Item item in game.Player.CurrentRoom.Inventory)
                        {
                            game.Output.WriteLine(item.LookDescription);
                        }
                    }
                }
                game.Output.Write("> ");
                input.ProcessInput();
            }
        }

        private enum CommandLineArguments
        {
            GameFilename = 0
        }

    }
}
