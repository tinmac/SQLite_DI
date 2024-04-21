using Microsoft.EntityFrameworkCore;
using SQLite_DI.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLite_DI.Db
{
    public class SQLite_DbContext :DbContext 
    {      
        public SQLite_DbContext(DbContextOptions<SQLite_DbContext> options) : base(options)
        {
        }

        public SQLite_DbContext()
        {
        }


        public DbSet<Person> People { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                optionsBuilder.UseSqlite(App.DbConString); 
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
            }

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

    }
}
