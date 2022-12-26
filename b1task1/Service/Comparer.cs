namespace b1.Service;

public class Comparer
{
    public static bool CompareTo(string str, string pattern, ref int countOfDropLines)
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
}