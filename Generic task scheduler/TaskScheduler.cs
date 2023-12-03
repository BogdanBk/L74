using Newtonsoft.Json;

class TaskScheduler<TTask, TPriority>
{
    private string filepath = "save.json";
    private List<TaskItem<TTask, TPriority>> tasks = new List<TaskItem<TTask, TPriority>>();
    private List<TaskItem<TTask, TPriority>> completedTasks = new List<TaskItem<TTask, TPriority>>();

    public TaskScheduler()
    {
        LoadTasks();
    }

    private void LoadTasks()
    {
        if (File.Exists(filepath))
        {
            string json = File.ReadAllText(filepath);
            tasks = JsonConvert.DeserializeObject<List<TaskItem<TTask, TPriority>>>(json);
        }
    }

    private void SaveTasks()
    {
        string json = JsonConvert.SerializeObject(tasks, Formatting.Indented);
        File.WriteAllText(filepath, json);
    }

    public void AddTask()
    {
        TaskItem<TTask, TPriority> newTask = new TaskItem<TTask, TPriority>();

        Console.WriteLine("Enter your task:");
        string taskInput = Console.ReadLine();
        newTask.Task = (TTask)Convert.ChangeType(taskInput, typeof(TTask));

        Console.WriteLine("Enter priority:");
        try
        {
            string priorityInput = Console.ReadLine();
            newTask.Priority = (TPriority)Convert.ChangeType(priorityInput, typeof(TPriority));
        }
        catch
        {
            Console.WriteLine("Invalid priority input");
        }

        tasks.Add(newTask);
        SaveTasks();
        Console.WriteLine("Task added. You can mark it as done later.");
    }

    public void SortTasks()
    {
        tasks = tasks.OrderBy(o => o.Priority).ToList();
        SaveTasks();
    }

    public void ExecuteNext(Action<TTask> executeTask)
    {
        if (tasks.Count > 0)
        {
            executeTask(tasks[0].Task);
            completedTasks.Add(tasks[0]);
            tasks.RemoveAt(0);
            SaveTasks();
        }
        else{ Console.WriteLine("No tasks to execute."); }
    }

    public void MarkTaskAsDone()
    {
        if (tasks.Count > 0)
        {
            completedTasks.Add(tasks[0]);
            tasks.RemoveAt(0);
            SaveTasks();
            Console.WriteLine("Task marked as done.");
        }
        else{ Console.WriteLine("No tasks to mark as done."); }
    }

    public bool WelcomeTask()
    {
        Console.WriteLine("Welcome to this simplified Task Scheduler");
        if (tasks.Count == 0)
        {
            Console.WriteLine("\nLooks like you have no tasks here.\n" +
                              "First of all, you should start with adding at least one");
            return false;
        }
        else if (tasks.Count >= 1)
        {
            Console.WriteLine("\nHere are all your tasks: ");
            return true;
        }
        else
        {
            Console.WriteLine("\nSomething went wrong in the WelcomeTask method");
            return false;
        }
    }

    public string UserChoice()
    {
        string choice;

        Console.WriteLine("\nType \"done\" to mark the task as done;\n" +
                          "Type \"create\" to create another task;\n" +
                          "Type \"tasks\" to get your current tasks one more time;\n" +
                          "Type \"execute\" to execute the task with the highest priority;\n" +
                          "Type \"exit\" to exit from this program.");
        Console.Write("Choice: ");

        choice = Console.ReadLine().ToLower();
        Console.WriteLine();
        return choice;
    }

    public void ShowAllTasks()
    {
        int temp = 0;

        foreach (var task in tasks)
        {
            Console.WriteLine($"Task: {tasks[temp].Task}, Priority: {tasks[temp].Priority}\n");
            ++temp;
        }
    }
}