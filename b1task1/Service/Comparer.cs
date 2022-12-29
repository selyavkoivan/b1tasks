namespace b1.Service;

public static class Comparer
{
    // проверяет, содержит ли строка подстроку, и ведет подсчет не содержащих подстроку строк
    public static bool Contains(string str, string pattern, ref int countOfDropLines)
    {
        if (str.Contains(pattern))
        {
            return true;
        }
        lock (pattern)
        {
            countOfDropLines++;
        }
        return false;
    }
    
    public static bool IsRussianLetter(char letter) => letter is 'ё' or 'Ё' or >= 'а' and <= 'я' or >= 'А' and <= 'Я';
    
    public static bool IsLetter(char letter) => char.IsLetter(letter);
}