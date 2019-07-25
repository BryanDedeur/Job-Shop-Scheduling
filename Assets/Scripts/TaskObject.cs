using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskObject : MonoBehaviour
{
    // -------------- ORDERING --------------------- //
    public int m_TaskID;
    //private TaskObject PreviousTask;
    private TaskObject m_NextTask;

    // -------------- TIME RELATED ----------------- //
    private int m_StartTime;
    public int m_Duration;
    private int m_EndTime;

    // -------------- REFRENCES -------------------- //
    private JobShopSchedulerObject m_JobShopSchedulerObject;
    private JobObject m_Job;
    private MachineObject m_Machine;

    // -------------- VISUALS ---------------------- //
    private GameObject m_GanttChartElement;

    // Constructor
    public TaskObject(JobShopSchedulerObject jsso, JobObject jo, MachineObject machine, int duration)
    {
        m_JobShopSchedulerObject = jsso;
        m_Job = jo;
        m_Machine = machine;
        m_Duration = duration;
        m_TaskID = jo.m_Tasks.Count;
        m_Machine.m_Tasks.Add(this);
    }

    public string GetPrintString()
    {
        return "[Machine:" + m_Machine.MachineID + " Job:" + m_Job.m_JobID + " Duration:" + m_Duration + "]";
    }
}

