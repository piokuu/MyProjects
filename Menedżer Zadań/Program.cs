namespace ListaZadan
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<TaskItem> list = new List<TaskItem>();
        Console.WriteLine("===============================");
            TextColor(ConsoleColor.Yellow, "      MENADŻER ZADAŃ");


            while (true)
            {
                Console.WriteLine("===============================");
                Console.WriteLine("[1] - Dodaj zadanie");
                Console.WriteLine("[2] - Usuń zadanie");
                Console.WriteLine("[3] - Wyświetl zadania");
                Console.WriteLine("[4] - Opuść program");
                Console.WriteLine("[5] - Oznacz zadanie jako ukończone");
                Console.WriteLine("===============================");

                ConsoleKey operation;
                do
                {
                    operation = Console.ReadKey().Key;
                    switch (operation)
                    {
                        case ConsoleKey.D1:
                            AddTask(list);
                            break;
                        case ConsoleKey.D2:
                            RemoveTask(list);
                            break;
                        case ConsoleKey.D3:
                            ShowTasks(list);
                            break;
                        case ConsoleKey.D4:
                            return;
                        case ConsoleKey.D5:
                            CompleteTask(list);
                            break;
                        default:
                            TextColor(ConsoleColor.Red, $"Nie ma takiej operacji! Wybierz prawidłową operacje:");
                            break;
                    }
                } while (operation != ConsoleKey.D1 && operation != ConsoleKey.D2 && operation != ConsoleKey.D3 && operation != ConsoleKey.D4 && operation != ConsoleKey.D5);
                TextColor(ConsoleColor.White, "\nNaciśnij dowolny klawisz, aby kontynuować...");
                Console.ReadKey();
                Console.Clear();
            }

        }

        static public void TextColor(ConsoleColor color, string text)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        static public void AddTask(List<TaskItem> list)
        {
            Console.WriteLine("\n\nOpisz zadanie:");
            string task = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(task))
            {
                list.Add(new TaskItem(task));
                TextColor(ConsoleColor.Green, "Dodano zadanie!");
            }
            else
            {
                TextColor(ConsoleColor.Red, "Nie można dodać pustego zadania!");
            }
        }

        static void ShowTasks(List<TaskItem> list)
        {
            if (list.Count == 0)
            {
                TextColor(ConsoleColor.Cyan, "\nLista zadań jest pusta.");
                return;
            }

            Console.WriteLine("\nTwoje zadania: ");
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {list[i]}");
            }
        }
        static void RemoveTask(List<TaskItem> list)
        {
            if (list.Count == 0)
            {
                TextColor(ConsoleColor.Red, "\nLista zadań jest pusta! Nie ma czego usuwać.");
                return;
            }

            ShowTasks(list);
            Console.WriteLine("\nWybierz numer zadania do usunięcia: ");

            int number;
            bool valid = int.TryParse(Console.ReadLine(), out number);

            if (number >= 1 && number <= list.Count)
            {
                list.RemoveAt(number - 1);
                TextColor(ConsoleColor.Green, "Zadanie zostało usunięte.");
            }
            else
            {
                TextColor(ConsoleColor.Red, "Nie ma takiego zadania.");
            }
        }

        static void CompleteTask(List<TaskItem> list)
        {
            if (list.Count == 0)
            {
                TextColor(ConsoleColor.Red, "\nLista zadań jest pusta! Nie ma czego oznaczać.");
                return;
            }

            ShowTasks(list);
            Console.WriteLine("\nWybierz numer zadania do oznaczenia jako ukończone: ");

            int number;
            bool valid = int.TryParse(Console.ReadLine(), out number);

            if (valid && number >= 1 && number <= list.Count)
            {
                list[number - 1].IsCompleted = true;
                TextColor(ConsoleColor.Green, "Zadanie zostało oznaczone jako ukończone!");
            }
            else
            {
                TextColor(ConsoleColor.Red, "Nie ma takiego zadania.");
            }
        }
    }
    class TaskItem
    {
        string Name;
        public bool IsCompleted { get; set; }

        public TaskItem(string name)
        {
            Name = name;
            IsCompleted = false;
        }

        public override string ToString()
        {
            return $"{(IsCompleted ? "[X]" : "[ ]")} {Name}";
        }
    }
}