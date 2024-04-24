using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Furnace : MonoBehaviour, IPickupPoint
{
    // Start is called before the first frame update
    public float currentPower = 15f;
    public float idealPower = 15f;
   
    private float targetPower = 15f;
    [SerializeField]
    private int coalBonus = 15;
    [SerializeField]
    private int waterBonus = 25;
    [SerializeField]
    private int enemyPenalty = 20;

    [SerializeField]
    private float extinguishSpeed = 1f;

    [SerializeField]
    private float bucketStrength = 0.5f;
    [SerializeField]
    private float coalStrength = 1f; 


    [SerializeField]
    private ParticleSystem fireEfx;
    [SerializeField]
    private float powerChangeDuration = 1f;
    [SerializeField]
    private float currentPowerChangeDuration = 0;
    [SerializeField]
    MMF_Player shaker;

    [SerializeField]
    Image tempertureIndicator;

    // Update is called once per frame
    void Update()
    {
        targetPower -= Time.deltaTime * extinguishSpeed;

        if(fireEfx != null)
        {
            var main = fireEfx.main;
            main.startSpeed = currentPower;    
        }

        if (targetPower != currentPower)
        {
            // Lept these 2:
            currentPower = Mathf.Lerp(currentPower, targetPower, Time.deltaTime);
        } 
    }

    private void IncreaseFireStrength(float strength)
    {  
        targetPower += strength;
        shaker.PlayFeedbacks();
    }

    public GameObject OnPickUp(GameObject inHand)
    {
        if (inHand != null)
        {
            // Check the tags:
            if (inHand.CompareTag("WaterBucket"))
            {
                WaterBucket bucket = inHand.GetComponent<WaterBucket>();
                if (bucket.isFilled)
                {
                    bucket.Empty();
                    IncreaseFireStrength(-bucketStrength);
                    GameManager.Instance.AddBonusCash(waterBonus);
                }
            }

            if (inHand.CompareTag("Coal"))
            {
                CoalItem coal = inHand.GetComponent<CoalItem>();
                if (coal)
                {
                    IncreaseFireStrength(coalStrength);
                    coal.OnExplode(coal.transform.position);
                }
            }
        }
        return null; 
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Coal"))
        {
            IncreaseFireStrength(coalStrength);
            GameManager.Instance.AddBonusCash(coalBonus);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            BasicEnemy be = collision.gameObject.GetComponent<BasicEnemy>();
            if (be != null) {
                IncreaseFireStrength(be.furnaceDamage);
                GameManager.Instance.AddBonusCash(-enemyPenalty);
            }
        }
    }
}
