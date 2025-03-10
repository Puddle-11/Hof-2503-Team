using System;
using UnityEngine;
public class Dream : MonoBehaviour
{
	private bool inRange;
	[SerializeField] private float dreamValue;
	[SerializeField] private GameObject prompt;
	public delegate void FailFunc();
	public FailFunc failFuncEvent;
	public void Update()
	{
		
		if (!DreamMiniGame.instance.ingame)
		{
			if (Input.GetKeyDown(KeyCode.E) && inRange)
			{
				DreamMiniGame.instance.InitSet(Collect, FailCollect);
			}
		}
		prompt.SetActive(inRange);
	}
	
	public void Collect()
	{
		DreamInvntory.instance.CollectDream(dreamValue);
		Destroy(gameObject);
	}
	
	public void FailCollect()
	{
		failFuncEvent?.Invoke();
		Destroy(gameObject);
		
	}
	
	public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			inRange = true;
		}
	}
	
	public void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			inRange = false;
		}
	}
}