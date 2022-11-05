using System;
using System.Text;

namespace Zork.Common
{
    public class Game
    {
        public World World { get; }

        public Player Player { get; }

        public bool IsRunning { get; private set; }

        public Room PreviousRoom { get; private set; }

        public IOutputService Output { get; private set; }

        public IInputService Input { get; private set; }

        public Game(World world, string startingLocation)
        {
            World = world;
            Player = new Player(World, startingLocation);
        }

        public void Run(IInputService input, IOutputService output)
        {
            Output = output;
            Input = input;
            IsRunning = true;
            Input.InputReceived += Input_InputReceived;


            
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
            if (subject != null && World.ItemsByName.TryGetValue(subject, out Item i))
            {
                theItem = i;
            }
            StringBuilder sb = new StringBuilder();
            string outputString;
            switch (command)
            {
                case Commands.QUIT:
                    outputString = "Thank you for playing!\n";
                    IsRunning = false;
                    break;

                case Commands.LOOK:
                    sb.Append($"{Player.CurrentRoom.Description}\n");
                    foreach (Item item in Player.CurrentRoom.Inventory)
                    {
                        sb.Append($"{item.LookDescription}\n");
                    }
                    outputString = sb.ToString();
                    Player.Moves++;
                    break;

                case Commands.NORTH:
                case Commands.SOUTH:
                case Commands.WEST:
                case Commands.EAST:
                    Directions direction = Enum.Parse<Directions>(command.ToString(), true);
                    if (Player.Move(direction))
                    {
                        outputString = $"You moved {command}.\n";
                    }
                    else
                    {
                        outputString = "The way is shut!\n";
                    }
                    Player.Moves++;
                    break;

                case Commands.REWARD:
                    Player.Score++;
                    outputString = "Your score went up!\n";
                    Player.Moves++;
                    break;

                case Commands.SCORE:
                    outputString = $"Your score would be {Player.Score} in {Player.Moves} move(s).\n";
                    Player.Moves++;
                    break;

                case Commands.DROP:
                    if (subject == null)
                    {
                        outputString = "What do you want to drop?";
                    }
                    else
                    {
                        outputString = Player.RemoveFromInv(theItem);
                    }
                    break;

                case Commands.TAKE:
                    if (subject == null)
                    {
                        outputString = "What do you want to take?";
                    }
                    else
                    {
                        outputString = Player.AddToInv(theItem);
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
                        outputString = sb.ToString();
                        
                    }
                    else
                    {
                        outputString = "You are empty handed.\n";
                    }
                    break;

                default:
                    outputString = "Unknown command.\n";
                    break;
            }

            Output.WriteLine(outputString);
        }

        private static Commands ToCommand(string commandString) => Enum.TryParse(commandString, true, out Commands result) ? result : Commands.UNKNOWN;
    }
}
