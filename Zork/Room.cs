using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Zork
{
    public class Room
    {
        [JsonProperty(Order = 1)]
        public string Name { get; private set; }
        
        [JsonProperty(Order = 2)]
        public string Description { get; private set; }

        [JsonProperty(PropertyName = "Neighbors", Order = 3)]
        private Dictionary<Directions, string> NeighborNames { get; set; }

        [JsonIgnore]
        public Dictionary<Directions, Room> Neighbors { get; private set; }

        [JsonIgnore]
        public List<Item> Inventory { get; private set; }

        [JsonProperty("Inventory")]
        private string[] InventoryNames { get; }

        public bool HasBeenVisited { get; set; }

        public Room(string name, string description, Dictionary<Directions, string> neighborNames, string[] inventoryNames)
        {
            Name = name;
            Description = description;
            Neighbors = new Dictionary<Directions, Room>();
            NeighborNames = neighborNames ?? new Dictionary<Directions, string>();
            InventoryNames = inventoryNames ?? new string[0];
        }

        public void UpdateNeighbors(World world)
        {
            foreach (KeyValuePair<Directions, string> neighborName in NeighborNames)
            {
                Neighbors.Add(neighborName.Key, world.RoomsByName[neighborName.Value]);
            }

            //NeighborNames = null;
        }

        public void UpdateInventory(World world)
        {
            Inventory = new list<Item>();
            foreach (var inventoryName in InventoryNames)
            {
                Inventory.Add(world.ItemsByName[inventoryName]);
            }
            InventoryNames = null;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
