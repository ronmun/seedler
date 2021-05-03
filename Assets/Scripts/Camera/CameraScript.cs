using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraScript : MonoBehaviour
{
    public GameObject pivotObject;
    public GameObject rotateGlobal;
    public GameObject rotatePlayer;
    public float speed;
    public float verticalSpeed;
    public float minZoomLimit;
    public float maxZoomLimit;
    public float verticalRotMaxLimit;
    public float verticalRotMinLimit;
    public float zoomSpeed;
    public float offsetMode0;
    public float offsetMode1;

    private Transform player;

    private Transform pivot;
    private float rotationSpeed;
    private float verticalRotationSpeed;
    private int cameraMode = 0;
    private bool zooming = false;
    private bool translating = false;
    private Transform objToFollow;
    private Transform objToLookAt;
    private Vector3 cameraOffsetPlayer;

    //Controls
    PlayerControls controls;
    Vector2 rotation;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.CameraInput.Rotate.performed += ctx => rotation = ctx.ReadValue<Vector2>();
        controls.CameraInput.Rotate.canceled += ctx => rotation = Vector2.zero;

        controls.CameraInput.Mode.started += ctx => ChangeCameraMode();
    }

    void Start()
    {
        pivot = pivotObject.transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        InitializeRotationPoints();
    }

    void InitializeRotationPoints()
    {
        rotateGlobal.transform.position = transform.position;
        offsetMode0 = (rotateGlobal.transform.position - pivot.position).magnitude;
        rotatePlayer.transform.position = (rotateGlobal.transform.position - pivot.transform.position).normalized;
        rotatePlayer.transform.position = player.position + (rotatePlayer.transform.position * offsetMode1);
        objToFollow = rotateGlobal.transform;
        objToLookAt = pivotObject.transform;
    }

    void Update()
    {
        Rotate();
        RotateVertical();
        FollowPoint();
        //ControllableZoom();
        //MoveCameraUp();
    }

    void LateUpdate()
    {
        cameraOffsetPlayer = rotatePlayer.transform.position - player.transform.position;
        //cameraOffsetPlayer = rotatePlayer.transform.position - new Vector3(player.position.x, player.position.y + rotatePlayer.transform.position.y, player.position.z);
        //rotatePlayer.transform.position = cameraOffsetPlayer.normalized * offsetMode1 + player.position;
        Vector3 newPos = cameraOffsetPlayer.normalized * offsetMode1 + player.position;
        newPos = new Vector3(newPos.x, rotatePlayer.transform.position.y, newPos.z);
        rotatePlayer.transform.position = Vector3.Slerp(rotatePlayer.transform.position, newPos, 0.5f);
    }

    void FollowPoint()
    {
        if(!translating)
            transform.position = Vector3.MoveTowards(transform.position, objToFollow.position, speed * Time.deltaTime);
    }

    void MoveCameraUp()
    {
        if (rotation.y > 0.4)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + (verticalSpeed * Time.deltaTime), transform.position.z);
            pivotObject.transform.position = new Vector3(pivotObject.transform.position.x, pivotObject.transform.position.y + (verticalSpeed * Time.deltaTime), pivotObject.transform.position.z);
        }
        if (rotation.y < -0.4)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + (-verticalSpeed * Time.deltaTime), transform.position.z);
            pivotObject.transform.position = new Vector3(pivotObject.transform.position.x, pivotObject.transform.position.y + (-verticalSpeed * Time.deltaTime), pivotObject.transform.position.z);
        }
    }

    void Rotate()
    {
        /*if(!translating)
            transform.RotateAround(pivot.position, Vector3.up, rotationSpeed * Time.deltaTime);*/
        if (!translating)
        {
            rotateGlobal.transform.RotateAround(pivotObject.transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
            rotatePlayer.transform.RotateAround(player.position, Vector3.up, rotationSpeed * Time.deltaTime);
        }

        if (rotation.x > 0.1)
            rotationSpeed = -speed;
        else if (rotation.x < -0.1)
            rotationSpeed = speed;
        else
            rotationSpeed = 0;
    }

    void RotateVertical()
    {
        /*if(!translating)
        {
            transform.LookAt(pivot.transform);
            Vector3 perpendicularVector = transform.position - pivot.position;  //distance
            perpendicularVector = Vector3.Cross(perpendicularVector, Vector3.up);
            transform.RotateAround(pivot.position, perpendicularVector, verticalRotationSpeed * Time.deltaTime);
        }*/

        if (!translating)
        {
            transform.LookAt(objToLookAt);
            
            Vector3 perpendicularVector = CalculateRotAxis(rotateGlobal.transform.position, pivotObject.transform.position);
            float velocity = CalculateSpeed(rotateGlobal.transform.position, pivotObject.transform.position);
            rotateGlobal.transform.RotateAround(pivotObject.transform.position, perpendicularVector, velocity * Time.deltaTime);

            perpendicularVector = CalculateRotAxis(rotatePlayer.transform.position, player.position);
            velocity = CalculateSpeed(rotatePlayer.transform.position, player.position);
            rotatePlayer.transform.RotateAround(player.position, perpendicularVector, velocity * Time.deltaTime);
        }


        /*float angle = Mathf.Asin(transform.position.y/Vector3.Distance(transform.position, pivot.transform.position));    //CAMBIAR ESTO PARA QUE SEA PARA LAS 2 CAMARAS
        angle = angle * Mathf.Rad2Deg;
        Debug.Log(angle);
        if (rotation.y > 0.4 && angle < verticalRotMaxLimit)
            verticalRotationSpeed = speed;
        else if (rotation.y < -0.4 && angle > -verticalRotMinLimit)
            verticalRotationSpeed = -speed;
        else
            verticalRotationSpeed = 0;*/
    }

    Vector3 CalculateRotAxis(Vector3 position, Vector3 pivot)
    {
        Vector3 perpendicularVector = position - pivot;  //distance
        perpendicularVector = Vector3.Cross(perpendicularVector, Vector3.up);
        return perpendicularVector;
    }

    float CalculateSpeed(Vector3 position, Vector3 pivot)
    {
        float angle = Mathf.Asin(position.y / Vector3.Distance(position, pivot));
        angle = angle * Mathf.Rad2Deg;
        Debug.Log(angle);

        float upSpeed = 0;

        if (rotation.y > 0.4 && angle < verticalRotMaxLimit)
            upSpeed = speed;
        else if (rotation.y < -0.4 && angle > -verticalRotMinLimit)
            upSpeed = -speed;

        return upSpeed;
    }

    void ChangeCameraMode()
    {
        if(!zooming && !translating)
        {
            zooming = true;

            if (cameraMode + 1 > 2)
                cameraMode = 0;
            else
                cameraMode++;

            Vector3 offset = (transform.position - pivot.transform.position).normalized;

            switch (cameraMode)
            {
                case 0:     //Camera global mode
                    //offset *= offsetMode0;
                    //pivot = pivotObject.transform;
                    objToFollow = rotateGlobal.transform;
                    objToLookAt = pivotObject.transform;
                    Zoom(maxZoomLimit, +zoomSpeed);
                    break;
                case 1:     //Camera player focus
                    offset *= offsetMode1;
                    //pivot = player;
                    objToFollow = rotatePlayer.transform;
                    objToLookAt = player.transform;
                    Zoom((maxZoomLimit / 4) * 3, -zoomSpeed);
                    break;
                case 2:     //Camera player focus+
                    Zoom(minZoomLimit, -zoomSpeed);
                    break;
            }
        }
    }

    void Zoom(float x, float speed) //Apply FovChange
    {
        StopCoroutine("ZoomCoroutine");     //I think this is not necesary the bool zooming does the job
        StartCoroutine(ZoomCoroutine(x, speed));
    }

    void Zoom(float x, float speed, Vector3 offset) //Apply FovChange + transition
    {
        translating = true;

        StopCoroutine("ZoomCoroutine");     //I think this is not necesary the bool zooming does the job
        StartCoroutine(ZoomCoroutine(x, speed));

        StopCoroutine("TranslationCoroutine");     //I think this is not necesary the bool zooming does the job
        StartCoroutine(TranslationCoroutine(offset));
    }

    IEnumerator ZoomCoroutine(float x, float speed)
    {
        if(speed > 0)
            speed *= 2; //The speed is doubled bc it needs to return faster than to go foward

        while(Camera.main.fieldOfView != x)
        {
            Camera.main.fieldOfView += speed * Time.deltaTime;

            if (speed > 0)
            {
                if (Camera.main.fieldOfView > x)
                    Camera.main.fieldOfView = x;
            }
            else
            {
                if (Camera.main.fieldOfView < x)
                    Camera.main.fieldOfView = x;
            }

            yield return null;
        }

        zooming = false;
    }

    IEnumerator TranslationCoroutine(Vector3 offset)
    {
        while(transform.position != pivot.transform.position + offset)
        {
            transform.position = Vector3.MoveTowards(transform.position, pivot.transform.position + offset, speed * Time.deltaTime);
            yield return null;
        }

        translating = false;
    }

    void ControllableZoom() //Con stick
    {
        transform.LookAt(pivot.transform);
        transform.position = Vector3.MoveTowards(transform.position, pivot.position, zoomSpeed * Time.deltaTime);         //Zoom con movimiento

         if (rotation.y > 0.4)
            zoomSpeed = speed;
        else if (rotation.y < -0.4)
            zoomSpeed = -speed;
        else
            zoomSpeed = 0;

        float distance = Vector3.Distance(Vector3.MoveTowards(transform.position, pivot.position, zoomSpeed * Time.deltaTime), pivot.position);
        if (distance < minZoomLimit)
        {
            if(zoomSpeed > 0)
                zoomSpeed = 0;
        }
        else if (distance > maxZoomLimit)
        {
            if (zoomSpeed < 0)
                zoomSpeed = 0;
        }

        /*float nextFov = Camera.main.fieldOfView + zoomSpeed * Time.deltaTime;
        if (nextFov < minZoomLimit)
            Camera.main.fieldOfView = minZoomLimit;
        else if (nextFov > maxZoomLimit)
            Camera.main.fieldOfView = maxZoomLimit;
        else
            Camera.main.fieldOfView = nextFov;

        if (rotation.y > 0.4)
            zoomSpeed = -speed;
        else if (rotation.y < -0.4)
            zoomSpeed = speed;
        else
            zoomSpeed = 0;*/
    }

    private void OnEnable()
    {
        controls.CameraInput.Enable();
    }

    private void OnDisable()
    {
        controls.CameraInput.Disable();
    }
}
