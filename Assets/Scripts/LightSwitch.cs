using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightSwitch : MonoBehaviour
{

    public GameObject txtToDisplay; // Text to display when player is near switch

    private bool isInTheZone = true; // Is the switch on or off

    public GameObject lights; // lights controlled by this switch

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
            isInTheZone = true; // Set the switch to on
            txtToDisplay.SetActive(true); // Show the text
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") // If the player exits the zone
        {
            isInTheZone = false; // Set the switch to off
            txtToDisplay.SetActive(false); // Hide the text
        }
    }

}
