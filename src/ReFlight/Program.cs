using System.Drawing;

namespace ReFlight;

public static class Program
{
    private static void Main()
    {
        Console.WriteLine("-- Re:Doll Flight System --");
        Console.WriteLine("");
        Console.WriteLine("Hint: You can see hint with `help` command everytime.");
        Console.WriteLine("Hint: You can exit with `Ctrl + C` everywhere. (but without save)");
        Console.WriteLine("");

        Console.CancelKeyPress += (sender, e) =>
        {
            Console.ResetColor();
        };

        FlightInfo flightInfo = new()
        {
            Speed = 0,
            Fuel = 10000,
            Power = 0,
            X = 0,
            Y = 0,
            Z = 0,
            Direction = "↑",
        };

        InfoDisplay(flightInfo);
        while (true)
        {
            flightInfo = Flight(flightInfo);
            // break;
        }
    }

    private static async void InfoDisplay(FlightInfo flightInfo)
    {
        while (true)
        {
            State(flightInfo);
            if (flightInfo.Fuel <= 0)
            {
                flightInfo.Fuel = 0;
                if (flightInfo.Power > 0)
                    flightInfo.Power = 0;
                flightInfo.Speed -= flightInfo.Speed / 5;
                if (flightInfo.Z > 0)
                    flightInfo.Z -= flightInfo.Speed;
                else if (flightInfo.Z <= 0 && flightInfo.Speed <= 0)
                {
                    Console.WriteLine("You cannot flight due to lack of fuel");
                    Environment.Exit(0);
                }
            }
            if (flightInfo.Z < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Serious Accident: Crash");
                Console.ResetColor();
                Environment.Exit(0);
                break;
            }

            if (flightInfo.Power > 0)
                flightInfo.Fuel -= flightInfo.Power;
            flightInfo.Speed += flightInfo.Power / 5;

            flightInfo.X += flightInfo.Direction switch
            {
                "→" => flightInfo.Speed,
                "←" => -flightInfo.Speed,
                _ => 0,
            };
            flightInfo.Y += flightInfo.Direction switch
            {
                "↑" => flightInfo.Speed,
                "↓" => -flightInfo.Speed,
                _ => 0,
            };
            if (flightInfo.Z != 0)
                flightInfo.Z += flightInfo.Speed;

            await Task.Delay(5000);
        }
    }

    private static FlightInfo Flight(FlightInfo flightInfo)
    {
        string userCommand = Console.ReadLine()?.Trim() ?? "";
        switch (userCommand)
        {
            case "exit":
                Environment.Exit(0);
                break;

            case "power":
                Console.WriteLine("<Power Mode>");
                userCommand = Console.ReadLine() ?? "";
                if (int.TryParse(userCommand, out int value)) {
                    flightInfo.Power = value;
                    Console.WriteLine($"Info: current power is {value}");
                }
                else
                    Console.WriteLine("Error: Unexpected value");
                break;

            case "flight":
                if (flightInfo.Speed < 20)
                    Console.WriteLine("Error: Lack of speed");
                else
                {
                    Console.WriteLine("Info: Take off");
                    flightInfo.Z = 10;
                }
                break;

            case "dir":
            case "direction":
                Console.WriteLine("<Direction Mode>");
                ConsoleKeyInfo inputKey = Console.ReadKey();
                flightInfo.Direction = inputKey.Key switch
                {
                    ConsoleKey.W => "↑",
                    ConsoleKey.A => "←",
                    ConsoleKey.S => "↓",
                    ConsoleKey.D => "→",
                    _ => "↑",
                };
                Console.WriteLine($"Info: current direction is {flightInfo.Direction}");
                break;

            default:
                Console.WriteLine("Error: Unexpected Command");
                break;
        }

        return flightInfo;
    }

    private static void State(FlightInfo flightInfo)
    {
        if (flightInfo.Fuel > 0)
            Console.ForegroundColor = ConsoleColor.Cyan;
        else
            Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("----------------------------");
        Console.WriteLine($"| Speed: {flightInfo.Speed}");
        Console.WriteLine($"| Power: {flightInfo.Power}");
        Console.WriteLine($"| Fuel: {flightInfo.Fuel}");
        Console.WriteLine($"| Direction: {flightInfo.Direction}");
        Console.WriteLine($"| X, Y, Z: {flightInfo.X}, {flightInfo.Y}, {flightInfo.Z}");
        Console.WriteLine("----------------------------");
        Console.ResetColor();
    }

    public class FlightInfo
    {
        public int Speed { get; set; }
        public int Fuel { get; set; }
        public int Power { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; } // Altitude
        public required string Direction { get; set; }
    }
}
