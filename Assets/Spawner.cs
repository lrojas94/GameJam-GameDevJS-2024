using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private Transform[] spawnPositions;
    [SerializeField]
    private List<GameObject> active;
    [SerializeField]
    private int maxActiveItems = 4;
    [SerializeField]
    private Pool pool;
    [SerializeField]
    private float maxTimeBetweenItems = 4.5f;
    [SerializeField]
    private Transform target;

    [SerializeField]
    private float spawnTimer = 0f;
    [SerializeField]
    private float spawnTime = 0f;
     

    private void Awake()
    {
        pool = GetComponent<Pool>();
        active=  new List<GameObject>();
        setSpawnTime();
    }

    void setSpawnTime()
    {
        spawnTime = UnityEngine.Random.Range(0f, maxTimeBetweenItems);
        spawnTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        bool canSpawnMore = active.Count < maxActiveItems && spawnTime != 0;
        if (canSpawnMore)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnTime) {
                GameObject instance = pool.GetItemInstance();
                Transform spawn  = spawnPositions[UnityEngine.Random.Range(0, spawnPositions.Length - 1)]; 
                instance.transform.position = spawn.position;
                
                if (target != null) {
                    AIDestinationSetter setter = instance.GetComponent<AIDestinationSetter>();
                    if (setter != null) {
                        setter.target = target;
                    }
                }
                
                active.Add(instance);

                
                setSpawnTime();
            }
        } else
        {
            // Check now if some are inactive:
            for (int i = 0; i < active.Count; i++)
            {
                GameObject instance = active[i];
                if (!instance.activeSelf)
                {
                    pool.AddToPool(instance);
                    active.RemoveAt(i);
                    i--;
                    setSpawnTime();
                }
            }
        }
    }
}
