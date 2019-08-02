using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JobShopSchedulerObject : MonoBehaviour
{

    public JobShopRandomSwapGA algorithm;

    private List<JobObject> m_JobObjects;
    private List<MachineObject> m_MachineObjects;

    public GameObject m_MachineObjectRef;
    public GameObject m_JobObjectRef;
    public GameObject m_TaskObjectRef;
    public GameObject m_ScheduleGui;

    private void Start()
    {
        RectTransform rect = m_ScheduleGui.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(Screen.width, Screen.height);
    }


    public void CreateObjects()
    {
        m_JobObjects = new List<JobObject>();
        m_MachineObjects = new List<MachineObject>();

        for (int y = 0; y < algorithm.NumMachines; y++)
        {
            
            m_MachineObjects.Add(MachineObject.CreateComponent(m_MachineObjectRef, this, y));
        }

        for (int y = 0; y < algorithm.NumJobs; y++)
        {
            JobObject currentJob = JobObject.CreateComponent(m_JobObjectRef, this, y);
            for (int m = 0; m < algorithm.NumMachines; m++)
            {
                MachineObject currentMachine = m_MachineObjects[algorithm.JobTaskMachines[y][m]];
                currentJob.AddTask(currentMachine, algorithm.JobTaskDurations[y][m]);
            }
            m_JobObjects.Add(currentJob);
        }
    }

    public void GenerateSchedule()
    {
        
        for (int machine = 0; machine < algorithm.NumMachines; machine++)
        {
            for (int task = 0; task < algorithm.NumJobs; task++)
            {
                int startTime = algorithm.MachineTaskEndTimes[machine][task] - m_MachineObjects[machine].m_Tasks[task].m_Duration;
                m_MachineObjects[machine].m_Tasks[task].UpdateStartTime(startTime);
            }
        }
        
        /*
        for (int currentJob = 0; currentJob < algorithm.NumJobs; currentJob++)
        {
            for (int currentJobTask = 0; currentJobTask < algorithm.NumMachines; currentJobTask++)
            {
                int currentMachine = algorithm.JobTaskMachines[currentJob][currentJobTask];

                int duration = algorithm.JobTaskDurations[currentJob][currentJobTask];

                int startTime = algorithm.MachineTaskEndTimes[currentMachine][currentJobTask] - m_MachineObjects[currentMachine].m_Tasks[currentJobTask].m_Duration;
                m_MachineObjects[currentMachine].m_Tasks[currentJobTask].UpdateStartTime(startTime);
            }
        }
        */

        for (int i = 0; i < m_JobObjects.Count; i++)
        {
            m_MachineObjects[i].Print();
            m_MachineObjects[i].UpdateGUI();
        }

    }

}
