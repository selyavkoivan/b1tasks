namespace b1.Service.Generator;

public static class StringGenerator
{
    private static DateTime StartDate => DateTime.Today.AddYears(-5);
    private static Random Generator => new();

    public static string GetRandomString()
        => $"{GetRandomDay():dd.MM.yyyy}||{GetRandomSymbols(Comparer.IsLetter)}||" +
           $"{GetRandomSymbols(Comparer.IsRussianLetter, 'Ё', 'ё')}||{GetRandomNumber()}||{GetRandomDouble():F8}||";
    
    private static DateTime GetRandomDay()
    {
        // количество дней в промежутке 5 лет
        var range = (DateTime.Today - StartDate).Days;
        return StartDate.AddDays(Generator.Next(range));
    }

    private static string GetRandomSymbols(Predicate<char> isLetter, char startSymbol = 'A', char endSymbol = 'z')
    {
        var randomString = string.Empty;
        do
        {
            // рандомный символ в указанном промежутке
            var symbol = (char)(startSymbol + Generator.Next(endSymbol - startSymbol));
            // проверка на соответствие символа условию
            if (isLetter(symbol))
            {
                randomString += symbol;
            }
            // пока не соберется 10 символов
        } while (randomString.Length != 10);
        return randomString;
    }

    private static int GetRandomNumber() => 1 + Generator.Next(100000000);
    
    private static double GetRandomDouble() => 1 + Generator.NextDouble() * 19;
}