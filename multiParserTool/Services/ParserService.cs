using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace multiParserTool.Services
{
    internal class ParserService
    {
        private void tets()
        {
            int selectMenu = 0;
            Console.WriteLine("Меню:\n0 - Удалить всё после спец символа" +
                    "\n1 - Добавить до/после спец символа");
            selectMenu = Convert.ToInt32(Console.ReadLine());
            switch (selectMenu)
            {
                case 0:
                    CutFile();
                    break;
                case 1:
                    AddToFile();
                    break;
            }


            Console.WriteLine("Приложение закончило свою работу!");
            Console.ReadKey();
        }

        private static void CutFile()
        {
            Console.WriteLine("С какой строчки начать?");
            int startIndex = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("До какого символа оставить текст?");
            char customChar = Convert.ToChar(Console.ReadLine());
            Console.WriteLine("Путь до файла. Пример: C:\\Test\\inset.txt");
            string path = Console.ReadLine();

            FileInfo fileInfo = new FileInfo(path);
            Console.WriteLine("Идёт чтение файла...");
            string[] fileLines = File.ReadAllLines(path);
            Console.WriteLine("Файл был прочтён!");
            Console.WriteLine("Начинаю удаление ненужных данных...");

            for (int i = startIndex; i < fileLines.Length; i++)
                fileLines[i] = fileLines[i].Split(customChar)[0];

            Console.WriteLine("Лишние данные отсечены!");
            Console.WriteLine("Создаю копию файла...");

            File.WriteAllLines(fileInfo.Directory + "\\" + "cutResult-" + fileInfo.Name, fileLines);
        }

        private static void AddToFile()
        {
            Console.WriteLine("С какой строчки начать?");
            int startIndex = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Текст, который нужно вставить:");
            string text = Console.ReadLine();

            Console.WriteLine("Вставить текст до спец. символа? (0 - до символа, 1 - после символа)");
            int mode = Convert.ToInt32(Console.ReadLine());

            string customChar = string.Empty;
            string customInsertChar = string.Empty;

            switch (mode)
            {
                case 0:
                    Console.WriteLine("До какого символа вставить текст (символ сохраняется)? Если символа нет, текст будет вставляться с начала строки.");
                    customChar = Console.ReadLine();
                    Console.WriteLine("Какой символ вставить после вставляемого текста?");
                    customInsertChar = Console.ReadLine();
                    break;
                case 1:
                    Console.WriteLine("После какого символа вставить текст (символ сохраняется)? Если символа нет, текст будет вставляться в конец строки.");
                    customChar = Console.ReadLine();
                    Console.WriteLine("Какой символ вставить до вставляемого текста?");
                    customInsertChar = Console.ReadLine();
                    break;
            }

            Console.WriteLine("Путь до файла. Пример: C:\\Test\\inset.txt");
            string path = Console.ReadLine();

            FileInfo fileInfo = new FileInfo(path);
            Console.WriteLine("Идёт чтение файла...");
            string[] fileLines = File.ReadAllLines(path);
            Console.WriteLine("Файл был прочтён!");
            Console.WriteLine("Начинаю добавление данных...");

            switch (mode)
            {
                case 0:
                    if (customInsertChar != "")
                        text += customInsertChar;

                    for (int i = startIndex; i < fileLines.Length; i++)
                        if (customChar != "")
                            fileLines[i] = fileLines[i].Insert(fileLines[i].IndexOf(customChar) - 1, text);
                        else
                            fileLines[i] = fileLines[i].Insert(0, text);
                    break;
                case 1:
                    if (customInsertChar != "")
                        text = customInsertChar + text;

                    for (int i = startIndex; i < fileLines.Length; i++)
                        if (customChar != "")
                            fileLines[i] = fileLines[i].Insert(fileLines[i].IndexOf(customChar), text);
                        else
                            fileLines[i] = fileLines[i].Insert(fileLines[i].Length, text);
                    break;
            }

            Console.WriteLine("Данные были добавлены!");
            Console.WriteLine("Создаю копию файла...");

            File.WriteAllLines(fileInfo.Directory + "\\" + "addResult-" + fileInfo.Name, fileLines);
        }
    }
}
