namespace multiParserTool.Services
{
    public class UIService
    {
        private const string mainTitle = " Powered by KrillDes ";
        public async Task<object> ShowMenu(string title, string[] menu, string[,] options, string[,] data)
        {
            while (true)
            {
                char result = Convert.ToChar(DrawMenu(title, menu));

                if (char.IsNumber(result))
                    return Int32.Parse(result.ToString());
                else
                    switch (result)
                    {
                        case 'u':
                            Console.Clear();
                            break;
                        case 'q':
                            Console.Clear();
                            Console.WriteLine("\nВыйти из приложения? (y/n)");
                            if (Console.ReadKey().KeyChar == 'y')
                                Environment.Exit(0);
                            break;
                    }

                Console.Clear();
            }
        }

        public async Task<object> ShowData(string title, string[] menu, string[,] options, string[][] data)
        {
            while (true)
            {
                char result = Convert.ToChar(DrawData(title, data));

                if (char.IsNumber(result))
                    return Int32.Parse(result.ToString());
                else
                    switch (result)
                    {
                        case 'c':
                            return 0;
                        case 'q':
                            Console.Clear();
                            Console.WriteLine("\nВыйти из приложения? (y/n)");
                            if (Console.ReadKey().KeyChar == 'y')
                                Environment.Exit(0);
                            break;
                    }

                Console.Clear();
            }
        }

        private string DrawMenu(string title, string[] menu)
        {
            ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();
            int cursorMinimalPosition = 2;
            int cursorMaxPosition = cursorMinimalPosition + menu.Length - 1;
            int cursorNowPosition = cursorMinimalPosition;
            int maxLength = menu.Max(x => x.Length);
            const int menuLength = 4;

            do
            {
                int cursorPosition = Console.WindowWidth / 2 - maxLength / 2 - 3;

                Console.Clear();

                if (cursorPosition <= 0)
                {
                    Console.Write("Разверните консоль для корректного отображения");
                    Console.ReadKey();
                    break;
                }

                Console.WriteLine("".PadLeft(Console.WindowWidth / 2 - mainTitle.Length / 2 - 2, Convert.ToChar("\u2500")) + Convert.ToChar("\u2524") +
                        mainTitle + Convert.ToChar("\u251C") + "".PadRight(Console.WindowWidth / 2 - mainTitle.Length / 2 - 1, Convert.ToChar("\u2500")));

                Console.CursorLeft = cursorPosition;
                Console.CursorVisible = false;

                if (title.Length > 0)
                    Console.WriteLine(Convert.ToChar("\u250C") + "".PadLeft(maxLength / 2 - title.Length / 2, Convert.ToChar("\u2500")) + Convert.ToChar("\u2524") +
                        title + Convert.ToChar("\u251C") + "".PadRight(maxLength / 2 - title.Length / 2, Convert.ToChar("\u2500")) + Convert.ToChar("\u2510"));
                else
                    Console.WriteLine(Convert.ToChar("\u250C") + "".PadRight(maxLength + menuLength / 2, Convert.ToChar("\u2500")) + Convert.ToChar("\u2510"));

                foreach (string item in menu)
                {
                    Console.CursorLeft = cursorPosition;
                    if (cursorNowPosition == Console.GetCursorPosition().Top)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                    }
                    Console.WriteLine("".PadLeft(1, Convert.ToChar("\u2502")) + "".PadLeft(1, ' ') + item + "".PadRight(maxLength - item.Length + 1, ' ') + "".PadRight(1, Convert.ToChar("\u2502")));
                    Console.ResetColor();
                }

                Console.CursorLeft = cursorPosition;
                Console.WriteLine(Convert.ToChar("\u2514") + "".PadRight(maxLength + menuLength / 2, Convert.ToChar("\u2500")) + Convert.ToChar("\u2518"));

                Console.CursorTop = Console.WindowHeight - 3;
                Console.WriteLine("".PadRight(Console.WindowWidth, Convert.ToChar("\u2500")));
                Console.CursorTop = Console.WindowHeight - 2;
                Console.WriteLine("q - выход");

                Console.SetCursorPosition(0, cursorNowPosition);

                keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.UpArrow)
                    if (cursorNowPosition > cursorMinimalPosition)
                        cursorNowPosition--;

