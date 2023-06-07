// See https://aka.ms/new-console-template for more information
using multiParserTool.Services;

UIService uIService = new UIService();

Console.OutputEncoding = System.Text.Encoding.UTF8;

string[] menu = { "Тестовое отображение заранее подготовленного текста ", "Парсер", "А теперь третья строчка, которая что-то сделает","asd", "asdasd",
"asddd", "ddd", "assss"};
string[,] options = { { "q", "выход" } };

while (true)
    switch (await Task.Run(() => uIService.ShowMenu("Menu", menu, options, null)))
    {
        case 0:
            Console.WriteLine("ЭХЭХЭХЭ)))");
            Console.ReadKey();
            break;
        case 1:
            await new ParserService().Menu(uIService);
            break;
    }
