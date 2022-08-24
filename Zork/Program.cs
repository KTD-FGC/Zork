using System;

namespace Zork
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork");

            string inputstring = Console.ReadLine().Trim();
            inputstring = inputstring.ToUpper();
            if (inputstring == "QUIT")
            {
                Console.WriteLine("Thank you for Playing");
            }
            else if (inputstring == "LOOK")
            {
                Console.WriteLine("There is an open field West of a whit house, with a boarded front door. \nA rubber mat saying 'Welcome to Zork!' lies by the door");
            }
            else
            {
                Console.WriteLine($"Unrecognized command: {inputstring}");
            }
        }
    }
}
