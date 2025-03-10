using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string playSceneName;
    [SerializeField] private GameObject defaultScreen;
    private GameObject currentLevel;
    
    private void Awake()
    {
        currentLevel = defaultScreen;
    }
    
    public void PlayGame()
    {
        if(CrossFade.instance != null) CrossFade.instance.StartCrossFade(LoadLevel, null, 1);
        else
        {
            LoadLevel();
        }
    }
    
    public void LoadLevel()
    {
        
        SceneManager.LoadScene(playSceneName);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void OpenNewMenu(GameObject _new)
    {
        currentLevel.SetActive(false);
        currentLevel = _new;
        currentLevel.SetActive(true);
        
        
    }
}
