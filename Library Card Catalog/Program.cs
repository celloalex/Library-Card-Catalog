using System;
using System.Runtime.Serialization;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Card_Catalog
{
    class Program
    {
        public static bool IsRunning { get; set; }

        static void Main(string[] args)
        {
            //bool is in Program class and allows changes to be made within different methods
            Program.IsRunning = true;

            Console.WriteLine("My Library Program \n(press enter)");
            Console.ReadLine();

            Console.Clear();
            Console.WriteLine("To begin, we need to open your XML file.");
            Console.WriteLine("Please input a filename that you would like to read.\nIf it does not exist, a new file will be created on your desktop with that name.");
            Console.WriteLine("If you would like to create/open the default file, just press enter.");

            //Read/Create a file from user input
            string name;
            name = Console.ReadLine();

            //string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\" + name + ".xml";

            //Create a new file if no input from user
            if (name == "")
                name = "Library Card Catalog";

            string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\" + name + ".xml";

            //checks to see if "Library Card Catalog.xml exists or not
            if (!System.IO.File.Exists(path))
            {
                //creates default file on users desktop and informs user of the file being created                 
                System.IO.FileStream file = System.IO.File.Create(path);
                Console.WriteLine("An XML file called {0} was created on your desktop.", name);
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("You are going to open the file called {0}. Press Enter.", name);
                Console.ReadLine();
            }

            //do while loop that keeps running until user decides to exit
            do
            {
                //Found an exception will pop if user does not enter any input/or user input is horribly off... 
                //I put a try catch to keep the exception at bay.
                try
                {
                    MainMenu();
                    EvaluateUserInput(Convert.ToInt32(Console.ReadLine()));
                }
                catch
                {
                    Console.WriteLine("Sorry your input was incorrect dingus...");
                    Console.WriteLine("Please hit the enter key ONCE to continue.");
                    Console.ReadLine();
                }
            } while (Program.IsRunning == true);
        }

        // Main display menu
        static void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Please select from the following options:");
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

            switch (userInput)
            {
                case 1: //List of all books
                    Console.WriteLine("Fantastic this is a list of all our books:");
                    //Console.readline is here for testing purposes
                    Console.ReadLine();
                    break;
                case 2: //Add a book
                    Console.WriteLine("Fantastic! Lets add a new book to our catalog.");
                    //CardCatalog.AddBook();
                    Console.ReadLine();
                    break;
                case 3: //Save and Exit
                    Console.WriteLine("Thank you for visiting the library!");
                    Program.IsRunning = false;
                    break;
                default: //For any other improper entry
                    Console.WriteLine("Sorry... You did not enter valid input!");
                    Console.ReadLine();
                    break;
            }
        }
    }
}

//Code to assist in writing xml Card catalog files and stuffs

public class CardCatalog
{

    static void WriteXML()
    {
        AddBook();
    }

    public class Book
    {
        public String title;
    }

    public static void AddBook()
    {
        Book overview = new Book();
        overview.title = "Library Card Catalog";
        System.Xml.Serialization.XmlSerializer writer =
            new System.Xml.Serialization.XmlSerializer(typeof(Book));

        var path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "//Library Card Catalog.xml";
        System.IO.FileStream file = System.IO.File.Create(path);

        writer.Serialize(file, overview);
        file.Close();
    }
}



//This Creates a file on the desktop 
//var path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "//Library Card Catalog.xml";
//System.IO.FileStream file = System.IO.File.Create(path);




//static void Main()
//{
//    XmlDocument doc = new XmlDocument();
//    XmlElement root = doc.CreateElement("Login");
//    XmlElement id = doc.CreateElement("id");
//    id.SetAttribute("userName", "Tushar");
//    id.SetAttribute("passWord", "Tushar");
//    XmlElement name = doc.CreateElement("Name");
//    name.InnerText = "Tushar";
//    XmlElement age = doc.CreateElement("Age");
//    age.InnerText = "24";

//    id.AppendChild(name);
//    id.AppendChild(age);
//    root.AppendChild(id);
//    doc.AppendChild(root);

//    doc.Save("test.xml");
//}
//}
