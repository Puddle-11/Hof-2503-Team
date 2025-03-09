using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class DreamMiniGame : MonoBehaviour
{

    [SerializeField] GameObject Cursor;
    [SerializeField] private float speed;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float bonusPadding;
    private float target;
    private bool movingLeft;
    private bool isKeyDown;
    private bool tookDream;
    [SerializeField] private Transform arrow;
    [SerializeField] private Transform inBounds;
    private RectTransform arrowRectTransform;
    private RectTransform inBoundsRectTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = minX;
        isKeyDown = false;
        tookDream = false;
        arrowRectTransform = arrow.GetComponent<RectTransform>();
        inBoundsRectTransform = inBounds.GetComponent<RectTransform>();

        float rtX = inBoundsRectTransform.sizeDelta.x;
        inBoundsRectTransform.sizeDelta = new Vector2(rtX += bonusPadding, inBoundsRectTransform.sizeDelta.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isKeyDown = true;
        }
        MoveCursor();
    }

    bool GetTookDream()
    {
        return tookDream;
    }

    void MoveCursor()
    {
        if (!isKeyDown) 
        {
            if (arrowRectTransform.localPosition.x < minX)
            {
                movingLeft = true;
                target = maxX;
            }
            if (arrowRectTransform.localPosition.x > maxX)
            {
                movingLeft = false;

                target = minX;
            }

            arrowRectTransform.localPosition += new Vector3(movingLeft ? speed : -speed, 0, 0);
        }
        else
        {
            if(arrowRectTransform.localPosition.x < (37f + (bonusPadding / 2)) && arrowRectTransform.localPosition.x > (-37f - (bonusPadding / 2)))
            {
                Debug.Log("Success!");
                tookDream = true;
            }
            else
            {
                Debug.Log("Failure!");
            }
        }
    }
}
