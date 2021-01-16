using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Console_ListOfObjects
{
    class Program
    {
        static void Main(string[] args)
        {

            // The main program

            List<VideoGame> games = new List<VideoGame>();
            SetTheme(ConsoleColor.White, ConsoleColor.DarkBlue);
            DisplayMainMenu(games);
        }
        private static void DisplayMainMenu(List<VideoGame> games)
        {

            // Displays the main program loop for menu options

            bool quitMainMenu = false;
            ConsoleKeyInfo menuChoiceKey;
            char menuChoice;
            do
            {
                Console.Clear();
                Console.WriteLine("\ta) List All Games");
                Console.WriteLine("\tb) Add Game");
                Console.WriteLine("\tc) Delete Game");
                Console.WriteLine("\td) Save Games");
                Console.WriteLine("\te) Load Games");
                Console.WriteLine("\tf) Load example list");
                Console.WriteLine("\tq) Quit Program");
                Console.WriteLine();
                Console.Write("\tMenu Choice:");

                menuChoiceKey = Console.ReadKey();
                menuChoice = menuChoiceKey.KeyChar;
                switch (menuChoice)
                {
                    case 'a':
                        DisplayGames(games);
                        DisplayContinuePrompt();
                        break;
                    case 'A':
                        DisplayGames(games);
                        DisplayContinuePrompt();
                        break;
                    case 'b':
                        AddGame(games);
                        break;
                    case 'B':
                        AddGame(games);
                        break;
                    case 'c':
                        DeleteGame(games);
                        break;
                    case 'C':
                        DeleteGame(games);
                        break;
                    case 'd':
                        SaveGames(games);
                        break;
                    case 'D':
                        SaveGames(games);
                        break;
                    case 'e':
                        LoadGames(games);
                        break;
                    case 'E':
                        LoadGames(games);
                        break;
                    case 'f':
                        ExampleGameList(games);
                        break;
                    case 'F':
                        ExampleGameList(games);
                        break;
                    case 'q':
                        quitMainMenu = true;
                        break;
                    case 'Q':
                        quitMainMenu = true;
                        break;
                }
            } while (!quitMainMenu);
        }
        private static void AddGame(List<VideoGame> games)
        {

            // Query user to input new game(s) into list

            // TODO - Need to add actual support for database IDs

            bool doneAdding = false;
            bool validResponse = false;
            do
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("\tAdd Games");
                Console.WriteLine();
                VideoGame videogame = new VideoGame();
                Console.Write("\tName: ");
                videogame.Name = Console.ReadLine();
                //Validate Rating
                do
                {
                    Console.Write("\tRating (1-10): ");
                    if (int.TryParse(Console.ReadLine(), out int rating))
                    {
                        videogame.Rating = rating;
                        if (rating >= 1 && rating <= 10)
                        {
                            validResponse = true;
                        }
                        else Console.WriteLine("\tEnter valid entry");                        
                    }
                    else Console.WriteLine("\tEnter valid entry");              
                } while (!validResponse);
                //Validate released
                validResponse = false;
                do
                {
                    Console.Write("\tReleased? (true/false): ");
                    if (bool.TryParse(Console.ReadLine(), out bool released))
                    {
                        videogame.IsReleased = released;
                        validResponse = true;
                    }
                    else Console.WriteLine("\tEnter valid entry");
                } while (!validResponse);
                //Validate console
                validResponse = false;
                do
                {
                    Console.Write("\tConsole? (Playstation/Xbox/Nintendo): ");
                    if (Enum.TryParse(Console.ReadLine(), out VideoGame.Console system))
                    {
                        videogame.System = system;
                        validResponse = true;
                    }
                    else Console.WriteLine("\tEnter valid entry");
                } while (!validResponse);
               
                games.Add(videogame);

                validResponse = false;
                do
                {
                    Console.Write("\tAdd Another? (yes/no): ");
                    string userResponse = Console.ReadLine().ToLower();
                    if (userResponse == "no")
                    {
                        doneAdding = true;
                        validResponse = true;
                    }
                    else if (userResponse == "yes") validResponse = true;
                    else Console.WriteLine("\tEnter valid entry");
                } while (!validResponse);

            } while (!doneAdding);
        }
        private static void DeleteGame(List<VideoGame> games)
        {

            // Query user to delete game using name field

            // TODO - If multiple games have same name, this deletes first instance. Need to switch to IDs.

            bool validResponse = false;
            List<string> gameNames = games.Select(g => g.Name).ToList();
            Console.Clear();
            DisplayGames(games);
            Console.WriteLine();
            Console.Write("\tEnter name of game to delete: ");
            string videoGame = Console.ReadLine();
            Console.WriteLine();

            if (gameNames.Contains(videoGame))
            {
                VideoGame game = games.FirstOrDefault(g => g.Name == videoGame);
                Console.WriteLine("\tRemoved " + videoGame);
                games.Remove(game);
                validResponse = true;
            }
            if (validResponse == false) Console.WriteLine("\tGame not found. Enter valid game name.");

            DisplayContinuePrompt();
        }
        private static void DisplayGames(List<VideoGame> games)
        {

            // Display entire list of games in table format

            Console.Clear();
            Console.WriteLine($"\t" + "Name".PadRight(30) + "System".PadRight(15) + "Released?".PadRight(10) + "Rating\n");
            int countSum = 0;
            int countTotal = 0;
            foreach (VideoGame videogame in games)
            {
                DisplayGameDetail(videogame);
                countTotal++;
                countSum += videogame.Rating;
            }
            Console.WriteLine("\t".PadRight(55) + "----");
            Console.WriteLine("\t(Average rating)".PadRight(56) + (countSum / countTotal));
        }
        private static void DisplayGameDetail(VideoGame videogame)
        {

            // Display details for specific game

            Console.WriteLine(
                "\t" +
                videogame.Name.PadRight(30) +
                videogame.System.ToString().PadRight(15) +
                (videogame.IsReleased ? "Yes" : "No").PadRight(10) +
                videogame.Rating.ToString()
                );
        }
        private static void SaveGames(List<VideoGame> games)
        {

            // Saves current database to file dataPath

            Console.Clear();
            string dataPath = @"Data\Games.txt";
            bool validResponse = false;
            bool doISave = true;
            if (File.Exists(dataPath))
            {
                do
                {
                    Console.Write("\tFile exists. Overwrite? (yes/no): ");
                    string response = Console.ReadLine();
                    if (response == "yes")
                    {
                        File.Delete(dataPath);
                        validResponse = true;
                    }
                    else if (response == "no")
                    {
                        doISave = false;
                        Console.WriteLine();
                        Console.WriteLine("\tFile not saved.");
                        DisplayContinuePrompt();
                        validResponse = true;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("\tEnter valid entry.");
                        Console.WriteLine();
                    }
                } while (!validResponse);
            }
            if (doISave)
            {
                string gameString;
                foreach (VideoGame videoGame in games)
                {
                    gameString = videoGame.Id + "," + videoGame.Name + "," + videoGame.Rating + "," + videoGame.IsReleased + "," + videoGame.System + "\n";
                    File.AppendAllText(dataPath, gameString);
                }
                Console.WriteLine();
                Console.WriteLine("\tGame database saved as Games.txt");
                DisplayContinuePrompt();
            }
        }
        private static void LoadGames(List<VideoGame> games)
        {

            // Loads file dataPath into database

            string dataPath = @"Data\Games.txt";
            bool validResponse = false;
            bool loadSuccess = true;
            Console.Clear();
            if (File.Exists(dataPath))
            {
                do
                {
                    Console.WriteLine($"\tThis will replace the current database with {dataPath}");
                    Console.Write("\tProceed? (yes/no): ");
                    string response = Console.ReadLine().ToLower();
                    Console.WriteLine();
                    if (response == "yes")
                    {
                        
                        int wordCount = 1;
                        List<VideoGame> newGames = new List<VideoGame>();
                        foreach (string line in File.ReadLines(dataPath))
                        {
                            VideoGame newGame = new VideoGame();
                            wordCount = 1;
                            string[] words = line.Split(',');
                            foreach (var word in words)
                            {
                                if (wordCount == 1)
                                {
                                    if (int.TryParse(word, out int id))
                                    {
                                        newGame.Id = id;
                                        wordCount++;
                                    }
                                    else
                                    {
                                        Console.WriteLine("\tError loading file.");
                                        loadSuccess = false;
                                        break;
                                    }
                                }
                                else if (wordCount == 2)
                                {
                                    newGame.Name = word;
                                    wordCount++;
                                }
                                else if (wordCount == 3)
                                {
                                    if (int.TryParse(word, out int rating))
                                    {
                                        newGame.Rating = rating;
                                        wordCount++;
                                    }
                                    else
                                    {
                                        Console.WriteLine("\tError loading file.");
                                        loadSuccess = false;
                                        break;
                                    }
                                }
                                else if (wordCount == 4)
                                {
                                    if (bool.TryParse(word, out bool isreleased))
                                    {
                                        newGame.IsReleased = isreleased;
                                        wordCount++;
                                    }
                                    else
                                    {
                                        Console.WriteLine("\tError loading file.");
                                        loadSuccess = false;
                                        break;
                                    }
                                }
                                else if (wordCount == 5)
                                {
                                    if (Enum.TryParse(word, out VideoGame.Console console))
                                    {
                                        newGame.System = console;
                                        wordCount++;
                                    }
                                    else
                                    {
                                        Console.WriteLine("\tError loading file.");
                                        loadSuccess = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("\tError loading file.");
                                    loadSuccess = false;
                                    break;
                                }
                            }
                            if (loadSuccess) newGames.Add(newGame);
                            else break;
                        }
                        if (loadSuccess)
                        {
                            games.Clear();
                            foreach (VideoGame videogame in newGames) games.Add(videogame);
                            Console.WriteLine($"\tLoaded file {dataPath}");
                        }
                        validResponse = true;
                    }
                    else if (response == "no")
                    {
                        Console.WriteLine("\tFile not loaded.");
                        validResponse = true;
                    }
                    else
                    {
                        Console.WriteLine("\tEnter valid entry.");
                        Console.WriteLine();
                    }
                } while (!validResponse);
            }
            else
            {
                Console.WriteLine("\tFile does not exist.");
            }
            Console.WriteLine();
            DisplayContinuePrompt();
        }
        private static void ExampleGameList(List<VideoGame> games)
        {

            // Create example list

            games.Clear();
            games.Add(new VideoGame(1, "Uncharted: Drake's Fortune", 8, true, VideoGame.Console.Playstation));
            games.Add(new VideoGame(2, "Marvel's Spider-Man", 8, true, VideoGame.Console.Playstation));
            games.Add(new VideoGame(3, "Halo Infinite", 5, false, VideoGame.Console.Xbox));
            games.Add(new VideoGame(4, "Super Mario Odyssey", 9, true, VideoGame.Console.Nintendo));
            Console.Clear();
            Console.WriteLine("\tExample list of games loaded.");
            DisplayContinuePrompt();
        }
        private static void DisplayContinuePrompt()
        {

            // Display user prompt

            Console.WriteLine();
            Console.WriteLine("\tPress any key to continue.");
            Console.ReadKey();
        }
        private static void SetTheme(ConsoleColor background, ConsoleColor foreground)
        {

            // Set properties of console window

            Console.WindowHeight = 40;
            Console.BackgroundColor = background;
            Console.ForegroundColor = foreground;
            Console.Clear();

        }
    }
}
