using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace : MonoBehaviour, IPickupPoint
{
    // Start is called before the first frame update
    public float currentPower = 15f;
    [SerializeField]
    private float maxPower = 15f;
    private float targetPower = 15f;

    [SerializeField]
    private float bucketStrength = 0.5f;


    [SerializeField]
    private ParticleSystem fireEfx;
    [SerializeField]
    private float powerChangeDuration = 0.5f;
    [SerializeField]
    private float currentPowerChangeDuration = 0;

    void Awake()
    {
        if (fireEfx != null)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(fireEfx != null)
        {
            var main = fireEfx.main;
            main.startSpeed = Mathf.Min(currentPower, maxPower);    
        }

        if (targetPower != currentPower)
        {
            // Lept these 2:
            currentPower = Mathf.Lerp(currentPower, targetPower, currentPowerChangeDuration / powerChangeDuration);
            currentPowerChangeDuration += Time.deltaTime;

        }
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
                    targetPower = currentPower - bucketStrength;
                    currentPowerChangeDuration = 0;
                }
            }
        }
        return null; 
    }
}
