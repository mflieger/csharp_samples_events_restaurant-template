using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using System.IO;

namespace Restaurant.Core
{
    public class Waiter
    {
        private Dictionary<string, Article> _articleList;
        private Dictionary<string, Guest> _guestList;
        private List <Task> _taskList;
        private DateTime _startTime;
        public event EventHandler<string> TaskFinished;

        public Waiter(DateTime startTime)
        {
            _articleList = new Dictionary<string, Article>();
            _guestList = new Dictionary<string, Guest>();
            _taskList = new List<Task>();
            _startTime = startTime;
            FastClock.Instance.OneMinuteIsOver += Instance_OneMinuteIsOver;
            InitializeArticlesTasksFromCSV();      
        }

        private void InitializeArticlesTasksFromCSV()
        {
            string articleFileName = MyFile.GetFullNameInApplicationTree("Articles.csv");
            string tasksFileName = MyFile.GetFullNameInApplicationTree("Tasks.csv");

            string[] articleLines = File.ReadAllLines(articleFileName, Encoding.Default);
            string[] taskLines = File.ReadAllLines(tasksFileName, Encoding.Default);

            CreateArticleList(articleLines);
            CreateTaskList(taskLines);
        }

        public void CreateArticleList(string[] input)
        {
            for (int i = 1; i < input.Length; i++)
            {
                string[] line = input[i].Split(';');
                Article article = new Article(line[0], Convert.ToDouble(line[1]), Convert.ToInt32(line[2]));
                _articleList.Add(line[0], article);
            }
        }

        public void CreateTaskList(string[] input)
        {
            for (int i = 1; i < input.Length; i++)
            {
                string[] line = input[i].Split(';');
                string guestName = line[1];
                OrderType orderType = (OrderType)Enum.Parse(typeof(OrderType), line[2]);
                Guest guest = new Guest(guestName);

                if (!_guestList.ContainsKey(guestName))
                {
                    _guestList.Add(guestName, guest);
                }
                Task task;
                if (string.IsNullOrEmpty(line[3]))
                {
                    task = new Task(Convert.ToInt32(line[0]), guest, orderType);
                }
                else
                {

                    task = new Task(Convert.ToInt32(line[0]), guest, orderType, _articleList[line[3]]);
                }
                _taskList.Add(task);
            }
        }

        private void Instance_OneMinuteIsOver(object sender, DateTime e)
        {
            DoDetectedTask();
        }
        
        public void DoDetectedTask()
        {
            while (_taskList.Count > 0 && _startTime.AddMinutes(_taskList.ElementAt(0)._delay) <= FastClock.Instance.Time)
            {
                Task task = _taskList.ElementAt(0);
                string message = "";
                
                switch(task._orderType)
                {
                    case OrderType.Order:
                        task._guest.AddArticle(task._article);
                        Task deliverTask = new Task(task._delay + task._article.TimeToBuild, task._guest, OrderType.Ready, task._article);
                        _taskList.Add(deliverTask);
                        _taskList.Sort();
                        message = task.ToString();
                        break;

                    case OrderType.Ready:
                        message = task.ToString();
                        break;

                    case OrderType.ToPay:
                        message = $"{task._guest.Name} pays {task._guest.Tab:f2}";
                        _guestList.Remove(task._guest.Name);
                        break;
                }
                _taskList.RemoveAt(0);
                TaskFinished?.Invoke(this, message);
            }
        }
    }

}
