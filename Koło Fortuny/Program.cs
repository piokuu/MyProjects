namespace koloFortuny
{
    internal class Program
    {
        static void Main(string[] args)
        {
            for (int j = 0; j < 2; j++)
            {
                for (int i = 0; i <= 3; i++)
                {
                    string dots = new string('.', i);
                    string spaces = new string(' ', 3 - i);
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write($"\rŁadowanie koła proszę czekać{dots}{spaces}");
                    Thread.Sleep(350);
                }
            }
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("╔══════════════════════════════════════════════╗");
            Console.WriteLine("║          KOŁO FORTUNY - Wersja 1.0           ║");
            Console.WriteLine("╚══════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();

            Console.Title = "Koło Fortuny - Menu Operacji";

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Aby zobaczyć dostępne operacje, wpisz: --pomoc");
            Console.ResetColor();

            FortuneWheel fortuneWheel = new FortuneWheel();
            string input;
            do
            {
                input = Console.ReadLine()?.ToLower();
                if (input != "--pomoc")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Nieznane polecenie.");
                    Console.ResetColor();
                }
            } while (input != "--pomoc");

            Console.Clear();
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("========= DOSTĘPNE OPERACJE =========");
                Console.WriteLine("1. dodaj          - Dodaj nową opcję");
                Console.WriteLine("2. usuń           - Usuń wybraną opcję");
                Console.WriteLine("3. usuń wszystkie - Usuń wszystkie opcje");
                Console.WriteLine("4. wyświetl       - Pokaż wszystkie opcje");
                Console.WriteLine("5. losuj          - Wylosuj jedną z opcji");
                Console.WriteLine("6. opuść program  - Wyłącza program");
                Console.WriteLine("======================================");
                Console.ResetColor();

                Console.Write("Wybierz operację: ");
                string operation = Console.ReadLine()?.ToLower();

                switch (operation)
                {
                    case "1":
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Wpisz treść wyboru:");
                        string addOption = Console.ReadLine();
                        fortuneWheel.Add(addOption);
                        break;
                    case "2":
                        bool valid;
                        int deleteOption;
                        fortuneWheel.ShowOptions();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Wpisz numer opcji do usunięcia:");
                        do
                        {
                            valid = int.TryParse(Console.ReadLine(), out deleteOption);
                            if (!valid)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Wpisz cyfrę opcji, którą chcesz usunąć");
                                Console.ResetColor();
                            }
                        } while (!valid);
                        fortuneWheel.Delete(deleteOption);
                        break;
                    case "3":
                        fortuneWheel.DeleteAll();
                        break;
                    case "4":
                        fortuneWheel.ShowOptions();
                        break;
                    case "5":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        fortuneWheel.Spin();
                        Console.ResetColor();
                        break;
                    case "6":
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Dziękujemy za korzystanie z naszego programu");
                        Console.ResetColor();
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Nieznana operacja.");
                        Console.ResetColor();
                        break;
                }

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Naciśnij dowolny klawisz, aby wrócić do menu...");
                Console.ResetColor();
                Console.ReadKey();
                Console.Clear();
            }
        }
    }

    class FortuneWheel
    {
        List<string> options;

        public FortuneWheel()
        {
            options = new List<string>();
        }

        public void Add(string option)
        {
            if (options.Contains(option))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ta opcja już istnieje");
                Console.ResetColor();
                return;
            }
            if (string.IsNullOrWhiteSpace(option))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Nie można dodać pustej opcji!");
                Console.ResetColor();
            }
            options.Add(option);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Pomyślnie dodano operacje: {option}");
            Console.ResetColor();
        }

        public void Delete(int option)
        {
            if (options.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Nie ma jeszcze żadnych dodanych opcji");
                Console.ResetColor();
                return;
            }

            if (options.Count >= option && option > 0)
            {
                string optionToDelete = options[option - 1];
                options.Remove(optionToDelete);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Usunięto opcję: {optionToDelete}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Opcja z takim numerem nie istnieje");
                Console.ResetColor();
            }
        }

        public void DeleteAll()
        {
            if (options.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Nie ma jeszcze żadnych dodanych opcji");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Czy na pewno chcesz usunąć wszystkie opcje ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("tak");
            Console.ResetColor();
            Console.Write(" / ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("nie");
            Console.ResetColor();

            Console.WriteLine();
            string deleteChoice = Console.ReadLine().ToLower();
            if (deleteChoice == "tak")
            {
                for (int j = 0; j < 2; j++)
                {
                    for (int i = 0; i <= 3; i++)
                    {
                        string dots = new string('.', i);
                        string spaces = new string(' ', 3 - i);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"\rUsuwanie opcji{dots}{spaces}");
                        Thread.Sleep(350);
                    }
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nPomyślnie usunięto wszystkie opcje");
                Console.ResetColor();
                options.Clear();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Operacja usunięcia została przerwana");
                Console.ResetColor();
            }
        }

        public void Spin()
        {
            if (options.Count == 0)
            {
                Console.WriteLine("Brak opcji do wylosowania.");
                return;
            }

            Random rnd = new Random();
            int index = rnd.Next(options.Count);

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Koło się kręci...");
            Thread.Sleep(700);

            for (int i = 0; i < 5; i++)
            {
                foreach (var option in options)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(option);
                    Thread.Sleep(300);
                }
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Wylosowano: {options[index]}");
        }

        public void ShowOptions()
        {
            if (options.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Brak opcji do wyświetlenia.");
                Console.ResetColor();
                return;
            }

            for (int i = 0; i < options.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{i + 1}. {options[i]}");
            }
            Console.ResetColor();
        }
    }
}
