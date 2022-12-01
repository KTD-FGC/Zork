using Newtonsoft.Json;
using System;
using System.Text;

namespace Zork.Common
{
    public class Game
    {
        public World World { get; }

        [JsonIgnore]
        public Player Player { get; }

        [JsonIgnore]
        public bool IsRunning { get; private set; }

        public Room PreviousRoom { get; private set; }

        [JsonIgnore]
        public IOutputService Output { get; private set; }

        [JsonIgnore]
        public IInputService Input { get; private set; }

        public Game(World world, string startingLocation)
        {
            World = world;
            Player = new Player(World, startingLocation);
        }

        public void Run(IInputService input, IOutputService output)
        {
            Output = output ?? throw new ArgumentNullException(nameof(output));
            Input = input ?? throw new ArgumentNullException(nameof(input)); ;
            
            IsRunning = true;
            Input.InputReceived += Input_InputReceived;
            Output.WriteLine("Welcome to Zork");
            Output.WriteLine(Player.CurrentRoom);
            Look();

        }
        private void Input_InputReceived(object sender, string inputString)
        {
            PreviousRoom = Player.CurrentRoom;
            const char separator = ' ';
            string[] commandTokens = inputString.Split(separator);
            Commands command = Commands.UNKNOWN;
            string subject = null;
            switch (commandTokens.Length)
            {
                case 0:
                    break;
                case 1:
                    command = ToCommand(commandTokens[0]);
                    break;
                case 2:
                    command = ToCommand(commandTokens[0]);
                    subject = commandTokens[1];
                    break;
                default:
                    break;
            }

            Item theItem = null;
            Enemy theEnemy = null;
            if (subject != null && World.ItemsByName.TryGetValue(subject, out Item i))
            {
                theItem = i;
            }

            if (subject != null && World.EnemiesByName.TryGetValue(subject, out Enemy e))
            {
                theEnemy = e;
            }
            StringBuilder sb = new StringBuilder();
            switch (command)
            {
                case Commands.QUIT:
                    Output.WriteLine("Thank you for playing!\n");
                    IsRunning = false;
                    break;
                case Commands.LOOK:
                    Look();
                    break;
                case Commands.NORTH:
                case Commands.SOUTH:
                case Commands.WEST:
                case Commands.EAST:
                    Directions direction = Enum.Parse<Directions>(command.ToString(), true);
                    if (Player.Move(direction))
                    {
                        Output.WriteLine($"You moved {command}.\n");
                        
                    }
                    else
                    {
                        Output.WriteLine("The way is shut!\n");
                    }
                    break;
                case Commands.REWARD:
                    Player.Score++;
                    Output.WriteLine("Your score went up!\n");
                    break;
                case Commands.SCORE:
                    Output.WriteLine($"Your score would be {Player.Score} in {Player.Moves} move(s).\n");
                    break;
                case Commands.DROP:
                    if (subject == null)
                    {
                        Output.WriteLine("What do you want to drop?");
                    }
                    else
                    {
                        Output.WriteLine(Player.RemoveFromInv(theItem));
                    }
                    break;
                case Commands.TAKE:
                    if (subject == null)
                    {
                        Output.WriteLine("What do you want to take?");
                    }
                    else
                    {
                        Output.WriteLine(Player.AddToInv(theItem));
                    }
                    break;
                case Commands.INVENTORY:
                    if (Player.Inventory != null)
                    {
                        sb.Append("Inventory:\n");
                        foreach (Item item in Player.Inventory)
                        {
                            sb.Append($"{item.InvDescription}\n");
                        }
                        Output.WriteLine(sb.ToString());
                    }
                    else
                    {
                        Output.WriteLine("You are empty handed.\n");
                    }
                    break;
                case Commands.ATTACK:
                    if (subject == null)
                    {
                        Output.WriteLine("What do you want to attack");
                    }
                    else
                    {
                        Output.WriteLine(Player.AttackEnemy(theEnemy));
                    }
                    break;
                default:
                    Output.WriteLine("Unknown command.\n");
                    break;
            }

            if (command != Commands.UNKNOWN)
            {
                Player.Moves++;
            }

            Output.WriteLine($"{Player.CurrentRoom}");
            if (ReferenceEquals(PreviousRoom, Player.CurrentRoom) == false)
            {
                Look();
                Encounter();
            }
        }

        private void Look()
        {
            Output.WriteLine(Player.CurrentRoom.Description);
            foreach (Item item in Player.CurrentRoom.Inventory)
            {
                Output.WriteLine(item.LookDescription);
            }
            foreach (Enemy enemy in Player.CurrentRoom.Foes)
            {
                if (enemy.IsAlive == true)
                {
                    Output.WriteLine(enemy.LivingDescription);
                }
                else if (enemy.IsAlive == false)
                {
                    Output.WriteLine(enemy.DeadDescription);
                }
            }
        }

        private void Encounter()
        {
            if (Player.CurrentRoom.Foes.Count > 0)
            {
                foreach (Enemy enemy in Player.CurrentRoom.Foes)
                {
                    if (enemy.IsAlive == true)
                    {
                        if ()
                        {
                            Output.WriteLine("You could not escape and you were killed.");
                            IsRunning = false;
                        }
                    }
                    else if (enemy.IsAlive == false)
                    {

                    }
                }
            }
        }

        private static Commands ToCommand(string commandString) => Enum.TryParse(commandString, true, out Commands result) ? result : Commands.UNKNOWN;
    }
}
