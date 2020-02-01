using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBody : MonoBehaviour
{

    public Vector3 gravityUp;
    public float gravity = -12f;
    public GameObject world;
    Rigidbody rb;
    CharacterController cc;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CharacterController>();
    }
    
    void FixedUpdate()
    {
        ApplyGravity();
    }
    

    void ApplyGravity()
    {
        gravityUp = (transform.position - world.transform.position).normalized;
        Vector3 localUp = transform.up;

        if (cc)
        {
            cc.Move(gravityUp * gravity * Time.deltaTime);
        }
        else if (rb)
        {
            rb.AddForce(gravityUp * gravity);
        }

        Quaternion targetRotation = Quaternion.FromToRotation(localUp, gravityUp) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 50f * Time.deltaTime);


    }

}
