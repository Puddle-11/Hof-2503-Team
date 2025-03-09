using UnityEngine;

public class Billboard : MonoBehaviour
{
  
    void Update()
    {
        transform.LookAt(PlayerController.instance.mainCam.transform.position);
    }
}
