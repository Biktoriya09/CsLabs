using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace sem_1_lab_04_event_log
{
    public class LogEntry
    {
        public DateTime Timestamp;
        public string Level;
        public string Category;
        public string Message;
    }

    public class Program
    {
        // преобразуем строки в объекты
        public static LogEntry[] ParseLog(string[] lines)
        {
            LogEntry[] logs = new LogEntry[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                LogEntry entry = new LogEntry();

                // берем первые 23 символа для времени
                string timeStr = line.Substring(0, 23);
                entry.Timestamp = DateTime.ParseExact(timeStr, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);

                // ищем границы первой пары скобок
                int open1 = line.IndexOf('[');
                int close1 = line.IndexOf(']', open1);

                // ищем границы второй пары скобок
                int open2 = line.IndexOf('[', close1);
                int close2 = line.IndexOf(']', open2);

                entry.Level = line.Substring(open1 + 1, close1 - open1 - 1);
                entry.Category = line.Substring(open2 + 1, close2 - open2 - 1);

                // все что после второй скобки это текст, Trim убирает пробел
                entry.Message = line.Substring(close2 + 1).Trim();

                logs[i] = entry;
            }

            return logs;
        }

        public static LogEntry[] FilterByDate(LogEntry[] logs, DateTime date)
        {
            List<LogEntry> filtered = new List<LogEntry>();

            foreach (LogEntry log in logs)
            {
                // сравниваем только даты без времени
                if (log.Timestamp.Date == date.Date)
                {
                    filtered.Add(log);
                }
            }

            return filtered.ToArray();
        }

        public static LogEntry[] FilterByLevel(LogEntry[] logs, string level)
        {
            List<LogEntry> filtered = new List<LogEntry>();

            foreach (LogEntry log in logs)
            {
                if (log.Level == level)
                {
                    filtered.Add(log);
                }
            }

            return filtered.ToArray();
        }

        public static LogEntry[] FilterByCategory(LogEntry[] logs, string category)
        {
            List<LogEntry> filtered = new List<LogEntry>();

            foreach (LogEntry log in logs)
            {
                if (log.Category == category)
                {
                    filtered.Add(log);
                }
            }

            return filtered.ToArray();
        }

        public static LogEntry[] Search(LogEntry[] logs, string text)
        {
            List<LogEntry> filtered = new List<LogEntry>();

            foreach (LogEntry log in logs)
            {
                // ищем подстроку без учета регистра
                if (log.Message.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    filtered.Add(log);
                }
            }

            return filtered.ToArray();
        }

        public static int CountByLevel(LogEntry[] logs, string level)
        {
            int count = 0;

            foreach (LogEntry log in logs)
            {
                if (log.Level == level)
                {
                    count++;
                }
            }

            return count;
        }

        public static string GetServerStatus(LogEntry[] logs)
        {
            bool hasFatalServer = false;
            bool hasErrorOrFatal = false;

            foreach (LogEntry log in logs)
            {
                if (log.Level == "Fatal" && log.Category == "Server")
                {
                    hasFatalServer = true;
                }

                if (log.Level == "Error" || log.Level == "Fatal")
                {
                    hasErrorOrFatal = true;
                }
            }

            if (hasFatalServer)
                return "КРИТИЧЕСКАЯ ОШИБКА: сервер остановлен";

            if (hasErrorOrFatal)
                return "требуется проверка";

            return "Сервер работает штатно";
        }

        public static void ExportFiltered(string path, LogEntry[] logs)
        {
            StringBuilder sb = new StringBuilder();

            foreach (LogEntry log in logs)
            {
                string timeStr = log.Timestamp.ToString("yyyy-MM-dd HH:mm:ss.fff");
                sb.AppendLine($"{timeStr} [{log.Level}][{log.Category}] {log.Message}");
            }

            File.WriteAllText(path, sb.ToString());
        }

        public static void Main()
        {


            string[] lines = File.ReadAllLines("event_server.log");
            LogEntry[] logs = ParseLog(lines);

            Console.WriteLine($"Всего записей: {logs.Length}");
            Console.WriteLine($"Ошибок: {CountByLevel(logs, "Error")}");
            Console.WriteLine($"Предупреждений: {CountByLevel(logs, "Warning")}");
            Console.WriteLine($"Статус сервера: {GetServerStatus(logs)}");

            // показываем все ошибки
            Console.WriteLine("\n--- Список ошибок ---");
            LogEntry[] errors = FilterByLevel(logs, "Error");
            foreach (LogEntry err in errors)
            {
                Console.WriteLine($"{err.Timestamp:HH:mm:ss} - {err.Message}");
            }
        }
    }
}