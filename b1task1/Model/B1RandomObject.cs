namespace b1.Model;

public class B1RandomObject
{
    public int B1RandomObjectId { get; set; }
    
    public DateTime RandomDate { get; set; }
    public string RandomLatinSymbols { get; set; }
    public string RandomRussianSymbols { get; set; }
    public int RandomNumber { get; set; }
    public double RandomDouble { get; set; }

    public B1RandomObject()
    {
    }

    public B1RandomObject(string randomString)
    {
        var randomStringArray = randomString.Split("||");
        RandomDate = DateTime.Parse(randomStringArray[0]);
        RandomLatinSymbols = randomStringArray[1];
        RandomRussianSymbols = randomStringArray[2];
        RandomNumber = int.Parse(randomStringArray[3]);
        RandomDouble = double.Parse(randomStringArray[4]);
    }
}