class Program
{
    static void Main()
    {
        TaskScheduler<string, int> taskScheduler = new TaskScheduler<string, int>();

        string choice;
        do
        {
            bool isFirstTask = taskScheduler.WelcomeTask();

            if (!isFirstTask)
            {
                taskScheduler.AddTask();
            }
            else
            {
                ProcessUserChoice(taskScheduler);
            }

        } while ((choice = taskScheduler.UserChoice()) != "exit");
    }

    static void ProcessUserChoice(TaskScheduler<string, int> taskScheduler)
    {
        taskScheduler.SortTasks();
        taskScheduler.ShowAllTasks();

        string choice = taskScheduler.UserChoice();
        switch (choice)
        {
            case "done":
                taskScheduler.MarkTaskAsDone();
                break;
            case "create":
                taskScheduler.AddTask();
                break;
            case "tasks":
                taskScheduler.SortTasks();
                break;
            case "execute":
                taskScheduler.ExecuteNext(task => Console.WriteLine($"Executing task: {task}"));
                break;
            case "exit":
                break;
            default:
                Console.WriteLine("Invalid choice input.");
                break;
        }
    }
}
