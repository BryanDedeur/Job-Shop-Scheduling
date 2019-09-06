using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPrinter : MonoBehaviour
{
    public Test test;

    public void updateBestMakespan(float newValue)
    {
        print(test.bestMakespan);
    }
}
