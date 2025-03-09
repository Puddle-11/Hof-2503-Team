using UnityEngine;

public class Billboard : MonoBehaviour
{
  
    void Update()
    {
        transform.LookAt(PlayerController.instance.transform.position);
    }
}
