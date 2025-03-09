using System;
using UnityEngine;

public class Dream : MonoBehaviour
{
   private bool inRange;
   [SerializeField] private float dreamValue;
   [SerializeField] private GameObject prompt;
   public void Update()
   {
      if (Input.GetKeyDown(KeyCode.E) && inRange)
      {
         DreamInvntory.instance.CollectDream(dreamValue);
         Destroy(gameObject);
      }
      prompt.SetActive(inRange);
   }
   
   public void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.tag == "Player")
      {
         inRange = true;
      }
   }
   public void OnTriggerExit(Collider other)
   {
      if (other.gameObject.tag == "Player")
      {
         inRange = false;

      }
   }
   
}
