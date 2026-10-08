using System.Security.Cryptography.X509Certificates;

namespace sem_1_lab_00_player_profile
{
    internal class Program
    {
        static void Main()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Hello friend, let's start create your profile! \n--------------------------- \nName: ");
            string name = Console.ReadLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Age: ");
            string age = Console.ReadLine();
            int a = Convert.ToInt32(age);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Complexety(easy, medium, hard): ");
            string complexety = Console.ReadLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Gender(f, m): ");
            string gender = Console.ReadLine();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("");
            Console.WriteLine("--------Final player card--------");
            Console.WriteLine($"Name: {name}");
            Console.WriteLine($"Age: {a}");
            Console.WriteLine($"Complexety: {complexety}");
            Console.WriteLine($"Gender: {gender}");
        }
    }
}
