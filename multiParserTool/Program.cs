// See https://aka.ms/new-console-template for more information
using multiParserTool.Services;

AppDomain.CurrentDomain.ProcessExit += new EventHandler(Exit);

UIService uIService = new UIService();
ParserService parserService = new ParserService();

Console.OutputEncoding = System.Text.Encoding.UTF8;

string[] menu = { "Тестовое отображение заранее подготовленного текста ", "Парсер", "Инфа" };
string[,] options = { { "q", "выход" } };

Console.CancelKeyPress += delegate
{
    uIService.Clear();
    Console.CursorVisible = true;
};

while (true)
    switch (await Task.Run(() => uIService.ShowMenu("Menu", menu, options, null)))
    {
        case 0:
            Console.WriteLine("ЭХЭХЭХЭ)))");
            Console.ReadKey();
            break;
        case 1:
            await parserService.Menu(uIService);
            break;
        case 2:
            await Task.Run(() => uIService.ShowMenu("Info", new string[] { "Telegram - https://t.me/KrillDes", "В меню" }, options, null));
            break;
    }


void Exit(object sender, EventArgs e)
{
    Console.CursorVisible = true;
}
