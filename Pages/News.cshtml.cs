using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.Models;
using Services.News;

namespace ProjectStation.Pages
{
    public class NewsModel : PageModel
    {
        public IEnumerable<NewsSnippet> NewsSnippets{ get; set; }
        INewsSnippetRepository newsSnippetRepository;


        public NewsModel(INewsSnippetRepository newsSnippetRepository)
        {
            this.newsSnippetRepository = newsSnippetRepository;
        }

        public void OnGet()
        {
            this.NewsSnippets = newsSnippetRepository.GetAllNewsSnippets();
        }



    }
}