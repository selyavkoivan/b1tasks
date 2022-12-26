using b1.Service;
using b1.Service.Generator;

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
    Console.WriteLine(menu);
    switch (Console.ReadLine())
    {
        case "1": 
            FileService.ToOneFile(Comparer.CompareTo);
            break;
        case "2": 
            Console.WriteLine("Enter string for filtering");
            FileService.ToOneFile(Comparer.CompareTo, Console.ReadLine(), true);
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