using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class DBDemoDbContext:DbContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<Person> People { get; set; }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().ToTable("Countries");
            modelBuilder.Entity<Person>().ToTable("People");
            modelBuilder.Entity<Person>().Property(e => e.TIN)
                .HasColumnName("Transaction Identification Number")
                .HasColumnType("varchar(10)")
                .HasDefaultValue("ABC123");

           

            string jcountries= System.IO.File.ReadAllText("JCountries.json");
            string jpersons = System.IO.File.ReadAllText("JPersons.json");

            List<Country> countries=System.Text.Json.JsonSerializer.Deserialize<List<Country>>(jcountries);
            List<Person> persons = System.Text.Json.JsonSerializer.Deserialize<List<Person>>(jpersons);

            foreach (Person p in persons)
            {

                modelBuilder.Entity<Person>().HasData(p);
            }


            foreach (Country c in countries)
            {

                modelBuilder.Entity<Country>().HasData(c);
            }

        }


        public DBDemoDbContext(DbContextOptions opts):base(opts)
        {
            
        }

        public List<Person> dbCopyP()
        {
            return People.FromSqlRaw("EXECUTE [dbo].[Second_Get]").ToList();
        }
    }
}
