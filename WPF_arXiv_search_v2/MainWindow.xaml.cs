using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Navigation;

namespace WPF_arXiv_search
{
    public partial class MainWindow : Window
    {
        private readonly ArxivParser arxivParser = new ArxivParser();
        private List<Article> currentArticles = new List<Article>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string searchQuery = SearchQueryTextBox.Text;
                string maxResultsText = MaxResultsTextBox.Text;

                if (string.IsNullOrWhiteSpace(searchQuery))
                {
                    MessageBox.Show("Введите поисковый запрос",
                                  "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!int.TryParse(maxResultsText, out int maxResults) || maxResults <= 0)
                {
                    maxResults = 5; // Значение по умолчанию
                }

                ArxivFeed feed = arxivParser.Search(searchQuery, maxResults: maxResults);

                currentArticles = ConvertToArticles(feed);
                ResultsListBox.ItemsSource = currentArticles;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}",
                              "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SortAscButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentArticles == null || currentArticles.Count == 0) return;

            currentArticles = currentArticles
                .OrderBy(a => a.Title)
                .ToList();

            ResultsListBox.ItemsSource = currentArticles;
        }

        private void SortDescButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentArticles == null || currentArticles.Count == 0) return;

            currentArticles = currentArticles
                .OrderByDescending(a => a.Title)
                .ToList();

            ResultsListBox.ItemsSource = currentArticles;
        }

        private List<Article> ConvertToArticles(ArxivFeed feed)
        {
            var articles = new List<Article>();

            foreach (var entry in feed.Entries)
            {
                articles.Add(new Article
                {
                    Title = entry.Title,
                    Authors = string.Join(", ", entry.Contributors),
                    Summary = entry.Summary,
                    PdfLink = entry.Id
                });
            }

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

    public class Article : IComparable<Article>
    {
        public string Title { get; set; }
        public string Authors { get; set; }
        public string Summary { get; set; }
        public string PdfLink { get; set; }

        public int CompareTo(Article other)
        {
            if (other == null) return 1;
            return string.Compare(Title, other.Title, StringComparison.OrdinalIgnoreCase);
        }
    }
}
