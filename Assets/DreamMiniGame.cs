using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DreamMiniGame : MonoBehaviour
{

    [SerializeField] GameObject Cursor;
    [SerializeField] private float speed;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    private float target;
    private bool movingLeft;
    [SerializeField] private Transform arrow;
    private RectTransform rectTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = minX;
        rectTransform = arrow.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveCursor();
    }

    void MoveCursor()
    {
        
        if (rectTransform.localPosition.x < minX )
        {
            movingLeft = true;
            target = maxX;
        }
        if (rectTransform.localPosition.x > maxX)
        {
            movingLeft = false;

            target = minX;
        }
        
        
        rectTransform.localPosition += new Vector3( movingLeft ?  speed : -speed, 0, 0);
        
    }
}
