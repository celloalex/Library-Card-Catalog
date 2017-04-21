using System;
using System.Runtime.Serialization;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Library_Card_Catalog
{
    class Program
    {
        public static bool IsRunning { get; set; }

        public static string Path { get; set; }

        static List<Books> myBooks = new List<Books>();

        static void Main(string[] args)
        {
            //bool is in Program class and allows changes to be made within different methods
            Program.IsRunning = true;

            Console.WriteLine("My Library Program \n(press enter)");
            Console.ReadLine();

            Console.Clear();
            Console.WriteLine("To begin, we need to open your XML file.");
            Console.WriteLine("Please input a filename that you would like to read." +
                "\nIf it does not exist, a new file will be created on your desktop with that name.");
            Console.WriteLine("If you would like to create/open the default file, just press enter.");

            //Read/Create a file from user input
            string name;
            name = Console.ReadLine();

            //Create a new file if no input from user
            if (name == "")
                name = "Library Card Catalog";

            //sets path of file
            Path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\" + name + ".xml";

            //checks to see if "Library Card Catalog.xml exists or not
            if (!System.IO.File.Exists(Path))
            {
                //creates default file on users desktop and informs user of the file being created                 
                System.IO.FileStream file = System.IO.File.Create(Path);
                file.Close();
                Console.WriteLine("An XML file called {0} was created on your desktop. Press Enter.", name);
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("You are going to open the file called {0}. Press Enter.", name);
                Console.ReadLine();

                //Immediately open the file and create a list from what's in it.
                ReadFile(Path);
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
                catch (Exception ex)
                {
                    Console.WriteLine("There was an error, dingus...");
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Please hit the Enter key ONCE to continue.");
                    Console.ReadLine();
                }
            } while (Program.IsRunning == true);
        }

        //Opens the file and immediatly deserializes it
        public static void ReadFile(string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Open))
            {
                try
                {
                    var XML = new XmlSerializer(typeof(List<Books>));
                    myBooks = (List<Books>)XML.Deserialize(stream);
                }
                catch
                {
                    var XML = new XmlSerializer(typeof(List<Books>));
                }
            }
        }

        // Main display menu
        static void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Library Options:");
            Console.WriteLine("1) List of all our books");
            Console.WriteLine("2) Add a Book to our catalog");
            Console.WriteLine("3) Save all changes and exit the program");
            Console.WriteLine();
            Console.WriteLine("Please select one of the following choices and press Enter.");
        }

        //Evaluates user Input from Main Menu 
        private static void EvaluateUserInput(int userInput)
        {
            Console.Clear();
            Console.WriteLine("You entered {0}...", userInput);

            switch (userInput)
            {
                case 1: //List of all books
                    Console.WriteLine("Fantastic this is a list of all our books:\n");
                    foreach (var book in myBooks)
                    {
                        Console.WriteLine("Title: {0} Author: {1}\n", book.BookTitle, book.BookAuthor);
                    }
                    Console.WriteLine("Press Enter to return to Menu.");
                    //ObjectBook.ListBooks();
                    //Program.ReadFile(Path);
                    Console.ReadLine();
                    break;

                case 2: //Add a book
                    Console.WriteLine("Fantastic! Lets add a new book to our catalog.");
                    AddBook();
                    break;

                case 3: //Save and Exit
                    Console.WriteLine("Thank you for visiting the library!");
                    Books.saveList(Path, myBooks);
                    Program.IsRunning = false;
                    break;

                default: //For any other improper entry
                    Console.WriteLine("Sorry... You did not enter valid input!");
                    Console.ReadLine();
                    break;
            }
        }

        //asks user for input on what to add to catalog
        private static void AddBook()
        {
            Console.Clear();
            //asks the user what book they want to add
            Console.WriteLine("What is the title of the book you are adding?");
            string bookTitle = Console.ReadLine();

            //follows up and asks user the author of the book they just entered
            Console.WriteLine("Who is the author of {0}?", bookTitle);
            string bookAuthor = Console.ReadLine();

            //Test to see if string is empty at all
            if (bookTitle == "" || bookAuthor == "")
            {
                Console.WriteLine("Sorry, but both the Title and the Author must contain data.");
                Console.ReadLine();
            }
            else
            {
                //kicks book title/author out to ObjectBook method to get sorted out
                Books bookA = new Books();
                bookA.BookTitle = bookTitle;
                bookA.BookAuthor = bookAuthor;
                myBooks.Add(bookA);
            }
        }
    }

    public class Books
    {
        //book properties
        public string BookTitle { get; set; }
        public string BookAuthor { get; set; }

        //Saves list of books and serializes it
        public static void saveList(string fileName, List<Books> books)
        {
            using (var stream = new FileStream(Program.Path, FileMode.Create))
            {
                var xmlInput = new XmlSerializer(typeof(List<Books>));
                xmlInput.Serialize(stream, books);
            }
        }
    }
}
