using System.Globalization;
using System.Text;
using Microsoft.Data.SqlClient;

namespace b1.Service.Generator;

public class DatabaseService
{
    public static void ToDatabase()
    {
        var connection = new SqlConnection("Server=DESKTOP-1OH4LVK\\SQLEXPRESS;Database=b1task1;Trusted_Connection=True;TrustServerCertificate=true");
        connection.Open();
        var command = new SqlCommand("delete from RandomStrings", connection);
        command.ExecuteNonQuery();
        var counter = 0;
        var linesCounter = 0;
        Parallel.For(0, 100, i =>
        {
            var strings = File.ReadAllLines($"{i}.txt", Encoding.UTF8);
            linesCounter += strings.Length;
            strings.AsParallel().ForAll(s =>
            {
                var split = s.Split("||");
                var commandStr = $"INSERT INTO RandomStrings VALUES('{split[0]}','{split[1]}','{split[2]}'," +
                                 $"{split[3]},{double.Parse(split[4]).ToString(CultureInfo.InvariantCulture)})";
                var command = new SqlCommand(commandStr, connection);
                lock (connection)
                {
                    command.ExecuteNonQuery();
                    if (++counter % 10000 == 0)
                    {
                        Console.WriteLine($"""
                        count of added lines {counter}
                        count of readed lines {linesCounter}
                        left to add {10000000 - counter}
                        {'=':100}
                        """);
                    }
                }
            });
        });
    }

    public static void CalculateValues()
    {
        var connection = new SqlConnection("Server=DESKTOP-1OH4LVK\\SQLEXPRESS;Database=b1task1;Trusted_Connection=True;TrustServerCertificate=true");
        connection.Open();
        {
            var command = new SqlCommand("exec CalculateSum", connection);

            using var reader = command.ExecuteReader();
            reader.Read();
            Console.WriteLine($"sum of nums : {reader.GetInt64(0)}");
        }
        {
            var command = new SqlCommand("exec CalculateMedian", connection);

            using var reader = command.ExecuteReader();
            reader.Read();
            Console.WriteLine($"median : {reader.GetDouble(0):F8}");
        }
    }
}