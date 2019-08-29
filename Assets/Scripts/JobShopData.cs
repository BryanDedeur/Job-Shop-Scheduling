using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobShopData : MonoBehaviour
{   
    // Public members
    [TextArea(7, 51)]
    public string ScheduleInput;
    public string Description;
    public bool RandomSwap;
    public bool RandomInvert;

    // Scheduling Generation Variables
    // public bool RestartAlgorithm;
    // public bool RunAlgorithm;
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
    private int CurrentMakespan;

    // Job Task Corresponding Matrixs (first list is task, nested list is job)
    public List<List<int>> JobTaskIDs;
    public List<List<int>> JobTaskMachines;
    public List<List<int>> JobTaskDurations;
    public List<List<int>> JobTaskEndTimes;

    // For tracking the task indexs
    private List<int> NextJobTaskIndexs;
    private List<int> NextMachineTaskIndexs;

    // Machine Task Corresponding Matrixs
    public List<List<int>> MachineTaskEndTimes;

    // Data Collecting
    public List<List<int>> ScheduleMakespanRunningAverage;

    public void ClearAllData() {
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
    }

    // Start is called before the first frame update
    void Start()
    {
        ClearAllData();
    }
}
