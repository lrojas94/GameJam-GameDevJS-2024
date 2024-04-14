using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalPickup : MonoBehaviour, IPickupPoint
{
    [SerializeField]
    private GameObject coalPrefab;

    public GameObject OnPickUp(GameObject inHand)
    {
        if (inHand == null)
        {
            // Play pickup animation
            // Return this object to the player.
            GameObject instance = GameObject.Instantiate(coalPrefab);
            return instance; 
        } 
        else
        {
            return null;
        }
    }
}
