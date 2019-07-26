using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MachineObject : MonoBehaviour
{


    // -------------- ORDERING --------------------- //
    public string MachineName;
    public int MachineID;

    // -------------- TIME RELATED ----------------- //

    public int m_TotalDuration;
    public int m_ProductionTime;
    public int m_StartTime;
    public int m_EndTime;

    // -------------- REFRENCES -------------------- //
    public JobShopSchedulerObject m_JobShopSchedulerObject;
    public List<TaskObject> m_Tasks;
    public int m_NumberOfTasks;
    private static GameObject gameObjectRef;
    public GameObject m_GuiRow;

    // -------------- VISUALS ---------------------- //


    public static MachineObject CreateComponent(GameObject where, JobShopSchedulerObject jsso, int machineNum)
    {
        gameObjectRef = Instantiate(where);
        gameObjectRef.transform.parent = jsso.transform;

        MachineObject machine = gameObjectRef.AddComponent<MachineObject>();
        machine.m_JobShopSchedulerObject = jsso;
        machine.m_Tasks = new List<TaskObject>();
        machine.MachineID = machineNum;

        gameObjectRef.name = "Machine" + machineNum;

        machine.m_GuiRow = GameObject.Find(gameObjectRef.name + "/MachineRowPanel");
        machine.m_GuiRow.transform.SetParent(jsso.m_ScheduleGui.transform);

        return machine;
    }

    public void AddTask(TaskObject task)
    {
        m_Tasks.Add(task);
        m_NumberOfTasks += 1;
        m_TotalDuration += task.m_Duration;
    }

    public void UpdateGUI()
    {
        RectTransform parentRect = m_JobShopSchedulerObject.m_ScheduleGui.GetComponent<RectTransform>();
        RectTransform rect = m_GuiRow.GetComponent<RectTransform>();
        rect.sizeDelta = parentRect.sizeDelta;
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, rect.sizeDelta.y / m_JobShopSchedulerObject.NumMachines);
        rect.anchoredPosition = new Vector3(0, -(rect.sizeDelta.y * MachineID), -5);

        for (int i = 0; i < m_Tasks.Count; i++)
        {
            m_Tasks[i].UpdateGUI();
        }
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
