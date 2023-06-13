namespace multiParserTool.Services
{
    public class UIService
    {
        private const string mainTitle = " Powered by KrillDes ";

        public void Clear()
        {
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < Console.WindowHeight; i++)
            {
                Console.SetOut(Console.Out);
                Console.Write(new string(' ', Console.WindowWidth));
            }
            Console.SetCursorPosition(0, 0);
        }

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
                        case 'c':
                            return null;
                        case 'u':
                            Console.Clear();
                            break;
                        case 'q':
                            Clear();
                            Console.WriteLine("\nВыйти из приложения? (y/n)");
                            if (Console.ReadKey(true).KeyChar == 'y')
                                Environment.Exit(0);
                            break;
                    }
            }
        }

        public async Task<object> ShowData(string title, string[] menu, string[,] options, string[][] data)
        {
            while (true)
            {
                object result = DrawData(title, data);

                if (result != null)
                    return result;
                else
                    switch (result)
                    {
                        case 'c':
                            return 0;
                        case 'q':
                            Clear();
                            Console.WriteLine("\nВыйти из приложения? (y/n)");
                            if (Console.ReadKey(true).KeyChar == 'y')
                                Environment.Exit(0);
                            break;
                    }
            }
        }

        private string DrawMenu(string title, string[] menu)
        {
            ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();
            int cursorMinimalPosition = 2;
            int cursorMaxPosition = cursorMinimalPosition + menu.Length - 1;
            int cursorNowPosition = cursorMinimalPosition;
            const int menuLength = 4;
            int maxLength = menu.Max(x => x.Length);

            if (maxLength % 2 != 0)
                maxLength++;

            do
            {
                Clear();

                int cursorPosition = Console.WindowWidth / 2 - maxLength / 2 - 3;

                if (cursorPosition <= 0)
                {
                    Console.Write("Разверните консоль для корректного отображения");
                    Console.ReadKey();
                    break;
                }

                Console.WriteLine("".PadLeft(Console.WindowWidth / 2 - mainTitle.Length / 2 - 2, Convert.ToChar("\u2500")) + Convert.ToChar("\u2524") +
                        mainTitle + Convert.ToChar("\u251C") + "".PadRight(Console.WindowWidth / 2 - mainTitle.Length / 2 - 2, Convert.ToChar("\u2500")));

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
                Console.WriteLine("q - выход" + Convert.ToChar("\u0009") + "esc - назад");

                Console.SetCursorPosition(0, cursorNowPosition);

                keyInfo = Console.ReadKey(true);

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

                if (keyInfo.Key == ConsoleKey.Escape)
                    return "c";
            }
            while (keyInfo.KeyChar != 'q');
            return "q";
        }

        private string[][] DrawData(string title, string[][] data)
        {
            ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();
            int cursorMinimalPosition = 2;
            int cursorMaxPosition = cursorMinimalPosition + data.Length - 1;
            int cursorNowPosition = cursorMinimalPosition;
            const int menuLength = 4;

            do
            {
                Clear();

                int maxLength = data.Max(x => string.Join("", x).Length);
                int cursorPosition = Console.WindowWidth / 2 - maxLength / 2 - 3;

                if (maxLength % 2 != 0)
                    maxLength++;

                if (cursorPosition <= 0)
                {
                    Console.Write("Разверните консоль для корректного отображения");
                    Console.ReadKey();
                    break;
                }

                Console.WriteLine("".PadLeft(Console.WindowWidth / 2 - mainTitle.Length / 2 - 2, Convert.ToChar("\u2500")) + Convert.ToChar("\u2524") +
                        mainTitle + Convert.ToChar("\u251C") + "".PadRight(Console.WindowWidth / 2 - mainTitle.Length / 2 - 2, Convert.ToChar("\u2500")));

                Console.CursorLeft = cursorPosition;
                Console.CursorVisible = false;

                if (title.Length > 0)
                    Console.WriteLine(Convert.ToChar("\u250C") + "".PadLeft(maxLength / 2 - title.Length / 2, Convert.ToChar("\u2500")) + Convert.ToChar("\u2524") +
                        title + Convert.ToChar("\u251C") + "".PadRight(maxLength / 2 - title.Length / 2, Convert.ToChar("\u2500")) + Convert.ToChar("\u2510"));
                else
                    Console.WriteLine(Convert.ToChar("\u250C") + "".PadRight(maxLength + menuLength / 2, Convert.ToChar("\u2500")) + Convert.ToChar("\u2510"));

                foreach (string[] item in data)
                {
                    string setting = string.Empty;

                    Console.CursorLeft = cursorPosition;
                    if (cursorNowPosition == Console.GetCursorPosition().Top)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                    }

                    foreach (string subItem in item)
                        setting += subItem;

                    Console.WriteLine("".PadLeft(1, Convert.ToChar("\u2502")) + "".PadLeft(1, ' ') + setting + "".PadRight(maxLength - setting.Length + 1, ' ') + "".PadRight(1, Convert.ToChar("\u2502")));
                    Console.ResetColor();
                }

                Console.CursorLeft = cursorPosition;
                Console.WriteLine(Convert.ToChar("\u2514") + "".PadRight(maxLength + menuLength / 2, Convert.ToChar("\u2500")) + Convert.ToChar("\u2518"));

                Console.CursorTop = Console.WindowHeight - 3;
                Console.WriteLine("".PadRight(Console.WindowWidth, Convert.ToChar("\u2500")));
                Console.CursorTop = Console.WindowHeight - 2;
                Console.WriteLine("esc - назад" + Convert.ToChar("\u0009") + "bacspace - удалить");

                Console.SetCursorPosition(0, cursorNowPosition);

                keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.UpArrow)
                    if (cursorNowPosition > cursorMinimalPosition)
                        cursorNowPosition--;

                if (keyInfo.Key == ConsoleKey.DownArrow)
                    if (cursorNowPosition < cursorMaxPosition)
                        cursorNowPosition++;

                if (keyInfo.Key == ConsoleKey.Enter)
                    EdithData(Console.CursorTop += cursorMaxPosition, data[cursorNowPosition - cursorMinimalPosition]);

                if (keyInfo.Key == ConsoleKey.Backspace)
                    data[cursorNowPosition - cursorMinimalPosition][1] = string.Empty;

            } while (keyInfo.Key != ConsoleKey.Escape);
            return data;
        }

        private void EdithData(int cursorTop, string[] data)
        {
            Console.CursorVisible = true;
            Console.CursorTop = cursorTop;
            Console.WriteLine(string.Join("", data));
            Console.Write(data[0]);
            var userData = Console.ReadLine();
            Console.CursorVisible = false;

            if (!string.IsNullOrEmpty(userData))
                data[1] = userData;
        }
    }
}
