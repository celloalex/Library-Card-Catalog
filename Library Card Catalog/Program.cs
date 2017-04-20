using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Card_Catalog
{
    class Program
    {
        static void Main(string[] args)
        {
            //THINGS TO KEEP
            //need someway to load file in the event that it has been run on users computer before
            //LOTSA LOTSA STUDYING MWUARGH (arnold mwuargh)

            //bool to keep the do while loop running until user decideds to exit
            bool programEnd = true;

            //do while loop that keeps running until user decides to exit
            do
            {
                MainMenu();

                //Found an exception will pop if user does not enter any input/or user input is horribly off... 
                //I put a try catch to keep the exception at bay.
                try
                {
                    string userStringInput = Console.ReadLine();
                    int userInput = Convert.ToInt32(userStringInput);

                    EvaluateUserInput(userInput);
                }
                catch
                {
                    Console.WriteLine("Sorry your input was incorrect dingus...");
                    Console.WriteLine("Please hit the enter key ONCE to continue.");
                    Console.ReadLine();          
                }          
            } while (programEnd == true);

            Console.ReadLine();
        }

        // Main display menu
        static void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the library! Please select from the following options:");
            Console.WriteLine("1) List of all our books");
            Console.WriteLine("2) Add a Book to our catalog");
            Console.WriteLine("3) Save all changes and exit the program");
            Console.WriteLine();
            Console.WriteLine("Please select one of the following choices.");
        }

        //Evaluates user Input from Main Menu 
        private static void EvaluateUserInput(int userInput)
        {
            Console.Clear();
            Console.WriteLine("You entered {0}...", userInput);

            if(userInput == 1)
            {
                Console.WriteLine("Fantastic this is a list of all our books:");
                //Console.readline is here for testing purposes
                Console.ReadLine();
                //call another method
            }
            else if (userInput == 2)
            {
                Console.WriteLine("Fantastic! Lets add a new book to our catalog.");
                //Console.readline is here for testing purposes
                Console.ReadLine();
                //call another method
            }
            else if(userInput == 3)
            {
                Console.WriteLine("Thank you for visiting the library!");
                //Console.readline is here for testing purposes
                Console.ReadLine();
                //changes boolean to false to terminate program
            }
            else
            {
                Console.WriteLine("Sorry... You did not enter valid input!");
                Console.ReadLine();
            }

        }
    }

    class UserInput
    {
        //Excitement about to start here m8!!!!
    }
}
