using System;
using UnityEngine;
public class HouseCollider : MonoBehaviour
{
	private bool inBounds;
	[SerializeField] private Transform doorstepRespawnPoint;
	private bool failed;
	[SerializeField] private GameObject houseFront;
	[SerializeField] private Dream[] allDreams;
	
	public void OnEnable()
	{
		for (int i = 0; i < allDreams.Length; i++)
		{
			allDreams[i].failFuncEvent += HouseFailed;
		}
	}
	
	public void OnDisable()
	{
		for (int i = 0; i < allDreams.Length; i++)
		{
			allDreams[i].failFuncEvent -= HouseFailed;
		}
	}
	
	private void Update()
	{
		if (failed && inBounds)
		{
			inBounds = false;
			if(CrossFade.instance != null) CrossFade.instance.StartCrossFade(MovePLayer, null, 0.5f);
			else
			{
				MovePLayer();
			}			
		}
	}
	
	private void MovePLayer()
	{
		PlayerController.instance.gameObject.transform.position = doorstepRespawnPoint.position;
		
	}
	
	void HouseFailed()
	{
		failed = true;
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			inBounds = true;
			houseFront.SetActive(false);
			
		}
	}
	
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			inBounds = false;
			houseFront.SetActive(true);

		}
	}
}