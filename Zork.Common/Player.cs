﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace Zork.Common
{
    public class Player
    {
        public event EventHandler<int> MovesChanged;

        public event EventHandler<Room> LocationChanged;

        public event EventHandler<int> ScoreChanged;

        public Room CurrentRoom
        {
            get => _currentRoom;
            set
            {
                if (_currentRoom != value)
                {
                    _currentRoom = value;
                    LocationChanged?.Invoke(this, CurrentRoom);
                }
            }
        }

        public List<Item> Inventory { get; }

        public int Moves
        {
            get
            {
                return _moves;
            }

            set
            {
                if (_moves != value)
                {
                    _moves = value;
                    MovesChanged?.Invoke(this, _moves);
                }
            }
        }

        public int Score
        {
            get
            {
                return _score;
            }

            set
            {
                if (_score != value)
                {
                    _score = value;
                    ScoreChanged?.Invoke(this, _score);
                }
            }
        }

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
            Inventory = new List<Item>();
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

        public string AddToInv(Item item)
        {
            if (CurrentRoom.Inventory.Count == 0)
            {
                return "There is nothing here.\n";
            }
            else if (item == null || !CurrentRoom.Inventory.Contains(item))
            {
                return "I can see no such thing.\n";
            }
            else
            {
                Inventory.Add(item);
                CurrentRoom.Inventory.Remove(item);
                return "Taken.\n";
            }
        }
        public string RemoveFromInv(Item item)
        {
            if (Inventory.Count == 0)
            {
                return "You are empty handed.\n";
            }
            else if (item == null || !Inventory.Contains(item))
            {
                return "You are not holding that.\n";
            }
            else
            {
                Inventory.Remove(item);
                CurrentRoom.Inventory.Add(item);
                return "Dropped.\n";
            }
        }

        private int _moves;
        private int _score;

        private World _world;
        private Room _currentRoom;
    }
}
