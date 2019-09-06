using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;
 
 
public class Test : MonoBehaviour
{

    public float bestMakespan = 0;
 
    public void UpdateBestmakespan (float newData)
    {
        bestMakespan = newData;
        MyOwnEventTriggered(bestMakespan);
    }
    
    //my event
    [Serializable]
    public class CustomEvent : UnityEvent { }
 
    [SerializeField]
    private CustomEvent valueChangedEvent = new CustomEvent();
    public CustomEvent onValueChangedEvent(float newValue)
    {
        get { return valueChangedEvent; } set { valueChangedEvent = value; }
    }
 
    public void MyOwnEventTriggered(float bestMakespan)
    {
        onValueChangedEvent(bestMakespan);
    }
 
}