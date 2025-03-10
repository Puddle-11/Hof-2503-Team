using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] public GameObject menuPause;
    [SerializeField] public GameObject menuActive;
    [SerializeField] public GameObject menuWin;
    [SerializeField] public GameObject menuLose;

    [SerializeField] public float timer = 600f;
    [SerializeField] TMP_Text enemyCountText;
    public Image playerHPBar;

    public GameObject player;

    public bool isPaused;
    int enemyCount;

    // Start is called before the first frame update
    void Awake() // awake opens before start
    {
        Instance = this;
        player = GameObject.FindWithTag("Player"); //Try to use tags instead of strings, less of them, so it is easier to find
    }

    // Update is called once per frame
    void Update()
    {
        time();

        if (Input.GetButtonDown("Cancel"))
        {
            if (menuActive == null)
            {
                statePause();
                menuActive = menuPause;
                menuActive.SetActive(isPaused);
            }
            else if (menuActive == menuPause)
            {
                stateUnPause();
            }
        }

    }

    public void time()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            youLose();
        }
    }

    public void statePause()
    {
        isPaused = !isPaused;
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void stateUnPause()
    {
        isPaused = !isPaused;
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        menuActive.SetActive(isPaused);
        menuActive = null;
    }

    public void updateGameGoal(int amount)
    {
        enemyCount += amount;
        enemyCountText.text = enemyCount.ToString("F0");

        if(enemyCount <= 0)
        {
            statePause();
            menuActive = menuWin;
            menuActive.SetActive(isPaused);
        }
    }

    public void youLose()
    {
        statePause();
        menuActive = menuLose;
        menuActive.SetActive(isPaused);
    }
}
