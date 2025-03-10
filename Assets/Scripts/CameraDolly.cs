using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CameraDolly : MonoBehaviour
{
	[SerializeField] private AnimationCurve relativeSpeed;
	[SerializeField] private Vector3 dir;
	[SerializeField] private float totalTime;
	private float timer;
	[SerializeField] private float minDist;
	[SerializeField] private float maxDist;
	[SerializeField] private GameObject target;
	[SerializeField] private string nextScene;
	[SerializeField] private float sceneHangTime;
	private bool changeSceneQueued;
    // Update is called once per frame
    void Update()
    {
	    timer = Mathf.Clamp(timer + Time.deltaTime, 0, totalTime);
	    // Vector3 position = dir.normalized * relativeSpeed.Evaluate(((timer / totalTime) ) * (maxDist - minDist) + minDist);
	    Vector3 position = dir.normalized * (relativeSpeed.Evaluate(timer/totalTime) * (maxDist - minDist) + minDist);
	    transform.position = target.transform.position + position;
	    transform.LookAt(target.transform.position);
	    if (timer / totalTime > 0.99f)
	    {
		    StartCoroutine(StartSceneChange());
	    }
    }
    
    private IEnumerator StartSceneChange()
    {
	    if(changeSceneQueued) yield break;
	    
	    changeSceneQueued = true;
	    yield return new WaitForSeconds(sceneHangTime);
	    CrossFade.instance.StartCrossFade(ChangeScene, null, 1);
	    
	    changeSceneQueued = false;
    }
    
    private void ChangeScene()
    {
	    
	    SceneManager.LoadScene(nextScene);
    }
    public void OnDrawGizmos()
    {
	    Debug.DrawRay(target.transform.position, dir.normalized * 5);
    }
}
