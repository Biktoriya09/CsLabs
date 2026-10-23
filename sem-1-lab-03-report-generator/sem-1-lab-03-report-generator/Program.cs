using System;
using System.Globalization;
using System.IO;


namespace sem_1_lab_03_report_generator
{
    public class Program
    {
        public static string BuildReport(string[] lines)
        {
            // флаг, показывающий, что событие идет
            bool isEventRunning = false;
            DateTime eventDate = DateTime.MinValue;
            string winner = "";
            int winnerPoints = 0;
            string questItem = "";
            string consolationReward = "";
            int warnings = 0;
            int errors = 0;

            foreach (string line in lines)
            {
                // проверяем начало события
                if (line.Contains("Событие началось:"))
                {
                    isEventRunning = true;
                    string dateStr = line.Substring(0, 10);
                    eventDate = DateTime.ParseExact(dateStr, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    continue;
                }

                // обрабатываем только внутри события
                if (isEventRunning)
                {
                    if (line.Contains("закрыто"))
                    {
                        break;
                    }

                    // ищем имя победителя
                    if (line.Contains("объявлены победителями"))
                    {
                        int startIndex = line.IndexOf("] ") + 2;
                        int endIndex = line.IndexOf(" объявлены победителями");
                        winner = line.Substring(startIndex, endIndex - startIndex);
                    }

                    // ищем очки победителя (независимо от порядка строк)
                    if (line.Contains("очков события") && !line.Contains("утешительную награду:"))
                    {
                        int startIndex = line.IndexOf("] ") + 2;
                        int midIndex = line.IndexOf(" получили");

                        if (midIndex > startIndex)
                        {
                            string clanName = line.Substring(startIndex, midIndex - startIndex);
                            int pointsStart = midIndex + " получили ".Length;
                            int pointsEnd = line.IndexOf(" очков", pointsStart);

                            if (pointsEnd > pointsStart)
                            {
                                string pointsStr = line.Substring(pointsStart, pointsEnd - pointsStart);
                                int pointsValue;

                                if (int.TryParse(pointsStr, out pointsValue))
                                {
                                    if (winner == "" || winner == clanName)
                                    {
                                        winnerPoints = pointsValue;
                                    }
                                }
                            }
                        }
                    }

                    // ищем ивентовый предмет
                    if (line.Contains("ивентовый предмет:"))
                    {
                        string marker = "ивентовый предмет: ";
                        int startIndex = line.IndexOf(marker) + marker.Length;
                        questItem = line.Substring(startIndex).Trim();
                    }

                    // ищем утешительную награду и склоняем
                    if (line.Contains("утешительную награду:"))
                    {
                        int startIndex = line.IndexOf("] ") + 2;
                        int midIndex = line.IndexOf(" получили");

                        if (midIndex > startIndex)
                        {
                            string clanName = line.Substring(startIndex, midIndex - startIndex);

                            // динамическое склонение
                            string[] words = clanName.Split(' ');
                            if (words.Length == 2)
                            {
                                string firstWord = words[0].Substring(0, words[0].Length - 2) + "ых";
                                string secondWord = words[1].Substring(0, words[1].Length - 1) + "ов";
                                clanName = firstWord + " " + secondWord;
                            }

                            int pointsStart = line.IndexOf(": ", midIndex) + 2;
                            int pointsEnd = line.IndexOf(" очков", pointsStart);

                            if (pointsEnd > pointsStart)
                            {
                                string pointsValue = line.Substring(pointsStart, pointsEnd - pointsStart);
                                consolationReward = clanName + ": " + pointsValue + " очков";
                            }
                        }
                    }

                    if (line.Contains("[Warning]"))
                    {
                        warnings++;
                    }

                    if (line.Contains("[Error]"))
                    {
                        errors++;
                    }
                }
            }

            // собираем текст отчета (стиль через интерполяцию)
            string finalReport = "# Итоги события: Восстание Ледяного Пламени\n\n";
            finalReport += $"Дата: {eventDate:dd.MM.yyyy}\n";
            finalReport += $"Победитель: {winner}\n";
            finalReport += $"Очки победителя: {winnerPoints}\n";
            finalReport += $"Ивентовый предмет: {questItem}\n";

            if (consolationReward != "")
            {
                finalReport += $"Утешительная награда {consolationReward}\n";
            }
            else
            {
                finalReport += "Утешительная награда: \n";
            }

            finalReport += $"Предупреждений во время события: {warnings}\n";
            finalReport += $"Ошибок во время события: {errors}";

            return finalReport;
        }

        public static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            string[] logLines = File.ReadAllLines("event_server.log");
            Console.WriteLine(BuildReport(logLines));
        }
    }
}