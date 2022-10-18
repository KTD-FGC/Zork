using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Zork
{
    class Program
    {


        static void Main(string[] args)
        {

            Game game = JsonConvert.DeserializeObject<Game>(File.ReadAllText(@"Content\Game.json"));
            Console.WriteLine("Welcome to Zork");
            game.Run();
        }

        private enum CommandLineArguments
        {
            GameFilename = 0
        }

    }
}
