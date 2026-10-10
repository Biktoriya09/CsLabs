namespace sem_1_lab_02_server_config
{
    internal class Program
    {
        public static string CheckPort(int port)
        {
            if (port < 1 || port > 65535)
            {
                return $"Порт {port} вне диапазона 1..65535.";
            }
            return "";
        }

        public static string CheckPlayers(int maxPlayers)
        {
            if (maxPlayers <= 0)
            {
                return $"Количество игроков ({maxPlayers}) должно быть больше нуля.";
            }
            return "";
        }

        public static string CheckViewDistance(int viewDistance)
        {
            if (viewDistance <= 0)
            {
                return $"Дальность прорисовки ({viewDistance}) должна быть больше нуля.";
            }
            return "";
        }

        public static string WarnPort(int port)
        {
            if (port >= 1 && port < 1024)
            {
                return $"Порт {port} в системном диапазоне, лучше взять больше 1024.";
            }
            return "";
        }

        public static string WarnLoad(int maxPlayers, int viewDistance)
        {
            if (maxPlayers > 100 && viewDistance > 16)
            {
                return $"Много игроков ({maxPlayers}) и большая дальность ({viewDistance}) — будет нагрузка.";
            }
            return "";
        }

        public static string WarnOpen(bool whitelist, bool onlineMode)
        {
            if (whitelist == false && onlineMode == false)
            {
                return "Нет whitelist и online-mode, сервер открыт для всех.";
            }
            return "";
        }

        public static string WarnHardcore(bool hardcore, bool pvp)
        {
            if (hardcore == true && pvp == false)
            {
                return "Хардкор без PvP: игроки не смогут защищаться.";
            }
            return "";
        }

        static void Main()
        {
            int port = 25565;
            int maxPlayers = 120;
            int viewDistance = 18;

            bool onlineMode = false;
            bool whitelist = false;
            bool pvp = false;
            bool hardcore = true;

            Console.WriteLine("=== Проверка конфигурации сервера ===");
            Console.WriteLine($"Порт: {port}");
            Console.WriteLine($"Игроков: {maxPlayers}");
            Console.WriteLine($"Дальность: {viewDistance}");
            Console.WriteLine($"Online-mode: {onlineMode}");
            Console.WriteLine($"Whitelist: {whitelist}");
            Console.WriteLine($"PvP: {pvp}");
            Console.WriteLine($"Hardcore: {hardcore}");
            Console.WriteLine();

            string errors = "";
            errors = errors + CheckPort(port);
            errors = errors + CheckPlayers(maxPlayers);
            errors = errors + CheckViewDistance(viewDistance);

            string warnings = "";
            warnings = warnings + WarnPort(port);
            warnings = warnings + WarnLoad(maxPlayers, viewDistance);
            warnings = warnings + WarnOpen(whitelist, onlineMode);
            warnings = warnings + WarnHardcore(hardcore, pvp);

            if (errors != "")
            {
                Console.WriteLine("Запуск невозможен:");
                Console.WriteLine(errors);
            }
            else if (warnings != "")
            {
                Console.WriteLine("Можно запустить, но нужно предупредить администратора:");
                Console.WriteLine(warnings);
            }
            else
            {
                Console.WriteLine("Сервер готов к запуску.");
            }
        }
    }
}
