using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
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
        Random rnd;
          
        private readonly IGenericRepository<Person> _PersonDb;



        public Microsoft.UI.Dispatching.DispatcherQueue TheDispatcher { get; set; }


        private ObservableCollection<Person> peopleOC = new ObservableCollection<Person>();
        public ObservableCollection<Person> PeopleOC
        {
            get => peopleOC;
            set => Set(ref peopleOC, value);
        }

        private string _Message;
        public string Message
        {
            get => _Message;
            set => Set(ref _Message, value);
        }



        public Main_VM(IGenericRepository<Person> personDb)
        {
            _PersonDb = personDb;
        }


        public void LoadDb()
        {
            Debug.WriteLine($"Loading Db...");

            var people = _PersonDb.GetAll();

            PeopleOC = new ObservableCollection<Person>(people);

            Message = $"{PeopleOC.Count} records";
        }


        public void SeedDb(int seed_count)
        {
            Message = ($"Seeding {seed_count} records...");

            Task.Run(async() =>
            {
                for (int i = 0; i < seed_count; i++)
                {
                    rnd = new Random();
                    var first = GenerateName(rnd.Next(3, 8));
                    var last = GenerateName(rnd.Next(3, 10));
                    int age = rnd.Next(18, 75);

                    var toAdd = new Person { FirstName = first, LastName = last, Age = age };

                    _PersonDb.Insert(toAdd);

                    TheDispatcher.TryEnqueue(() =>
                    {
                        PeopleOC.Insert(0,toAdd);
                        Message = $"{PeopleOC.Count} records";
                    });

                    if(seed_count < 101)
                        await Task.Delay(1); // slow down the loop to show the ui updating
                }
            });

        }


        public void Update()
        {
            rnd = new Random();

            foreach (var person in PeopleOC)
            {
                int new_age = rnd.Next(18, 75);
               
                //Debug.WriteLine($"Age   old {person.Age}  new {new_age}");
                
                person.Age = new_age;

                _PersonDb.Update(person);
            }
        }


        public void DeleteAll()
        {
            Message = $"Deleting all records in Db...";

            Task.Delay(10);

            foreach (var person in PeopleOC)
            {
                // PeopleOC.Remove(person);
                _PersonDb.Delete(person);
            }

            PeopleOC.Clear();

            Message = $"{PeopleOC.Count} records";
        }


        public void Delete10()
        {
            Message = $"Deleting 10 records in Db...";

            int from = 9;
            if (PeopleOC.Count < 9)
                from = PeopleOC.Count - 1;

            for (int i = from; i >= 0; i--)
            {
                var toDel = PeopleOC[i];
                _PersonDb.Delete(toDel);
                PeopleOC.RemoveAt(i);
            }

            Message = $"{PeopleOC.Count} records";
        }




        // HELPERS
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
