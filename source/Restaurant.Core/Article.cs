using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core
{
    public class Article
    {
        public string Name { get; private set; }
        public double Price { get; private set; }
        public int TimeToBuild { get; private set; }

        public Article(string name, double price, int timeToBuild)
        {
            Name = name;
            Price = price;
            TimeToBuild = timeToBuild;
        }
    }
}
