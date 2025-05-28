using System;
using System.IO;

namespace PhonesRepositoryLogger
{
    public class Logger
    {
        private readonly string logFilePath;

        public Logger()
        {
            // Базовый каталог приложения
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            // Поднимаемся на 2 уровня вверх из bin\Debug\
            string projectRoot = Path.GetFullPath(Path.Combine(baseDir, @"..\.."));
            // Формируем путь к файлу относительно корня проекта
            logFilePath = Path.Combine(projectRoot, "app.log");
        }

        public void Log(string message)
        {
            try
            {
                File.AppendAllText(logFilePath, $"[{DateTime.Now}] {message}\n");
            }
            catch
            {
                // Намеренно пусто - если не получилось записать лог, просто игнорируем
            }
        }
    }
}
