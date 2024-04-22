using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupController : MonoBehaviour
{
    [SerializeField]
    private float rayLength = 1.5f;
    [SerializeField]
    private GameObject holdingObject;
    [SerializeField]
    private Transform holdingObjectTransform;
    [SerializeField]
    private Transform raycastPosition;
    [SerializeField]
    private float raycastSize = 1f;

    private TopDown3DPlayerController controller;

    [SerializeField]
    MMF_Player throwFeedback;

    // Start is called before the first frame update
    private void Awake()
    {
        if (raycastPosition == null)
        {
            raycastPosition = transform;
        }

        if (controller  == null)
        {
            controller = gameObject.GetComponent<TopDown3DPlayerController>();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawWireCube(raycastPosition.position + raycastPosition.forward * rayLength, Vector3.one * raycastSize);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RaycastHit hit;
            int mask = LayerMask.GetMask("PickUp");
            bool hitFound = Physics.BoxCast(raycastPosition.position + raycastPosition.forward, Vector3.one / 2 * raycastSize, raycastPosition.TransformDirection(Vector3.forward), out hit, Quaternion.identity, rayLength, mask);
            // Does the ray intersect any objects excluding the player layer
            if (hitFound)
            {

                IPickupPoint pickupPoint = hit.collider.gameObject.GetComponent<IPickupPoint>();
                if (pickupPoint != null) {
                    // then we can give it a shot at taking it...
                    GameObject newPickup = pickupPoint.OnPickUp(holdingObject);
                    if (newPickup != null && holdingObject == null) {
                        Collider c = newPickup.GetComponent<Collider>();
                        if (c != null)
                        {
                            // We don't really want this to crash with other stuff.
                            c.enabled = false;
                        }
                        Rigidbody holdingObjectRb = newPickup.GetComponent<Rigidbody>();
                        if (holdingObjectRb != null)
                        {
                            holdingObjectRb.isKinematic = true;
                            holdingObjectRb.useGravity = false;
                        }

                        holdingObject = newPickup;
                        holdingObject.transform.parent = holdingObjectTransform;
                        holdingObject.transform.localPosition = Vector3.zero;
                        holdingObject.transform.localRotation = Quaternion.identity;
                        controller.isHoldingObject = true;
                    }
                }
            } else if (holdingObject != null)
            {
                // Drop holding object :shrug:
                holdingObject.transform.parent = null;
                Collider c = holdingObject.GetComponent<Collider>();
                if (c != null)
                {
                    // We don't really want this to crash with other stuff.
                    c.enabled = true;
                }

                Rigidbody holdingObjectRb = holdingObject.GetComponent<Rigidbody>();
                if (holdingObjectRb != null) {
                    holdingObjectRb.isKinematic = false;
                    holdingObjectRb.useGravity = true;
                }

                IHoldable holdable = holdingObject.GetComponent<IHoldable>();
                if (holdable != null )
                {
                    holdable.OnPutDown(gameObject);
                }

                holdingObject = null;
                controller.isHoldingObject = false;
                if (throwFeedback != null)
                {
                    throwFeedback.PlayFeedbacks();
                }
                
            }
        }
    }
}
