using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TRS
{
    /// <summary>
    /// Логика взаимодействия для PageRed.xaml
    /// </summary>
    public class EmptyTextToVisibilityConverter : IValueConverter
    {

        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {

            if (value is string text && string.IsNullOrEmpty(text))
                return Visibility.Visible;
            return Visibility.Collapsed;
        }
        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public partial class PageRed : Page
    {
        TaskManager taskManager = new TaskManager();
        private Task task;

        public PageRed(Task task)
        {
            InitializeComponent();
            this.task = task;
            this.DataContext = task;
            TypeTask.ItemsSource = Base.dataBase.Type.ToList(); TypeTask.SelectedValuePath = "ID_Type";
            TypeTask.DisplayMemberPath = "Title_Type";
            TypeTask.SelectedIndex = task.ID_Task;
        }

        private void SaveTask_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                task.Title_Task = TitleTask.Text;
                task.ID_Type = (int)TypeTask.SelectedValue;
                task.Info = InfoTask.Text;

                Base.dataBase.SaveChanges();
                taskManager.Tasks = taskManager.LoadTasks(); 
                MessageBox.Show("Изменения сохранены");
                NavigationService.Navigate(new PageInstructions());
                taskManager.Tasks = taskManager.LoadTasks();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении изменений: {ex.Message}");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Task del = Base.dataBase.Task.FirstOrDefault(x => x.ID_Task == task.ID_Task);
            Base.dataBase.Task.Remove(del);
            Base.dataBase.SaveChanges();
            taskManager.Tasks = taskManager.LoadTasks();
            NavigationService.GoBack();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}