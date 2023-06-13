using System.Text;

namespace multiParserTool.Services
{
    public class ParserService
    {
        private int startIndex { get; set; }
        private string customChar { get; set; }
        private string path { get; set; }
        private int parsePosition { get; set; }
        private string insertText { get; set; }

        private string[][] data
        {
            get
            {
                return new[] { new[] { "С какой строчки начать:", startIndex.ToString() },
                new []{ "Символ:", customChar}, new []{ "Путь до файла:", path }, new[]{"Парсинг до символа или после 0/1:", parsePosition.ToString() },
                 new[]{"Вставляемый текст:", insertText}};
            }
            set
            {
                startIndex = Convert.ToInt32(value[0][1]);
                customChar = value[1][1];
                path = value[2][1];
                parsePosition = Convert.ToInt32(value[3][1]);
                insertText = value[4][1];
            }
        }

        public async Task<int> Menu(UIService uIService)
        {
            try
            {
                string[] menu = { "Настройки парсера", "Предпросмотр результата", "Вырезать текст", "Вставить текст", "В меню" };

                while (true)
                    switch (await Task.Run(() => uIService.ShowMenu("Parser", menu, null, null)))
                    {
                        case 0:
                            data = (string[][])await Task.Run(() => uIService.ShowData("Settings", null, null, data));
                            break;
                        case 2:
                            CutFile();
                            break;
                        case 3:
                            AddToFile();
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

        private void CutFile()
        {
            Console.Clear();

            Console.WriteLine("Подождите...");

            string readLine;
            int progressBar = 0;

            using (StreamReader streamReader = new StreamReader(path, Encoding.UTF8, true, 1024))
            using (StreamWriter streamWriter = new StreamWriter(Path.Combine(Path.GetDirectoryName(path), "cut-" + Path.GetFileName(path))))
                while ((readLine = streamReader.ReadLine()) != null)
                    if (progressBar >= startIndex)
                    {
                        if (parsePosition == 0)
                            streamWriter.WriteLine(readLine.Substring(readLine.IndexOf(customChar) + 1));
                        else
                            streamWriter.WriteLine(readLine.Substring(0, readLine.IndexOf(customChar)));
                    }
                    else
                    {
                        progressBar++;
                        streamWriter.WriteLine(readLine);
                    }
        }

        private void AddToFile()
        {
            Console.Clear();

            string readLine;
            int progressBar = 0;

            Console.WriteLine("Подождите...");

            using (StreamReader streamReader = new StreamReader(path, Encoding.UTF8, true, 1024))
            using (StreamWriter streamWriter = new StreamWriter(Path.Combine(Path.GetDirectoryName(path), "add-" + Path.GetFileName(path))))
                while ((readLine = streamReader.ReadLine()) != null)
                    if (progressBar >= startIndex)
                        switch (parsePosition)
                        {
                            case 0:
                                if (!string.IsNullOrEmpty(customChar))
                                    streamWriter.WriteLine(readLine.Insert(readLine.IndexOf(customChar), insertText));
                                else
                                    streamWriter.WriteLine(readLine.Insert(0, insertText));
                                break;
                            case 1:
                                if (!string.IsNullOrEmpty(customChar))
                                    streamWriter.WriteLine(readLine.Insert(readLine.IndexOf(customChar) + 1, insertText));
                                else
                                    streamWriter.WriteLine(readLine.Insert(readLine.Length, insertText));
                                break;
                        }
                    else
                    {
                        progressBar++;
                        streamWriter.WriteLine(readLine);
                    }
        }

        private string StreamFile(string mode)
        {
            string readLine;
            int progressBar = 0;

            using (StreamReader streamReader = new StreamReader(File.OpenRead(path), Encoding.UTF8, true, 1024))
            using (StreamWriter streamWriter = new StreamWriter(Path.Combine(Path.GetDirectoryName(path), "cutResult-" + Path.GetFileName(path))))
                while ((readLine = streamReader.ReadLine()) != null)
                {
                    if (progressBar >= startIndex)
                        continue;
                    else
                    {
                        progressBar++;
                        streamWriter.WriteLine(readLine);
                    }
                }
            return string.Empty;
        }

        private void WriteFile(string pathSourceFile, string resultFileName, string[] allLines)
        {

        }
    }
}
