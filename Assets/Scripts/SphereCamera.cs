using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCamera : MonoBehaviour
{

    float xDelta;
    float yDelta;

    float x;
    float y;

    float smoothX;
    float smoothY;

    public float xSpeed;
    public float ySpeed;

    public float yMinLimit = -10f;
    public float yMaxLimit = 80f;

    public float distance = 6f;

    public bool inverseXAxis;
    public bool InverseYAxis;

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

    [Header("Smoothing")]
    public float pivotSmoothTime = 1f;
    Vector3 pivotSpeed;

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
        if(PauseGameMenu.GameIsPaused)
        {
            xDelta = 0;
            yDelta = 0;
            return;
        }
        xDelta = GetCameraAxis("Mouse X") * xSpeed * distance;
        yDelta = GetCameraAxis("Mouse Y") * ySpeed * distance;

        float prevX = x;
        float prevY = y;

        x += inverseXAxis ? xDelta : -xDelta;
        y += InverseYAxis ? yDelta : -yDelta;

        //if (y > yMaxLimit)
        //{
        //   y = yMaxLimit;
        //}
        //else if (y < yMinLimit)
        //{
        //    y = yMinLimit;
        //}

        //y = ClampAngle(y, yMinLimit, yMaxLimit);


        smoothX = Mathf.Lerp(smoothX, x, Time.deltaTime * pivotSmoothTime);
        smoothY = Mathf.Lerp(smoothY, y, Time.deltaTime * pivotSmoothTime);

        //xDelta = prevX - smoothX;
        //yDelta = prevY - smoothX;

        xDelta = inverseXAxis ? xDelta : -xDelta;
        yDelta = InverseYAxis ? yDelta : -yDelta;

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    //start camera shake
        //    StartCameraShake();

        //}
        if (y > yMaxLimit)
        {
            yDelta -= (y - yMaxLimit);
        }
        else if (y < yMinLimit)
        {
            yDelta += (yMinLimit - y);
        }
        y = ClampAngle(y, yMinLimit, yMaxLimit);


    }

    void LateUpdate()
    {
        if (PauseGameMenu.GameIsPaused)
        {
            xDelta = 0;
            yDelta = 0;
            return;
        }

        if (cameraShaking)
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

        //pivot.position = Vector3.SmoothDamp(pivot.position, follow.transform.position + characerOffset, ref pivotSpeed ,pivotSmoothTime);
        //pivot.position = Vector3.Lerp(pivot.position, follow.transform.position + characerOffset, Time.deltaTime * pivotSmoothTime);

        //rotate pivot to be in follow rotation
        //pivot.rotation = follow.transform.rotation;

        //rotate pivot by x/y delta


        //pivot up should be follow up

        pivot.transform.up = follow.transform.up;

        //Quaternion.Euler(yDelta, xDelta, 0);

        Quaternion newRotLocal = Quaternion.Euler(y, x, 0);

        //pivot.rotation = follow.transform.rotation;

        //Quaternion rotWorld = follow.transform.rotation * newRotLocal;

        transform.RotateAround(pivot.position, pivot.up, xDelta);
        transform.RotateAround(pivot.position, transform.right, yDelta);

    }

    void CameraShake()
    {
        cameraCenterPos = transform.localPosition;

        if (shakeTimer < shakeDuration)
        {
            float amplitude = Mathf.Exp(decreaseFactor * -shakeTimer);

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

    public void StartCameraShake()
    {
        if (cameraShaking == true)
        {
            transform.localPosition = cameraCenterPos;
        }


        cameraShaking = true;
        shakeTimer = 0.0f;
        cameraCenterPos = transform.localPosition;
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
