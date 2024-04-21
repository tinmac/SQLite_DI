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


        public DbSet<Person> People { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                optionsBuilder
                   .UseSqlite($"{App.DbConString}")
                   .EnableSensitiveDataLogging(true)
                   .EnableThreadSafetyChecks(true)
                   .EnableDetailedErrors()
                   .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            

                Debug.WriteLine($"OnConfiguring...");
           
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"OnConfiguring Error: {ex.Message}");
            }

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

    }
}
