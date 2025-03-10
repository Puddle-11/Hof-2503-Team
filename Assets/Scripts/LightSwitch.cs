using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightSwitch : MonoBehaviour
{

    [SerializeField] private GameObject txtToDisplay; // Text to display when player is near switch

    private bool isInTheZone = true; // Is the player in the zone of the switch

    [SerializeField] public GameObject lights; // lights controlled by this switch

    private void Start()
    {

        isInTheZone = false; // default switch to the off position when the player is not in the zone

        txtToDisplay.SetActive(false); // Hide the text

    }

    private void Update()
    {
        if (isInTheZone && Input.GetKeyDown(KeyCode.E)) // Toggle the lights on and off
        {
            lights.SetActive(!lights.activeSelf); // Toggle the lights
            gameObject.GetComponent<AudioSource>().Play(); // Play the sound
            gameObject.GetComponent<Animation>().Play("switch"); // Play the animation
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") // If the player enters the zone
        {
            isInTheZone = true; // Player is in the zone
            txtToDisplay.SetActive(true); // Show the text
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") // If the player exits the zone
        {
            isInTheZone = false; // Player isn't in the zone
            txtToDisplay.SetActive(false); // Hide the text
        }
    }

}
