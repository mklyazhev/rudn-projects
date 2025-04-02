# -*- coding: utf-8 -*-

import sys
import urllib.request
from urllib.parse import quote
import feedparser
from feedparser.mixin import _FeedParserMixin

# Меняем кодировку консоли на UTF-8, чтобы не было проблем с выводом текста
if sys.stdout.encoding != 'utf-8':
    sys.stdout.reconfigure(encoding='utf-8')


def setup_namespaces():
    # Настройка пространств имен для feedparser
    _FeedParserMixin.namespaces["http://a9.com/-/spec/opensearch/1.1/"] = "opensearch"
    _FeedParserMixin.namespaces["http://arxiv.org/schemas/atom"] = "arxiv"


def fetch_arxiv_data(search_query, start=0, max_results=5):
    # Получение данных из arXiv API
    base_url = "http://export.arxiv.org/api/query?"
    search_query_encoded = quote(f'"{search_query}"')
    query = f"search_query=all:{search_query_encoded}&start={start}&max_results={max_results}"
    
    response = urllib.request.urlopen(base_url + query).read()
    return response.replace(b"author", b"contributor")


def parse_and_print_results(feed):
    # Парсинг и вывод результатов
    print(f"Feed title: {feed.feed.title}")
    print(f"Feed last updated: {feed.feed.updated}")
    print(f"Total results: {feed.feed.opensearch_totalresults}")
    
    for entry in feed.entries:
        print("\nEntry:")
        print(f"Title: {entry.title}")
        print(f"Authors: {', '.join(a.name for a in entry.contributors)}")
        print(f"Abstract: {entry.summary[:200]}...")
        print(f"PDF-link: {entry.id}")


if __name__ == "__main__":
    if len(sys.argv) < 2:
        print("Usage: python arxiv.py '\"<query_string>\" [<num_articles>]")
        print("Example: python arxiv.py 'language models' 5")
        sys.exit(1)
    
    search_term = sys.argv[1]
    max_results = int(sys.argv[2]) if len(sys.argv) > 2 else 5
    
    setup_namespaces()
    xml_data = fetch_arxiv_data(search_term, max_results=max_results)
    feed = feedparser.parse(xml_data)
    
    parse_and_print_results(feed)
