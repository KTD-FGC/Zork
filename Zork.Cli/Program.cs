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
            game.Run(input, output);
            while (game.IsRunning)
            {
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
