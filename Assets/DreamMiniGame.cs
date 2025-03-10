using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
public class DreamMiniGame : MonoBehaviour
{
	
	public static DreamMiniGame instance;
	[SerializeField] GameObject Cursor;
	[SerializeField] private float speed;
	[SerializeField] private float minX;
	[SerializeField] private float maxX;
	[SerializeField] private float bonusPadding;
	private float target;
	private bool movingLeft;
	[SerializeField] private float baseDelta;
	[SerializeField] private Transform arrow;
	[SerializeField] private Transform inBounds;
	[SerializeField] private Transform activeParent;
	private RectTransform arrowRectTransform;
	private RectTransform inBoundsRectTransform;
	private Action currSLink;
	private Action currFLink;
	public bool ingame;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
		arrowRectTransform = arrow.GetComponent<RectTransform>();
		inBoundsRectTransform = inBounds.GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update()
	{
		if (ingame)
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				PressKey();
			}
		}
		MoveCursor();
	}
	
	public void InitSet(Action _Slink, Action _FLink)
	{
		StartCoroutine(setIngame(true));
		arrowRectTransform.anchoredPosition = new Vector2(0, arrowRectTransform.anchoredPosition.y);
		currSLink = _Slink;
		currFLink = _FLink;
		inBoundsRectTransform.sizeDelta = new Vector2(baseDelta + bonusPadding, inBoundsRectTransform.sizeDelta.y);
		activeParent.gameObject.SetActive(true);
		Time.timeScale = 0;
	}
	
	void MoveCursor()
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
	
	void PressKey()
	{
		
		if (arrowRectTransform.localPosition.x < (37f + (bonusPadding / 2)) &&
		    arrowRectTransform.localPosition.x > (-37f - (bonusPadding / 2)))
		{
			currSLink?.Invoke();
			
		}
		else
		{
			currFLink?.Invoke();
		}
		
		StartCoroutine(setIngame(false));
		activeParent.gameObject.SetActive(false);
		Time.timeScale = 1;
	}
	
	public IEnumerator setIngame(bool _state)
	{
		yield return null;
		ingame = _state;
		
		
	}
}