using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TopDown3DPlayerController : MonoBehaviour
{
    [SerializeField]
    private float topPlayerSpeed = 15f;
    [SerializeField]
    private float acceleration = 1f;
    [SerializeField]
    private float currentSpeed = 0f;

    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private float maxTilt = 15f;


    public bool isHoldingObject = false;
    public bool isRunning = false;
    [SerializeField]
    Animator playerAnimator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (playerAnimator != null) {

            playerAnimator.SetBool("isHoldingObject", isHoldingObject);
            playerAnimator.SetBool("isRunning", isRunning);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 direction = new Vector2(horizontal, vertical).normalized;
        float angle = Vector2.SignedAngle(direction, Vector2.up);
        Vector3 euler = transform.localEulerAngles;
        
        
        if (direction.magnitude > 0)
        {
            if (currentSpeed < topPlayerSpeed)
            {
                currentSpeed += acceleration * Time.deltaTime;
            }

            euler.y = angle;
            isRunning = true;
        } else {
            currentSpeed = 0f;
            isRunning = false;
        }

        Vector3 velocity = (new Vector3(horizontal, 0, vertical)).normalized * Mathf.Min(topPlayerSpeed, currentSpeed);
        rb.velocity = velocity;
        // euler.x = -maxTilt * (currentSpeed / topPlayerSpeed);
        transform.localEulerAngles = euler;
    }
}
