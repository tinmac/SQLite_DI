using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using SQLite_DI.Db;
using SQLite_DI.Model;
using SQLite_DI.Stuff;

namespace SQLite_DI.ViewModel
{
    public class Main_VM : BaseINPC
    {
        private readonly IPersonDb _PersonDb;


        private ObservableCollection<Person> peopleOC = new ObservableCollection<Person>();
        public ObservableCollection<Person> PeopleOC
        {
            get => peopleOC;
            set => Set(ref peopleOC, value);
        }

        private string personCount;
        public string PersonCount
        {
            get => personCount;
            set => Set(ref personCount, value);
        }


        public Main_VM(IPersonDb personDb)
        {
            _PersonDb = personDb;
        }

        public async Task LoadDb()
        {
            Debug.WriteLine($"Loading Db...");

            var people = await _PersonDb.GetAll();

            PeopleOC = new ObservableCollection<Person>(people);

            PersonCount = $"{PeopleOC.Count} records";
        }

        public async Task SeedDb()
        {
            Debug.WriteLine($"Seeding Db...");

            for (int i = 0; i < 10; i++)
            {
                Random rnd = new Random();

                var first = GenerateName(rnd.Next(3, 8));
                var last = GenerateName(rnd.Next(3, 10));               
                int age = rnd.Next(18, 75);

                await _PersonDb.Insert(new Person { FirstName = first, LastName = last, Age = age });
            }
        }

        public static string GenerateName(int len)
        {
            Random r = new Random();
            string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
            string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
            string Name = "";
            Name += consonants[r.Next(consonants.Length)].ToUpper();
            Name += vowels[r.Next(vowels.Length)];
            int b = 2; //b tells how many times a new letter has been added. It's 2 right now because the first two letters are already in the name.
            while (b < len)
            {
                Name += consonants[r.Next(consonants.Length)];
                b++;
                Name += vowels[r.Next(vowels.Length)];
                b++;
            }

            return Name;
        }
    }
}
