using LinqPractice;
using System;
using System.Collections.Generic;
using System.Linq;  

var games = new List<Game>
{
    new Game { Title = "The Legend of Zelda: Breath of the Wild", Genre = "Action-Adventure", ReleaseYear = 2017, Rating = 9.5, price = 60 },
    new Game { Title = "God of War", Genre = "Action", ReleaseYear = 2018, Rating = 9.3, price = 50 },
    new Game { Title = "Red Dead Redemption 2", Genre = "Action-Adventure", ReleaseYear = 2018, Rating = 9.7, price = 70 },
    new Game { Title = "The Witcher 3: Wild Hunt", Genre = "RPG", ReleaseYear = 2015, Rating = 9.8, price = 40 },
    new Game { Title = "Hades", Genre = "Roguelike", ReleaseYear = 2020, Rating = 9.0, price = 25 },
    new Game { Title = "Celeste", Genre = "Platformer", ReleaseYear = 2018, Rating = 8.9, price = 20 },
    new Game { Title = "Among Us", Genre = "Party", ReleaseYear = 2018, Rating = 7.5, price = 5 },
    new Game { Title = "Cyberpunk 2077", Genre = "RPG", ReleaseYear = 2020, Rating = 7.0, price = 60 },
    new Game { Title = "Minecraft", Genre = "Sandbox", ReleaseYear = 2011, Rating = 9.0, price = 30 },
    new Game { Title = "Fortnite", Genre = "Battle Royale", ReleaseYear = 2017, Rating = 8.0, price = 0 }
};  


// to get all game titles using LINQ
var allGames = games.Select(g => g.Title);


 foreach (var title in allGames)
{
    Console.WriteLine(title);
}


// to get all RPG games using LINQ
Console.WriteLine("\nRPG Games:");

var rpgGames = games.Where(g => g.Genre == "RPG");

foreach (var game in rpgGames)
{
    Console.WriteLine($"{game.Title} - {game.Genre}");
}


// to get the highest rated game using LINQ
var modernGames = games.Any(g => g.ReleaseYear >= 2018);
Console.WriteLine($"\nAre there any modern games (released in or after 2018)? {modernGames}");  

// to get games sorted by release year using LINQ
var sortedByYear = games.OrderByDescending(g => g.ReleaseYear);
Console.WriteLine("\nGames sorted by release year (newest to oldest):");
foreach (var game in sortedByYear)
{
    Console.WriteLine($"{game.Title} - {game.ReleaseYear}");
}   

// to get average price of all games using LINQ
var averagePrice = games.Average(g => g.price);
Console.WriteLine($"\nAverage game price: ${averagePrice:F2}");   


// to get the highest rated game using LINQ
var highestRating = games.Max(g => g.Rating);
var highestRatedGame = games.First(g => g.Rating == highestRating);
Console.WriteLine($"\nHighest Rated Game: {highestRatedGame.Title} with a rating of {highestRatedGame.Rating}");    

// to group games by genre using LINQ
var gamesGroupBy = games.GroupBy(games => games.Genre);
foreach (var group in gamesGroupBy)
{
    Console.WriteLine($"\nGenre: {group.Key}");
    foreach (var game in group)
    {
        Console.WriteLine($" - {game.Title}");
    }
}

// to get all RPG games under $30, sorted by release year using LINQ
Console.WriteLine("\nAffordable RPG Games (under $30), sorted by release year:");

var budgetAdventureGame = games
.Where(g => g.Genre == "RPG" && g.price < 50)
.OrderBy(g => g.ReleaseYear)
.Select(g => $"{g.Title} - ${g.price} - Released in {g.ReleaseYear}");

foreach (var game in budgetAdventureGame)
{
    Console.WriteLine(game);
}

// to get all Action-Adventure games using LINQ query syntax
Console.WriteLine("\nAction-Adventure Games:");
var adventureGames = games.Where(g => g.Genre == "Action-Adventure");

var adventureGameQueery = from game in games
                          where game.Genre == "Action-Adventure"
                          orderby game.Rating descending
                          select game;
foreach (var game in adventureGameQueery)
{
    Console.WriteLine($"{game.Title} - Rating: {game.Rating}");
}

// to get the cheapest game using LINQ
var cheapestGame = games.OrderBy(g => g.price).First();
Console.WriteLine($"\nCheapest Game: {cheapestGame.Title} at ${cheapestGame.price}");   


// // to get distinct genres using LINQ
var genres = games.Select(g => g.Genre).Distinct();
Console.WriteLine("\nDistinct Game Genres:");
foreach (var genre in genres)
{
    Console.WriteLine(genre);
}