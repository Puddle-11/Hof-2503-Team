using UnityEngine;

public class SinWaveBob : MonoBehaviour
{
    [SerializeField] private float bobSpeed;
    [SerializeField] private float bobMag;
    private Vector3 offsetPos;
    void Start()
    {
        offsetPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = offsetPos + new Vector3(0, Mathf.Sin(Time.time * bobSpeed) * bobMag ,0);
    }
}
