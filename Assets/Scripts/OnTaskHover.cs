using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnTaskHover : MonoBehaviour
{
//    private RectTransform rectTransform;
//    private Vector2 previousSize;
//    private bool hitting;
//    private Color colorValue;
//    private Image image;
//    GameObject textitem;
//
//
//    private void OnMouseHit()
//    {
//        colorValue = image.color;
//        image.color = new Color(image.color.r / 2, image.color.g / 2, image.color.b / 2);
//
//        // rectTransform.sizeDelta = rectTransform.sizeDelta * 2;
//        RectTransform rect = textitem.GetComponent<RectTransform>();
//        rect.position = transform.position;
//
//        GameObject text = transform.Find("DescriptionText").gameObject;
//        text.SetActive(true);
//
//    }
//
//    private void OnMouseLeft()
//    {
//        image.color = colorValue;
//        //rectTransform.sizeDelta = rectTransform.sizeDelta * .5f;
//
//        GameObject text = transform.Find("DescriptionText").gameObject;
//        text.SetActive(false);
//    }
//
//    private void Start()
//    {
//        rectTransform = GetComponent<RectTransform>();
//        image = GetComponent<Image>();
//        hitting = false;
//        textitem = transform.Find("DescriptionText").gameObject;
//
//    }
//
//    private void Update()
//    {
//        Vector2 localMousePosition = rectTransform.InverseTransformPoint(Input.mousePosition);
//        if (rectTransform.rect.Contains(localMousePosition))
//        {
//            if (!hitting)
//            {
//                OnMouseHit();
//                hitting = true;
//            }
//
//        }
//        else
//        {
//            if (hitting)
//            {
//                OnMouseLeft();
//                hitting = false;
//            }
//        }
//    }
}
