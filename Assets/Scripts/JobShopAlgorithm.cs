using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobShopAlgorithm : MonoBehaviour
{
    private JobShopData jsd;
    public JobShopSchedulerObject jsso;
    public int CurrentMakespan = 999999;
    public List<int> CurrentSchedule;
    public int LastMakespan = 999999;
    public List<int> LastSchedule;

    public long longestIteration = 0;
    private int ItterationCounter = 0;


    // Take previous schedule and adjust stuff
    void MakeScheduleFromLast()
    {
        CurrentSchedule = LastSchedule;
        if (jsd.RandomSwap)
            CurrentSchedule = RandomSwapListItems(ref LastSchedule);

        if (jsd.RandomInvert)
            CurrentSchedule = RandomInvertListItems(ref LastSchedule);
    }

    // Picks two random list items and randomly swaps them
    List<int> RandomInvertListItems(ref List<int> passedList)
    {
        List<int> newSwappedList = passedList;

        int randomIndex1 = (int) UnityEngine.Random.Range(0f, jsd.NumTasks);
        int randomIndex2 = (int) UnityEngine.Random.Range(0f, jsd.NumTasks);
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

        int randomIndex1 = (int) UnityEngine.Random.Range(0f, jsd.NumTasks);
        int randomIndex2 = (int) UnityEngine.Random.Range(0f, jsd.NumTasks);

        int temp = newSwappedList[randomIndex1];
        newSwappedList[randomIndex1] = newSwappedList[randomIndex2];
        newSwappedList[randomIndex2] = temp;

        return newSwappedList;
    }


    // Procudes a makespan based on the passed schedule
    int ComputeScheduleMakespan(ref List<int> schedule)
    {
        for (int i = 0; i < jsd.NumJobs; i++)
        {
            jsd.NextJobTaskIndexs[i] = 0;
        }

        for (int i = 0; i < jsd.NumMachines; i++)
        {
            jsd.NextMachineTaskIndexs[i] = 0;
        }

        for (int index = 0; index < jsd.NumTasks; index++)
        {
            int currentJob = schedule[index] / jsd.NumMachines; 
            int currentJobTask = jsd.NextJobTaskIndexs[currentJob]; 
            jsd.NextJobTaskIndexs[currentJob]++;
            int currentMachine = jsd.JobTaskMachines[currentJob][currentJobTask];
            int currentMachineTask = jsd.NextMachineTaskIndexs[currentMachine]; 
            jsd.NextMachineTaskIndexs[currentMachine]++;
            int duration = jsd.JobTaskDurations[currentJob][currentJobTask];
            
            if (currentMachineTask == 0 && currentJobTask == 0)
            {
                jsd.MachineTaskEndTimes[currentMachine][currentMachineTask] = duration;
                jsd.JobTaskEndTimes[currentJob][currentJobTask] = duration;
             } 
                else if (currentMachineTask > 0 && currentJobTask == 0)
                {
                    int previousMachineTaskEndTime = jsd.MachineTaskEndTimes[currentMachine][currentMachineTask - 1];
                    jsd.MachineTaskEndTimes[currentMachine][currentMachineTask] = previousMachineTaskEndTime + duration;
                    jsd.JobTaskEndTimes[currentJob][currentJobTask] = previousMachineTaskEndTime + duration;
                } else if (currentMachineTask == 0 && currentJobTask > 0)
                {
                    int previousJobTaskEndTime = jsd.JobTaskEndTimes[currentJob][currentJobTask - 1];
                    jsd.MachineTaskEndTimes[currentMachine][currentMachineTask] = previousJobTaskEndTime + duration;
                    jsd.JobTaskEndTimes[currentJob][currentJobTask] = previousJobTaskEndTime + duration; 
            }
            else if (currentMachineTask > 0 && currentJobTask > 0)
            {
                int previousJobTaskEndTime = jsd.JobTaskEndTimes[currentJob][currentJobTask - 1];
                int previousMachineTaskEndTime = jsd.MachineTaskEndTimes[currentMachine][currentMachineTask - 1];

                int greaterEndTime = Mathf.Max(previousJobTaskEndTime, previousMachineTaskEndTime);
                jsd.MachineTaskEndTimes[currentMachine][currentMachineTask] = greaterEndTime + duration;
                jsd.JobTaskEndTimes[currentJob][currentJobTask] = greaterEndTime + duration;
            }
        }

        int longestMakespan = 0;
        for (int i = 0; i < jsd.NumMachines; i++)
        {
            if (jsd.MachineTaskEndTimes[i][jsd.NumJobs - 1] > longestMakespan)
                longestMakespan = jsd.MachineTaskEndTimes[i][jsd.NumJobs - 1];
        }

        return longestMakespan;
    }

    // Primary function
    // @returns best makespan
    void MinimizeMakespan()
    {
        for (int i = 0; i < jsd.NumIterationPerRender; i++)
        {
            if (jsd.IterationNumber == 0)
            {
                LastSchedule = jsd.DefaultListSchedule;
                LastMakespan = ComputeScheduleMakespan(ref LastSchedule);
                jsd.UpdateVisuals();
                jsso.CreateObjects();
            }
            MakeScheduleFromLast();
            CurrentMakespan = ComputeScheduleMakespan(ref CurrentSchedule);
            
            jsd.IterationNumber += 1;
            ItterationCounter += 1;

            if (ItterationCounter > jsd.IterationCap)
            {
                jsd.CurrentSample += 1;
                ItterationCounter = 0;
                print("Break at itteration" + jsd.IterationNumber);
                if (jsd.IterationNumber > longestIteration)
                    longestIteration = jsd.IterationNumber;
                jsd.IterationNumber = 0;
                break;
            }
            // compare makespans and keep the better schedule
            if (CurrentMakespan < LastMakespan)
            {
                ItterationCounter = 0;
                LastMakespan = CurrentMakespan;
                LastSchedule = CurrentSchedule;
                
                if (CurrentMakespan < jsd.BestMakespan)
                {
                    jsd.BestMakespan = CurrentMakespan;
                    jsd.BestListSchedule = CurrentSchedule;
                    jsd.UpdateVisuals();
                    jsso.GenerateSchedule();
                }
            }
            jsso.UpdateSlider();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CurrentSchedule = new List<int>();
        LastSchedule = new List<int>();
        jsd = transform.GetComponent<JobShopData>();
    }

    // Update is called once per frame
    void Update()
    {
        if (jsd.RunAlgorithm)
        {
            MinimizeMakespan();
        }
    }
}
