using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Security;
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
                char separator = ' ';
                string[] commandTokens = inputstring.Split(separator);
                Commands command = Commands.UNKNOWN;
                string subject = null;
                switch (commandTokens.Length)
                {
                    case 0:
                        continue;
                    case 1:
                        command = ToCommand(commandTokens[0]);
                        break;
                    case 2:
                        command = ToCommand(commandTokens[0]);
                        subject = commandTokens[1];
                        break;
                    default:
                        break;
                }

                string outputString;
                switch (command)
                {
                    case Commands.QUIT:
                        isRunning = false;
                        outputString = "Thank you for playing!";
                        break;

                    case Commands.LOOK:
                        outputString = Player.CurrentRoom.Description;
                        Player.Moves++;
                        break;

                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.WEST:
                    case Commands.EAST:
                        Directions direction = Enum.Parse<Directions>(command.ToString(), true);
                        if (Player.Move(direction))
                        {
                            outputString = $"You moved {command}.";
                        }
                        else
                        {
                            outputString = "The way is shut!";
                        }
                        Player.Moves++;
                        break;

                    case Commands.REWARD:
                        Player.Score++;
                        outputString = "Your score went up!";
                        Player.Moves++;
                        break;

                    case Commands.SCORE:
                        outputString = $"Your score would be {Player.Score} in {Player.Moves} move(s).";
                        Player.Moves++;
                        break;

                    case Commands.DROP:
                        outputString = null;
                        break;

                    case Commands.TAKE:
                        outputString = null;
                        break;

                    case Commands.INVENTORY:
                        outputString = null;
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
