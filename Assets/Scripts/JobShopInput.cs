using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobShopInput : MonoBehaviour
{
    public JobShopData jsd;
    public JobShopAlgorithm jsa;

    void ParseData()
    {
        jsd.ClearAllData();

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
}
