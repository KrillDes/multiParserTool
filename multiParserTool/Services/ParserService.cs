using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace multiParserTool.Services
{
    public class ParserService
    {
        private int startIndex { get; set; }
        private string customChar { get; set; }
        private string path { get; set; }
        private int parsePosition { get; set; }
        private string insertText { get; set; }

        private string[][] data { get; set; }

        public ParserService()
        {
            data = new[] { new []{ "С какой строчки начать:", startIndex.ToString()},
            new []{ "Символ:", customChar}, new []{ "Путь до файла:", path }, new[]{"Парсинг до символа или после 0/1:", parsePosition.ToString() },
            new[]{"Вставляемый текст:", insertText} };
        }

        public async Task<int> Menu(UIService uIService)
        {
            try
            {
                string[] menu = { "Настройки парсера", "Вырезать текст", "Вставить текст", "В меню" };

                while (true)
                    switch (await Task.Run(() => uIService.ShowMenu("Parser", menu, null, null)))
                    {
                        case 0:
                            data = (string[][])await Task.Run(() => uIService.ShowData("Settings", null, null, data));
                            break;
                        case 1:
                            CutFile(Convert.ToInt32(data[0][1]), Convert.ToChar(data[1][1]), data[2][1], Convert.ToInt32(data[3][1]));
                            break;
                        case 2:
                            AddToFile(Convert.ToInt32(data[0][1]), data[1][1], data[2][1], Convert.ToInt32(data[3][1]), data[4][1]);
                            break;
                        default:
                            return 0;
                    }
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                return 0;
            }
        }

        private void CutFile(int startIndex, char customChar, string path, int mode)
        {
            Console.Clear();

            FileInfo fileInfo = new FileInfo(path);
            Console.WriteLine("Идёт чтение файла...");
            string[] fileLines = File.ReadAllLines(path);
            Console.WriteLine("Файл был прочтён!");
            Console.WriteLine("Начинаю удаление ненужных данных...");

            if (mode == 0)
                for (int i = startIndex; i < fileLines.Length; i++)
                    fileLines[i] = fileLines[i].Substring(0, fileLines[i].IndexOf(customChar));
            else
                for (int i = startIndex; i < fileLines.Length; i++)
                    fileLines[i] = fileLines[i].Substring(fileLines[i].IndexOf(customChar) + 1);

            Console.WriteLine("Лишние данные отсечены!");
            Console.WriteLine("Создаю копию файла...");

            File.WriteAllLines(fileInfo.DirectoryName + "cutResult-" + fileInfo.Name, fileLines);
        }

        private static void AddToFile(int startIndex, string customChar, string path, int mode, string text)
        {
            Console.Clear();

            FileInfo fileInfo = new FileInfo(path);
            Console.WriteLine("Идёт чтение файла...");
            string[] fileLines = File.ReadAllLines(path);
            Console.WriteLine("Файл был прочтён!");
            Console.WriteLine("Начинаю добавление данных...");

            switch (mode)
            {
                case 0:
                    for (int i = startIndex; i < fileLines.Length; i++)
                        if (!string.IsNullOrEmpty(customChar))
                            fileLines[i] = fileLines[i].Insert(fileLines[i].IndexOf(customChar), text);
                        else
                            fileLines[i] = fileLines[i].Insert(0, text);
                    break;
                case 1:
                    for (int i = startIndex; i < fileLines.Length; i++)
                        if (!string.IsNullOrEmpty(customChar))
                            fileLines[i] = fileLines[i].Insert(fileLines[i].IndexOf(customChar) + 1, text);
                        else
                            fileLines[i] = fileLines[i].Insert(fileLines[i].Length, text);
                    break;
            }

            Console.WriteLine("Данные были добавлены!");
            Console.WriteLine("Создаю копию файла...");

            var t = Path.GetDirectoryName(Path.GetFullPath(path));

            File.WriteAllLines(Path.GetDirectoryName(Path.GetFullPath(path)) + "addResult-" + Path.GetFileName(path), fileLines);
        }
    }
}
