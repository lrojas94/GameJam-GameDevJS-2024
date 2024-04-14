using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TopDown3DPlayerController : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 10f;
    [SerializeField]
    private Rigidbody rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 direction = new Vector2(horizontal, vertical).normalized;
        float angle = Vector2.SignedAngle(direction, Vector2.up);
        Vector3 velocity = (new Vector3(horizontal, 0, vertical)).normalized * playerSpeed;
        Vector3 euler = transform.localEulerAngles;
        rb.velocity = velocity;
        if (velocity.magnitude > 0)
        {
            euler.y = angle;
        }
        euler.x = velocity.magnitude > 0 ? -15.0f : 0;
        transform.localEulerAngles = euler;
    }
}
