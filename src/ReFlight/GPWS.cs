namespace ReFlight;

/// <summary>
/// Ground Proximity Warning System
/// </summary>
public static class GPWS
{
    public static void PullUp()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Info <GPWS>: PULL UP");
        Console.ResetColor();
    }

    public static void DontSink()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Info <GPWS>: DON'T SINK");
        Console.ResetColor();
    }

    public static void SinkRate()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Info <GPWS>: SINK RATE");
        Console.ResetColor();
    }

    public static void Callouts(int value)
    {
        // Round numbers
        int height;
        int level = value / 500;
        if (level != 0)
            height = level * 500;
        else
        {
            level = value / 5;
            height = level * 5;
        }

        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"Info <GPWS>: {height} FT");
        Console.ResetColor();
    }
}
