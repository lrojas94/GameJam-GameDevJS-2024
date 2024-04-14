using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalItem : MonoBehaviour, IHoldable, IPickupPoint
{
    [SerializeField]
    private float throwSpeed = 1f;

    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnPutDown(GameObject player)
    {
        Vector3 forceDirection = player.transform.forward;
        rb.AddForce(forceDirection * throwSpeed, ForceMode.Force);
    }

    public GameObject OnPickUp(GameObject inHand)
    {
        if (inHand == null)
        {
            return gameObject;
        }

        return null;
    }
}
