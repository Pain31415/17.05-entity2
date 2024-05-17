using System;
using System.Linq;

namespace _17._05_entity2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SeedDatabase();

            Console.WriteLine("Search Results:");
            SearchGameByName("Adventure Quest");
            SearchGamesByStudio("Epic Games");
            SearchGamesByStudioAndName("Epic Games", "Adventure Quest");
            SearchGamesByStyle("Adventure");
            SearchGamesByReleaseYear(2020);

            DisplaySinglePlayerGames();
            DisplayMultiplayerGames();
            DisplayGameWithMostCopiesSold();
            DisplayGameWithLeastCopiesSold();
            DisplayTop3PopularGames();
            DisplayTop3UnpopularGames();

            AddNewGame(new Game { Name = "New Game", Studio = "New Studio", Style = "Action", ReleaseDate = new DateTime(2022, 1, 1), IsSinglePlayer = true, CopiesSold = 100 });
            UpdateGame("New Game", "New Studio", new Game { Name = "Updated Game", Studio = "Updated Studio", Style = "RPG", ReleaseDate = new DateTime(2023, 1, 1), IsSinglePlayer = false, CopiesSold = 200 });
            DeleteGame("Updated Game", "Updated Studio");
        }

        private static void SeedDatabase()
        {
            using (var context = new GameContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var games = new[]
                {
                    new Game { Name = "Adventure Quest", Studio = "Epic Games", Style = "Adventure", ReleaseDate = new DateTime(2020, 1, 1), IsSinglePlayer = true, CopiesSold = 1000 },
                    new Game { Name = "Battle Arena", Studio = "Infinity Ward", Style = "Action", ReleaseDate = new DateTime(2021, 5, 20), IsSinglePlayer = false, CopiesSold = 1500 },
                    new Game { Name = "Mystery Mansion", Studio = "Ubisoft", Style = "Puzzle", ReleaseDate = new DateTime(2019, 8, 15), IsSinglePlayer = true, CopiesSold = 500 }
                };

                context.Games.AddRange(games);
                context.SaveChanges();
            }
        }

        private static void SearchGameByName(string name)
        {
            using (var context = new GameContext())
            {
                var game = context.Games.FirstOrDefault(g => g.Name == name);
                if (game != null)
                {
                    Console.WriteLine($"Found Game: {game.Name}, Studio: {game.Studio}, Style: {game.Style}, Release Date: {game.ReleaseDate.ToShortDateString()}");
                }
                else
                {
                    Console.WriteLine($"Game with name {name} not found.");
                }
            }
        }

        private static void SearchGamesByStudio(string studio)
        {
            using (var context = new GameContext())
            {
                var games = context.Games.Where(g => g.Studio == studio).ToList();
                if (games.Any())
                {
                    Console.WriteLine($"Games by Studio {studio}:");
                    foreach (var game in games)
                    {
                        Console.WriteLine($"Name: {game.Name}, Style: {game.Style}, Release Date: {game.ReleaseDate.ToShortDateString()}");
                    }
                }
                else
                {
                    Console.WriteLine($"No games found by Studio {studio}.");
                }
            }
        }

        private static void SearchGamesByStudioAndName(string studio, string name)
        {
            using (var context = new GameContext())
            {
                var game = context.Games.FirstOrDefault(g => g.Studio == studio && g.Name == name);
                if (game != null)
                {
                    Console.WriteLine($"Found Game: {game.Name}, Studio: {game.Studio}, Style: {game.Style}, Release Date: {game.ReleaseDate.ToShortDateString()}");
                }
                else
                {
                    Console.WriteLine($"Game with name {name} and studio {studio} not found.");
                }
            }
        }

        private static void SearchGamesByStyle(string style)
        {
            using (var context = new GameContext())
            {
                var games = context.Games.Where(g => g.Style == style).ToList();
                if (games.Any())
                {
                    Console.WriteLine($"Games with Style {style}:");
                    foreach (var game in games)
                    {
                        Console.WriteLine($"Name: {game.Name}, Studio: {game.Studio}, Release Date: {game.ReleaseDate.ToShortDateString()}");
                    }
                }
                else
                {
                    Console.WriteLine($"No games found with Style {style}.");
                }
            }
        }

        private static void SearchGamesByReleaseYear(int year)
        {
            using (var context = new GameContext())
            {
                var games = context.Games.Where(g => g.ReleaseDate.Year == year).ToList();
                if (games.Any())
                {
                    Console.WriteLine($"Games released in {year}:");
                    foreach (var game in games)
                    {
                        Console.WriteLine($"Name: {game.Name}, Studio: {game.Studio}, Style: {game.Style}, Release Date: {game.ReleaseDate.ToShortDateString()}");
                    }
                }
                else
                {
                    Console.WriteLine($"No games found released in {year}.");
                }
            }
        }

        private static void DisplaySinglePlayerGames()
        {
            using (var context = new GameContext())
            {
                var games = context.Games.Where(g => g.IsSinglePlayer).ToList();
                Console.WriteLine("Single Player Games:");
                foreach (var game in games)
                {
                    Console.WriteLine($"Name: {game.Name}, Studio: {game.Studio}, Style: {game.Style}, Release Date: {game.ReleaseDate.ToShortDateString()}");
                }
            }
        }

        private static void DisplayMultiplayerGames()
        {
            using (var context = new GameContext())
            {
                var games = context.Games.Where(g => !g.IsSinglePlayer).ToList();
                Console.WriteLine("Multiplayer Games:");
                foreach (var game in games)
                {
                    Console.WriteLine($"Name: {game.Name}, Studio: {game.Studio}, Style: {game.Style}, Release Date: {game.ReleaseDate.ToShortDateString()}");
                }
            }
        }

        private static void DisplayGameWithMostCopiesSold()
        {
            using (var context = new GameContext())
            {
                var game = context.Games.OrderByDescending(g => g.CopiesSold).FirstOrDefault();
                if (game != null)
                {
                    Console.WriteLine($"Game with Most Copies Sold: {game.Name}, Copies Sold: {game.CopiesSold}");
                }
            }
        }

        private static void DisplayGameWithLeastCopiesSold()
        {
            using (var context = new GameContext())
            {
                var game = context.Games.OrderBy(g => g.CopiesSold).FirstOrDefault();
                if (game != null)
                {
                    Console.WriteLine($"Game with Least Copies Sold: {game.Name}, Copies Sold: {game.CopiesSold}");
                }
            }
        }

        private static void DisplayTop3PopularGames()
        {
            using (var context = new GameContext())
            {
                var games = context.Games.OrderByDescending(g => g.CopiesSold).Take(3).ToList();
                Console.WriteLine("Top 3 Popular Games:");
                foreach (var game in games)
                {
                    Console.WriteLine($"Name: {game.Name}, Copies Sold: {game.CopiesSold}");
                }
            }
        }

        private static void DisplayTop3UnpopularGames()
        {
            using (var context = new GameContext())
            {
                var games = context.Games.OrderBy(g => g.CopiesSold).Take(3).ToList();
                Console.WriteLine("Top 3 Unpopular Games:");
                foreach (var game in games)
                {
                    Console.WriteLine($"Name: {game.Name}, Copies Sold: {game.CopiesSold}");
                }
            }
        }

        private static void AddNewGame(Game newGame)
        {
            using (var context = new GameContext())
            {
                var existingGame = context.Games.FirstOrDefault(g => g.Name == newGame.Name && g.Studio == newGame.Studio);
                if (existingGame == null)
                {
                    context.Games.Add(newGame);
                    context.SaveChanges();
                    Console.WriteLine($"Game {newGame.Name} added successfully.");
                }
                else
                {
                    Console.WriteLine($"Game {newGame.Name} already exists.");
                }
            }
        }

        private static void UpdateGame(string name, string studio, Game updatedGame)
        {
            using (var context = new GameContext())
            {
                var game = context.Games.FirstOrDefault(g => g.Name == name && g.Studio == studio);
                if (game != null)
                {
                    game.Name = updatedGame.Name;
                    game.Studio = updatedGame.Studio;
                    game.Style = updatedGame.Style;
                    game.ReleaseDate = updatedGame.ReleaseDate;
                    game.IsSinglePlayer = updatedGame.IsSinglePlayer;
                    game.CopiesSold = updatedGame.CopiesSold;

                    context.SaveChanges();
                    Console.WriteLine($"Game {name} updated successfully.");
                }
                else
                {
                    Console.WriteLine($"Game {name} not found.");
                }
            }
        }

        private static void DeleteGame(string name, string studio)
        {
            using (var context = new GameContext())
            {
                var game = context.Games.FirstOrDefault(g => g.Name == name && g.Studio == studio);
                if (game != null)
                {
                    Console.WriteLine($"Are you sure you want to delete the game {name} by {studio}? (y/n)");
                    var confirmation = Console.ReadLine();
                    if (confirmation?.ToLower() == "y")
                    {
                        context.Games.Remove(game);
                        context.SaveChanges();
                        Console.WriteLine($"Game {name} deleted successfully.");
                    }
                }
                else
                {
                    Console.WriteLine($"Game {name} not found.");
                }
            }
        }
    }
}