using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public string id;
    public bool isParent = true;
    public bool growOnAwake = false;
    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private int itemsPerBatch = 10;
    private Queue<GameObject> queue = new Queue<GameObject>();

    private void Awake()
    {
        if (growOnAwake)
        {
            GrowPool();
        }
    }

    public void GrowPool()
    {
        for (int i = 0; i < itemsPerBatch; i++)
        {
            GameObject instaceToAdd = Instantiate(prefab, isParent ? transform : null);
            AddToPool(instaceToAdd);
        }
    }

    public void SetPrefab(GameObject prefab) {
        this.prefab = prefab;
        queue = new Queue<GameObject>();
        this.GrowPool();
    }

    public void AddToPool(GameObject instance)
    {
        instance.SetActive(false);
        queue.Enqueue(instance);
    }

    public GameObject GetItemInstance()
    {
        if (queue.Count == 0)
        {
            GrowPool();
        }

        GameObject instance = queue.Dequeue();
        instance.SetActive(true);
        return instance;
    }

}