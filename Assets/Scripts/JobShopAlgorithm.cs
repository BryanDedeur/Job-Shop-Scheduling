using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JobShopAlgorithm : MonoBehaviour
{
    // Public members
    [TextArea(7, 51)]
    public string ScheduleInput;
    public string Description;
    public bool RandomSwap;
    public bool RandomInvert;

    // Scheduling Generation Variables
    public bool RestartAlgorithm;
    public bool RunAlgorithm;
    public string BestSchedule;
    public int BestMakespan;
    public long GenerationNumber = 0;
    public int NumGenerationsPerRender;
    public int MaxGenerations; // this should calculate the max
    public int CurrentSample;
    public int MaxSamples;

    // Private members
    public int NumJobs;
    public int NumMachines;
    public int NumTasks;

    // Scheduling Generation Variables
    private List<int> LastSchedule;
    private int LastMakespan;
    private List<int> CurrentSchedule;
    public int CurrentMakespan;

    // Job Task Corresponding Matrixs (first list is task, nested list is job)
    public List<List<int>> JobTaskIDs;
    public List<List<int>> JobTaskMachines;
    public List<List<int>> JobTaskDurations;
    public List<List<int>> JobTaskEndTimes;

    private List<int> NextJobTaskIndexs;

    // Machine Task Corresponding Matrixs
    public List<List<int>> MachineTaskEndTimes;

    private List<int> NextMachineTaskIndexs;

    private JobShopSchedulerObject jsso;

    // Take previous schedule and adjust stuff
    void MakeScheduleFromLast()
    {
        CurrentSchedule = LastSchedule;
        if (RandomSwap)
            CurrentSchedule = RandomSwapListItems(ref LastSchedule);

        if (RandomInvert)
            CurrentSchedule = RandomInvertListItems(ref LastSchedule);
    }

    // Picks two random list items and randomly swaps them
    List<int> RandomInvertListItems(ref List<int> passedList)
    {
        List<int> newSwappedList = passedList;

        int randomIndex1 = (int) UnityEngine.Random.Range(0f, NumTasks);
        int randomIndex2 = (int) UnityEngine.Random.Range(0f, NumTasks);
        int j = (int) Mathf.Max((float) randomIndex1, (float) randomIndex2);
        for (int i = (int) Mathf.Min((float) randomIndex1, (float) randomIndex2); i < j; i++) {
            int temp = newSwappedList[i];
            newSwappedList[i] = newSwappedList[j];
            newSwappedList[j] = temp;
            j--;
        }

        return newSwappedList;
    }

    // Picks two random list items and randomly swaps them
    List<int> RandomSwapListItems(ref List<int> passedList)
    {
        List<int> newSwappedList = passedList;

        int randomIndex1 = (int) UnityEngine.Random.Range(0f, NumTasks);
        int randomIndex2 = (int) UnityEngine.Random.Range(0f, NumTasks);

        int temp = newSwappedList[randomIndex1];
        newSwappedList[randomIndex1] = newSwappedList[randomIndex2];
        newSwappedList[randomIndex2] = temp;

        return newSwappedList;
    }


    // Procudes a makespan based on the passed schedule
    int ComputeScheduleMakespan(ref List<int> schedule)
    {

        for (int i = 0; i < NumJobs; i++)
        {
            NextJobTaskIndexs[i] = 0;
        }

        for (int i = 0; i < NumMachines; i++)
        {
            NextMachineTaskIndexs[i] = 0;
        }

        for (int index = 0; index < NumTasks; index++)
        {
            //print(schedule[index]);
            int currentJob = schedule[index] / NumMachines; // this needs to be fixed doesnt work
            int currentJobTask = NextJobTaskIndexs[currentJob]; 
            //print(currentJobTask); //------------------------------------------------------------------------------------------------------------------------------------
            NextJobTaskIndexs[currentJob]++;
            int currentMachine = JobTaskMachines[currentJob][currentJobTask];
            int currentMachineTask = NextMachineTaskIndexs[currentMachine]; // this is causing errors
            NextMachineTaskIndexs[currentMachine]++;
            int duration = JobTaskDurations[currentJob][currentJobTask];

            // print("Current Machine: " + currentMachine);
            // print("Current Machine Task: " + currentMachineTask);
            if (currentMachineTask == 0 && currentJobTask == 0)
            {
                MachineTaskEndTimes[currentMachine][currentMachineTask] = duration;
                JobTaskEndTimes[currentJob][currentJobTask] = duration;
             } 
                else if (currentMachineTask > 0 && currentJobTask == 0)
                {
                    int previousMachineTaskEndTime = MachineTaskEndTimes[currentMachine][currentMachineTask - 1];
                    MachineTaskEndTimes[currentMachine][currentMachineTask] = previousMachineTaskEndTime + duration;
                    JobTaskEndTimes[currentJob][currentJobTask] = previousMachineTaskEndTime + duration;
                } else if (currentMachineTask == 0 && currentJobTask > 0)
                {
                    int previousJobTaskEndTime = JobTaskEndTimes[currentJob][currentJobTask - 1];
                    MachineTaskEndTimes[currentMachine][currentMachineTask] = previousJobTaskEndTime + duration;
                    JobTaskEndTimes[currentJob][currentJobTask] = previousJobTaskEndTime + duration; 
            }
            else if (currentMachineTask > 0 && currentJobTask > 0)
            {
                int previousJobTaskEndTime = JobTaskEndTimes[currentJob][currentJobTask - 1];
                int previousMachineTaskEndTime = MachineTaskEndTimes[currentMachine][currentMachineTask - 1];

                int greaterEndTime = Mathf.Max(previousJobTaskEndTime, previousMachineTaskEndTime);
                MachineTaskEndTimes[currentMachine][currentMachineTask] = greaterEndTime + duration;
                JobTaskEndTimes[currentJob][currentJobTask] = greaterEndTime + duration;
            }
        }

        int longestMakespan = 0;
        for (int i = 0; i < NumMachines; i++)
        {
            if (MachineTaskEndTimes[i][NumJobs - 1] > longestMakespan)
                longestMakespan = MachineTaskEndTimes[i][NumJobs - 1];
        }

        return longestMakespan;
    }

    private string generationOutput = "";
    private string makespanOutput = "";

    // Primary GA function
    // @returns best makespan
    int MinimizeMakespan()
    {
        for (int i = 0; i < NumGenerationsPerRender; i++)
        {
            //print("Ran algorithm " + GenerationNumber + " times");
            GenerationNumber += 1;
            MakeScheduleFromLast();
            CurrentMakespan = ComputeScheduleMakespan(ref CurrentSchedule);

            // compare makespans and keep the better schedule
            if (CurrentMakespan < LastMakespan)
            {
                LastMakespan = CurrentMakespan;
                LastSchedule = CurrentSchedule;

                generationOutput += GenerationNumber + ", ";
                makespanOutput += CurrentMakespan + ", ";

                BestSchedule = ConvertListToString(ref LastSchedule);
                BestMakespan = LastMakespan;

                jsso.GenerateSchedule();

            }
            jsso.UpdateSlider();
        }
        return BestMakespan;
    }

    // Takes the schedule input and initializes the arrays
    void ReadScheduleInput()
    {
        // reset shop objects
        //jsso.Clear();

        // reset data values
        NumJobs = 0;
        NumMachines = 0;
        NumTasks = 0;

        LastSchedule = new List<int>();
        LastMakespan = 999999;
        CurrentSchedule = new List<int>();
        CurrentMakespan = 999999; 

        JobTaskIDs = new List<List<int>>();
        JobTaskMachines = new List<List<int>>();
        JobTaskDurations = new List<List<int>>();
        JobTaskEndTimes = new List<List<int>>();
        NextJobTaskIndexs = new List<int>();

        MachineTaskEndTimes = new List<List<int>>();
        NextMachineTaskIndexs = new List<int>();

        char[] separators = new char[] {' '};
        if (ScheduleInput != "")
        {
            int rowColIndex = 0;
            string[] textLines = ScheduleInput.Split("\n"[0]);
            for (int i = 0; i < textLines.Length; i++)
            { 
                if (i == 0 && textLines[i].Length > 20)
                {
                    Description = textLines[i]; // capture the description
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
                                NumTasks = NumJobs * NumMachines;
                                for (int task = 0; task < NumTasks; task++)
                                {
                                    LastSchedule.Add(task);
                                }
                                break;
                        }
                    }
                }
                else
                { // else create a job object with the tasks from the row input                    
                    int jobIndex = i - rowColIndex;

                    List<int> tempJobTaskTaskIDs = new List<int>();
                    List<int> tempJobTaskMachines = new List<int>();
                    List<int> tempJobTaskDurations = new List<int>();
                    List<int> tempJobTaskEndTimes = new List<int>();

                    bool isMachineID = true;
                    int taskIndex = 0;
                    foreach (var value in textLines[i].Split(separators, StringSplitOptions.RemoveEmptyEntries))
                    {
                        int valueFound = 0;
                        bool success = Int32.TryParse(value, out valueFound);
                        if (success)
                        {
                            if (isMachineID == true)
                            {
                                tempJobTaskMachines.Add(valueFound);
                                tempJobTaskTaskIDs.Add(LastSchedule[((jobIndex) * (taskIndex + 1)) - 1]);
                                tempJobTaskEndTimes.Add(0);
                            }
                            else // duration value
                            {
                                tempJobTaskDurations.Add(valueFound);
                                taskIndex++;

                            }
                            isMachineID = !isMachineID;
                        }
                        else
                        {
                            Console.WriteLine("Conversion of '{0}' failed.", value ?? "<null>");
                            break;
                        }
                    }

                    NextJobTaskIndexs.Add(0);

                    JobTaskIDs.Add(tempJobTaskTaskIDs);
                    JobTaskDurations.Add(tempJobTaskDurations);
                    JobTaskMachines.Add(tempJobTaskMachines);
                    JobTaskEndTimes.Add(tempJobTaskEndTimes);
                }
            }

            for (int machine = 0; machine < NumMachines; machine++)
            {
                List<int> tempMachineEndTimes = new List<int>();
                for (int task = 0; task < NumJobs; task++)
                {
                    tempMachineEndTimes.Add(0);
                }

                MachineTaskEndTimes.Add(tempMachineEndTimes);
                NextMachineTaskIndexs.Add(0);
            }
        }
        MakeScheduleFromLast();
    }

    string ConvertListToString(ref List<int> passedList)
    {
        string scheduleString = "";
        for (int i = 0; i < passedList.Count; i++)
        {
            scheduleString = scheduleString + passedList[i] + " ";
        }
        return scheduleString;
    }

    // Start is called before the first frame update
    void Start()
    {
        jsso = transform.GetComponent<JobShopSchedulerObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (RestartAlgorithm) {
            //ReadScheduleInput();

            jsso.CreateObjects();
            RunAlgorithm = true;
        }

        if (RunAlgorithm && MaxGenerations > GenerationNumber)
        {
            BestMakespan = MinimizeMakespan();

        } else if (MaxGenerations == 0)
        {
            BestMakespan = MinimizeMakespan();
        }

        if (MaxGenerations <= GenerationNumber) {
            if (MaxSamples > CurrentSample) {
                CurrentSample += 1;
                GenerationNumber = 0;
                ReadScheduleInput();
            } else {
                if (RunAlgorithm) {
                    RunAlgorithm = false;
                    jsso.PrintResults();
                }

            }
        } else {
        }
    }

    private void OnApplicationQuit()
    {
        Debug.Log(generationOutput);
        Debug.Log(makespanOutput);
        Debug.Log("Best makespan found is: " + BestMakespan + " units of time, after " + GenerationNumber + " generations");
        Debug.Log(BestSchedule);
        ComputeScheduleMakespan(ref LastSchedule); // so we have the results ready for display
    }
}
