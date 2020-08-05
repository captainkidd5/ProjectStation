using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.News
{
    public interface INewsSnippetRepository
    {
        IEnumerable<NewsSnippet> Search(string searchTerm);
        IEnumerable<NewsSnippet> GetAllNewsSnippets();
        NewsSnippet GetNewsSnippet(int id);
        NewsSnippet Update(NewsSnippet updatedNewsSnippet);
        NewsSnippet AddNewsSnippet(NewsSnippet newNewsSnippet);
        NewsSnippet Delete(int id);
    }
}
