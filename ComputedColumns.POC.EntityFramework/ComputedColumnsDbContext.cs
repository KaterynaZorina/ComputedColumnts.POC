using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using ComputedColumns.POC.EntityFramework.Entities;
using ComputedColumns.POC.EntityFramework.ValuGenerators;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ComputedColumns.POC.EntityFramework
{
    public class ComputedColumnsDbContext: DbContext
    {
        public ComputedColumnsDbContext()
        {
            
        }
        
        public ComputedColumnsDbContext(DbContextOptions<ComputedColumnsDbContext> options)
            : base(options)
        {}
        
        public DbSet<Animal> Animals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {           
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ComputedColumnsDb;Trusted_Connection=True;ConnectRetryCount=0");
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>().HasKey(a => a.Id);
            modelBuilder.Entity<Animal>().Property(a => a.Name).IsRequired(false);
            modelBuilder.Entity<Animal>().Property(a => a.Breed).IsRequired(false);
            modelBuilder.Entity<Animal>().Property(a => a.RegistrationCode).IsRequired(false);
            modelBuilder.Entity<Animal>().Property(a => a.OwnerName).IsRequired(false);
            modelBuilder.Entity<Animal>().Property(a => a.Description).IsRequired(false);

            modelBuilder.Entity<Animal>().Property(a => a.ComputedProperty)
                .HasValueGenerator<ComputedPropertyGenerator>()
                .IsRequired(false);
            modelBuilder.Entity<Animal>().HasIndex(a => a.ComputedProperty).IsClustered(false);
        }

        public static void Seed(ComputedColumnsDbContext dbContext)
        {
            var hasAnyAnimals = dbContext.Animals.Any();
            
            if(hasAnyAnimals)
                return;

            var animalsToAdd = ReadMockData();
            
            dbContext.Animals.AddRange(animalsToAdd);
            dbContext.SaveChanges();
        }

        private static List<Animal> ReadMockData()
        {
            using var r = new StreamReader($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/SeedData/MOCK_DATA.json");
            
            string json = r.ReadToEnd();
            var animalsList = JsonConvert.DeserializeObject<List<Animal>>(json);

            return animalsList;
        }
    }
}