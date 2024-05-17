using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Model_and_context
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Studio { get; set; }
        public string Style { get; set; }
        public DateTime ReleaseDate { get; set; }
        public bool IsSinglePlayer { get; set; }
        public int CopiesSold { get; set; }
    }

    public class GameContext : DbContext
    {
        public DbSet<Game> Games { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=GameDb;Trusted_Connection=True");
        }
    }
}