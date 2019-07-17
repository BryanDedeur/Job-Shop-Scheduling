using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Job object contains information about the tasks and the time duration of the job
public class JobObject : MonoBehaviour
{
    public List<TaskObject> Tasks;

    private int NumberOfTasks;
    private int TotalDuration;
    private int StartTime;

    // Constructor 
    public JobObject()
    {
        Tasks = new List<TaskObject>();
        NumberOfTasks = 0;
        TotalDuration = 0;
        StartTime = 0;
    }

    private int CalculateTotalDuration()
    {
        int sumOfDurations = 0;
        int sumOfHoldTimes = 0;
        // subtract sum of times with start time
        return sumOfDurations;
    }

    // Adds a task to the end of the task list
    public void AddTask(ref TaskObject newTask)
    {
        Tasks.Add(newTask);
        TotalDuration = CalculateTotalDuration();
    }

    // Adds a task infront of the preceeding task. If there is a task infront of the preceeding task it will insert inbetween
    public void AddTask(ref TaskObject newTask, ref TaskObject preceedingTask)
    {
        TotalDuration = CalculateTotalDuration();
    }

    // Removes the specified task from the job list
    public void RemoveTask(ref TaskObject newTask)
    {
        TotalDuration = CalculateTotalDuration();
    }

    // Returns the total job duration from start time to end time
    public int GetJobDuration()
    {
        return TotalDuration;
    }

    // Sets the starting time of the job
    public void SetJobStartTime()
    {
    }
}
