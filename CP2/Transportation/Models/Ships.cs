namespace Transportation.Models;

public class Ships
{
    public DateTime EndOfTitanic()
    {
        // 15 de abril de 1912
        return new DateTime(1912, 4, 15);
    }

    public DateTime EndOfBritannic()
    {
        return new DateTime(1916, 11, 21);
    }

    public DateTime EndOfOlympic()
    {
        return new DateTime(1935, 4, 12);
    }
}
