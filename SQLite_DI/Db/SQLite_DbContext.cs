using Microsoft.EntityFrameworkCore;
using SQLite_DI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLite_DI.Db
{
    public class SQLite_DbContext :DbContext 
    {

        // Added for Generic repository:
        // see: https://chathuranga94.medium.com/generic-repository-pattern-for-asp-net-core-9e3e230e20cb 
      
        public SQLite_DbContext(DbContextOptions<SQLite_DbContext> options) : base(options)
        {
            //OnConfiguring(DbContextOptionsBuilder optionsBuilder) => { }
        }

        public SQLite_DbContext()
        {
        }

        // Setting up Nuget
        // - Use Microsoft.EntityFrameworkCore.Sqlite
        // - Not Microsoft.EntityFrameworkCore.Sqlite.Core
        // - You also MUST add 'Microsoft.EntityFrameworkCore.Sqlite' even though there is no using statement here, its still a required nuget package


        public DbSet<Person> People { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            // Are we using Migrations or Quick and dirty?
            // see here: https://github.com/aspnet/EntityFramework/issues/3042

            try
            {
                optionsBuilder.UseSqlite(App.DbConString); 
            }
            catch (Exception ex)
            {
                // breakpoint
            }

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

    }
}
