using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core
{
    class Task : IComparable<Task>
    {
        public int _delay;
        public Guest _guest;
        public OrderType _orderType;
        public Article _article;

        public Task(int delay, Guest name, OrderType orderType, Article article)
        {
            _delay = delay;
            _guest = name;
            _orderType = orderType;
            _article = article;
        }

        public Task(int delay, Guest name, OrderType orderType)
        {
            _delay = delay;
            _guest = name;
            _orderType = orderType;
        }

        public int CompareTo(Task other)
        {
            return _delay.CompareTo(other._delay);
        }

        public override string ToString()
        {
            return $"{_orderType} {_guest.Name} {_article.Name}";
        }
    }
}
