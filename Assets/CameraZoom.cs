using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] Camera mainCamera;

    //-0.1 down, 0.1 up, 0 when stationary
    private float zoomLevel;
    private float minimumZoom = 1f;
    private float maximumZoom = 20f;
    private float currentSpeed;
    public float smoothness = 0.30f;
    public float multiplier = 2f;

    private void Start()
    {
        zoomLevel = mainCamera.orthographicSize;
        currentSpeed = 0;

    }

    private void Update()
    {
       
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        zoomLevel -= scrollWheel * multiplier;
        zoomLevel = Mathf.Clamp(zoomLevel, minimumZoom, maximumZoom);

        mainCamera.orthographicSize = Mathf.SmoothDamp(mainCamera.orthographicSize, zoomLevel, ref currentSpeed, smoothness);
    }


}

