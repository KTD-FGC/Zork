using System;
using System.Collections.Generic;
using System.Text;

namespace Zork
{
    internal class Game
    {
        public World World { get; }

        public Player Player { get; }

        public Game(World world, string startingLocation)
        {
            World = world;
            Player = new Player(World, startingLocation);
        }

        public void Run()
        {
            Room previousRoom = null;

            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine(Player.CurrentRoom);
                if (ReferenceEquals(previousRoom, Player.CurrentRoom) == false && Player.CurrentRoom.HasBeenVisited == false)
                {
                    Console.WriteLine(Player.CurrentRoom.Description);
                    previousRoom = Player.CurrentRoom;
                    Player.CurrentRoom.HasBeenVisited = true;
                }

                Console.Write("> ");
                string inputstring = Console.ReadLine().Trim();
                Commands command = ToCommand(inputstring);

                string outputString;
                switch (command)
                {
                    case Commands.QUIT:
                        isRunning = false;
                        outputString = "Thank you for playing!";
                        break;

                    case Commands.LOOK:
                        outputString = Player.CurrentRoom.Description;
                        break;

                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.WEST:
                    case Commands.EAST:
                        Directions direction = (Directions)command;
                        if (Player.Move(direction) == false)
                        {
                            outputString = $"You moved {command}.";
                        }
                        else
                        {
                            outputString = "The way is shut!";
                        }
                        break;

                    default:
                        outputString = "Unknown command.";
                        break;
                }

                Console.WriteLine(outputString);
            }
        }
        private static Commands ToCommand(string commandString) => Enum.TryParse(commandString, true, out Commands result) ? result : Commands.UNKNOWN;
    }
}
