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

    public void OnExplode(Vector3 explosionPosition, int damage = 0)
    {
        GameObject explosion = AssetManager.Instance.ExplosionPool.GetItemInstance();
        explosion.transform.position = explosionPosition;
        explosionPosition.y += 3;

        if (damage > 0)
        {
            DamagePopup.ShowDamage(damage, explosionPosition);
        }
        
        gameObject.SetActive(false);
        AssetManager.Instance.CoalPool.AddToPool(gameObject);    

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Furnace")) {
            // Explode this one:
            OnExplode(collision.contacts[0].point);
            return;
        }

        float impactForce = collision.impulse.magnitude;
        float baseDamage = 10f;
        float damage = baseDamage * impactForce;
        bool canDamage = collision.gameObject.CompareTag("Enemy");

        if (canDamage && impactForce > 5f && damage > 0)
        { 
            OnExplode( collision.contacts[0].point, (int)Mathf.Ceil(damage));
        }
    }
}
