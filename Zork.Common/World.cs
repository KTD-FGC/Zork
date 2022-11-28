using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.Serialization;

namespace Zork.Common
{
    public class World
    {
        public Room[] Rooms { get; }

        public Item[] Items { get; }

        public Enemy[] Enemies { get; }

        [JsonIgnore]
        public Dictionary<string, Room> RoomsByName { get; }

        [JsonIgnore]
        public Dictionary<string, Item> ItemsByName { get; }

        [JsonIgnore]
        public Dictionary<string, Enemy> EnemiesByName { get; }

        public World(Room[] rooms, Item[] items, Enemy[] enemies)
        {
            Rooms = rooms;
            RoomsByName = new Dictionary<string, Room>(StringComparer.OrdinalIgnoreCase);
            foreach (Room room in rooms)
            {
                RoomsByName.Add(room.Name, room);
            }
            Items = items;
            ItemsByName = new Dictionary<string, Item>(StringComparer.OrdinalIgnoreCase);
            foreach (Item item in items)
            {
                ItemsByName.Add(item.Name, item);
            }
            Enemies = enemies;
            EnemiesByName = new Dictionary<string, Enemy>(StringComparer.OrdinalIgnoreCase);
            foreach (Enemy enemy in enemies)
            {
                EnemiesByName.Add(enemy.Name, enemy);
            }
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext streamingContext)
        {
            foreach (Room room in Rooms)
            {
                room.UpdateNeighbors(this);
                room.UpdateInventory(this);
                room.UpdateEnemies(this);
            }
        }
    }
}
