using UnityEngine;
using System.Collections;

public class NoiseDevice : MonoBehaviour
{
    [SerializeField] AudioSource AudioDevice;
    [SerializeField] AudioClip[] AudioClips;
    [SerializeField] SphereCollider SphereRadius;

    private bool inProximity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Random.InitState(25);
        AudioDevice.enabled = true;
        AudioDevice.PlayOneShot(AudioClips[Random.Range(0, AudioClips.Length)]);

        inProximity = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (inProximity)
        {
           if (Input.GetKeyDown(KeyCode.E))
           {
               AudioDevice.Stop();
               AudioDevice.enabled = false;
           }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inProximity = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inProximity = false;
        }
    }
}
