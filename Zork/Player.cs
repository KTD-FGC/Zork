﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Zork
{
    internal class Player
    {
        public Room CurrentRoom
        {
            get
            {
                return _world.Rooms[_location.Row, _location.Column];
            }
        }
        public int score { get; }
        
        public int Moves { get; }

        public Player (World world)
        {
            world = _world;
        }

        static Commands ToCommand(string commandString) => Enum.TryParse<Commands>(commandString, true, out Commands command) ? command : Commands.UNKNOWN;

        public bool Move(Commands command)
        {
            bool didMove;

            switch (command)
            {
                case Commands.NORTH when _location.Row < _world.Rooms.GetLength(0) - 1:
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

                case Commands.EAST when _location.Column < _world.Rooms.GetLength(1) - 1:
                    _location.Column++;
                    didMove = true;
                    break;

                default:
                    didMove = false;
                    break;
            }

            return didMove;
        }

        private World _world;
        private static (int Row, int Column) _location = (1, 1);
    }
}
