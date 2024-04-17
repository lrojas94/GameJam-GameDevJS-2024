using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class CoalItem : MonoBehaviour, IHoldable
{
    [SerializeField]
    private float throwSpeed = 1f;
    [SerializeField]
    private GameObject explosion;

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

    public void OnExplode(Vector3 explosionPosition)
    {
        GameObject instance = GameObject.Instantiate(explosion);
        instance.transform.position = explosionPosition;
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (explosion != null && collision.gameObject.CompareTag("Furnace")) {
            // Explode this one:
            OnExplode(collision.contacts[0].point);
        }
    }
}
