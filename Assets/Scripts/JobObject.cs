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
    private static GameObject gameObjectRef;

    // -------------- VISUALS ---------------------- //
    public Color ColorRef;

    // Constructor
    public static JobObject CreateComponent(GameObject where, JobShopSchedulerObject jsso, int jobNum)
    {
        gameObjectRef = Instantiate(where);
        gameObjectRef.transform.parent = jsso.transform;
        
        JobObject job = gameObjectRef.AddComponent<JobObject>();
        job.m_JobShopSchedulerObject = jsso;
        job.m_Tasks = new List<TaskObject>();
        job.m_NumberOfTasks = 0;
        job.m_TotalDuration = 0;
        job.m_StartTime = 0;
        job.m_EndTime = 0;
        job.m_JobID = jobNum;

        gameObjectRef.name = "Job" + job.m_JobID;

        job.ColorRef = new Color(
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            Random.Range(0f, 1f)
        );

        return job;
    }



    // Adds a task to the end of the task list
    public void AddTask(MachineObject machine, int duration)
    {
        m_Tasks.Add(TaskObject.CreateComponent(m_JobShopSchedulerObject.m_TaskObjectRef, m_JobShopSchedulerObject, this, machine, m_Tasks.Count, duration));
        m_NumberOfTasks += 1;
        m_TotalDuration += duration;
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
