using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AssetManager : MonoBehaviour
{ 
    public static AssetManager Instance;
    public Pool DamagePopupPool;
    public Pool ExplosionPool;
    public Pool CoalPool;

    private void Awake()
    {
        if (Instance == null) {
           Instance = this;
        }

        DamagePopupPool.GrowPool();
        ExplosionPool.GrowPool();
        CoalPool.GrowPool();
    } 
}
