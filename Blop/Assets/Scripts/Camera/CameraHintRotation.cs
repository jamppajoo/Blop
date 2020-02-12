using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class CameraHintRotation : MonoBehaviour
{
    public float speed;
    public float repositionTime = 1f;
    private Vector3 touchCurrentposition, touchOldPosition, touchOffset;
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
                Timing.KillCoroutines(repositionCameraCoroutineName);

            }
            // Move the cube if the screen has the finger moving.
            if (touch.phase == TouchPhase.Moved)
            {
                currentPos.x = (currentPos.x - width) / width;
                currentPos.y = (currentPos.y - height) / height;
                touchCurrentposition = new Vector3(currentPos.x, currentPos.y, 0.0f);
                touchMoved = true;
            }
            else
                touchMoved = false;

            if (touch.phase == TouchPhase.Ended)
                RepositionCamera();
        }

        currentPos = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            Timing.KillCoroutines(repositionCameraCoroutineName);
            currentPos.x = (currentPos.x - width) / width;
            currentPos.y = (currentPos.y - height) / height;
            touchCurrentposition = new Vector3(currentPos.x, currentPos.y, 0.0f);
            touchOldPosition = new Vector3(currentPos.x, currentPos.y, 0.0f);
        }

        else if (Input.GetMouseButton(0))
        {
            currentPos.x = (currentPos.x - width) / width;
            currentPos.y = (currentPos.y - height) / height;
            touchCurrentposition = new Vector3(currentPos.x, currentPos.y, 0.0f);
            if ((touchCurrentposition - touchOldPosition).magnitude > 0)
                touchMoved = true;
            else
                touchMoved = false;

        }

        if (Input.GetMouseButtonUp(0))
        {
            touchMoved = false;
            RepositionCamera();
        }
    }

    private void GetTouchOffset()
    {
        touchOffset = (touchCurrentposition - touchOldPosition);

        touchOldPosition = touchCurrentposition;
        RotateCamera();
    }
    private void RotateCamera()
    {
        touchOffset *= speed;
        gameObject.transform.rotation *= Quaternion.Euler(-touchOffset.y, touchOffset.x, touchOffset.z);
        EventManager.DisableIngameButtons();
    }
    private void RepositionCamera()
    {
        Timing.RunCoroutine(_RepositionCamera(repositionTime), repositionCameraCoroutineName);

    }
    private IEnumerator<float> _RepositionCamera(float timeToMove)
    {
        Quaternion currentRotation = gameObject.transform.rotation;
        float time = 0;
        while (time < timeToMove)
        {
            gameObject.transform.rotation = Quaternion.Lerp(currentRotation, startingRotation, time / timeToMove);
            time += Time.deltaTime;
            EventManager.DisableIngameButtons();

            yield return 0;
        }
            EventManager.EnableIngameButtons();
        touchOffset = Vector3.zero;
        touchCurrentposition = Vector3.zero;
    }
}
