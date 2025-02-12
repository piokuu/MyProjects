namespace SystemBankowy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BankAccount account = new BankAccount();

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("=======================");
                Console.WriteLine("Witaj w koncie bankowym");
                Console.WriteLine("=======================");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\nTwoje saldo: {BankAccount.Balance} zł");
                Console.ResetColor();

                Console.WriteLine("[1] Wpłać środki.");
                Console.WriteLine("[2] Wypłać środki.");
                Console.WriteLine("[3] Historia transakcji.");
                Console.WriteLine("[4] Opuść aplikacje.");

                bool valid;
                int operation;
                do
                {
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    valid = int.TryParse(Console.ReadLine(), out operation);
                    Console.ResetColor();
                    switch (operation)
                    {
                        case 1:
                            account.DepositMoney();
                            break;
                        case 2:
                            account.WithdrawMoney();
                            break;
                        case 3:
                            account.ShowTransactions();
                            break;
                        case 4:
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Dziękujemy za korzystanie z naszej aplikacji.");
                            Console.ResetColor();
                            return;
                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Nie ma takiej operacji!");
                            Console.ResetColor();
                            break;
                    }
                } while (!(operation == 1 || operation == 2 || operation == 3 || operation == 4 || operation == default));
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("\nKliknij dowolny przycisk, aby kontynuować...");
                Console.ResetColor();
                Console.ReadKey();

                Console.Clear();
            }

        }
    }

    class BankAccount
    {
        public static decimal Balance { get; set; }
        Dictionary<int, (string Type, decimal Amount)> transactions = new Dictionary<int, (string Type, decimal Amount)>();
        int transactionId = 1;
        const string filePath = "KontoBankowe.txt";

        public BankAccount()
        {
            LoadFromFile();
        }
        public void DepositMoney()
        {
            decimal cash;
            bool valid;
            Console.Write("Wprowadź ilość pieniędzy do wpłaty: ");
            valid = decimal.TryParse(Console.ReadLine(), out cash);
            if (!valid)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("To nie są cyfry!");
                Console.ResetColor();
            }
            else if (cash > 3000 || cash < 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Można wpłacić od 1 do 3000 zł maksymalnie");
                Console.ResetColor();

            }
            else
            {
                transactions.Add(transactionId++, ("Wpłata", cash));
                Balance += cash;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Wpłata zakończona sukcesem.");
                Console.ResetColor();
                SaveToFile();
            }
        }

        public void WithdrawMoney()
        {
            decimal cash;
            bool valid;
            int transaction = 1;
            do
            {
                Console.Write("Wprowadź ilość pieniędzy do wypłaty: ");
                valid = decimal.TryParse(Console.ReadLine(), out cash);

                if (Balance < cash)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Brak środków na koncie!");
                    Console.ResetColor();
                    return;
                }

                if (!valid)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Wprowadź prawidłowe liczby!");
                    Console.ResetColor();
                }
                else if (cash > 3000 || cash < 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Można wypłacić od 0 do 3000 zł maksymalnie");
                    Console.ResetColor();
                }
                else
                {
                    transactions.Add(transactionId++, ("Wypłata", cash));
                    Balance -= cash;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Wypłata zakończona sukcesem.");
                    Console.ResetColor();
                    SaveToFile();
                }
            } while (!valid);
        }

        public void ShowTransactions()
        {
            if (transactions.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Nie wykonano żadnych transakcji!");
                Console.ResetColor();
                return;
            }
            Console.WriteLine("\nHistoria transakcji:");
            foreach (var transaction in transactions)
            {
                Console.WriteLine($"{transaction.Key}. {transaction.Value.Type} {transaction.Value.Amount} zł");
            }
        }

        private void SaveToFile()
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine($"Saldo: {Balance} zł");
                writer.WriteLine("Historia transakcji:");
                foreach (var transaction in transactions)
                {
                    writer.WriteLine($"{transaction.Key}. {transaction.Value.Type} {transaction.Value.Amount} zł");
                }
            }
        }

        private void LoadFromFile()
        {
            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.StartsWith("Saldo"))
                        {
                            string[] parts = line.Split(':');
                            if (decimal.TryParse(parts[1].Trim().Replace("zł", ""), out decimal loadedBalance))
                            {
                                Balance = loadedBalance;
                            }
                        }
                        if (line.StartsWith("Historia"))
                        {
                            continue;
                        }
                        if (line.Contains("Wpłata") || line.Contains("Wypłata"))
                        {
                            string[] parts = line.Split(' ');
                            string type = parts[1];
                            decimal amount = decimal.Parse(parts[2].Replace("zł", ""));
                            transactions.Add(transactionId++, (type, amount));
                        }
                    }
                }
            }
        }
    }
}
