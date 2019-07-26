using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JobShopSchedulerObject : MonoBehaviour
{


    [TextArea(7, 51)]
    public string JobMatrixInput;
    public string m_MatrixDescription;

    public int NumJobs = 0;
    public int NumMachines = 0;

    public int m_TotalProductionTime = 100;

    private List<JobObject> m_JobObjects;
    private List<MachineObject> m_MachineObjects;

    public GameObject m_MachineObjectRef;
    public GameObject m_JobObjectRef;
    public GameObject m_TaskObjectRef;
    public GameObject m_ScheduleGui;

    private int m_TotalProductionDuration;

    private void CreateJobShop()
    {
        char[] separators = new char[] { ' ' };

        m_JobObjects = new List<JobObject>();
        m_MachineObjects = new List<MachineObject>();

        if (JobMatrixInput == "")
        {
            print("Matrix is empty");
        }
        else
        {

            int rowColIndex = 0;
            var textLines = JobMatrixInput.Split("\n"[0]);
            for (int i = 0; i < textLines.Length; i++)
            {
                // if the first row is description text then
                if (i == 0 && textLines[i].Length > 20)
                {
                    m_MatrixDescription = textLines[i];
                    rowColIndex = 1;
                } // if the current row is row col information
                else if (rowColIndex == i)
                {
                    int count = 0;
                    foreach (var token in textLines[i].Split(separators, StringSplitOptions.RemoveEmptyEntries))
                    {
                        count++;
                        switch (count)
                        {
                            case 1:
                                NumJobs = Convert.ToInt32(token);
                                break;
                            case 2:
                                NumMachines = Convert.ToInt32(token);
                                // add all the machines now so duplicates don't get created later
                                for (int y = 0; y < NumMachines; y++)
                                {
                                    m_MachineObjects.Add(MachineObject.CreateComponent(m_MachineObjectRef, this, y));
                                }
                                break;
                            default:
                                break;
                        }
                    }
                } else
                { // else create a job object with the tasks from the row input
                    JobObject currentJob = JobObject.CreateComponent(m_JobObjectRef, this, m_JobObjects.Count);

                    // TaskObject currentTask = new TaskObject(this, currentJob);
                    MachineObject currentMachine = m_MachineObjects[0];

                    bool isMachineID = true;
                    foreach (var value in textLines[i].Split(separators, StringSplitOptions.RemoveEmptyEntries))
                    {
                        int valueFound = 0;
                        bool success = Int32.TryParse(value, out valueFound);
                        if (success)
                        {
                            if (isMachineID == true)
                            {
                                currentMachine = m_MachineObjects[valueFound];
                            }
                            else
                            {
                                currentJob.AddTask(currentMachine, valueFound);
                            }
                            isMachineID = !isMachineID;
                        }
                        else
                        {
                            Console.WriteLine("Conversion of '{0}' failed.", value ?? "<null>");
                            break;
                        }
                    }

                    m_JobObjects.Add(currentJob);
                }
            }
        }
    }

    /*
    // jobNumber is the index of the job to prioritize using 0 base indexing
    void PrioritizeJob(int jobNumber)
    {
        // make sure job number is valid
        if (jobNumber < NumJobs && jobNumber > -1)
        {
            float lastStartTime = 0;
            // compute the start times for jobNumber as starting first
            for (int task = 0; task < NumMachines; task++)
            {
                Vector3 taskInfo = JobSchedule[jobNumber][task];
                taskInfo.z = taskInfo.z + lastStartTime;
                JobSchedule[jobNumber][task] = taskInfo;
                lastStartTime = lastStartTime + JobSchedule[jobNumber][task].y;
            }

            lastStartTime = 0;
            for (int job = 0; job < jobNumber; job++)
            {
                for (int task = 0; task < NumMachines; task++)
                {
                    int machineID = task;
                    
                }
            }
        }



        // adjust the schedules for all the other tasks
        for (int job = jobNumber + 1; job < NumJobs; job++)
        {
            for (int task = 0; task < NumMachines; task++)
            {
                
            }
        }
    }
    void GenerateSchedule()
    {
        int[] MachineOccupiedTimes = new int[NumMachines];
        for (int task = 0; task < NumMachines; task++)
        {
            for (int job = 0; job < NumJobs; job++)
            {
                MachineOccupiedTimes[Convert.ToInt32(JobSchedule[job][task].x)] = MachineOccupiedTimes[Convert.ToInt32(JobSchedule[job][task].x)] + Convert.ToInt32(JobSchedule[job][task].y);
                Vector3 taskInfo = JobSchedule[job][task];
                taskInfo.z = MachineOccupiedTimes[Convert.ToInt32(JobSchedule[job][task].x)];

                JobSchedule[job][task] = taskInfo;
            }
        }

    }
    */

    void GenerateSchedule()
    {

        int latestEndTime = 0;
        // Considering no machine overlap constraint
        for (int machine = 0; machine < m_MachineObjects.Count; machine++)
        {
            TaskObject lastTask = null;
            for (int task = 0; task < m_MachineObjects[machine].m_Tasks.Count; task++)
            {
                if (lastTask == null)
                {
                    m_MachineObjects[machine].m_Tasks[task].UpdateStartTime(0);
                    lastTask = m_MachineObjects[machine].m_Tasks[task];
                } else
                {
                    m_MachineObjects[machine].m_Tasks[task].UpdateStartTime(lastTask.m_EndTime);
                }
            }
        }

        
        // Considering job precedence constraint
        for (int job = 0; job < m_JobObjects.Count; job++)
        {
            TaskObject lastTask = null;
            for (int task = 0; task < m_MachineObjects[job].m_Tasks.Count; task++)
            {
                if (lastTask != null)
                {
                    if (m_JobObjects[job].m_Tasks[task].m_StartTime < lastTask.m_EndTime)
                    {
                        m_JobObjects[job].m_Tasks[task].UpdateStartTime(lastTask.m_EndTime);
                    }
                }

                // need to push back tasks on the machine to avoid overlap

                

                lastTask = m_JobObjects[job].m_Tasks[task];

                if (latestEndTime < m_JobObjects[job].m_Tasks[task].m_EndTime)
                {
                    latestEndTime = m_JobObjects[job].m_Tasks[task].m_EndTime;
                }
            }
        }
        m_TotalProductionTime = latestEndTime;
    }


        // Start is called before the first frame update
    void Start()
    {



        CreateJobShop();
        GenerateSchedule();
        for (int i = 0; i < m_JobObjects.Count; i++)
        {
            m_MachineObjects[i].Print();
            m_MachineObjects[i].UpdateGUI();
        }
        //PrioritizeJob(0);
        /*
        GenerateSchedule();
        print(MatrixDescription);
        for (int x = 0; x < NumJobs; x++)
        {
            string printStuff = "";
            for (int y = 0; y < NumMachines; y++)
            {
                printStuff = printStuff + JobSchedule[x][y] + " ";
            }
            print(printStuff);
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
