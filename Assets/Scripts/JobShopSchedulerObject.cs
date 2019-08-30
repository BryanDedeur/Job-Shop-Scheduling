﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JobShopSchedulerObject : MonoBehaviour
{
/*

    public JobShopAlgorithm algorithm;

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

        for (int y = 0; y < algorithm.NumMachines; y++)
        {
            m_MachineObjects.Add(MachineObject.CreateComponent(m_MachineObjectRef, this, y));
        }

        for (int j = 0; j < algorithm.NumJobs; j++)
        {
            JobObject currentJob = JobObject.CreateComponent(m_JobObjectRef, this, j);
            for (int t = 0; t < algorithm.NumMachines; t++)
            {
                int machineIndex = algorithm.JobTaskMachines[j][t];
                int duration = algorithm.JobTaskDurations[j][t];

                MachineObject currentMachine = m_MachineObjects[machineIndex];

                currentJob.AddTask(currentMachine, duration);
            }
            m_JobObjects.Add(currentJob);
            currentJob = null;
        }
    } 
    
    public void UpdateSlider()
    {
        plotter.ShowSample(algorithm.GenerationNumber, algorithm.CurrentMakespan);
        plotter.UpdateSlider(algorithm.GenerationNumber);
    }

    public void GenerateSchedule()
    {
        for (int job = 0; job < algorithm.NumJobs; job++)
        {
            for (int task = 0; task < algorithm.NumMachines; task++)
            {
                print("Job: " + job + "Task: " + task);
                int startTime = algorithm.JobTaskEndTimes[job][task] - m_JobObjects[job].m_Tasks[task].m_Duration;
                m_JobObjects[job].m_Tasks[task].UpdateStartTime(startTime);
            }
        }

        for (int i = 0; i < m_MachineObjects.Count; i++)
        {
            //m_MachineObjects[i].Print();
            m_MachineObjects[i].UpdateGUI();
        }

        plotter.PlotDot(algorithm.GenerationNumber, algorithm.BestMakespan);        
    }

    public void PrintResults(){
        plotter.PrintResults();
    }

    public void Clear(){
        // m_ScheduleGui
    }*/

}
