using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

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

    private int[] JobProcessingTimes;


    // Start is called before the first frame update
    void Start()
    {
        if (JobMatrix == "")
        {
            print("Matrix is empty");
        } else
        {
            string description = JobMatrix.Split("\n"[0])[0];
            //int numJobs = JobMatrix.Split("\n"[0])[1];

            var textLines = JobMatrix.Split("\n"[0]);
            for (int i = 0; i < textLines.Length; i++)
            {
                print(textLines[i]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
