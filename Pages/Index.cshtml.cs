using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Models.Models;
using Services.News;

namespace ProjectStation.Pages
{
    public class IndexModel : PageModel
    {


        public List<NewsSnippet> NewsSnippets { get; set; }

        INewsSnippetRepository newsSnippetRepository;


        public IndexModel(INewsSnippetRepository newsSnippetRepository)
        {
            this.newsSnippetRepository = newsSnippetRepository;

        }

        public void OnGet()
        {
            this.NewsSnippets = newsSnippetRepository.GetAllNewsSnippets().ToList();


            while(NewsSnippets.Count > 6)
            {
                NewsSnippets.RemoveAt(0);
            }
        }

    }

}

