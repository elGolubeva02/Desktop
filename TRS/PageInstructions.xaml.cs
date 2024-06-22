using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TRS
{
    /// <summary>
    /// Логика взаимодействия для PageInstructions.xaml
    /// </summary>
    public partial class PageInstructions : Page
    {
        TaskManager taskManager = new TaskManager();
        List<Task> filterTask;
        public PageInstructions()
        {
            InitializeComponent();
            LVInstructions.ItemsSource = taskManager.Tasks;
            LVInstructions.ItemsSource = taskManager.Tasks.ToList();
            LVInstructions.Items.Refresh();
            NewTaskType.ItemsSource = Base.dataBase.Type.ToList(); NewTaskType.SelectedValuePath = "ID_Type";
            NewTaskType.DisplayMemberPath = "Title_Type";
            CB.ItemsSource = Base.dataBase.Model.ToList(); CB.SelectedValuePath = "ID_Model";
            CB.DisplayMemberPath = "Title_Model";
            CB.SelectedIndex = 3;
        }
        private void Filter(object sender, RoutedEventArgs e)
        {
            filterTask = Base.dataBase.Task.ToList();

            if (CB.SelectedIndex == 3)
            {
                filterTask = filterTask;
            }
            else
            {
                List < Task > taskAll= filterTask;
                List<Type> dbType = Base.dataBase.Type.ToList();
                foreach (Task task in taskAll)
                {
                    foreach(Type type in dbType)
                    {
                        if(task.ID_Type == type.ID_Type)
                        {
                            if(type.ID_Model == CB.SelectedIndex)
                            {
                                filterTask.Add(task);
                            }
                        }
                    }
                }
                LVInstructions.ItemsSource = filterTask;
            }


            if (TB.Text != "")
            {
                filterTask = filterTask.Where(x => x.Title_Task.Contains(TB.Text)).ToList();
            }
            LVInstructions.ItemsSource = filterTask;
        }
        private void Red_Click(object sender, RoutedEventArgs e)
        {
            StackPanel sp = (StackPanel)sender;
            int id = Convert.ToInt32(sp.Uid);
            Task task = Base.dataBase.Task.FirstOrDefault(x => x.ID_Task == id);
            if (task != null)
            {
                 NavigationService.Navigate(new PageRed (task));
            }
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {

            Task task = new Task
            {
                Title_Task = NewTaskTitle.Text,
                ID_Type = (int)NewTaskType.SelectedIndex+1,
                Info = NewTaskInfo.Text
            };

            Base.dataBase.Task.Add(task);
            Base.dataBase.SaveChanges();
            taskManager.Tasks =
            taskManager.Tasks = taskManager.LoadTasks();
            LVInstructions.ItemsSource = taskManager.Tasks.ToList();
            LVInstructions.Items.Refresh();
            NewTaskTitle.Text = "";
            NewTaskInfo.Text = "";
            NewTaskType.SelectedIndex = -1;
        }

        //private void DeleteTask_Click(object sender, RoutedEventArgs e)
        //{
        //    StackPanel sp = (StackPanel)sender;
        //    int id = Convert.ToInt32(sp.Uid);
        //    taskManager.DeleteTask(id);
        //    LVInstructions.Items.Refresh();
        //}
    }
}


//private void Red_Click(object sender, RoutedEventArgs e)
//        {
//            Button sp = (Button)sender;
//            int id = Convert.ToInt32(sp.Uid);
//            Task task = Base.dataBase.Task.FirstOrDefault(x => x.ID_Task == id);
//           // NavigationService.Navigate(new PageRed(task));

//        }
//    }
//}
