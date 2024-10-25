using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
   private void OnCollisionEnter2D(Collision2D col)
   {
      PlayerController controller = col.gameObject.GetComponent<PlayerController>();
      if (controller != null)
      {
         controller.PickupKey();
         Destroy(gameObject);
      }
   }
}
