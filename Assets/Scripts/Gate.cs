using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private bool useDreamCount;
    [SerializeField] private int countNeeded;
    [SerializeField] private Dream dreamTrigger;
    [SerializeField] private GameObject gateObj;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
    void Update()
    {
        
        if (!useDreamCount)
        {
            if (dreamTrigger == null)
            {
                gateObj.SetActive(false);
                
            }
        }
        else
        {
            if (DreamInvntory.instance.dreamBar >= countNeeded)
            {
                gateObj.SetActive(false);
            }
        }
    }
}
