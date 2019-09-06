using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakespanPlotter : MonoBehaviour
{
    public JobShopAlgorithm jsa;
    public GameObject image;
    private RectTransform rect;
    public GameObject slider;

    private Vector2 lastPos;
    private bool firstDot = true;

    private float xMax = 0;
    private float yMax = 0;

    private GameObject tempDot;
    private GameObject tempLine;

    private List<int> xValues;
    private List<int> yValues;
    private RectTransform sliderRect;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        sliderRect = slider.GetComponent<RectTransform>();
        lastPos = new Vector2();
        xValues = new List<int>();
        yValues = new List<int>();
    }

    public void UpdateSlider(long itteration) {
        float x = rect.sizeDelta.x * (float) itteration / (float) xMax;
        sliderRect.anchoredPosition = new Vector3(x, 10f);

        if (itteration > xMax - 1) {
            if (tempDot != null) 
                Destroy(tempDot);

            if (tempLine != null) 
                Destroy(tempLine);

             firstDot = true;
        }
    }

    private GameObject CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB, Color ColorRef) {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(transform, false);
        gameObject.GetComponent<Image>().color = new Color(1,1,1, .5f);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 1f);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);

        Image image = gameObject.GetComponent<Image>();
        image.color = ColorRef;

        return gameObject;
    }

    public void ShowSample(long itteration, int makespan) {
         
        if (tempDot != null) 
            Destroy(tempDot);

        if (tempLine != null) 
            Destroy(tempLine);

        tempDot = Instantiate(image, transform);
        tempDot.SetActive(true);

        RectTransform dotrect = tempDot.GetComponent<RectTransform>();
        float x = rect.sizeDelta.x * (float) itteration / (float) xMax;
        float y = rect.sizeDelta.y * (float) makespan / yMax;
        dotrect.anchoredPosition = new Vector3(x, y);
        if (lastPos != new Vector2())
         tempLine = CreateDotConnection(lastPos, new Vector2(x, y), new Color(1,0,0));
        
    }

    public void PlotDot(long itteration, int makespan) {
        
        xMax = jsa.longestIteration;

        yMax = jsa.longestIteration * 1.25f;


        GameObject dot = Instantiate(image, transform);
        dot.SetActive(true);

        RectTransform dotrect = dot.GetComponent<RectTransform>();
        float x = rect.sizeDelta.x * (float) itteration / (float) xMax;
        float y = rect.sizeDelta.y * (float) makespan / yMax;
        dotrect.anchoredPosition = new Vector3(x, y);

        if (firstDot) {
            firstDot = false;
        } else {
            Color newColor = new Color(1,1,1);
            newColor.a = .5f;
            if (lastPos != new Vector2())
                CreateDotConnection(lastPos, new Vector2(x, y), newColor);
        }
        lastPos = new Vector2(x, y);
        xValues.Add((int) x);
        yValues.Add((int) y);
        
    }

    string ConvertListToString(ref List<int> passedList)
    {
        string scheduleString = "";
        for (int i = 0; i < passedList.Count; i++)
        {
            scheduleString = scheduleString + passedList[i] + ", ";
        }
        return scheduleString;
    }

    public void PrintResults() {
        print("Itteration, " + ConvertListToString(ref xValues));
        print("Makespan, " + ConvertListToString(ref yValues));
    }


}
