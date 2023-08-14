namespace Storm.Logging;

static class Log {
    public static void Normal(string msg) {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("[MSG] - " + msg);
        Console.ForegroundColor = ConsoleColor.White;
    }
    public static void Info(string msg) {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("[INFO] - " + msg);
        Console.ForegroundColor = ConsoleColor.White;
    }
    public static void Warning(string msg) {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("[WARNING] - " + msg);
        Console.ForegroundColor = ConsoleColor.White;
    }
    public static void Error(string msg) {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("[ERROR] - " + msg);
        Console.ForegroundColor = ConsoleColor.White;
    }
}