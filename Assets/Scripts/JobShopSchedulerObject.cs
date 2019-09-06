using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;

public class JobShopSchedulerObject : MonoBehaviour
{
    public JobShopData jsd;
    public JobShopAlgorithm jsa;

    public List<JobObject> m_JobObjects;
    public List<MachineObject> m_MachineObjects;

    public GameObject m_MachineObjectRef;
    public GameObject m_JobObjectRef;
    public GameObject m_TaskObjectRef;
    public GameObject m_ScheduleGui;
    public GameObject m_MakespanGui;
    
    private MakespanPlotter plotter;

    private void Start()
    {
        RectTransform rect = m_ScheduleGui.GetComponent<RectTransform>();
        plotter = m_MakespanGui.GetComponent<MakespanPlotter>();
    }

    public void CreateObjects() {
   
        m_JobObjects = new List<JobObject>();
        m_MachineObjects = new List<MachineObject>();

        for (int y = 0; y < jsd.NumMachines; y++)
        {
            m_MachineObjects.Add(MachineObject.CreateComponent(m_MachineObjectRef, this, y));
            Transform machine = m_MachineObjects[y].transform.Find("Machine").GetComponent<Transform>();
            machine.position = machine.position + new Vector3(10, 0, UnityEngine.Random.Range(0, 200.0f));
        }

        for (int j = 0; j < jsd.NumJobs; j++)
        {
            JobObject currentJob = JobObject.CreateComponent(m_JobObjectRef, this, j);
            for (int t = 0; t < jsd.NumMachines; t++)
            {
                int machineIndex = jsd.JobTaskMachines[j][t];
                int duration = jsd.JobTaskDurations[j][t];

                MachineObject currentMachine = m_MachineObjects[machineIndex];

                currentJob.AddTask(currentMachine, duration);
            }
            m_JobObjects.Add(currentJob);
            currentJob = null;
        }
    } 
    
    public void UpdateSlider()
    {
        plotter.ShowSample(jsd.IterationNumber, jsa.CurrentMakespan);
        plotter.UpdateSlider(jsd.IterationNumber);
    }

    public void GenerateSchedule()
    {
        for (int job = 0; job < jsd.NumJobs; job++)
        {
            for (int task = 0; task < jsd.NumMachines; task++)
            {
                //print("Job: " + job + "Task: " + task);
                int startTime = jsd.JobTaskEndTimes[job][task] - m_JobObjects[job].m_Tasks[task].m_Duration;
                m_JobObjects[job].m_Tasks[task].UpdateStartTime(startTime);
            }
        }

        for (int i = 0; i < m_MachineObjects.Count; i++)
        {
            //m_MachineObjects[i].Print();
            m_MachineObjects[i].UpdateGUI();
        }

        plotter.PlotDot(jsd.IterationNumber, jsd.BestMakespan);        
    }

    public void PrintResults(){
        plotter.PrintResults();
    }

    public void Clear(){
        // m_ScheduleGui
    }
}
