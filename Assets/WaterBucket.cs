using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBucket : MonoBehaviour, IHoldable
{
    public bool isFilled;
    [SerializeField]
    Animator animator;

    public void OnPutDown(GameObject player)
    {
        // Reset rotation:
        Vector3 euler = transform.rotation.eulerAngles;
        euler.x = 0;
        transform.rotation = Quaternion.Euler(euler);
    }

    public void Fill()
    {
        if (isFilled)
        {
            return;
        }

        animator.SetTrigger("Fill");
        isFilled = true;
    }

    public void Empty()
    {
        if (!isFilled)
        {
            return;
        }

        animator.SetTrigger("Empty");
        isFilled = false;
    }
}
