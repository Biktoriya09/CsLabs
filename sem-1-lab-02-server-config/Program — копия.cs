namespace sem_1_lab_02_server_config
{
    internal class Program
    {
        public static string CheckConfiguration(int players, int memory, bool isPublic, bool hasPassword)
        {
            if (players <= 0)
            {
                return "Запуск невозможен: количество игроков должно быть больше нуля.";
            }

            if (memory < 2)
            {
                return "Запуск невозможен: серверу недостаточно оперативной памяти.";
            }

            if (isPublic == true && hasPassword == true)
            {
                return "Запуск возможен с предупреждением: публичный сервер защищён паролем.";
            }

            if (players > 100 && memory < 8)
            {
                return "Запуск возможен с предупреждением: для такого количества игроков рекомендуется больше оперативной памяти.";
            }

            return "Сервер готов к запуску.";
        }

        static void Main()
        {
            int players = 50;
            int memory = 8;
            bool isPublic = true;
            bool hasPassword = false;

            Console.WriteLine("Проверка конфигурации сервера");
            Console.WriteLine($"Игроков: {players}");
            Console.WriteLine($"Память: {memory}");
            Console.WriteLine($"Публичный: {isPublic}");
            Console.WriteLine($"Есть пароль: {hasPassword}");
            Console.WriteLine();

            string result = CheckConfiguration(players, memory, isPublic, hasPassword);
            Console.WriteLine(result);
        }
    }
}



