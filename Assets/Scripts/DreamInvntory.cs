using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class DreamInvntory : MonoBehaviour
{
    public static DreamInvntory instance;
    [SerializeField] private Slider fillBar;
    [SerializeField] private TextMeshProUGUI textObject;
    public float dreamsNeeded;
    public float dreamBar;
    public bool endSequence;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Two dream inventory instances found");
            Destroy(gameObject);
        }
    }
    
    private void Update()
    {
        fillBar.value = dreamBar / dreamsNeeded;
        textObject.text = dreamBar + "/" + dreamsNeeded;
        if (fillBar.value >= 1 && !endSequence)
        {
            endSequence = true;
            HitEndScreen();
        }
    }
    
    private void HitEndScreen()
    {
        Debug.Log("Finished Level");
       GameManager.Instance.statePause();
        GameManager.Instance.menuWin.SetActive(true);
    }

    public void youLose()
    {
        GameManager.Instance.timer -= Time.deltaTime;
        if (GameManager.Instance.timer <= 0)
        {
            youLose();
        }
        GameManager.Instance.statePause();
        GameManager.Instance.menuLose.SetActive(true);
    }

    public void CollectDream(float _val)
    {
        dreamBar += _val;
        
    }
 
}
