using SQLite_DI.Stuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLite_DI.Model
{
    public class Person: BaseINPC
    {
        public int Id { get; set; }


        private string firstName;
        public string FirstName
        {
            get => firstName;
            set => Set(ref firstName, value);
        }

        private string lastName;
        public string LastName
        {
            get => lastName;
            set => Set(ref lastName, value);
        }

        private int age;
        public int Age
        {
            get => age;
            set => Set(ref age, value);
        }
    }   
}
