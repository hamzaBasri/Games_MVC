using GamesWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace GamesWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Adventure", DisplayOrder = 2 },
                new Category { Id = 3, Name = "RPG", DisplayOrder = 3 },
                new Category { Id = 4, Name = "Simulation", DisplayOrder = 4 },
                new Category { Id = 5, Name = "Strategy", DisplayOrder = 5 },
                new Category { Id = 6, Name = "Sports", DisplayOrder = 6 },
                new Category { Id = 7, Name = "Puzzle", DisplayOrder = 7 },
                new Category { Id = 8, Name = "Horror", DisplayOrder = 8 },
                new Category { Id = 9, Name = "Survival", DisplayOrder = 9 },
                new Category { Id = 10, Name = "Shooter", DisplayOrder = 10 },
                new Category { Id = 11, Name = "Fighting", DisplayOrder = 11 },
                new Category { Id = 12, Name = "Racing", DisplayOrder = 12 },
                new Category { Id = 13, Name = "MMO", DisplayOrder = 13 },
                new Category { Id = 14, Name = "Casual", DisplayOrder = 14 },
                new Category { Id = 15, Name = "Educational", DisplayOrder = 15 },
                new Category { Id = 16, Name = "Music", DisplayOrder = 16 },
                new Category { Id = 17, Name = "Party", DisplayOrder = 17 },
                new Category { Id = 18, Name = "Board", DisplayOrder = 18 },
                new Category { Id = 19, Name = "Card", DisplayOrder = 19 },
                new Category { Id = 20, Name = "Battle Royale", DisplayOrder = 20 },
                new Category { Id = 21, Name = "MOBA", DisplayOrder = 21 },
                new Category { Id = 22, Name = "RTS", DisplayOrder = 22 },
                new Category { Id = 23, Name = "TBS", DisplayOrder = 23 },
                new Category { Id = 24, Name = "Rhythm", DisplayOrder = 24 },
                new Category { Id = 25, Name = "Sandbox", DisplayOrder = 25 },
                new Category { Id = 26, Name = "Open World", DisplayOrder = 26 },
                new Category { Id = 27, Name = "Metroidvania", DisplayOrder = 27 },
                new Category { Id = 28, Name = "Stealth", DisplayOrder = 28 },
                new Category { Id = 29, Name = "Tower Defense", DisplayOrder = 29 },
                new Category { Id = 30, Name = "Visual Novel", DisplayOrder = 30 },
                new Category { Id = 31, Name = "Idle", DisplayOrder = 31 },
                new Category { Id = 32, Name = "Clicker", DisplayOrder = 32 },
                new Category { Id = 33, Name = "Incremental", DisplayOrder = 33 },
                new Category { Id = 34, Name = "Tycoon", DisplayOrder = 34 },
                new Category { Id = 35, Name = "City Builder", DisplayOrder = 35 },
                new Category { Id = 36, Name = "Life Sim", DisplayOrder = 36 },
                new Category { Id = 37, Name = "Dating Sim", DisplayOrder = 37 },
                new Category { Id = 38, Name = "Dungeon Crawler", DisplayOrder = 38 },
                new Category { Id = 39, Name = "Roguelike", DisplayOrder = 39 },
                new Category { Id = 40, Name = "Roguelite", DisplayOrder = 40 },
                new Category { Id = 41, Name = "Tactical RPG", DisplayOrder = 41 },
                new Category { Id = 42, Name = "JRPG", DisplayOrder = 42 },
                new Category { Id = 43, Name = "ARPG", DisplayOrder = 43 },
                new Category { Id = 44, Name = "CRPG", DisplayOrder = 44 },
                new Category { Id = 45, Name = "MMORPG", DisplayOrder = 45 },
                new Category { Id = 46, Name = "Action-Adventure", DisplayOrder = 46 },
                new Category { Id = 47, Name = "Sandbox Survival", DisplayOrder = 47 }
            );
        }
    }
}
