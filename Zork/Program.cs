using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

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
            const string defaultFilename = @"Content\Rooms.json";
            string roomFilename = (args.Length > 0 ? args[(int)CommandLineArguments.RoomsFilename] : defaultFilename);

            InitializeRooms(roomFilename);
            Console.WriteLine("Welcome to Zork");

            Room previousRoom = null;

            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine(CurrentRoom);
                if(ReferenceEquals(previousRoom, CurrentRoom) == false && CurrentRoom.HasBeenVisited == false)
                {
                    Console.WriteLine(CurrentRoom.Description);
                    previousRoom = CurrentRoom;
                    CurrentRoom.HasBeenVisited = true;
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

        private static void InitializeRooms(string roomsFilename)
        {
            Rooms = JsonConvert.DeserializeObject<Room[,]>(File.ReadAllText(roomsFilename));
        }

        private static Room[,] Rooms;

        private enum CommandLineArguments
        {
            RoomsFilename = 0
        }

        private static (int Row, int Column) _location = (1, 1);

    }
}
