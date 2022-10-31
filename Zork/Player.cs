﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace Zork
{
    public class Player
    {
        public Room CurrentRoom
        {
            get => _currentRoom;
            set => _currentRoom = value;
        }

        public List<Item> Inventory { get; }

        public int Moves { get; set; }

        public int Score { get; set; }

        [JsonIgnore]
        public string LocationName 
        {
            get { return CurrentRoom?.Name; } 
            set { CurrentRoom = _world.RoomsByName.GetValueOrDefault(value); }
        }

        public Player(World world, string startingLocation)
        {
            _world = world;
            LocationName = startingLocation;
        }

        public bool Move(Directions direction)
        {
            bool didMove = CurrentRoom.Neighbors.TryGetValue(direction, out Room neighbor);
            if (didMove)
            {
                CurrentRoom = neighbor;
            }

            return didMove;
        }

        private World _world;
        private Room _currentRoom;
    }
}
