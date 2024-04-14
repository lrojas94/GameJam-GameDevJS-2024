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

        Vector3 velocity = (new Vector3(horizontal, 0, vertical)).normalized * playerSpeed;
        Vector3 euler = transform.localEulerAngles;

        rb.velocity = velocity;

        if (horizontal > 0)
        {
            // Face left:
            euler.y = 90; 
        } else if (horizontal < 0)
        {
            // Face right
            euler.y = -90;
        } else if (vertical > 0) {
            // Face forward
            euler.y = 0;
        } else if (vertical < 0)
        {
            // Face camera
            euler.y = 180;
        }

        float rotationX = velocity.magnitude > 0 ? - 15.0f : 0; 
        euler.x = rotationX;
        transform.localEulerAngles = Vector3.Lerp(Vector3.zero, euler, 1);
    }
}
