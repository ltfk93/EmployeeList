using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data;
using System.Data.SqlClient;
using EmployeeList.Model;
using System.Threading.Tasks;

namespace EmployeeList.Logic
{
    internal class DbHandler
    {
        private SqlConnection _connection;
        private SqlCommand _command;
        private SqlDataReader _reader;
        List<Person> currentList = null;

        public DbHandler(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _command = new SqlCommand();
        }
        public bool addPerson(string firstNameInput, string lastNameInput, int ageInput)
        {
            if (!exists(firstNameInput, lastNameInput))
            {
                addToDB(firstNameInput,lastNameInput, ageInput);
                return true;
            }
            else
            {
                Console.WriteLine($"{firstNameInput} {lastNameInput} already exists in the list!");
                return false;
            }
        }
        public bool removePerson(string firstName, string lastName)
        {
            if(exists(firstName, lastName))
            {
                string query = $"DELETE FROM Person WHERE firstName ='{firstName}' AND lastName = '{lastName}';";
                _command.CommandType = CommandType.Text;
                _command.CommandText = query;
                _command.Connection = _connection;
                _connection.Open();
                _command.ExecuteNonQuery();
                _connection.Close();

                Console.WriteLine($"{firstName} {lastName} has been removed from the list.");
                return true;
            }

            Console.WriteLine($"{firstName} {lastName} does not exist in the list!");
            return false;
        }

        public void deleteTable()
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("WARNING! You are about the delete all entries from the person table. Are you sure(Enter Y/Yes to confirm or N/No to cancel");
            Console.ResetColor();
            bool validInput = false;
            string answer = Console.ReadLine();
            do
            {
                if (answer.ToLower().Equals("y") || answer.ToLower().Equals("yes"))
                {
                    validInput = true;
                    string query = $"DELETE FROM Person;";
                    _command.CommandType = CommandType.Text;
                    _command.CommandText = query;
                    _command.Connection = _connection;
                    _connection.Open();
                    _command.ExecuteNonQuery();
                    _connection.Close();

                    Console.WriteLine("The person table has been cleared.");
                }
                else if (answer.ToLower() == "n" || answer.ToLower().Equals("no"))
                {
                    validInput = true;
                    Console.WriteLine("Returning to the main menu...");
                    Thread.Sleep(3000);
                }
                else
                {
                    Console.WriteLine("Invalid input. Please type in Y/Yes to clear the table or N/No to exit");
                    answer = Console.ReadLine();
                }
            } while (!validInput);
        }

        public bool editPerson(string firstName, string lastName, string newFirstName, string newLastName)
        {
            if(exists(firstName, lastName))
            {
                string query = $"UPDATE Person SET firstName = '{newFirstName}', lastName = '{newLastName}' WHERE firstName ='{firstName}' AND lastName = '{lastName}';";
                _command.CommandType = CommandType.Text;
                _command.CommandText = query;
                _command.Connection = _connection;
                _connection.Open();
                _command.ExecuteNonQuery();
                _connection.Close();

                Console.WriteLine($"The name of {firstName} {lastName} has been changed to {newFirstName} {newLastName}");
                return true;
            }

            Console.WriteLine($"{firstName} {lastName} does not exist in the list!");
            return false;

        }
        public void addToDB(string firstNameInput, string lastNameInput, int ageInput)
        {
            //string query = $"INSERT INTO Person(firstName,lastName,age) VALUES('" + firstNameInput + "', '" + lastNameInput + "', '" + ageInput + "')";
            string query2 = $"INSERT INTO Person(firstName,lastName,age) VALUES('{firstNameInput}', '{lastNameInput}', '{ageInput}')" ;
            _command.CommandType = CommandType.Text;
            _command.CommandText = query2;
            _command.Connection = _connection;
            _connection.Open();
            _command.ExecuteNonQuery();
            _connection.Close();
        }
        public bool exists(string firstName, string lastName)
        {
            currentList = GetList();
            for(int i = 0;i < currentList.Count;i++)
            {
                if(currentList[i].GetFirstName().ToLower().Equals(firstName.ToLower()) && currentList[i].GetLastName().ToLower().Equals(lastName.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }
        public List<Person> GetList()
        {
            List<Person> list = new List<Person>();

            _command.CommandType = CommandType.Text;
            _command.CommandText = "SELECT * from Person";
            _command.Connection = _connection;

            _connection.Open();

            _reader =   _command.ExecuteReader();
            while (_reader.Read())
            {
                list.Add(new Person(_reader.GetInt32(0),_reader.GetString(1),_reader.GetString(2),_reader.GetInt32(3)));
            }

            _connection.Close();


            return list;
        }

        public void printList()
        {
            List<Person> personList = GetList();
            for(int i = 0;i < personList.Count; i++)
            {
                Console.WriteLine(personList[i].getFullInfo());
            }
        }
    }
}