                if (keyInfo.Key == ConsoleKey.DownArrow)
                    if (cursorNowPosition < cursorMaxPosition)
                    {
                        cursorNowPosition++;
                    }

                if (keyInfo.Key == ConsoleKey.Enter)
                    return (cursorNowPosition - cursorMinimalPosition).ToString();
            }
            while (keyInfo.KeyChar != 'q');
            return "q";
        }

        private string DrawData(string title, string[][] data)
        {
            //ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();
            //do
            //{
            //    Console.Clear();
            //    Console.WriteLine("Тест");

            //    Console.CursorTop = Console.WindowHeight - 3;
            //    Console.WriteLine("".PadRight(Console.WindowWidth, Convert.ToChar("\u2500")));
            //    Console.CursorTop = Console.WindowHeight - 2;
            //    Console.WriteLine("c - назад");

            //    keyInfo = Console.ReadKey();
            //}

            ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();
            int cursorMinimalPosition = 2;
            int cursorMaxPosition = cursorMinimalPosition + data.Length - 1;
            int cursorNowPosition = cursorMinimalPosition;
            int maxLength = data.Max(x => x.Max(x => x.Length));
            const int menuLength = 4;
            do
            {
                int cursorPosition = Console.WindowWidth / 2 - maxLength / 2 - 3;

                Console.Clear();

                if (cursorPosition <= 0)
                {
                    Console.Write("Разверните консоль для корректного отображения");
                    Console.ReadKey();
                    break;
                }

                Console.WriteLine("".PadLeft(Console.WindowWidth / 2 - mainTitle.Length / 2 - 2, Convert.ToChar("\u2500")) + Convert.ToChar("\u2524") +
                        mainTitle + Convert.ToChar("\u251C") + "".PadRight(Console.WindowWidth / 2 - mainTitle.Length / 2 - 1, Convert.ToChar("\u2500")));

                Console.CursorLeft = cursorPosition;
                Console.CursorVisible = false;

                if (title.Length > 0)
                    Console.WriteLine(Convert.ToChar("\u250C") + "".PadLeft(maxLength / 2 - title.Length / 2, Convert.ToChar("\u2500")) + Convert.ToChar("\u2524") +
                        title + Convert.ToChar("\u251C") + "".PadRight(maxLength / 2 - title.Length / 2, Convert.ToChar("\u2500")) + Convert.ToChar("\u2510"));
                else
                    Console.WriteLine(Convert.ToChar("\u250C") + "".PadRight(maxLength + menuLength / 2, Convert.ToChar("\u2500")) + Convert.ToChar("\u2510"));

                foreach (string[] item in data)
                {
                    //TODO: Сделать грамотный вывод инфы
                    foreach (string subItem in item)
                    {
                        Console.CursorLeft = cursorPosition;
                        if (cursorNowPosition == Console.GetCursorPosition().Top)
                        {
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.BackgroundColor = ConsoleColor.White;
                        }
                        Console.WriteLine(item);// "".PadLeft(1, Convert.ToChar("\u2502")) + "".PadLeft(1, ' ') + item + "".PadRight(maxLength - item.Length + 1, ' ') + "".PadRight(1, Convert.ToChar("\u2502")));
                        Console.ResetColor();
                    }
                }

                Console.CursorLeft = cursorPosition;
                Console.WriteLine(Convert.ToChar("\u2514") + "".PadRight(maxLength + menuLength / 2, Convert.ToChar("\u2500")) + Convert.ToChar("\u2518"));

                Console.CursorTop = Console.WindowHeight - 3;
                Console.WriteLine("".PadRight(Console.WindowWidth, Convert.ToChar("\u2500")));
                Console.CursorTop = Console.WindowHeight - 2;
                Console.WriteLine("c - назад");

                Console.SetCursorPosition(0, cursorNowPosition);

                keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.UpArrow)
                    if (cursorNowPosition > cursorMinimalPosition)
                        cursorNowPosition--;

                if (keyInfo.Key == ConsoleKey.DownArrow)
                    if (cursorNowPosition < cursorMaxPosition)
                    {
                        cursorNowPosition++;
                    }

                if (keyInfo.Key == ConsoleKey.Enter)
                    return (cursorNowPosition - cursorMinimalPosition).ToString();
            } while (keyInfo.KeyChar != 'c');
            return "c";
        }

        private string EdithData(string[,] data)
        {
            return string.Empty;
        }
    }
}
