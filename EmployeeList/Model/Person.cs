using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeList.Model
{
    class Person
    {
        private string firstName, lastName;
        private int id, age;

        public Person(int id, string firstName, string lastName, int age)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.age = age;
            this.id = id;
        }

        public string GetFirstName()
        {
            return firstName;
        }
        
        public string GetLastName()
        {
            return lastName;
        }

        public int GetAge()
        {
            return age;
        }

        public int GetID()
        {
            return id;
        }

        public string getFullInfo()
        {
            return $"Full name: {GetFirstName()} {GetLastName()}\nAge: {GetAge()}\n";
        }

    }
}
