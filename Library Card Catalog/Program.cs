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

        List<Books> myBooks = new List<Books>();

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
            }
        
            //Immediately open the file and create a list from what's in it. Path is an important variable.
            
        

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

        public static void ReadFile(string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Open))
            {
                var XML = new XmlSerializer(typeof(List<Books>));

                List<Books> myBooks = (List<Books>)XML.Deserialize(stream);
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
                    //ObjectBook.ListBooks();
                    Console.ReadLine();
                    break;

                case 2: //Add a book
                    Console.WriteLine("Fantastic! Lets add a new book to our catalog.");
                    AddBook();
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
        private static void AddBook()
        {
            Console.Clear();
            //asks the user what book they want to add
            Console.WriteLine("What is the title of the book you are adding?");
            string bookTitle = Console.ReadLine();
            
            //follows up and asks user the author of the book they just entered
            Console.WriteLine("Who is the author of {0}?", bookTitle);
            string bookAuthor = Console.ReadLine();

            if(bookTitle == "" || bookAuthor == "")
            {
                Console.WriteLine("Sorry, but both the Title and the Author must contain data.");
                Console.ReadLine();
            }
            else
            {
                //kicks book title/author out to ObjectBook method to get sorted out
                //ObjectBook a = new ObjectBook(bookTitle, bookAuthor);
                //ObjectBook.AddBook(bookTitle, bookAuthor);
                Books Booka = new Books();
                Booka.BookTitle = bookTitle;
                Booka.BookAuthor = bookAuthor;
                Booka.addBook(Program.Path, List<Books>);
            }
        }
    }
    // WORKING NO CHANGEY// WORKING NO CHANGEY// WORKING NO CHANGEY// WORKING NO CHANGEY// WORKING NO CHANGEY// WORKING NO CHANGEY
    //    public class ObjectBook
    //    {
    //        //book properties
    //        public string BookTitle { get; set; }
    //        public string BookAuthor { get; set; }

    //        //Constructor
    //        public ObjectBook(string BookTitle, string BookAuthor)
    //        {
    //            this.BookAuthor = BookAuthor;
    //            this.BookTitle = BookTitle;
    //        }

    //        //method to add books by accessing file and adding a book/ author line by line
    //        //both are labeled for readability can be modified later if a bot needs to comb the program
    //        public static void AddBook(string BookTitle, string BookAuthor)
    //        {
    //            //writes title to to file
    //            string filePrintTitle = ("Title: " + BookTitle);
    //            System.IO.File.AppendAllText(Program.Path, filePrintTitle + Environment.NewLine);

    //            //writes author to file
    //            string filePrintAuthor = ("Author: " + BookAuthor);
    //            System.IO.File.AppendAllText(Program.Path, filePrintAuthor + Environment.NewLine);

    //        }

    //        //method to access file and write list of books on the console screen
    //        public static void ListBooks()
    //        {
    //            StreamReader reader = System.IO.File.OpenText(Program.Path);
    //            int count = 1;
    //            while (!reader.EndOfStream)
    //            {
    //                Console.Write(count + ".) ");
    //                count++;

    //                Console.WriteLine(reader.ReadLine());
    //                Console.WriteLine("    " + reader.ReadLine());
    //            }
    //            Console.WriteLine("\nPress Enter to return to Menu.");
    //            //helps with resource management and memory usage - according to joe "streamreader can get a bit weird"
    //            reader.Close();
    //            reader.Dispose();
    //        }



    public class Books
    {
        //book properties
        public string BookTitle { get; set; }
        public string BookAuthor { get; set; }

        public void addBook(string fileName, List<Books> books)
        {
            using (var stream = new FileStream(Program.Path, FileMode.Create))
            {
                var xmlInput = new XmlSerializer(typeof(List<Books>));
                xmlInput.Serialize(stream, books);
            }

            using (var stream = new FileStream(Program.Path, FileMode.Open))
            {
                var xmlSave = new XmlSerializer(typeof(List<Books>));
                List<Books> save = (List<Books>)xmlSave.Deserialize(stream);

            }
        }
    }
}


////more notes//more notes//more notes//more notes//more notes//more notes//more notes//more notes//more notes//more notes//more notes//more notes//more notes//more notes
//public class XMLWrite
//{

//    static void Main(string[] args)
//    {
//        WriteXML();
//    }

//    public class Book
//    {
//        public String title;
//    }

//    public static void WriteXML()
//    {
//        Book overview = new Book();
//        overview.title = "Serialization Overview";
//        System.Xml.Serialization.XmlSerializer writer =
//            new System.Xml.Serialization.XmlSerializer(typeof(Book));

//        var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//SerializationOverview.xml";
//        System.IO.FileStream file = System.IO.File.Create(path);

//        writer.Serialize(file, overview);
//        file.Close();
//    }
//}



////NOTES GALORE LOOK BELOW
//                |
//                |
//                |
//                |   
//             \  | /
//              \  /
//               \/

//     static void WriteXML()
// {
//     AddBook();
// }

// public class Book
// {
//     public String title;
// }

//// public static void AddBook()
//{


//Console.WriteLine("What is the name of the book that you want to add?");
//string bookName = Console.ReadLine();

//Console.WriteLine("Who is the author of this book?");
//string bookAuthor = Console.ReadLine();

//System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(Book));

//var path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "//Library Card Catalog.xml";

//XmlElement name = .CreateElement("Name");
//name.InnerText = "Tushar";
//XmlElement age = .CreateElement("Age");
//age.InnerText = "24";





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



//public class Robot : IDisposable
//{
//    private static List<bool> UsedCounter = new List<bool>();
//    private static object Lock = new object();

//    public int ID { get; private set; }

//    public Robot()
//    {

//        lock (Lock)
//        {
//            int nextIndex = GetAvailableIndex();
//            if (nextIndex == -1)
//            {
//                nextIndex = UsedCounter.Count;
//                UsedCounter.Add(true);
//            }

//            ID = nextIndex;
//        }
//    }

//    public void Dispose()
//    {
//        lock (Lock)
//        {
//            UsedCounter[ID] = false;
//        }
//    }


//    private int GetAvailableIndex()
//    {
//        for (int i = 0; i < UsedCounter.Count; i++)
//        {
//            if (UsedCounter[i] == false)
//            {
//                return i;
//            }
//        }

//        // Nothing available.
//        return -1;
//    }