using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.Models;
using Services.News;

namespace ProjectStation.Pages.NewsStuff
{
    public class IndexModel : PageModel
    {


        private INewsSnippetRepository newsSnippetRepository;

        public IEnumerable<NewsSnippet> NewsSnippets { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public IndexModel(INewsSnippetRepository newsSnippetRepository)
        {
            this.newsSnippetRepository = newsSnippetRepository;

        }


        public void OnGet()
        {
            NewsSnippets = newsSnippetRepository.Search(SearchTerm);
        }
    }
}