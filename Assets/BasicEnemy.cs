using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public bool forceDie = false;
    [SerializeField]
    private float totalHealth = 100;
    [SerializeField]
    private float health = 100;

    [SerializeField]
    MMF_Player damageFeedback;

    [SerializeField]
    MMF_Player deathFeedback;
    private void OnEnable()
    {
        health = totalHealth;
        forceDie = false;
    }

    private void Update()
    {
        if (forceDie)
        {
            forceDie = true;
            OnDeath();
        }
    }

    public void TakeDamage(float damage, Vector3 contactPoint)
    {
        health -= damage;
        if (health < 0)
        {
            OnDeath();
            if (damage != 999)
            {
                // Killed by a player hit:
                GameManager.Instance.AddBonusCash(10);
                Debug.Log("Added some cash");
            }
        }
        else
        {
            damageFeedback.PlayFeedbacks();
        }

        contactPoint.y += 3;
        DamagePopup.ShowDamage((int)Mathf.Ceil(damage), contactPoint);
    }

    public void OnDeath()
    {
        deathFeedback.PlayFeedbacks();
        forceDie = false;
    } 

    private void OnCollisionEnter(Collision collision)
    {
        float baseDamage = 10f;
        float impactForce = collision.impulse.magnitude;
        float damage = baseDamage * impactForce;

        bool canDamage = collision.gameObject.CompareTag("Coal") || collision.gameObject.CompareTag("WaterBucket");
        Vector3 contactPoint = collision.contacts[0].point;

        if (canDamage && impactForce > 5f && damage > 0)
        {

            TakeDamage(damage, contactPoint);
        }



        if (collision.gameObject.CompareTag("Furnace"))
        {
            TakeDamage(999, contactPoint);
        }
    }
}
