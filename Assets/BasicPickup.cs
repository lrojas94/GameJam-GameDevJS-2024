using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPickup : MonoBehaviour, IPickupPoint
{
    [SerializeField]
    MMF_Player pickupFeedback;
    public GameObject OnPickUp(GameObject inHand)
    {
        if (inHand == null)
        {
            Debug.Log("Pick me up");
            if (pickupFeedback != null)
            {
                pickupFeedback.PlayFeedbacks();
            }

            return gameObject;
        }

        return null;
    }
}
