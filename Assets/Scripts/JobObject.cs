using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Job object contains information about the tasks and the time duration of the job
public class JobObject : MonoBehaviour
{

    // -------------- ORDERING --------------------- //
    public int m_JobID;

    // -------------- TIME RELATED ----------------- //

    public int m_ProductionTime;
    // total duration does not include the wait times
    public int m_TotalDuration;
    public int m_StartTime;
    public int m_EndTime;

    // -------------- REFRENCES -------------------- //
    public JobShopSchedulerObject m_JobShopSchedulerObject;
    public List<TaskObject> m_Tasks;
    public int m_NumberOfTasks;

    // -------------- VISUALS ---------------------- //




    // Constructor 
    public JobObject(JobShopSchedulerObject jsso)
    {
        m_JobShopSchedulerObject = jsso;
        m_Tasks = new List<TaskObject>();
        m_NumberOfTasks = 0;
        m_TotalDuration = 0;
        m_StartTime = 0;
        m_EndTime = 0;
        m_JobID = jsso.m_JobObjects.Count;
    }

    // Adds a task to the end of the task list
    public void AddTask(MachineObject machine, int duration)
    {
        m_Tasks.Add(new TaskObject(m_JobShopSchedulerObject, this, machine, duration));
        m_NumberOfTasks += 1;
        m_TotalDuration += duration;
    }

    // Removes the specified task from the job list
    public void RemoveTask(ref TaskObject newTask)
    {

    }

    // Sets the starting time of the job
    public void SetJobStartTime()
    {
    }

    public void Print()
    {
        string printString = "";
        for (int i = 0; i < m_Tasks.Count; i++)
        {
            printString = printString + m_Tasks[i].GetPrintString() + "\t";
        }
        Debug.Log(printString);
    }
}
