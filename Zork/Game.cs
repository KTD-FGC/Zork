using System;
using System.Collections.Generic;
using System.Text;

namespace Zork
{
    internal class Game
    {
        public World World { get; set; }

        public Player Player { get; set; }

        public void Run()
        {
            InitializeRoomDescriptions();

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
            private void InitializeRoomDescriptions()
            {
                var roomMap = new Dictionary<string, Room>();

                foreach (Room room in Rooms)
                {
                    roomMap.Add(room.Name, room);
                }

                roomMap["Rocky Trail"].Description = "You are on a rock-strewen trail.";
                roomMap["South of House"].Description = "You are facing the South of a white house. There is no door here, all the windows are barred.";
                roomMap["Canyon View"].Description = "You are at the top of the Great Canyon on its south wall.";

                roomMap["Forest"].Description = "This is a forest, with trees in all directions around you.";
                roomMap["West of House"].Description = "This is an open field west of a white house, with a boarded front door.";
                roomMap["Behind House"].Description = "You are behind the white house. In one corner of the house, there is a small window which is slightly ajar.";

                roomMap["Dense Woods"].Description = "This is a dimly lit forest, with large trees all around. To the east, there appears to be sunlight.";
                roomMap["North of House"].Description = "You are facing the north side of a white house. There is no door here, all the windows are barred.";
                roomMap["Clearing"].Description = "You are in a clearing, whit a forest surrounding you on the west and south.";
            }

        static Commands ToCommand(string commandString) => Enum.TryParse<Commands>(commandString, true, out Commands command) ? command : Commands.UNKNOWN;
    }
}
