using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCamera : MonoBehaviour
{

    float xDelta;
    float yDelta;

    float x;
    float y;

    public float xSpeed;
    public float ySpeed;

    public float distance = 6f;

    bool inverseXAxis;
    bool InverseYAxis;

    public GameObject follow;
    public Vector3 lookAtOffset;

    public Transform pivot;


    private float GetCameraAxis(string axisName)
    {
        try
        {
            return Input.GetAxis(axisName);
        }
        catch (UnityEngine.UnityException exp)
        {
            Debug.Log(exp);
            return 0.0f;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Quaternion rotation = Quaternion.Euler(y, x, 0);

        Vector3 negDistance = follow.transform.forward * -distance;

        Vector3 localPos = rotation * negDistance;

        transform.localPosition = localPos;

        //transform.position = rotation * negDistance + follow.transform.position + lookAtOffset;

    }

    // Update is called once per frame
    void Update()
    {
        xDelta = GetCameraAxis("Mouse X") * xSpeed * distance;
        yDelta = GetCameraAxis("Mouse Y") * ySpeed * distance;

        x += inverseXAxis ? xDelta : -xDelta;
        //y += InverseYAxis ? yDelta : -yDelta;


        //y = ClampAngle(y, yMinLimit, yMaxLimit);

    }

    void LateUpdate()
    {
        MovePivot();
    }

    void MovePivot()
    {
        //move pivot to follow position
        Vector3 characerOffset = follow.transform.rotation * lookAtOffset;
        pivot.position = follow.transform.position + characerOffset;

        //rotate pivot to be in follow rotation
        //pivot.rotation = follow.transform.rotation;

        //rotate pivot by x/y delta
        xDelta = inverseXAxis ? xDelta : -xDelta;
        yDelta = InverseYAxis ? yDelta : -yDelta;

        

        //Quaternion.Euler(yDelta, xDelta, 0);

        Quaternion newRotLocal = Quaternion.Euler(y, x, 0);

        pivot.rotation = follow.transform.rotation;

        Quaternion rotWorld = follow.transform.rotation * newRotLocal;

        transform.RotateAround(pivot.position, pivot.up, xDelta);
        transform.RotateAround(pivot.position, transform.right, yDelta);


    }


    

    void FixedUpdate()
    { }

    



}
