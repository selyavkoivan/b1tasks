using System.Globalization;
using System.Text;
using b1.Context;
using b1.Model;
using b1.Service.Generator;
using Microsoft.Data.SqlClient;

namespace b1.Service;

public class FileService
{
    public delegate bool Comparer(string str, string pattern, ref int counter);


    public static void GenerateFiles()
    {
        Parallel.For(0, 100, i =>
        {
            using var fileStream = new FileStream($"{i}.txt", FileMode.Create);
            using var writer = new StreamWriter(fileStream);
            for(var j = 0; j <100000; j++)
            {
                writer.WriteLine(StringGenerator.GetRandomString());
            }
        });
    }
    
    public static void ToOneFile(Comparer checkString, string pattern = "", bool printCountOfDropLines = false)
    {
        using var fileStream = new FileStream($"toOne.txt", FileMode.Create);
        using var writer = new StreamWriter(fileStream, Encoding.UTF8);
        var countOfDropLines = 0;
        Parallel.For(0, 100, i =>
        {
            var strings = File.ReadAllLines($"{i}.txt", Encoding.UTF8);
            strings.AsParallel().Where(s => checkString(s, pattern, ref countOfDropLines)).ForAll(s =>
            {
                lock (writer)
                {
                    writer.WriteLine(s);
                }
            });
            
        });
        if (printCountOfDropLines)
        {
            Console.WriteLine($"count of drop lines : {countOfDropLines}");
        }
    }
}