using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JobShopAlgorithmOld : MonoBehaviour
{


    // private JobShopSchedulerObject jsso;

    // // Take previous schedule and adjust stuff
    // void MakeScheduleFromLast()
    // {
    //     CurrentSchedule = LastSchedule;
    //     if (RandomSwap)
    //         CurrentSchedule = RandomSwapListItems(ref LastSchedule);

    //     if (RandomInvert)
    //         CurrentSchedule = RandomInvertListItems(ref LastSchedule);
    // }

    // // Picks two random list items and randomly swaps them
    // List<int> RandomInvertListItems(ref List<int> passedList)
    // {
    //     List<int> newSwappedList = passedList;

    //     int randomIndex1 = (int) UnityEngine.Random.Range(0f, NumTasks);
    //     int randomIndex2 = (int) UnityEngine.Random.Range(0f, NumTasks);
    //     int j = (int) Mathf.Max((float) randomIndex1, (float) randomIndex2);
    //     for (int i = (int) Mathf.Min((float) randomIndex1, (float) randomIndex2); i < j; i++) {
    //         int temp = newSwappedList[i];
    //         newSwappedList[i] = newSwappedList[j];
    //         newSwappedList[j] = temp;
    //         j--;
    //     }

    //     return newSwappedList;
    // }

    // // Picks two random list items and randomly swaps them
    // List<int> RandomSwapListItems(ref List<int> passedList)
    // {
    //     List<int> newSwappedList = passedList;

    //     int randomIndex1 = (int) UnityEngine.Random.Range(0f, NumTasks);
    //     int randomIndex2 = (int) UnityEngine.Random.Range(0f, NumTasks);

    //     int temp = newSwappedList[randomIndex1];
    //     newSwappedList[randomIndex1] = newSwappedList[randomIndex2];
    //     newSwappedList[randomIndex2] = temp;

    //     return newSwappedList;
    // }


    // // Procudes a makespan based on the passed schedule
    // int ComputeScheduleMakespan(ref List<int> schedule)
    // {

    //     for (int i = 0; i < NumJobs; i++)
    //     {
    //         NextJobTaskIndexs[i] = 0;
    //     }

    //     for (int i = 0; i < NumMachines; i++)
    //     {
    //         NextMachineTaskIndexs[i] = 0;
    //     }

    //     for (int index = 0; index < NumTasks; index++)
    //     {
    //         //print(schedule[index]);
    //         int currentJob = schedule[index] / NumMachines; // this needs to be fixed doesnt work
    //         int currentJobTask = NextJobTaskIndexs[currentJob]; 
    //         //print(currentJobTask); //------------------------------------------------------------------------------------------------------------------------------------
    //         NextJobTaskIndexs[currentJob]++;
    //         int currentMachine = JobTaskMachines[currentJob][currentJobTask];
    //         int currentMachineTask = NextMachineTaskIndexs[currentMachine]; // this is causing errors
    //         NextMachineTaskIndexs[currentMachine]++;
    //         int duration = JobTaskDurations[currentJob][currentJobTask];

    //         // print("Current Machine: " + currentMachine);
    //         // print("Current Machine Task: " + currentMachineTask);
    //         if (currentMachineTask == 0 && currentJobTask == 0)
    //         {
    //             MachineTaskEndTimes[currentMachine][currentMachineTask] = duration;
    //             JobTaskEndTimes[currentJob][currentJobTask] = duration;
    //          } 
    //             else if (currentMachineTask > 0 && currentJobTask == 0)
    //             {
    //                 int previousMachineTaskEndTime = MachineTaskEndTimes[currentMachine][currentMachineTask - 1];
    //                 MachineTaskEndTimes[currentMachine][currentMachineTask] = previousMachineTaskEndTime + duration;
    //                 JobTaskEndTimes[currentJob][currentJobTask] = previousMachineTaskEndTime + duration;
    //             } else if (currentMachineTask == 0 && currentJobTask > 0)
    //             {
    //                 int previousJobTaskEndTime = JobTaskEndTimes[currentJob][currentJobTask - 1];
    //                 MachineTaskEndTimes[currentMachine][currentMachineTask] = previousJobTaskEndTime + duration;
    //                 JobTaskEndTimes[currentJob][currentJobTask] = previousJobTaskEndTime + duration; 
    //         }
    //         else if (currentMachineTask > 0 && currentJobTask > 0)
    //         {
    //             int previousJobTaskEndTime = JobTaskEndTimes[currentJob][currentJobTask - 1];
    //             int previousMachineTaskEndTime = MachineTaskEndTimes[currentMachine][currentMachineTask - 1];

    //             int greaterEndTime = Mathf.Max(previousJobTaskEndTime, previousMachineTaskEndTime);
    //             MachineTaskEndTimes[currentMachine][currentMachineTask] = greaterEndTime + duration;
    //             JobTaskEndTimes[currentJob][currentJobTask] = greaterEndTime + duration;
    //         }
    //     }

    //     int longestMakespan = 0;
    //     for (int i = 0; i < NumMachines; i++)
    //     {
    //         if (MachineTaskEndTimes[i][NumJobs - 1] > longestMakespan)
    //             longestMakespan = MachineTaskEndTimes[i][NumJobs - 1];
    //     }

    //     return longestMakespan;
    // }

    // private string generationOutput = "";
    // private string makespanOutput = "";

    // // Primary GA function
    // // @returns best makespan
    // int MinimizeMakespan()
    // {
    //     for (int i = 0; i < NumGenerationsPerRender; i++)
    //     {
    //         //print("Ran algorithm " + GenerationNumber + " times");
    //         GenerationNumber += 1;
    //         MakeScheduleFromLast();
    //         CurrentMakespan = ComputeScheduleMakespan(ref CurrentSchedule);

    //         // compare makespans and keep the better schedule
    //         if (CurrentMakespan < LastMakespan)
    //         {
    //             LastMakespan = CurrentMakespan;
    //             LastSchedule = CurrentSchedule;

    //             generationOutput += GenerationNumber + ", ";
    //             makespanOutput += CurrentMakespan + ", ";

    //             BestSchedule = ConvertListToString(ref LastSchedule);
    //             BestMakespan = LastMakespan;

    //             jsso.GenerateSchedule();

    //         }
    //         jsso.UpdateSlider();
    //     }
    //     return BestMakespan;
    // }

    // string ConvertListToString(ref List<int> passedList)
    // {
    //     string scheduleString = "";
    //     for (int i = 0; i < passedList.Count; i++)
    //     {
    //         scheduleString = scheduleString + passedList[i] + " ";
    //     }
    //     return scheduleString;
    // }

    // // Start is called before the first frame update
    // void Start()
    // {
    //     ReadScheduleInput();
    //     jsso = transform.GetComponent<JobShopSchedulerObject>();
    //     jsso.CreateObjects();

    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     if (RestartAlgorithm) {
    //         //ReadScheduleInput();

    //         RunAlgorithm = true;
    //     }

    //     if (RunAlgorithm && MaxGenerations > GenerationNumber)
    //     {
    //         BestMakespan = MinimizeMakespan();

    //     } else if (MaxGenerations == 0)
    //     {
    //         BestMakespan = MinimizeMakespan();
    //     }

    //     if (MaxGenerations <= GenerationNumber) {
    //         if (MaxSamples > CurrentSample) {
    //             CurrentSample += 1;
    //             GenerationNumber = 0;
    //             ReadScheduleInput();
    //         } else {
    //             if (RunAlgorithm) {
    //                 RunAlgorithm = false;
    //                 jsso.PrintResults();
    //             }

    //         }
    //     } else {
    //     }
    // }

    // private void OnApplicationQuit()
    // {
    //     Debug.Log(generationOutput);
    //     Debug.Log(makespanOutput);
    //     Debug.Log("Best makespan found is: " + BestMakespan + " units of time, after " + GenerationNumber + " generations");
    //     Debug.Log(BestSchedule);
    //     ComputeScheduleMakespan(ref LastSchedule); // so we have the results ready for display
    // }
}
