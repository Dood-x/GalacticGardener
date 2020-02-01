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

    public float yMinLimit = -10f;
    public float yMaxLimit = 80f;

    public float distance = 6f;

    bool inverseXAxis;
    bool InverseYAxis;

    public GameObject follow;
    public Vector3 lookAtOffset;

    public Transform pivot;

    Vector3 cameraCenterPos;
    bool cameraShaking = false;

    // How long the object should shake for.
    public float shakeDuration = 0.2f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.2f;
    public float decreaseFactor = 1.0f;

    float shakeTimer;



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
        pivot.rotation = follow.transform.rotation;

    }

    // Update is called once per frame
    void Update()
    {
        xDelta = GetCameraAxis("Mouse X") * xSpeed * distance;
        yDelta = GetCameraAxis("Mouse Y") * ySpeed * distance;

        x += inverseXAxis ? xDelta : -xDelta;
        y += InverseYAxis ? yDelta : -yDelta;


        xDelta = inverseXAxis ? xDelta : -xDelta;
        yDelta = InverseYAxis ? yDelta : -yDelta;

        if (y > yMaxLimit)
        {
            yDelta -= (y - yMaxLimit);
        }
        else if (y < yMinLimit)
        {
            yDelta += (yMinLimit - y);
        }

        y = ClampAngle(y, yMinLimit, yMaxLimit);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //start camera shake
            cameraShaking = true;
            shakeTimer = 0.0f;
            cameraCenterPos = transform.localPosition;

        }


    }

    void LateUpdate()
    {
        if(cameraShaking)
        {
            transform.localPosition = cameraCenterPos;
        }

        MovePivot();


        if(cameraShaking)
        {
            CameraShake();
        }
    }

    void MovePivot()
    {
        //move pivot to follow position
        Vector3 characerOffset = follow.transform.rotation * lookAtOffset;
        pivot.position = follow.transform.position + characerOffset;

        //rotate pivot to be in follow rotation
        //pivot.rotation = follow.transform.rotation;

        //rotate pivot by x/y delta


        //pivot up should be follow up

        pivot.transform.up = follow.transform.up;

        //Quaternion.Euler(yDelta, xDelta, 0);

        Quaternion newRotLocal = Quaternion.Euler(y, x, 0);

        //pivot.rotation = follow.transform.rotation;

        Quaternion rotWorld = follow.transform.rotation * newRotLocal;

        transform.RotateAround(pivot.position, pivot.up, xDelta);
        transform.RotateAround(pivot.position, transform.right, yDelta);



    }

    void CameraShake()
    {
        cameraCenterPos = transform.localPosition;

        if (shakeTimer < shakeDuration)
        {
            float amplitude = Mathf.Exp(decreaseFactor * -shakeTimer);
            //float amplitude = Mathf.Lerp(shakeAmount, 0.0f,  1 - shakeTimer / shakeDuration);
            //float x = Random.Range(-1f, 1f) * shakeAmount;
            //float y = Random.Range(-1f, 1f) * shakeAmount;
            Debug.Log(amplitude);

            //transform.localPosition = new Vector3(x, y, cameraCenterPos.z);

            transform.localPosition += Random.insideUnitSphere * amplitude * shakeAmount;

            shakeTimer += Time.deltaTime /** decreaseFactor*/;
        }
        else
        {
            shakeTimer = 0f;
            cameraShaking = false;
            transform.localPosition = cameraCenterPos;
        }
    }


    public static float ClampAngle(float angle, float min, float max)
    {
        angle = Mathf.Clamp(angle, min, max);

        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;

        return angle;
    }


}
