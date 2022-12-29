using System.Globalization;
using b1.Constants;
using Microsoft.Data.SqlClient;

namespace b1.Service;

public class DatabaseService
{
    public static void ToDatabase()
    {
        var counter = 0;
        // параллельно выполняем перенос каждого файла в бд
        Parallel.For(0, FileConstants.CountOfFiles, i =>
        {
            // открываем подклоючение к бд
            using var connection = new SqlConnection(DatabaseConstants.ConnString);
            connection.Open();

            // создаем StreamReader для чтения файла
            using var fileStream = new FileStream($"{FileConstants.FileDirectory}\\{i}.txt", FileMode.Open);
            using var reader = new StreamReader(fileStream);

            // пока не конец файла
            while (!reader.EndOfStream)
            {
                // создаем запрос на вставку данных
                var split = reader.ReadLine()!.Split("||");
                var commandStr = $"INSERT INTO RandomObjects VALUES('{split[0]}','{split[1]}','{split[2]}'," +
                                 $"{split[3]},{double.Parse(split[4]).ToString(CultureInfo.InvariantCulture)})";
                var command = new SqlCommand(commandStr, connection);
                // синхронизируем доступ к соединению
                lock (connection)
                {
                    command.ExecuteNonQuery();
                    // выводим информацию о процессе добавления записей
                    if (++counter % 10000 == 0)
                    {
                        Console.WriteLine($"{counter}/{FileConstants.TotalCountOfLines - counter}");
                    }
                }
            }
        });
    }

    public static void CalculateValues()
    {
        // открываем подклоючение к бд
        using var connection = new SqlConnection(DatabaseConstants.ConnString);
        connection.Open();
        
        // выполняем процедуру подсчета суммы чисел
        {
            using var command = new SqlCommand("exec CalculateSum", connection);

            using var reader = command.ExecuteReader();
            reader.Read();
            
            // Int64, потому что число больше 2^32
            Console.WriteLine($"sum of nums : {reader.GetInt64(0)}");
        }
        
        // выполняем процедуру подсчета медианы
        {
            using var command = new SqlCommand("exec CalculateMedian", connection);

            // устанавливаю CommandTimeout (у меня команда выполнялась долго и из-за этого вызывалось исключение)
            command.CommandTimeout = int.MaxValue;

            using var reader = command.ExecuteReader();
            reader.Read();
            Console.WriteLine($"median : {reader.GetDouble(0):F8}");
        }
    }
}