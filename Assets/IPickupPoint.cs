using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickupPoint
{
    [SerializeField]
    public abstract GameObject OnPickUp(GameObject inHand);
}
