using b1.Service;

// генерируем строки
FileService.GenerateFiles();

const string menu = """
              1. Move all lines to one file
              2. Move filtered lines to one file  
              3. Move all lines to database
              4. Calculate sum and median from database
              0. Exit
              >> 
              """;

while (true)
{
    try
    {
        Console.Write(menu);
        switch (Console.ReadLine())
        {
            case "1":
                FileService.ToOneFile(Comparer.Contains);
                break;
            case "2":
                Console.WriteLine("Enter string for filtering");
                FileService.ToOneFile(Comparer.Contains, Console.ReadLine()!, true);
                break;
            case "3":
                DatabaseService.ToDatabase();
                break;
            case "4":
                DatabaseService.CalculateValues();
                break;
            case "0": return;
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
}