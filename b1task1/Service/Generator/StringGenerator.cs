namespace b1.Service.Generator;

public class StringGenerator
{
    private static DateTime StartDate => DateTime.Today.AddYears(-5);
    private static Random Generator => new();

    public static string GetRandomString()
        => $"{GetRandomDay():dd.MM.yyyy}||{GetRandomSymbols(IsLetter)}||" +
           $"{GetRandomSymbols(IsRussianLetter, 'Ё', 'ё')}||{GetRandomNumber()}||{GetRandomDouble():F8}||";
    
    private static DateTime GetRandomDay()
    {
        var range = (DateTime.Today - StartDate).Days;           
        return StartDate.AddDays(Generator.Next(range));
    }

    private static string GetRandomSymbols(Predicate<char> isLetter, char startSymbol = 'A', char endSymbol = 'z')
    {
        var randomString = string.Empty;
        do
        {
            var symbol = (char)(startSymbol + Generator.Next(endSymbol - startSymbol));
            if (isLetter(symbol))
            {
                randomString += symbol;
            }
           
        } while (randomString.Length != 10);
        return randomString;
    }

    private static int GetRandomNumber() => 1 + Generator.Next(100000000);
    
    private static double GetRandomDouble() => 1 + Generator.NextDouble() * 19;

    private static bool IsRussianLetter(char letter) => letter is 'ё' or 'Ё' or >= 'а' and <= 'я' or >= 'А' and <= 'Я';
    
    private static bool IsLetter(char letter) => char.IsLetter(letter);
}