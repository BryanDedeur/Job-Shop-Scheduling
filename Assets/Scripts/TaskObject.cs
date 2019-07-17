using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskObject : MonoBehaviour
{

    private int StartTime;
    private int Duration;
    private int EndTime;

    //private TaskObject PreviousTask;
    private TaskObject NextTask;

    private JobObject job;
    private MachineObject Machine;

    // Constructor
    public TaskObject()
    {

    }
}
