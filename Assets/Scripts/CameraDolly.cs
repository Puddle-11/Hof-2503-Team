using System;
using Unity.VisualScripting;
using UnityEngine;

public class CameraDolly : MonoBehaviour
{
	[SerializeField] private AnimationCurve relativeSpeed;
	[SerializeField] private Vector3 dir;
	[SerializeField] private float totalTime;
	private float timer;
	[SerializeField] private float minDist;
	[SerializeField] private float maxDist;
	[SerializeField] private GameObject target;

    // Update is called once per frame
    void Update()
    {
	    timer = Mathf.Clamp(timer + Time.deltaTime, 0, totalTime);
	    // Vector3 position = dir.normalized * relativeSpeed.Evaluate(((timer / totalTime) ) * (maxDist - minDist) + minDist);
	    Vector3 position = dir.normalized * (relativeSpeed.Evaluate(timer/totalTime) * (maxDist - minDist) + minDist);
	    transform.position = target.transform.position + position;
	    transform.LookAt(target.transform.position);
    }
    
    public void OnDrawGizmos()
    {
	    Debug.DrawRay(target.transform.position, dir.normalized * 5);
    }
}
