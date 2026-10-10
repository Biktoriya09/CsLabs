namespace sem_1_lab_01_server_monitoring
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("=======================================================");
            Console.WriteLine("|       ДИАГНОСТИКА ИГРОВОГО СЕРВЕРА ПЕРЕД СМЕНОЙ       |");
            Console.WriteLine("=======================================================");
            Console.WriteLine();


            Console.Write("Название сервера: ");
            string serverName = Console.ReadLine();

            Console.Write("Ник администратора: ");
            string adminNick = Console.ReadLine();

            Console.Write("Регион (первая буква): ");
            string regionInput = Console.ReadLine();

            Console.Write("Количество игроков онлайн: ");
            string onlineInput = Console.ReadLine();

            Console.Write("Максимум игроков: ");
            string maxOnlineInput = Console.ReadLine();

            Console.Write("Пинг в мс: ");
            string pingInput = Console.ReadLine();

            Console.Write("Загрузка ЦП в %: ");
            string cpuInput = Console.ReadLine();

            Console.Write("Свободная ОЗУ в ГБ: ");
            string ramInput = Console.ReadLine();

            Console.Write("Часов без перезапуска: ");
            string uptimeInput = Console.ReadLine();

            Console.Write("Средний уровень игроков: ");
            string avgLevelInput = Console.ReadLine();

            Console.Write("Сервер в Европе? (true/false): ");
            string europeInput = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine("Обработка данных...");
            Console.WriteLine();
            

            int onlinePlayers = Convert.ToInt32(onlineInput);
            int maxPlayers = Convert.ToInt32(maxOnlineInput);
            double ping = Convert.ToDouble(pingInput);
            double cpuLoad = Convert.ToDouble(cpuInput);
            double freeRam = Convert.ToDouble(ramInput);
            int uptimeHours = Convert.ToInt32(uptimeInput);
            double avgLevel = Convert.ToDouble(avgLevelInput);
            bool isEurope = Convert.ToBoolean(europeInput);

            char regionLetter = regionInput[0];

            byte serverPort = 8080 % 256; // 8080 не влезает в byte, поэтому берём остаток

            short maxPing = 200;

            long totalSeconds = (long)uptimeHours * 3600;

            float tickRate = 64.0f;

            decimal donationSum = 15234.75m;

            double fillPercent = (double)onlinePlayers / maxPlayers * 100;

            int freeSlots = maxPlayers - onlinePlayers;

            double playersPerHour = (double)onlinePlayers / uptimeHours;

            byte extraSlots = 10;
            int totalSlots = maxPlayers + extraSlots;

            char firstDigitChar = '7';
            int digitValue = firstDigitChar - '0';

            double floatSum = 0.2 + 0.3;

            int bigNumber = 300;
            byte smallByte = (byte)bigNumber;
            
            
            
            Console.WriteLine("+-----------------------------------------------------+");
            Console.WriteLine("|              ОСНОВНАЯ ИНФОРМАЦИЯ                    |");
            Console.WriteLine("+-----------------------------------------------------+");
            Console.WriteLine($"| Сервер: \t\t\t| {serverName}");
            Console.WriteLine($"| Админ: \t\t\t| {adminNick}");
            Console.WriteLine($"| Регион: \t\t\t| {regionLetter} ({(isEurope ? "Европа" : "другое")})");
            Console.WriteLine("+-----------------------------------------------------+");
            Console.WriteLine();

            Console.WriteLine("+-----------------------------------------------------+");
            Console.WriteLine("|              НАГРУЗКА                                |");
            Console.WriteLine("+-----------------------------------------------------+");
            Console.WriteLine($"| Игроков: \t\t\t| {onlinePlayers} / {maxPlayers}");
            Console.WriteLine($"| Заполненность: \t\t| {fillPercent:F1} %");
            Console.WriteLine($"| Свободно слотов: \t\t| {freeSlots}");
            Console.WriteLine($"| Пинг: \t\t\t| {ping} мс");
            Console.WriteLine($"| ЦП: \t\t\t\t| {cpuLoad} %");
            Console.WriteLine($"| ОЗУ: \t\t\t\t| {freeRam} ГБ");
            Console.WriteLine("+-----------------------------------------------------+");
            Console.WriteLine();

            Console.WriteLine("+-----------------------------------------------------+");
            Console.WriteLine("|              АПТАЙМ И СТАТИСТИКА                     |");
            Console.WriteLine("+-----------------------------------------------------+");
            Console.WriteLine($"| Часов работы: \t\t| {uptimeHours}");
            Console.WriteLine($"| Секунд работы: \t\t| {totalSeconds}");
            Console.WriteLine($"| Игроков в час: \t\t| {playersPerHour:F2}");
            Console.WriteLine($"| Средний уровень: \t\t| {avgLevel}");
            Console.WriteLine("+-----------------------------------------------------+");
            Console.WriteLine();

            Console.WriteLine("+-----------------------------------------------------+");
            Console.WriteLine("|              ТЕХНИЧЕСКИЕ ПАРАМЕТРЫ                   |");
            Console.WriteLine("+-----------------------------------------------------+");
            Console.WriteLine($"| Порт (byte): \t\t\t| {serverPort}");
            Console.WriteLine($"| Макс. пинг (short): \t\t| {maxPing}");
            Console.WriteLine($"| Tick rate (float): \t\t| {tickRate}");
            Console.WriteLine($"| Донаты (decimal): \t\t| {donationSum}");
            Console.WriteLine($"| Слотов всего (int): \t\t| {totalSlots}");
            Console.WriteLine("+-----------------------------------------------------+");
            Console.WriteLine();

            Console.WriteLine("+-----------------------------------------------------+");
            Console.WriteLine("|              ПРИМЕРЫ РАБОТЫ С ТИПАМИ                 |");
            Console.WriteLine("+-----------------------------------------------------+");
            Console.WriteLine($"| '7' - '0' = \t\t\t| {digitValue} (char в int)");
            Console.WriteLine($"| 0.2 + 0.3 = \t\t\t| {floatSum} (неточность double)");
            Console.WriteLine($"| (byte)300 = \t\t\t| {smallByte} (потеря данных)");
            Console.WriteLine("+-----------------------------------------------------+");
            Console.WriteLine();

            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}