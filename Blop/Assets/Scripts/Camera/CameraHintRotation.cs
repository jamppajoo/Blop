﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class CameraHintRotation : MonoBehaviour
{
    public float speed;
    private Vector3 touchCurrentposition, touchStartPosition, touchOffset;
    private float width;
    private float height;
    private bool touchMoved = false;
    private Vector2 currentPos;

    private Quaternion startingRotation;

    private const string repositionCameraCoroutineName = "RepositionCameraCoroutine";

    private void Awake()
    {
        startingRotation = gameObject.transform.rotation;
    }

    private void Update()
    {
        GetTouchPosition();
        if (touchMoved)
            GetTouchOffset();

    }

    private void GetTouchPosition()
    {
        width = (float)Screen.width / 2.0f;
        height = (float)Screen.height / 2.0f;
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            currentPos = touch.position;

            if (touch.phase == TouchPhase.Began)
            {
                currentPos.x = (currentPos.x - width) / width;
                currentPos.y = (currentPos.y - height) / height;
                touchStartPosition = new Vector3(currentPos.x, currentPos.y, 0.0f);
                Timing.KillCoroutines(repositionCameraCoroutineName);

            }
            // Move the cube if the screen has the finger moving.
            if (touch.phase == TouchPhase.Moved)
            {
                currentPos.x = (currentPos.x - width) / width;
                currentPos.y = (currentPos.y - height) / height;
                touchCurrentposition = new Vector3(currentPos.x, currentPos.y, 0.0f);
                touchMoved = true;
                // Position the cube.
                //transform.position = position;
            }
            else
                touchMoved = false;

            if (touch.phase == TouchPhase.Ended)
                RepositionCamera();
        }

        currentPos = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            currentPos.x = (currentPos.x - width) / width;
            currentPos.y = (currentPos.y - height) / height;
            touchStartPosition = new Vector3(currentPos.x, currentPos.y, 0.0f);
            Timing.KillCoroutines(repositionCameraCoroutineName);
        }

        if (Input.GetMouseButton(0))
        {
            currentPos.x = (currentPos.x - width) / width;
            currentPos.y = (currentPos.y - height) / height;
            touchCurrentposition = new Vector3(currentPos.x, currentPos.y, 0.0f);
            touchMoved = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            touchMoved = false;
            RepositionCamera();
        }


    }

    private void GetTouchOffset()
    {
        touchOffset = touchCurrentposition - touchStartPosition;
        RotateCamera();
    }
    private void RotateCamera()
    {
        touchOffset *= speed;
        gameObject.transform.rotation = Quaternion.Euler(-touchOffset.y, touchOffset.x, touchOffset.z);
    }
    private void RepositionCamera()
    {
        Timing.RunCoroutine(_RepositionCamera(1), repositionCameraCoroutineName);

    }
    private IEnumerator<float> _RepositionCamera(float timeToMove)
    {
        Quaternion currentRotation = gameObject.transform.rotation;
        float time = 0;
        while (time < timeToMove)
        {
            gameObject.transform.rotation = Quaternion.Lerp(currentRotation, startingRotation, time / timeToMove);
            Debug.Log("Time:" + gameObject.transform.rotation);
            time += Time.deltaTime;

            yield return 0;
        }
        touchOffset = Vector3.zero;
    }
}
