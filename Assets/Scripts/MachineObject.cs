using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MachineObject : MonoBehaviour
{
//    // -------------- ORDERING --------------------- //
//    public string MachineName;
//    public int MachineID;
//
//    // -------------- TIME RELATED ----------------- //
//
//    public int m_TotalDuration;
//    public int m_ProductionTime;
//    public int m_StartTime;
//    public int m_EndTime;
//
//    // -------------- REFRENCES -------------------- //
//    public JobShopSchedulerObject m_JobShopSchedulerObject;
//    public List<TaskObject> m_Tasks;
//    public int m_NumberOfTasks;
//    private static GameObject gameObjectRef;
//    public GameObject m_GuiRow;
//    public GameObject m_Label;
//
//    // -------------- VISUALS ---------------------- //
//
//
//    public static MachineObject CreateComponent(GameObject where, JobShopSchedulerObject jsso, int machineNum)
//    {
//        gameObjectRef = Instantiate(where);
//        gameObjectRef.transform.parent = jsso.transform;
//
//        MachineObject machine = gameObjectRef.AddComponent<MachineObject>();
//        machine.m_JobShopSchedulerObject = jsso;
//        machine.m_Tasks = new List<TaskObject>();
//        machine.MachineID = machineNum;
//
//        gameObjectRef.name = "Machine" + machineNum;
//
//        machine.m_GuiRow = GameObject.Find(gameObjectRef.name + "/MachineRowPanel");
//        machine.m_GuiRow.transform.SetParent(jsso.m_ScheduleGui.transform);
//
//        machine.m_Label = GameObject.Find(gameObjectRef.name + "/LabelPanel");
//        machine.m_Label.transform.SetParent(machine.m_GuiRow.transform);
//
//
//
//        return machine;
//    }
//
//    public void AddTask(TaskObject task)
//    {
//        m_Tasks.Add(task);
//        m_NumberOfTasks += 1;
//        m_TotalDuration += task.m_Duration;
//    }
//
//    public void UpdateGUI()
//    {
//        RectTransform parentRect = m_JobShopSchedulerObject.m_ScheduleGui.GetComponent<RectTransform>();
//        RectTransform rect = m_GuiRow.GetComponent<RectTransform>();
//        //rect.sizeDelta = parentRect.sizeDelta;
//        rect.sizeDelta = new Vector2(rect.sizeDelta.x, parentRect.rect.height / m_JobShopSchedulerObject.algorithm.NumMachines);
//        rect.anchoredPosition = new Vector3(0, -(rect.sizeDelta.y * MachineID), -5);
//        rect.position = new Vector3(rect.position.x, rect.position.y, -5);
//
//        parentRect = rect;
//
//        rect = m_Label.GetComponent<RectTransform>();
//        rect.sizeDelta = new Vector2(rect.sizeDelta.x, parentRect.sizeDelta.y);
//        rect.anchoredPosition = new Vector3(0, 0, 0);
//        rect.position = new Vector3(rect.position.x, rect.position.y, 0);
//
//        GameObject text = m_Label.transform.Find("Text").gameObject;
//        RectTransform textRect = text.GetComponent<RectTransform>();
//        Text textDisplay = text.GetComponent<Text>();
//        textDisplay.text = "  Machine " + MachineID;
//
//        for (int i = 0; i < m_Tasks.Count; i++)
//        {
//            m_Tasks[i].UpdateGUI();
//        }
//    }
//
//    public void Print()
//    {
//        string printString = "";
//        for (int i = 0; i < m_Tasks.Count; i++)
//        {
//            printString = printString + m_Tasks[i].GetPrintString() + "\t";
//        }
//        Debug.Log(printString);
//    }
//
//    //private GanttChartUIMachineElement GanttChartElement;
}
