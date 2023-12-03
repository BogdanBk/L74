[Serializable]
class TaskItem<TTask, TPriority>
{
    private TTask task;
    private TPriority priority;

    public TTask Task
    {
        get { return task; }
        set { task = value; }
    }
    public TPriority Priority
    {
        get { return priority; }
        set { priority = value; }
    }
}