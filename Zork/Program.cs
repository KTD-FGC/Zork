using System;

namespace Zork
{
    class Program
    {
        private static Room CurrentRoom
        {
            get
            {
                return Rooms[_location.Row, _location.Column];
            }
        }

        static void Main(string[] args)
        {
            Room westOfHouse = new Room("WestOfHouse");

            Console.WriteLine(westOfHouse.Name);
            Console.WriteLine(westOfHouse.Description);

            Console.WriteLine("Welcome to Zork");

            bool isRunning = true;
            while (isRunning)
            {
                Console.Write($"{CurrentRoom}\n ");
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
                        outputString = CurrentRoom.Description;
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

        private static void InitializeRoomDescriptions()
        {
            Rooms[0, 0].Description = "You are on a rock-strewen trail.";
            Rooms[0, 1].Description = "You are facing the South of a white house. There is no door here, all the windows are barred.";
            Rooms[0, 2].Description = "You are at the top of the Great Canyon on its south wall.";

            Rooms[1, 0].Description = "This is a forest, with trees in all directions around you.";
            Rooms[1, 1].Description = "This is an open field west of a white house, with a boarded front door.";
            Rooms[1, 2].Description = "You are behind the white house. In one corner of the house, there is a small window which is slightly ajar.";

            Rooms[2, 0].Description = "This is a dimly lit forest, with large trees all around. To the east, there appears to be sunlight.";
            Rooms[2, 1].Description = "You are facing the north side of a white house. There is no door here, all the windows are barred.";
            Rooms[2, 2].Description = "You are in a clearing, whit a forest surrounding you on the west and south.";
        }

        private static readonly Room[,] Rooms = {
            { new Room ("Rocky Trail"), new Room ("South of House"), new Room("Canyon View") },
            {new Room ("Forest"), new Room ("West of House"), new Room ("Behind House") },
            {new Room ("Dense Woods"), new Room ("North of House"), new Room ("Clearing") }
        };

        private static (int Row, int Column) _location = (1, 1);
    }
}
