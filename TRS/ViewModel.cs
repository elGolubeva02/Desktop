using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRS
{
    public partial class TaskModel
    {
        public int ID_Task { get; set; }
        public string Title_Task { get; set; }
        public int ID_Type { get; set; }
        public string Info { get; set; }
        public string Type { get; set; }
        public string Title_Model { get; set; }
    }

    public class TaskManager
    {
        public List<TaskModel> Tasks { get; set; }

        public TaskManager()
        {
            Tasks = LoadTasks();
        }

        public List<TaskModel> LoadTasks()
        {
            List<TaskModel> instructions = new List<TaskModel>();
            List<Task> dbTasks = Base.dataBase.Task.ToList(); 

            foreach (Task dbTask in dbTasks)
            {
                TaskModel taskModel = new TaskModel
                {
                    ID_Task = dbTask.ID_Task,
                    Title_Task = dbTask.Title_Task,
                    ID_Type = dbTask.ID_Type,
                    Info = dbTask.Info
                };

                Type type = Base.dataBase.Type.FirstOrDefault(x => x.ID_Type == taskModel.ID_Type);
                if (type != null)
                {
                    taskModel.Type = type.Title_Type;
                }

                instructions.Add(taskModel);
            }
            return instructions;
        }
        public void AddTask(TaskModel newTask)
        {
            

        }

        public void UpdateTask(TaskModel updatedTask)
        {
            Task task = Base.dataBase.Task.FirstOrDefault(x => x.ID_Task == updatedTask.ID_Task);
            if (task != null)
            {
                task.Title_Task = updatedTask.Title_Task;
                task.ID_Type = updatedTask.ID_Type;
                task.Info = updatedTask.Info;
                Base.dataBase.SaveChanges();

                TaskModel taskModel = Tasks.FirstOrDefault(x => x.ID_Task == updatedTask.ID_Task);
                if (taskModel != null)
                {
                    taskModel.Title_Task = updatedTask.Title_Task;
                    taskModel.ID_Type = updatedTask.ID_Type;
                    taskModel.Info = updatedTask.Info;
                }
            }
        }

        public void DeleteTask(int taskId)
        {
            Task task = Base.dataBase.Task.FirstOrDefault(x => x.ID_Task == taskId);
            if (task != null)
            {
                Base.dataBase.Task.Remove(task);
                Base.dataBase.SaveChanges();
                TaskModel taskModel = Tasks.FirstOrDefault(x => x.ID_Task == taskId);
                if (taskModel != null)
                {
                    Tasks.Remove(taskModel);
                }
            }
        }
    }
}