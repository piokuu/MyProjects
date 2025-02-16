using System.Text;

namespace ProstySystemLogowania
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Login user = new Login();
            user.LoadData();

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=============================================");
                Console.WriteLine("        Witaj w Prostym Systemie Logowania   ");
                Console.WriteLine("=============================================");
                Console.ResetColor();

                Console.WriteLine("\nWybierz opcję:");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("[1] Zaloguj się");
                Console.WriteLine("[2] Utwórz nowe konto");
                Console.WriteLine("[3] Zmień hasło");
                Console.WriteLine("[4] Wygeneruj losowe hasło (do skopiowania)");
                Console.WriteLine("[5] Opuść aplikację");
                Console.ResetColor();

                int choice;
                bool valid;
                Console.Write("\nTwój wybór: ");
                valid = int.TryParse(Console.ReadLine(), out choice);

                switch (choice)
                {
                    case 1:
                        user.LoginUser();
                        break;
                    case 2:
                        user.Register();
                        break;
                    case 3:
                        user.ChangePassword();
                        break;
                    case 4:
                        user.GeneratePassword();
                        break;
                    case 5:
                        user.SaveData();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nDziękujemy za korzystanie z aplikacji!");
                        Console.ResetColor();
                        return;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nNiepowodzenie! Wybierz opcje z dostępnych!");
                        Console.ResetColor();
                        break;
                }

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("\nWciśnij dowolny przycisk, aby kontynuować...");
                Console.ResetColor();
                Console.ReadKey();
            }
        }
    }

    class Login
    {
        private static Dictionary<string, string> login = new Dictionary<string, string>();
        private const string filePath = "user_date.txt";

        public void SaveData()
        {
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                foreach (var user in login)
                {
                    sw.WriteLine($"{user.Key}:{user.Value}");
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nDane zostały zapisane pomyślnie.");
            Console.ResetColor();
        }

        public void LoadData()
        {
            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        var parts = line.Split(':');
                        if (parts.Length == 2)
                        {
                            login[parts[0]] = parts[1];
                        }
                    }
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nDane użytkowników zostały załadowane.");
                Console.ResetColor();
            }
        }

        public void Register()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=============================================");
            Console.WriteLine("         REJESTRACJA - Utwórz nowe konto     ");
            Console.WriteLine("=============================================");
            Console.ResetColor();

            Console.Write("\nPodaj nazwę użytkownika: ");
            string userName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(userName))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nNazwa użytkownika nie może być pusta!");
                Console.ResetColor();
                return;
            }
            if (login.ContainsKey(userName))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nUżytkownik o podanej nazwie już istnieje! Spróbuj inny login.");
                Console.ResetColor();
                return;
            }
            if (!(userName.Length > 4 && userName.Length < 15))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nNiepowodzenie! Nazwa musi mieć od 4 do 15 znaków maksymalnie!");
                Console.ResetColor();
                return;
            }

            Console.Write("Ustaw hasło: ");
            string password = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(password))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nHasło nie może być puste!");
                Console.ResetColor();
                return;
            }
            if (!(password.Length > 8 && password.Length < 20))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nNiepowodzenie! Hasło musi mieć od 8 do 20 znaków maksymalnie!");
                Console.ResetColor();
                return;
            }

            login.Add(userName, password);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nRejestracja zakończona sukcesem!");
            Console.ResetColor();
        }

        public bool LoginUser()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=============================================");
            Console.WriteLine("             LOGOWANIE - Zaloguj się         ");
            Console.WriteLine("=============================================");
            Console.ResetColor();

            Console.Write("\nPodaj login: ");
            string usernameInput = Console.ReadLine();

            Console.Write("Podaj hasło: ");
            string passwordInput = Console.ReadLine();
            if (login.ContainsKey(usernameInput) && login[usernameInput] == passwordInput)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nLogowanie zakończone sukcesem!");
                Console.ResetColor();
                return true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nBłędny login lub hasło. Spróbuj ponownie.");
                Console.ResetColor();
                return false;
            }
        }

        public void ChangePassword()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=============================================");
            Console.WriteLine("           ZMIANA HASŁA - Wprowadź dane      ");
            Console.WriteLine("=============================================");
            Console.ResetColor();

            Console.Write("\nPodaj nazwę użytkownika: ");
            string userName = Console.ReadLine();

            Console.Write("Podaj obecne hasło: ");
            string password = Console.ReadLine();

            if (login.ContainsKey(userName) && login[userName] == password)
            {
                Console.Write("Podaj nowe hasło: ");
                string newPassword = Console.ReadLine();

                Console.Write("Powtórz nowe hasło: ");
                string repeatPassword = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(newPassword))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nNowe hasło nie może być puste!");
                    Console.ResetColor();
                    return;
                }

                if (!(newPassword.Length > 8 && newPassword.Length < 20))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nNiepowodzenie! Hasło musi mieć od 8 do 20 znaków maksymalnie!");
                    Console.ResetColor();
                    return;
                }

                if (newPassword == repeatPassword)
                {
                    login[userName] = newPassword;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nHasło zostało zmienione!");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nPodane hasła nie są zgodne.");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nBłędny login lub hasło. Spróbuj ponownie.");
                Console.ResetColor();
            }
        }
        public void GeneratePassword()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=============================================");
            Console.WriteLine("               GENERATOR HASŁA              ");
            Console.WriteLine("=============================================");
            Console.ResetColor();
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*";
            StringBuilder password = new StringBuilder();
            Random rnd = new Random();

            Console.WriteLine("Podaj długość hasła");

            if (int.TryParse(Console.ReadLine(), out int length) && length > 0)
            {
                for (int i = 0; i < length; i++)
                {
                    password.Append(chars[rnd.Next(chars.Length)]);
                }
                Console.WriteLine($"Wygenerowane hasło: {password}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Nieprawidłowa długość");
                Console.ResetColor();
            }
        }
    }
}