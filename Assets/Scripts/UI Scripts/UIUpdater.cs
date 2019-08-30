using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUpdater : MonoBehaviour
{
    public void UpdateTextUI(string gameObjectName, string newText)
    {
        GameObject.Find(gameObjectName).GetComponent<Text>().text = newText;
    }
}
