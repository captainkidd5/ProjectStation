using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.News
{
    public class SQLNewsSnippetRepository : INewsSnippetRepository
    {
        private readonly AppDbContext context;

        public SQLNewsSnippetRepository(AppDbContext context)
        {
            this.context = context;
        }
        public NewsSnippet AddNewsSnippet(NewsSnippet newNewsSnippet)
        {

            context.Database.ExecuteSqlRaw("spInsertNewsSnippet {0},{1},{2}",
                                            newNewsSnippet.Title,
                                            newNewsSnippet.Date,
                                            newNewsSnippet.Description);


            return newNewsSnippet;
        }



        public NewsSnippet Delete(int id)
        {
            NewsSnippet NewsSnippet = context.NewsSnippets.Find(id);
            if (NewsSnippet != null)
            {
                context.NewsSnippets.Remove(NewsSnippet);
                context.SaveChanges();
            }
            return NewsSnippet;

        }

        public IEnumerable<NewsSnippet> GetAllNewsSnippets()
        {
            return context.NewsSnippets
                .FromSqlRaw<NewsSnippet>("SELECT * FROM NewsSnippets")
                .ToList();
        }

        public NewsSnippet GetNewsSnippet(int id)
        {
            SqlParameter parameter = new SqlParameter("Id", id);

            return context.NewsSnippets.FromSqlRaw<NewsSnippet>("spGetNewsSnippetById @Id", parameter)
                .ToList()
                .FirstOrDefault();
        }

        public IEnumerable<NewsSnippet> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return context.NewsSnippets;
            }

            return context.NewsSnippets.Where(x => x.Title.Contains(searchTerm));
        }

        public NewsSnippet Update(NewsSnippet updatedNewsSnippet)
        {
            var NewsSnippet = context.NewsSnippets.Attach(updatedNewsSnippet);
            NewsSnippet.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return updatedNewsSnippet;
        }
    }
}
