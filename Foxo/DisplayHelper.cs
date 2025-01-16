namespace Foxo
{
    public class DisplayHelper
    {
        public static void PrintLogo()
        {
            Console.Clear();
            Console.WriteLine(" ________ ________  _____ ______   ________   \r\n|\\  _____\\\\   __  \\|\\   _ \\  _   \\|\\   __  \\  \r\n\\ \\  \\__/\\ \\  \\|\\  \\ \\  \\\\\\__\\ \\  \\ \\  \\|\\  \\ \r\n \\ \\   __\\\\ \\  \\\\\\  \\ \\  \\\\|__| \\  \\ \\   ____\\\r\n  \\ \\  \\_| \\ \\  \\\\\\  \\ \\  \\    \\ \\  \\ \\  \\___|\r\n   \\ \\__\\   \\ \\_______\\ \\__\\    \\ \\__\\ \\__\\   \r\n    \\|__|    \\|_______|\\|__|     \\|__|\\|__|   \r\n                                              \r\n                                              \r\n                                              ");
        }

        public static void PrintOptions(string[] options, bool slots) {
            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine($"{(slots ? "Slot " : "")}{i + 1} - {options[i]}");
            }
            Console.WriteLine();
        }
    }
}
