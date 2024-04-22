using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalPickup : MonoBehaviour, IPickupPoint
{
    [SerializeField]
    MMF_Player pickupFeedback;
    public GameObject OnPickUp(GameObject inHand)
    {
        if (inHand == null)
        {
            // Play pickup animation
            // Return this object to the player.
            GameObject instance = AssetManager.Instance.CoalPool.GetItemInstance();
            if (pickupFeedback != null)
            {
                pickupFeedback.PlayFeedbacks();
            }

            return instance; 
        } 
        else
        {
            return null;
        }
    }
}
