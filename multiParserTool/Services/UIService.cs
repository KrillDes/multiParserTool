namespace multiParserTool.Services
{
    public class UIService
    {
        private string mainTitle = " Powered by KrillDes ";
        public async Task<object> ShowMenu(string title, string[] menu, string[,] data)
        {
            DrawMenu(title, menu, data);
            var result = Console.ReadLine();

            switch (result)
            {
                case "u":
                    Console.Clear();
                    break;
                case "q":
                    Environment.Exit(0);
                    break;
            }

            return null;
        }

        private void DrawMenu(string title, string[] menu, string[,] data)
        {
            Console.WriteLine("".PadLeft(Console.WindowWidth / 2 - mainTitle.Length / 2, '-') + mainTitle + "".PadRight(Console.WindowWidth / 2 - mainTitle.Length / 2 - 1, '-'));
            Console.WriteLine("".PadLeft(Console.WindowWidth / 2 - title.Length / 2, '-') + title + "".PadRight(Console.WindowWidth / 2 - title.Length / 2, '-'));
            Console.WriteLine("\n\n");

            var maxLength = menu.Max(x => x.Length);

            Console.WriteLine("".PadRight(maxLength + 4, '_'));

            foreach (string item in menu)
            {
                Console.WriteLine("".PadLeft(1, '|') + "".PadLeft(1, ' ') + item + "".PadRight(maxLength - item.Length + 1, ' ') + "".PadRight(1, '|'));
            }

            //TODO:Сделать надчёркивание
            //Console.WriteLine("".PadLeft(1, '|') + "".PadLeft(1, ' ') + item + "".PadRight(maxLength - item.Length + 1, ' ') + "".PadRight(1, '|'));
        }
    }
}
