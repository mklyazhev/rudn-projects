using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Navigation;

namespace WPF_arXiv_search
{
    public partial class MainWindow : Window
    {
        private string pythonInterpreterPath, pythonScriptPath;

        public MainWindow()
        {
            InitializeDirectories();
            InitializeComponent();
        }

        private void InitializeDirectories()
        {
            try
            {
                string projectName = "WPF_arXiv_search";
                string pythonProjectName = "pyarxivsearchtool";
                string pythonVenvName = ".venv";
                // Базовый каталог приложения
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;

                // Поднимаемся на 3 уровня вверх из bin\Debug\
                string projectRoot = Path.GetFullPath(Path.Combine(baseDir, @"..\..\.."));

                pythonScriptPath = Path.Combine(projectRoot, projectName, pythonProjectName, "main.py");
                string venvPath = Path.Combine(projectRoot, projectName, pythonProjectName, pythonVenvName);
                pythonInterpreterPath = Path.Combine(venvPath, "bin", "python.exe");

                // Проверка существования файлов
                if (!File.Exists(pythonScriptPath))
                {
                    MessageBox.Show($"Python скрипт не найден: {pythonScriptPath}",
                                  "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                if (!File.Exists(pythonInterpreterPath))
                {
                    MessageBox.Show($"Интерпретатор Python не найден: {pythonInterpreterPath}\n" +
                                   $"Убедитесь, что виртуальное окружение '{pythonVenvName}' создано.",
                                  "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка во время инициализации путей: {ex.Message}",
                              "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string searchQuery = SearchQueryTextBox.Text;
                string maxResults = MaxResultsTextBox.Text;

                if (string.IsNullOrWhiteSpace(searchQuery))
                {
                    MessageBox.Show("Введите поисковый запрос",
                                  "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var processInfo = new ProcessStartInfo
                {
                    FileName = pythonInterpreterPath,
                    Arguments = $"{pythonScriptPath} \"{searchQuery}\" {maxResults}",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    WorkingDirectory = Path.GetDirectoryName(pythonScriptPath)
                };

                using (var process = new Process { StartInfo = processInfo })
                {
                    var outputBuilder = new System.Text.StringBuilder();
                    var errorBuilder = new System.Text.StringBuilder();

                    process.OutputDataReceived += (s, args) => outputBuilder.AppendLine(args.Data);
                    process.ErrorDataReceived += (s, args) => errorBuilder.AppendLine(args.Data);

                    process.Start();
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();
                    process.WaitForExit();

                    string error = errorBuilder.ToString();
                    if (!string.IsNullOrWhiteSpace(error))
                    {
                        MessageBox.Show($"Ошибка выполнения скрипта:\n{error}",
                                      "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    var articles = ParseResults(outputBuilder.ToString());
                    Dispatcher.Invoke(() => ResultsListBox.ItemsSource = articles);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}",
                              "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private List<Article> ParseResults(string pythonOutput)
        {
            var articles = new List<Article>();
            var lines = pythonOutput.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            Article currentArticle = null;

            foreach (var line in lines)
            {
                if (line.StartsWith("Entry:"))
                {
                    if (currentArticle != null) articles.Add(currentArticle);
                    currentArticle = new Article();
                }
                else if (currentArticle != null)
                {
                    if (line.StartsWith("Title:"))
                        currentArticle.Title = line.Substring(6).Trim();
                    else if (line.StartsWith("Authors:"))
                        currentArticle.Authors = line.Substring(8).Trim();
                    else if (line.StartsWith("Abstract:"))
                        currentArticle.Summary = line.Substring(9).Trim();
                    else if (line.StartsWith("PDF-link:"))
                        currentArticle.PdfLink = line.Substring(9).Trim();
                }
            }

            if (currentArticle != null) articles.Add(currentArticle);
            return articles;
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
                e.Handled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось открыть ссылку: {ex.Message}",
                              "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    public class Article
    {
        public string Title { get; set; }
        public string Authors { get; set; }
        public string Summary { get; set; }
        public string PdfLink { get; set; }
    }
}
