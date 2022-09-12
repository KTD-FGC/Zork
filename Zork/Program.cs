using System;

namespace Zork
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork");

            bool isRunning = true;
            while (isRunning)
            {
                Console.Write($"{Rooms[_location.Row, _location.Column]}\n> ");
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
                        outputString = "This is an open field west of a white house, with a boarded front door.\nA rubber mat saying 'Welcome to Zork!' lies by the door.";
                        break;

                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.WEST:
                    case Commands.EAST:
                        if (Move(command))
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

        static Commands ToCommand(string commandString) => Enum.TryParse<Commands>(commandString, true, out Commands command) ? command : Commands.UNKNOWN;

        private static bool Move(Commands command)
        {
            bool didMove;

            switch (command)
            {
                case Commands.NORTH when _location.Row < Rooms.GetLength(0) -1:
                    _location.Row++;
                    didMove = true;
                    break;

                case Commands.SOUTH when _location.Row > 0:
                    _location.Row--;
                    didMove = true;
                    break;

                case Commands.WEST when _location.Column > 0:
                    _location.Column--;
                    didMove = true;
                    break;

                case Commands.EAST when _location.Column < Rooms.GetLength(1) - 1:
                    _location.Column++;
                    didMove = true;
                    break;

                default:
                    didMove = false;
                    break;
            }

            return didMove;
        }

        private static readonly string[,] Rooms = {
            { "Rocky Trail", "South of House", "Canyon View" },
            {"Forest", "West of House", "Behind House"},
            {"Dense Woods", "North of House", "Clearing" }
        };
        //private static Location _location = new Location() { Row = 1, Column = 1 };
        private static (int Row, int Column) _location = (1, 1);
    }

    internal class Location
    {
        //public int Row;
        //public int Column;
    }
}
