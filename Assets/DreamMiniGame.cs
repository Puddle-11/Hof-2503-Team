using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DreamMiniGame : MonoBehaviour
{

    [SerializeField] GameObject Cursor;
    Vector3 LeftBoundry;
    Vector3 RightBoundry;
    Vector3 Move;
    Vector3 Temp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LeftBoundry = new Vector3(-123, 30, 0);
        RightBoundry = new Vector3(123, 30, 0);
    }

    // Update is called once per frame
    void Update()
    {
        MoveCursor();
    }

    void MoveCursor()
    {
        Move = Cursor.transform.position;
        Temp = new Vector3(Move.x * 1.25f * Time.deltaTime, Move.y, Move.z);
        Cursor.transform.position = Temp;
    }
}
