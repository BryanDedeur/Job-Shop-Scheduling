using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JobShopSchedulingScript : MonoBehaviour
{


    /*  
     *  line 1, matrix description
    */
    [TextArea(7, 51)]
    public string JobMatrix;

    // ------------------------ Processed Matrix Details --------------------- //
    private string MatrixDescription;
    private int NumJobs;
    private int NumMachines;


    // first list is jobs, seconds list is machines
    private List<List<Vector3>> JobSchedule;

    private List<List<Vector3>> MachineIntervals;


    private void ParseMatrix()
    {
        char[] separators = new char[] { ' ' };

        JobSchedule = new List<List<Vector3>>();
        MachineIntervals = new List<List<Vector3>>();

        if (JobMatrix == "")
        {
            print("Matrix is empty");
        }
        else
        {
            //int numJobs = JobMatrix.Split("\n"[0])[1];
            int rowColIndex = 0;
            var textLines = JobMatrix.Split("\n"[0]);
            for (int i = 0; i < textLines.Length; i++)
            {
                if (i == 0 && textLines[i].Length > 20)
                {
                    MatrixDescription = textLines[i];
                    rowColIndex = 1;
                }
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
                                break;
                            default:
                                break;
                        }
                    }
                } else
                {
                    // Make empty Machine Interval list
                    for (int job = 0; job < NumJobs; job++)
                    {
                        List<Vector3> sequence = new List<Vector3>();
                        for (int task = 0; task < NumMachines; task++)
                        {
                            Vector3 taskInfo = new Vector3();
                            sequence.Add(taskInfo);
                        }
                        MachineIntervals.Add(sequence);
                    }

                    // Create a Job Sequence Matrix
                    List<Vector3> jobSequence = new List<Vector3>();
                    Vector3 jobSequenceInfo = new Vector3();

                    bool isMachineID = true;
                    foreach (var value in textLines[i].Split(separators, StringSplitOptions.RemoveEmptyEntries))
                    {

                        int number = 0;
                        bool success = Int32.TryParse(value, out number);
                        if (success)
                        {
                            if (isMachineID == true)
                            {
                                jobSequenceInfo.x = number;
                            }
                            else
                            {
                                jobSequenceInfo.y = number;
                                jobSequence.Add(jobSequenceInfo);
                                jobSequenceInfo = new Vector3();
                            }
                            isMachineID = !isMachineID;
                            //Console.WriteLine("Converted '{0}' to {1}.", value, number);

                        }
                        else
                        {
                            Console.WriteLine("AtjobSequenceted conversion of '{0}' failed.",
                                               value ?? "<null>");
                            break;
                        }
                    }
                    JobSchedule.Add(jobSequence);
                }
            }
        }
    }


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

    // Start is called before the first frame update
    void Start()
    {
        ParseMatrix();
        //PrioritizeJob(0);
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
