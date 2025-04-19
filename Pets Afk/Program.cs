namespace PetsAfk
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Pets afk version 1.0";

            Player player = new Player();

            var dog = new Pets("Dog", 1.1, 40, ConsoleColor.Gray);
            var cat = new Pets("Cat", 1.1, 30, ConsoleColor.Gray);
            var bunny = new Pets("Bunny", 1.2, 25, ConsoleColor.Yellow);
            var bear = new Pets("Bear", 1.5, 5, ConsoleColor.Red);

            var basicEgg = new Eggs("Common Egg", 10, new List<Pets> { dog, cat, bunny, bear }, 15);

            var mouse = new Pets("Mouse", 2, 39.6, ConsoleColor.Gray);
            var wolf = new Pets("Wolf", 2, 24.6, ConsoleColor.Yellow);
            var fox = new Pets("Fox", 2.2, 19.6, ConsoleColor.DarkYellow);
            var polarBear = new Pets("Polar Bear", 2.5, 9.6, ConsoleColor.Blue);
            var panda = new Pets("Panda", 3, 6.5, ConsoleColor.Magenta);

            var spottedEgg = new Eggs("Spotted Egg", 110, new List<Pets> { mouse, wolf, fox, polarBear, panda }, 10);

            var iceCat = new Pets("Ice Cat", 3, 29.8, ConsoleColor.Gray);
            var deer = new Pets("Deer", 3.5, 24.8, ConsoleColor.Yellow);
            var iceWolf = new Pets("Ice Wolf", 5, 19.8, ConsoleColor.Cyan);
            var pig = new Pets("Pig", 6, 13.8, ConsoleColor.Blue);
            var iceDeer = new Pets("Ice Deer", 7, 7.8, ConsoleColor.Magenta);
            var iceDragon = new Pets("Ice Dragon", 10, 3.9, ConsoleColor.Red);

            var iceshardEgg = new Eggs("Iceshard Egg", 450, new List<Pets> { iceCat, deer, iceWolf, pig, iceDeer, iceDragon }, 10);

            var allEggs = new List<Eggs> { basicEgg, spottedEgg, iceshardEgg };


            Menu();
            GameLoop(player, allEggs);
        }

        public static Player Menu()
        {
            string option;
            Player player = null;

            do
            {
                Console.Title = "Pets afk - Menu";
                Console.Clear();
                WriteLogo();
                Say("1", "Start game");
                Say("2", "Info");
                Say("3", "Discord server");
                Say("4", "Quit");
                option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        Console.Title = "Pets afk - Loading";
                        ShowLoadingScreen();
                        Console.Clear();
                        if (player != null)
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine($"Welcome back!");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine($"Welcome!");
                            player = new Player();
                        }
                        Thread.Sleep(1000);
                        Console.ResetColor();
                        break;

                    case "2":
                        Console.Title = "Pets afk - Info";
                        ShowGameInfo();
                        break;

                    case "3":
                        Console.Title = "Pets afk - Discord";
                        ShowDiscordInfo();
                        break;

                    case "4":
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write("App closing. Have a nice day!");
                        for (int i = 0; i < 3; i++)
                        {
                            Thread.Sleep(300);
                            Console.Write(".");
                            Thread.Sleep(300);
                        }
                        Console.ResetColor();
                        Environment.Exit(0);
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Error. Wrong operation selected");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("Press any key to return...");
                        Console.ResetColor();
                        Console.ReadKey();
                        break;
                }
            } while (option != "1");

            return player;
        }

        public static void GameLoop(Player player, List<Eggs> allEggs)
        {
            Console.Title = "Pets afk - Game";
            string input;
            do
            {
                Console.Clear();
                WriteLogo();
                Console.WriteLine($"Coins: {player.Coins:F2}");
                if (player.ActivePet != null)
                {
                    Console.Write("Equip pet: ");
                    Console.ForegroundColor = player.ActivePet.Color;
                    Console.WriteLine(player.ActivePet.Name);
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine("Active Pet: None");
                }

                Say("1", "Coin farm (earn coins automatically)");
                Say("2", "Hatch an egg");
                Say("3", "Show eggs info");
                Say("4", "Your pets");
                Say("5", "Set active pet");
                Say("6", "Upgrades (Coming soon)");
                Say("7", "Shiny machine (Coming soon)");
                Say("8", "Return to main menu (restart game)");

                input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        StartCoinFarm(player);
                        break;
                    case "2":
                        HatchEggMenu(allEggs, player);
                        break;
                    case "3":
                        ShowEggChances(allEggs);
                        break;
                    case "4":
                        ShowPlayerPets(player);
                        break;
                    case "5":
                        SetActivePet(player);
                        break;
                    case "6":
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("Coming soon");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("Press any key to continue...");
                        Console.ResetColor();
                        break;
                    case "7":
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("Coming soon");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("Press any key to continue...");
                        Console.ResetColor();
                        break;
                    case "8":

                        System.Diagnostics.Process.Start(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
                        Environment.Exit(0);
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Error. Wrong operation selected");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("Press any key to return...");
                        Console.ResetColor();
                        Console.ReadKey();
                        break;
                }
            } while (input != "8");
        }

        public static void StartCoinFarm(Player player)
        {
            Console.Clear();
            Console.WriteLine("AFK Farm started. Press any key to stop.");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Every 10 seconds your multiplier increases by 5%!");
            Console.ResetColor();
            double boostMultiplier = 1.0;
            int boostInterval = 10000;
            int timeSinceLastBoost = 0;
            while (true)
            {

                double earned = player.ActivePet != null ? player.ActivePet.CoinMultiplier * 1 : 1;
                earned *= boostMultiplier;
                player.Coins += earned;

                Console.WriteLine($"+{earned:F2} coins (Boost: x{boostMultiplier:F2})");

                int delay = 2000;
                int step = 100;
                int waited = 0;

                while (waited < delay)
                {
                    if (Console.KeyAvailable)
                    {
                        Console.ReadKey(true);
                        return;
                    }
                    Thread.Sleep(step);
                    waited += step;
                    timeSinceLastBoost += step;

                    if (timeSinceLastBoost >= boostInterval)
                    {
                        boostMultiplier *= 1.05;
                        timeSinceLastBoost = 0;
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine($"BOOST! New Multiplier: x{boostMultiplier:F2}");
                        Console.ResetColor();
                    }
                }
            }
        }

        public static void ShowEggChances(List<Eggs> allEggs)
        {
            Console.Clear();
            Console.WriteLine("Egg chances:\n");

            foreach (var egg in allEggs)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(egg.Name + ":");
                Console.ResetColor();
                foreach (var pet in egg.AvailablePets)
                {
                    Console.ForegroundColor = pet.Color;
                    Console.WriteLine($"  {pet.Name} - Chance: {pet.Chances}% | Coin Multiplier: x{pet.CoinMultiplier}");
                }

                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"  Nothing - Chance: {egg.ChanceToHatchNothing}%");
                Console.ResetColor();
                Console.WriteLine();
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Press any key to return...");
            Console.ResetColor();
            Console.ReadKey();
        }

        public static void HatchEggMenu(List<Eggs> allEggs, Player player)
        {
            Console.Clear();
            Console.WriteLine("Choose an egg to hatch:");

            for (int i = 0; i < allEggs.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {allEggs[i].Name} - {allEggs[i].Price} coins");
            }

            Console.WriteLine("Choice: ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= allEggs.Count)
            {
                var selectedEgg = allEggs[choice - 1];

                if (selectedEgg.Price > player.Coins)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("you don't have enough coins");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ResetColor();
                    Console.ReadKey();
                    return;
                }
                player.Coins -= selectedEgg.Price;

                var pet = selectedEgg.Hatch();

                if (pet != null)
                {
                    Console.Write("You hatched: ");
                    Console.ForegroundColor = pet.Color;
                    Console.WriteLine(pet.Name);
                    Console.ResetColor();
                    Console.WriteLine($"Coin Multiplier: x{pet.CoinMultiplier}");
                    player.PetList.Add(pet);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You hatched... nothing. Better luck next time!");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\nPress any key to continue...");
            Console.ResetColor();
            Console.ReadKey();
        }

        public static void SetActivePet(Player player)
        {
            Console.Clear();
            if (player.PetList.Count == 0)
            {
                Console.WriteLine("You don't have any pets to activate.");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Press any key to continue...");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Select a pet to activate:");
            for (int i = 0; i < player.PetList.Count; i++)
            {
                var pet = player.PetList[i];
                Console.WriteLine($"[{i + 1}]");
                Console.ForegroundColor = pet.Color;
                Console.Write(pet.Name);
                Console.ResetColor();
                Console.WriteLine($" Coin Multiplier: x{pet.CoinMultiplier}");
            }
            bool valid;
            int option;
            do
            {
                valid = int.TryParse(Console.ReadLine(), out option);
                if (!valid)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Error! Try again.");
                    Console.ResetColor();
                }
            } while (!valid);

            try
            {
                player.ActivePet = player.PetList[option - 1];
            }
            catch (ArgumentOutOfRangeException r)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Error!");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Press any key to continue...");
                Console.ResetColor();
                Console.ReadKey();
                Console.ResetColor();
                return;
            }

        }

        public static void ShowPlayerPets(Player player)
        {
            Console.Clear();
            Console.WriteLine("Your pets:\n");
            foreach (var pet in player.PetList)
            {
                Console.ForegroundColor = pet.Color;
                Console.Write(pet.Name);
                Console.ResetColor();
                Console.WriteLine($" Coin Multiplier: x{pet.CoinMultiplier}\n");
            }
            Console.ResetColor();

            Console.WriteLine("\nPress any key to return.");
            Console.ReadKey();
        }

        public static void ShowGameInfo()
        {
            Console.Clear();
            Console.WriteLine("Welcome to 'Pets afk' version 1.0!");
            Console.WriteLine();
            Console.WriteLine("In this game, you manage your pets and collect coins.");
            Console.WriteLine("Your pets will collect resources even when you're not playing.");
            Console.WriteLine("Collect better and better pets and earn rewards as you progress through the game!");
            Console.WriteLine();
            Console.WriteLine("Game developed by piokkk");
            Console.WriteLine("Created 19 April 2025");
            Console.WriteLine("Version 1.0 - Initial release");
            Console.WriteLine("Thank you for playing!");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\nPress any key to return to the menu.");
            Console.ResetColor();
            Console.ReadKey();
        }

        public static void ShowDiscordInfo()
        {
            Console.Clear();
            Console.WriteLine("Join our community on Discord!");
            Console.WriteLine("We have a vibrant community of players, and we often host events and giveaways.");
            Console.WriteLine("To join, click the link below (the server is not real):");
            Console.WriteLine("\nDiscord server: https://discord.gg/pets-afk");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\nPress any key to return to the menu.");
            Console.ResetColor();
            Console.ReadKey();
        }

        public static void ShowLoadingScreen()
        {
            Console.Clear();
            Console.WriteLine("Starting pets afk. Please wait...");
            Thread.Sleep(500);

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("Loading resources");

            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(700);
                Console.Write(".");
            }
            Console.Write("\nWaking up pets");

            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(700);
                Console.Write(".");
            }
            Console.Write("\nLoading save data");

            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(700);
                Console.Write(".");
            }
            Console.WriteLine();
            Thread.Sleep(500);
            Console.ResetColor();
        }

        public static void WriteLogo()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(@"  ____      _                __ _    
 |  _ \ ___| |_ ___    __ _ / _| | __
 | |_) / _ \ __/ __|  / _` | |_| |/ /
 |  __/  __/ |_\__ \ | (_| |  _|   < 
 |_|   \___|\__|___/  \__,_|_| |_|\_\
                                     ");
            Console.ResetColor();
        }

        public static void Say(string prefix, string message)
        {
            Console.Write(" [");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(prefix);
            Console.ResetColor();
            Console.WriteLine("] " + message);
        }
    }

    class Pets
    {
        public Pets(string name, double coinMultiplier, double chances, ConsoleColor color)
        {
            CoinMultiplier = coinMultiplier;
            Chances = chances;
            Name = name;
            Color = color;
        }

        public string Name { get; }
        public double CoinMultiplier { get; }
        public double Chances { get; }
        public ConsoleColor Color { get; }

        public Pets Clone()
        {
            return new Pets(Name, CoinMultiplier, Chances, Color);
        }
    }

    class Player
    {
        public double Coins { get; set; } = 0;
        public List<Pets> PetList { get; set; } = new List<Pets>();
        public Pets ActivePet { get; set; }

        public void AddPet(Pets pet)
        {
            PetList.Add(pet);
        }
    }

    class Eggs
    {
        public string Name { get; }
        public int Price { get; }
        public List<Pets> AvailablePets { get; }
        public double ChanceToHatchNothing { get; }
        public Eggs(string name, int price, List<Pets> pets, int chanceToHatchNothing)
        {
            Name = name;
            Price = price;
            AvailablePets = pets;
            ChanceToHatchNothing = chanceToHatchNothing;
        }

        public Pets Hatch()
        {
            Random rnd = new Random();
            double roll = rnd.NextDouble();

            double current = 0.0;

            if (roll < ChanceToHatchNothing / 100.0)
            {
                return null;
            }

            current += ChanceToHatchNothing / 100.0;

            foreach (var pet in AvailablePets)
            {
                double chance = pet.Chances / 100.0;
                if (roll < current + chance)
                {
                    return pet.Clone();
                }
                current += chance;
            }

            return null;
        }

        public void ShowChances()
        {
            double total = AvailablePets.Sum(p => p.Chances) + ChanceToHatchNothing;

            Console.WriteLine($"Chances in {Name}:");
            foreach (var pet in AvailablePets)
            {
                double percent = (double)pet.Chances / total * 100;
                Console.WriteLine($" - {pet.Name}: {percent:F2}%");
            }

            double emptyPercent = (double)ChanceToHatchNothing / total * 100;
            Console.WriteLine($" - Nothing: {emptyPercent:F2}%");
        }
    }
}


