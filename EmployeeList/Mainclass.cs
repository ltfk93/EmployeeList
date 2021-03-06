using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using EmployeeList.Model;
using EmployeeList.Logic;

namespace EmployeeList
{
    internal class Mainclass
    {
        const string serverName = "DESKTOP-0M7SFEP\\SQLEXPRESS";
        const string databaseName = "PersonDatabase";
        const string userName = "desktop-0m7sfep\\ali";
        const string tableName = "dbo.Person";

        static string connectionString = $@"Data Source={serverName};
                                        Initial Catalog={databaseName};
                                        User ID={userName};
                                        Password=;
                                        Trusted_Connection=Yes";

        //List<Person> personList = new List<Person>();
        public static void Main(string[] args)
        {
            DbHandler dBHandler = new DbHandler(connectionString);
            int startProgram = 1;
            do
            {
                printMenu();
                int answer = 0;
                if(int.TryParse(Console.ReadLine(), out answer))
                {
                    switch(answer)
                    {
                        case 1:
                            {
                                Console.Clear();
                                dBHandler.printList();
                                Console.WriteLine("Press a key to return to the menu");
                                Console.ReadKey();
                                printMenu();
                                break;
                            }
                        case 2:
                            {
                                Console.Clear();
                                Console.WriteLine("Type in the first name.");
                                string firstName = Console.ReadLine();

                                Console.Clear();
                                Console.WriteLine("Type in the last name.");
                                string lastName = Console.ReadLine();

                                Console.Clear();
                                Console.WriteLine("Type in the age.");
                                bool validNumber = false;
                                int age = 0;
                                do
                                {
                                    if(!int.TryParse(Console.ReadLine(), out age))
                                    {
                                        Console.WriteLine("Invalid number provided! Please type in the age in numbers");
                                    }
                                    else
                                    {
                                        validNumber = true;
                                    }
                                }
                                while(!validNumber);

                                Console.Clear();
                                dBHandler.addPerson(firstName, lastName, age);

                                Console.WriteLine("Press a key to return to the menu");
                                Console.ReadKey();
                                printMenu();

                                break;
                            }
                        case 3:
                            {
                                Console.Clear();
                                Console.WriteLine("Type in the first name.");
                                string firstName = Console.ReadLine();

                                Console.Clear();
                                Console.WriteLine("Type in the last name.");
                                string lastName = Console.ReadLine();

                                Console.Clear();
                                Console.WriteLine("Type in the new first name.");
                                string newFirstName = Console.ReadLine();

                                Console.Clear();
                                Console.WriteLine("Type in the new last name.");
                                string newLastName = Console.ReadLine();

                                Console.Clear();
                                dBHandler.editPerson(firstName, lastName, newFirstName, newLastName);

                                Console.WriteLine("Press a key to return to the menu");
                                Console.ReadKey();
                                printMenu();
                                break;
                            }
                        case 4:
                            {
                                Console.Clear();
                                Console.WriteLine("Type in the first name.");
                                string firstName = Console.ReadLine();

                                Console.Clear();
                                Console.WriteLine("Type in the last name.");
                                string lastName = Console.ReadLine();

                                Console.Clear();
                                dBHandler.removePerson(firstName, lastName);

                                Console.WriteLine("Press a key to return to the menu");
                                Console.ReadKey();
                                printMenu();
                                break;
                            }
                        case 5:
                            {
                                Console.Clear();
                                dBHandler.deleteTable();
                                Console.WriteLine("Press a key to return to the menu");
                                Console.ReadKey();
                                printMenu();
                                break;
                            }
                        case 6:
                            {
                                Console.Clear();
                                int choise = -1;
                                int choise2 = -1;

                                bool validInput = false;
                                bool validInput2 = false;

                                Console.WriteLine("Choose which field to order by(1-3)\n1. First name\n2. Last name\n3. Age");

                                do
                                {
                                    if (int.TryParse(Console.ReadLine(), out choise))
                                    {
                                        validInput = choise > 0 && choise <= 3 ? true : false;
                                        if (!validInput) { Console.WriteLine("Please choose one of the valid numbers!"); }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Please type in a valid number!");
                                    }
                                } while (!validInput);

                                Console.Clear();
                                Console.WriteLine("Order ascending og descending(1-2)?\n1. Ascending\n2. Descending");
                                do
                                {
                                    if (int.TryParse(Console.ReadLine(), out choise2))
                                    {
                                        validInput2 = choise2 > 0 && choise2 <= 2 ? true : false;
                                        if (!validInput2) { Console.WriteLine("Please choose one of the valid numbers!"); }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Please type in a valid number!");
                                    }
                                } while (!validInput2);

                                string direction = choise2 == 1 ? "asc" : "desc";


                                string orderByText = choise == 1 ? "firstName" : choise == 2 ? "lastName" : "age";

                                Console.Clear();

                                string query = $"SELECT * FROM Person ORDER BY {orderByText} {direction}";
                                dBHandler.executeQuery(query);
                                Console.WriteLine("Press a key to return to the main menu");
                                Console.ReadKey();
                                printMenu();
                                break;
                            }
                        case -1:
                            {
                                startProgram = -1;
                                break;
                            }
                        default:
                            {
                                Console.Clear();
                                Console.WriteLine("Invalid input provided. Please provide a valid number from the list");
                                Console.ReadKey();
                                printMenu();
                                break;
                            }

                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid input provided. Please provide a valid number from the list");
                    Console.ReadKey();
                    printMenu();
                }
            }
            while (startProgram != -1);
        }
        protected static void printMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to my Console-based database application. Please enter a number from the following menu to choose an action:\n");
            Console.WriteLine("1. Print database content\n2. Add a person to the database\n3. Edit a person in the database\n4. Remove a person from the database\n5. Remove all content from the table\n6. Print a sorted list\n-1. Exit the program.");
        }
    }
}
