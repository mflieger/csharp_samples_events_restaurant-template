using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core
{
    class Guest
    {
        private List<Article> _articles;
        public string Name { get; private set; }

        public double Tab { get; private set; }

        public Guest(string name)
        {
            Name = name;
            _articles = new List<Article>();
        }

        public void AddArticle(Article article)
        {
            _articles.Add(article);
            Tab += article.Price;
        }
    }
}
