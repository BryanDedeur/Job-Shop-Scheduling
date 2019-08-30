using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JobShopInput : MonoBehaviour
{
    public GameObject jobShopData;
    private JobShopData jsd;
    public GameObject InputField;

    public void ParseData()
    {
        string text = InputField.GetComponent<InputField>().text;

        if (!String.IsNullOrEmpty(text))
        {
            jsd.ResetData();
            char[] separators = new char[] {' '};

            int rowColIndex = 0;
            string[] textLines = text.Split("\n"[0]);
            for (int i = 0; i < textLines.Length; i++)
            { 
                if (i == 0 && textLines[i].Length > 20)
                {
                    jsd.Description = textLines[i]; // capture the description
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
                                jsd.NumJobs = Convert.ToInt32(token);
                                break;
                            case 2:
                                jsd.NumMachines = Convert.ToInt32(token);
                                jsd.NumTasks = jsd.NumJobs * jsd.NumMachines;
                                for (int task = 0; task < jsd.NumTasks; task++)
                                {
                                    jsd.BestListSchedule.Add(task);
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
                                tempJobTaskTaskIDs.Add(jsd.BestListSchedule[((jobIndex) * (taskIndex + 1)) - 1]);
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

                    jsd.NextJobTaskIndexs.Add(0);

                    jsd.JobTaskIDs.Add(tempJobTaskTaskIDs);
                    jsd.JobTaskDurations.Add(tempJobTaskDurations);
                    jsd.JobTaskMachines.Add(tempJobTaskMachines);
                    jsd.JobTaskEndTimes.Add(tempJobTaskEndTimes);
                }
            }

            for (int machine = 0; machine < jsd.NumMachines; machine++)
            {
                List<int> tempMachineEndTimes = new List<int>();
                for (int task = 0; task < jsd.NumJobs; task++)
                {
                    tempMachineEndTimes.Add(0);
                }

                jsd.MachineTaskEndTimes.Add(tempMachineEndTimes);
                jsd.NextMachineTaskIndexs.Add(0);
            }
            //MakeScheduleFromLast();
            jsd.UpdateVisuals();
        }
    }

    private void Start()
    {
        jsd = jobShopData.GetComponent<JobShopData>();
    }
}
