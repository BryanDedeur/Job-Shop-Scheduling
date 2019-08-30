using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskObject : MonoBehaviour
{
//    // -------------- ORDERING --------------------- //
//    public int m_TaskID;
//    //private TaskObject PreviousTask;
//    private TaskObject m_NextTask;
//
//    // -------------- TIME RELATED ----------------- //
//    public int m_StartTime;
//    public int m_Duration;
//    public int m_EndTime;
//
//    // -------------- REFRENCES -------------------- //
//    private JobShopSchedulerObject m_JobShopSchedulerObject;
//    private JobObject m_Job;
//    private MachineObject m_Machine;
//    private static GameObject gameObjectRef;
//    private GameObject m_GuiElement;
//
//
//    // -------------- VISUALS ---------------------- //
//    //private GameObject m_GanttChartElement;
//
//    // Constructor
//    public static TaskObject CreateComponent(GameObject where, JobShopSchedulerObject jsso, JobObject jo, MachineObject machine, int id, int duration)
//    {
//        gameObjectRef = Instantiate(where);
//        gameObjectRef.transform.parent = jo.transform;
//
//        TaskObject task = gameObjectRef.AddComponent<TaskObject>();
//        task.m_JobShopSchedulerObject = jsso;
//        task.m_Job = jo;
//        task.m_Duration = duration;
//        task.m_TaskID = id;
//        task.m_Machine = machine;
//        task.m_Machine.AddTask(task);
//
//        gameObjectRef.name = "Task" + id;
//
//        task.m_GuiElement = GameObject.Find(gameObjectRef.name + "/TaskPanel");
//        task.m_GuiElement.transform.SetParent(machine.m_GuiRow.transform);
//
//        return task;
//    }
//
//    public void UpdateStartTime(int newStartTime)
//    {
//        m_StartTime = newStartTime;
//        m_EndTime = newStartTime + m_Duration;
//    }
//
//    public void UpdateGUI()
//    {
//        RectTransform parentRect = m_Machine.m_GuiRow.GetComponent<RectTransform>();
//        RectTransform rect = m_GuiElement.GetComponent<RectTransform>();
//        rect.sizeDelta = new Vector2(parentRect.rect.width - 75, parentRect.rect.height);
//        rect.anchoredPosition = new Vector3((rect.sizeDelta.x * ((float) m_StartTime / (float)m_JobShopSchedulerObject.algorithm.BestMakespan)), 0) + new Vector3(75, 0, 0);
//        rect.sizeDelta = new Vector2(rect.sizeDelta.x * ((float) m_Duration / (float) m_JobShopSchedulerObject.algorithm.BestMakespan), parentRect.rect.height);
//        
//
//        //Debug.Log((float) m_Duration / (float) m_JobShopSchedulerObject.m_TotalProductionTime);
//
//        Image image = m_GuiElement.GetComponent<Image>();
//        image.color = m_Job.ColorRef;
//
//        GameObject text = m_GuiElement.transform.Find("DescriptionText").gameObject;
//        RectTransform textRect = text.GetComponent<RectTransform>();
//        //textRect.sizeDelta = rect.sizeDelta;
//        //textRect.sizeDelta = new Vector2(textRect.sizeDelta.x, textRect.sizeDelta.y / 4);
//        //textRect.anchoredPosition = new Vector3(0, -textRect.sizeDelta.y * 0);
//        Text textDisplay = text.GetComponent<Text>();
//        textDisplay.text = "Job: " + m_Job.m_JobID + "\nTask: " + m_TaskID + "\nDuration: " + m_Duration + "\nTime: " + m_StartTime + " - " + m_EndTime;
//    }
//
//    public string GetPrintString()
//    {
//        return "[Machine:" + m_Machine.MachineID + " Job:" + m_Job.m_JobID + " Task:" + m_TaskID + " Duration:" + m_Duration + " End:" + m_EndTime + "]";
//    }
}

