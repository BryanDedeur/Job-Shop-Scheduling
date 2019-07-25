using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineObject : MonoBehaviour
{
    // -------------- ORDERING --------------------- //
    public string MachineName;
    public int MachineID;

    // -------------- TIME RELATED ----------------- //

    public int m_ProductionTime;
    public int m_StartTime;
    public int m_EndTime;

    // -------------- REFRENCES -------------------- //
    public JobShopSchedulerObject m_JobShopSchedulerObject;
    public List<TaskObject> m_Tasks;
    public int m_NumberOfTasks;

    // -------------- VISUALS ---------------------- //

    public MachineObject(JobShopSchedulerObject jsso, int id)
    {
        m_Tasks = new List<TaskObject>();
        m_JobShopSchedulerObject = jsso;
        MachineID = id;
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




    //private GanttChartUIMachineElement GanttChartElement;
}
