using System;
using Backend.Model;
using Microsoft.EntityFrameworkCore;

namespace Backend
{
	public class DatabaseContext : DbContext
    {
        // Setup database information and use and InMemory Database
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "ImageDatabase");
        }

        // Creating database table called Images with ImageDataModel
        public DbSet<ImageDataModel> Images { get; set; }
    }
}
