using System;
using System.Collections.Generic;

namespace Zork
{
    class Program
    {
        //private static Room CurrentRoom
        //{
        //    get
        //    {
        //        return Rooms[_location.Row, _location.Column];
        //    }
        //}

        static void Main(string[] args)
        {
            //InitializeRoomDescriptions();
            Console.WriteLine("Welcome to Zork");

            Game game = new Game();
            game.Run();

            //Room previousRoom = null;

            //bool isRunning = true;
            //while (isRunning)
            {
                //Console.WriteLine(CurrentRoom);
                //if(ReferenceEquals(previousRoom, CurrentRoom) == false && CurrentRoom.HasBeenVisited == false)
                //{
                //    Console.WriteLine(CurrentRoom.Description);
                //    previousRoom = CurrentRoom;
                //    CurrentRoom.HasBeenVisited = true;
                //}

                //Console.Write("> ");
                //string inputstring = Console.ReadLine().Trim();
                //Commands command = ToCommand(inputstring);

            //    string outputString;
            //    switch (command)
            //    {
            //        case Commands.QUIT:
            //            isRunning = false;
            //            outputString = "Thank you for playing!";
            //            break;

            //        case Commands.LOOK:
            //            outputString = CurrentRoom.Description;
            //            break;

            //        case Commands.NORTH:
            //        case Commands.SOUTH:
            //        case Commands.WEST:
            //        case Commands.EAST:
            //            if (Move(command))
            //            {
            //                outputString = $"You moved {command}.";
            //            }
            //            else
            //            {
            //                outputString = "The way is shut!";
            //            }
            //            break;

            //        default:
            //            outputString = "Unknown command.";
            //            break;
            //    }

            //    Console.WriteLine(outputString);
            }
        }

        //static Commands ToCommand(string commandString) => Enum.TryParse<Commands>(commandString, true, out Commands command) ? command : Commands.UNKNOWN;

        //private static bool Move(Commands command)
        //{
        //    bool didMove;

        //    switch (command)
        //    {
        //        case Commands.NORTH when _location.Row < Rooms.GetLength(0) -1:
        //            _location.Row++;
        //            didMove = true;
        //            break;

        //        case Commands.SOUTH when _location.Row > 0:
        //            _location.Row--;
        //            didMove = true;
        //            break;

        //        case Commands.WEST when _location.Column > 0:
        //            _location.Column--;
        //            didMove = true;
        //            break;

        //        case Commands.EAST when _location.Column < Rooms.GetLength(1) - 1:
        //            _location.Column++;
        //            didMove = true;
        //            break;

        //        default:
        //            didMove = false;
        //            break;
        //    }

        //    return didMove;
        //}

        //private static void InitializeRoomDescriptions()
        //{
        //    var roomMap = new Dictionary<string, Room>();

        //    foreach(Room room in Rooms)
        //    {
        //        roomMap.Add(room.Name, room);
        //    }

        //    roomMap["Rocky Trail"].Description = "You are on a rock-strewen trail.";
        //    roomMap["South of House"].Description = "You are facing the South of a white house. There is no door here, all the windows are barred.";
        //    roomMap["Canyon View"].Description = "You are at the top of the Great Canyon on its south wall.";
            
        //    roomMap["Forest"].Description = "This is a forest, with trees in all directions around you.";
        //    roomMap["West of House"].Description = "This is an open field west of a white house, with a boarded front door.";
        //    roomMap["Behind House"].Description = "You are behind the white house. In one corner of the house, there is a small window which is slightly ajar.";

        //    roomMap["Dense Woods"].Description = "This is a dimly lit forest, with large trees all around. To the east, there appears to be sunlight.";
        //    roomMap["North of House"].Description = "You are facing the north side of a white house. There is no door here, all the windows are barred.";
        //    roomMap["Clearing"].Description = "You are in a clearing, whit a forest surrounding you on the west and south.";
        //}

        //private static (int Row, int Column) _location = (1, 1);
    }
}
