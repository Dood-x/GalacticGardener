using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThirdPersonCameraWithLockOn;

public class PlayerController : MonoBehaviour
{
    //origin of the polar coordiante system
    public GameObject world;
    CharacterController cc;
    Rigidbody rb;

    public float gravity = -12f;

    public ThirdPersonCamera cam;

    [Header("Movement")]
    //movement speed in degrees
    public float angularMovementSpeed;
    public Vector3 gravityUp;


    // in degrees
    struct PolarCoordiantes
    {
        public float radius;
        public float theta;
        public float phi;
    }

    Quaternion positionDir;

    PolarCoordiantes polarTransform;

    Vector3 inputDirection;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        polarTransform = CartesianToPolar(transform.position);
        positionDir = Quaternion.LookRotation(transform.position);
        rb = GetComponent<Rigidbody>();
        if (rb)
        {
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }

        if(cc)
        {
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        inputDirection = new Vector3(h, 0, v);
        inputDirection.Normalize();
        inputDirection *= Mathf.Max(Mathf.Abs(h), Mathf.Abs(v));

        inputDirection *= Time.deltaTime * angularMovementSpeed;

        // cameras rotation unroated by the character rotation
        Quaternion localRot = Quaternion.Inverse(rb.rotation) * cam.transform.rotation;

        inputDirection = Quaternion.Euler(0, localRot.eulerAngles.y, 0) * inputDirection;


        // rotate the character towards movement 

        //transform.LookAt(transform.TransformDirection(inputDirection), rb.transform.up);
        if (!inputDirection.Equals(Vector3.zero))
        {
            transform.rotation = Quaternion.LookRotation(transform.TransformDirection(inputDirection), rb.transform.up);
        }


    }

    void FixedUpdate()
    {
        //rb.velocity = transform.TransformDirection(inputDirection);
        //rb.AddForce(transform.TransformDirection(inputDirection) * 1000f);
        rb.MovePosition(rb.position + transform.TransformDirection(inputDirection));

    }

    Vector3 PolarToCartesian(PolarCoordiantes pc)
    {
        Vector3 pos = world.transform.position;
        //Vector3 direction = new Vector3()

        return Vector3.zero;
    }

    PolarCoordiantes CartesianToPolar(Vector3 coordinates)
    {
        // in reference to world
        // x y z transform to local coordinte system of the planet

        coordinates = world.transform.InverseTransformPoint(coordinates);
        Debug.Log("coordiantes in local pos " + coordinates);

        PolarCoordiantes pc = new PolarCoordiantes();
        pc.radius = coordinates.magnitude;

        Vector3 yxProjection = coordinates;
        yxProjection.x = 0f;
        pc.theta = Vector3.Angle(Vector3.up, yxProjection);

        Debug.Log("theta "+ pc.theta);

        Vector3 xzProjection = coordinates;
        xzProjection.y = 0f;
        pc.theta = Vector3.Angle(Vector3.up, xzProjection);

        Debug.Log("theta " + pc.theta);

        return pc;
    }

    
}
