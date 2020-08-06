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
    public class NewsDetailsModel : PageModel
    {
        private readonly INewsSnippetRepository newsSnippetRepository;

        public NewsSnippet NewsSnippet{ get; set; }

        public NewsSnippet NewsSnippetForward { get; set; }
        public NewsSnippet NewsSnippetBackward { get; set; }
        public NewsDetailsModel(INewsSnippetRepository newsSnippetRepository)
        {
            this.newsSnippetRepository = newsSnippetRepository;
        }
        public void OnGet(int id)
        {
            this.NewsSnippet = newsSnippetRepository.GetNewsSnippet(id);

            NewsSnippetForward = newsSnippetRepository.GetNewsSnippet(id + 1);

            NewsSnippetBackward = newsSnippetRepository.GetNewsSnippet(id - 1);

        }
    }
}