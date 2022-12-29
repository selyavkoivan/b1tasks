using System.Globalization;
using System.Text;
using b1.Constants;
using b1.Service.Generator;
using Microsoft.Data.SqlClient;

namespace b1.Service;

public class FileService
{
    public delegate bool Contains(string str, string pattern, ref int counter);

    public static void GenerateFiles()
    {
        Console.WriteLine("... creating files");
        // параллельно создаются и заполняются файлы
        Parallel.For(0, FileConstants.CountOfFiles, i =>
        {
            using var fileStream = new FileStream($"{FileConstants.FileDirectory}\\{i}.txt", FileMode.Create);
            using var writer = new StreamWriter(fileStream);
            for(var j = 0; j <FileConstants.CountOfLines; j++)
            {
                // записывается в файл сгенерированная строка
                writer.WriteLine(StringGenerator.GetRandomString());
            }
        });
        Console.WriteLine("All files have been generated");
    }
    
    // в зависимости от передаваемых параметров перенесет либо все строки либо строки, соответствующие условию
    public static void ToOneFile(Contains checkString, string pattern = "", bool printCountOfDropLines = false)
    {
        // созлдаем StreamWriter для записи в файл
        using var fileStream = new FileStream($"{FileConstants.FileDirectory}\\toOne.txt", FileMode.Create);
        using var writer = new StreamWriter(fileStream, Encoding.UTF8);
        var countOfDropLines = 0;
        Parallel.For(0, FileConstants.CountOfFiles, i =>
            {
                // созлдаем StreamReader для чтения файла
                using var stream = new FileStream($"{FileConstants.FileDirectory}\\{i}.txt", FileMode.Open);
                using var reader = new StreamReader(stream);
                while (!reader.EndOfStream)
                {
                    // читаем строку
                    var line = reader.ReadLine();
                    // проверяем на соответствие условию
                    if (checkString(line!, pattern, ref countOfDropLines))
                    {
                        lock (writer)
                        {
                            // записываем в файл
                            writer.WriteLine(line);
                        }
                    }
                }
            }
        );
        if (printCountOfDropLines)
        {
            // выводим информацию о количестве удаленных строк
            Console.WriteLine($"count of drop lines : {countOfDropLines}");
        }
    }
}