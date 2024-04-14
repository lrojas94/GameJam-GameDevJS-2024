using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPickup : MonoBehaviour, IPickupPoint
{
    public GameObject OnPickUp(GameObject inHand)
    {
        Debug.Log(inHand);
        if (inHand != null)
        {
            if (inHand.CompareTag("WaterBucket"))
            {
                Debug.Log("I am a water bucket!");
            }
            return null;
        }
        else
        {
            return null;
        }
    }
}
