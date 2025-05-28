using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PhonesRepositoryLogger
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            // Базовый каталог приложения
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            // Поднимаемся на 2 уровня вверх из bin\Debug\
            string projectRoot = System.IO.Path.GetFullPath(System.IO.Path.Combine(baseDir, @"..\.."));
            // Формируем путь к файлу относительно корня проекта
            string jsonFilePath = System.IO.Path.Combine(projectRoot, "phonesList.json");

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    // Регистрируем сервисы
                    services.AddSingleton<App>();
                    services.AddSingleton<MainWindow>(provider =>
                    {
                        // Создаём логгер
                        var logger = new Logger();
                        var viewModel = new PhonesViewModel(jsonFilePath, logger);

                        return new MainWindow(viewModel);
                    });
                })
                .Build();

            var app = host.Services.GetService<App>();
            app?.Run();
        }
    }
}
