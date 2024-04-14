using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPickup : MonoBehaviour, IPickupPoint
{
    public GameObject OnPickUp(GameObject inHand)
    {
        if (inHand == null)
        {
            return gameObject;
        }

        return null;
    }
}
