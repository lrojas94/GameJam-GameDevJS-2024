using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHoldable
{
    [SerializeField]
    public abstract void OnPutDown(GameObject player);
}
