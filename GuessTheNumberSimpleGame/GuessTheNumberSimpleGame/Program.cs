using System.Reflection.PortableExecutable;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Channels;

namespace GuessTheNumberSimpleGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            int random = rnd.Next(1, 11);
            int user = 0;
            bool valid = false;
            int i = 0;
            bool[] array = new bool[10];

            Console.Title = "Zgadnij liczbę!";
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Zgadnij liczbę od 1 - 10\n");
            Console.ResetColor();

            do
            {
                do
                {
                    Console.Write("Podaj liczbę: ");
                    valid = int.TryParse(Console.ReadLine(), out user);
                } while (!valid);

                if (user > 10 || user < 1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Podaj liczbę z przedziału od 1 - 10\n");
                    Console.ResetColor();
                    continue;
                }

                if (array[user - 1])
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Podano tą samą liczbę!\n");
                    Console.ResetColor();
                    continue;
                }
                else
                {
                    array[user - 1] = true;
                }

                if (user > random)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("Wylosowana liczba jest MNIEJSZA od twojej!\n");
                    Console.ResetColor();
                }
                else if (user < random)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("Wylosowana liczba jest WIĘKSZA od twojej!\n");
                    Console.ResetColor();
                }
                i++;
            } while (user != random);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"BRAWO!!! Odgadłeś liczbę za {i} razem.");
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}
