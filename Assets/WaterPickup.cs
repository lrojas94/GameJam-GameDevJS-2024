using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPickup : MonoBehaviour, IPickupPoint
{
    [SerializeField]
    MMF_Player waterPickupFeedback;
    public GameObject OnPickUp(GameObject inHand)
    {
        Debug.Log(inHand);
        if (inHand != null)
        {
            if (inHand.CompareTag("WaterBucket"))
            {
                WaterBucket bucket = inHand.GetComponent<WaterBucket>();
                if (bucket != null) {
                    bucket.Fill();
                    if (waterPickupFeedback != null)
                    {
                        waterPickupFeedback.PlayFeedbacks();
                    }
                }
            }
            return null;
        }
        else
        {
            return null;
        }
    }
}
