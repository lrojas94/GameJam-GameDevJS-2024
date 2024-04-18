using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class CoalItem : MonoBehaviour, IHoldable
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

    public void OnExplode(Vector3 explosionPosition)
    {
        GameObject explosion = GameObject.Instantiate(AssetManager.Instance.ExplosionPrefab);
        explosion.transform.position = explosionPosition;
        explosionPosition.y += 3;
        DamagePopup.ShowDamage(100, explosionPosition);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Furnace")) {
            // Explode this one:
            OnExplode(collision.contacts[0].point);
        }
    }
}
