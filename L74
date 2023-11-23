using System;
using System.Collections.Generic;

public class TaskScheduler<TTask, TPriority>
{
    private readonly SortedDictionary<TPriority, Queue<TTask>> taskQueue = new SortedDictionary<TPriority, Queue<TTask>>();
    private readonly TaskExecution<TTask> taskExecution;

    public delegate void TaskExecution<T>(T task);

    public TaskScheduler(TaskExecution<TTask> taskExecution)
    {
        this.taskExecution = taskExecution ?? throw new ArgumentNullException(nameof(taskExecution));
    }

    // Додавання завдання до черги з вказаним пріоритетом
    public void AddTask(TTask task, TPriority priority)
    {
        if (!taskQueue.ContainsKey(priority))
        {
            taskQueue[priority] = new Queue<TTask>();
        }

        taskQueue[priority].Enqueue(task);
    }

    // Виконання завдання з найвищим пріоритетом
    public void ExecuteNext()
    {
        if (taskQueue.Count > 0)
        {
            var highestPriority = taskQueue.Keys.Max();
            var nextTask = taskQueue[highestPriority].Dequeue();

            if (taskQueue[highestPriority].Count == 0)
            {
                taskQueue.Remove(highestPriority);
            }

            taskExecution(nextTask);
        }
        else
        {
            Console.WriteLine("No tasks to execute.");
        }
    }

    // Метод для отримання завдання з консолі
    public TTask GetTaskFromConsole()
    {
        Console.Write("Enter a task: ");
        string input = Console.ReadLine();
        // Тут можна додатково реалізувати конвертацію рядка в тип TTask
        return (TTask)Convert.ChangeType(input, typeof(TTask));
    }

    // Метод для повернення завдання в пул
    public void ReturnTaskToPool(TTask task, TPriority priority)
    {
        AddTask(task, priority);
    }
}

class Program
{
    static void Main()
    {
        TaskScheduler<string, int> scheduler = new TaskScheduler<string, int>(ExecuteTask);

        // Приклад введення завдань з консолі
        for (int i = 0; i < 5; i++)
        {
            string task = scheduler.GetTaskFromConsole();
            int priority = i; // Тут можна ввести пріоритет з консолі або іншим способом
            scheduler.AddTask(task, priority);
        }

        // Виконання завдань з найвищим пріоритетом
        scheduler.ExecuteNext();
        scheduler.ExecuteNext();
    }

    // Метод для виконання завдання
    static void ExecuteTask(string task)
    {
        Console.WriteLine($"Executing task: {task}");
    }
}
