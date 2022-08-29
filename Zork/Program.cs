using System;

namespace Zork
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork");

            string inputstring = Console.ReadLine().Trim().ToUpper();

            Commands command = ToCommand(inputstring);

            if (command == Commands.QUIT)
            {
                Console.WriteLine("Thank you for Playing");
            }
            else if (command == Commands.LOOK)
            {
                Console.WriteLine("There is an open field West of a whit house, with a boarded front door. \nA rubber mat saying 'Welcome to Zork!' lies by the door");
            }
            else if (command == Commands.NORTH)
            {
                Console.WriteLine("You go North");
            }
            else if (command == Commands.SOUTH)
            {
                Console.WriteLine("You go South");
            }
            else if (command == Commands.WEST)
            {
                Console.WriteLine("You go West");
            }
            else if (command == Commands.EAST)
            {
                Console.WriteLine("You go East");
            }
            else
            {
                Console.WriteLine($"Unrecognized command: {inputstring}");
            }
        }

        static Commands ToCommand(string commandString)
        {
            return Enum.TryParse<Commands>(commandString, true, out Commands command) ? command : Commands.UNKNOWN;
        }

        static bool IsEven(int value)
        {
            return value % 2 == 0 ? true : false;
        }
    }
}
