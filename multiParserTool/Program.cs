// See https://aka.ms/new-console-template for more information
using multiParserTool.Services;

UIService uIService = new UIService();

string[] menu = { "0 - One", "1 - TwoMAXXX"};

while (true)
{
    if (await Task.Run(() => uIService.ShowMenu("Menu", menu, null).ToString() == "q"))
        return;
}

