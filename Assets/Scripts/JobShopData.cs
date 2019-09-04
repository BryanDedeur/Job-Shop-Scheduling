using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JobShopData : MonoBehaviour
{
    // Public members
    public string Description;
    public bool RandomSwap;
    public bool RandomInvert;

    // Scheduling Generation Variables
    public bool RunAlgorithm;

//    public string BestSchedule;
    public int BestMakespan;
    public long IterationNumber;
    public int NumIterationPerRender = 1;
    public int IterationCap;
    public int CurrentSample;
    public int NumSamples = 30;

    // Private members
    public int NumJobs;
    public int NumMachines;
    public int NumTasks;

    // Scheduling Generation Variables
    public List<int> DefaultListSchedule;
    public List<int> BestListSchedule;

    // Job Task Corresponding Matrixs (first list is task, nested list is job)
    public List<List<int>> JobTaskIDs;
    public List<List<int>> JobTaskMachines;
    public List<List<int>> JobTaskDurations;
    public List<List<int>> JobTaskEndTimes;

    // For tracking the task indexs
    public List<int> NextJobTaskIndexs;
    public List<int> NextMachineTaskIndexs;

    // Machine Task Corresponding Matrixs
    public List<List<int>> MachineTaskEndTimes;

    // Data Collecting
    //public List<List<int>> ScheduleMakespanRunningAverage;

    public GameObject BestScheduleTextField;

    private UIUpdater UIUpdater;

    public void ResetData()
    {
        RunAlgorithm = false;
        //BestSchedule = "";
        IterationNumber = 0;
        //MaxIterations = 999999;
        CurrentSample = 0;

        NumJobs = 0;
        NumMachines = 0;
        NumTasks = 0;

        BestListSchedule = new List<int>();
        BestMakespan = 999999; // something huge

        JobTaskIDs = new List<List<int>>();
        JobTaskMachines = new List<List<int>>();
        JobTaskDurations = new List<List<int>>();
        JobTaskEndTimes = new List<List<int>>();
        NextJobTaskIndexs = new List<int>();

        MachineTaskEndTimes = new List<List<int>>();
        NextMachineTaskIndexs = new List<int>();

        UpdateVisuals();
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

    public void UpdateVisuals()
    {
        UIUpdater.UpdateTextUI("ShopDetailsText", 
             Description + "\n" +
                    "Jobs: " + NumJobs.ToString() + "\n" +
                    "Machines: " + NumMachines.ToString() + "\n" +
                    "Tasks: " + NumTasks.ToString());
        UIUpdater.UpdateTextUI("ScheduleDetailsText",
            "Best Schedule Makespan: " + BestMakespan.ToString() + "\n" +
            "Average Iterations Until Minimized Makespan: " + "\n" +
            "Running Average Minimize Success Rate: " + "\n");
        
        BestScheduleTextField.transform.GetComponent<InputField>().text = ConvertListToString(ref BestListSchedule);
    }

    // Start is called before the first frame update
    void Start()
    {
        UIUpdater = GetComponent<UIUpdater>();
        ResetData();
    }
}
