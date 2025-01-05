namespace PodajWynikDodawaniaGra
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool playAgain;
            do
            {
                Random rnd = new Random();
                int random1 = rnd.Next(1, 11);
                int random2 = rnd.Next(1, 11);
                int wynik = random1 + random2;
                int user;

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{random1} + {random2} = ");
                Console.ResetColor();

                do
                {
                    Console.Write("Podaj wynik: ");
                    while (!int.TryParse(Console.ReadLine(), out user))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("To nie jest cyfra!");
                        Console.ResetColor();
                    }

                    if (user != wynik)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Spróbuj jeszcze raz!");
                        Console.ResetColor();
                    }
                } while (user != wynik);


                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Brawo!!!");
                Console.ResetColor();

                playAgain = CzyUzytkownikChceGrac();
            } while (playAgain);
        }

        static bool CzyUzytkownikChceGrac()
        {
            while (true)
            {
                Console.Write("Czy chcesz grać dalej (tak/nie)? ");
                string next = Console.ReadLine()?.ToLower();
                if (next == "tak")
                    return true;
                if (next == "nie")
                    return false;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Nie rozumiem odpowiedzi. Wpisz 'tak' lub 'nie'.");
                Console.ResetColor();
            }
        }
    }
}