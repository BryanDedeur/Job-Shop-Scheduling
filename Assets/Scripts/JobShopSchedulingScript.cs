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

    private List<List<int>> JobSequence;

    private void ParseMatrix()
    {
        char[] separators = new char[] { ' ' };

        JobSequence = new List<List<int>>();

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
                    List<int> temp = new List<int>();

                    foreach (var value in textLines[i].Split(separators, StringSplitOptions.RemoveEmptyEntries))
                    {
                        int number;

                        bool success = Int32.TryParse(value, out number);
                        if (success)
                        {
                            Console.WriteLine("Converted '{0}' to {1}.", value, number);
                            temp.Add(number);
                        }
                        else
                        {
                            Console.WriteLine("Attempted conversion of '{0}' failed.",
                                               value ?? "<null>");
                            break;
                        }
                    }
                    JobSequence.Add(temp);
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ParseMatrix();
        print(MatrixDescription);
        for (int x = 0; x < NumJobs; x++)
        {
            string printStuff = "";
            for (int y = 0; y < NumMachines * 2; y++)
            {
                printStuff = printStuff + JobSequence[x][y] + " ";
            }
            print(printStuff);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
