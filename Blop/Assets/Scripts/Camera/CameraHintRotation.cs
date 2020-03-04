using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using UnityEngine.EventSystems;

public class CameraHintRotation : MonoBehaviour
{
    public float speed;
    public float repositionTime = 1f;
    private Vector3 touchCurrentPosition, touchOldPosition, touchOffset;
    private float width, height;
    private bool touchMoved = false;
    private Vector2 currentPos;
    private bool uiControlsInUse = false;
    
    private const string repositionCameraCoroutineName = "RepositionCameraCoroutine";

    private CameraMovement cameraMovement;

    private void Awake()
    {
        cameraMovement = gameObject.GetComponent<CameraMovement>();
    }

    private void Update()
    {
        if (GameManager.Instance.hintActive)
        {
            GetTouchPosition();
            if (touchMoved)
                GetTouchOffset();
        }

    }

    private void GetTouchPosition()
    {
        width = (float)Screen.width / 2.0f;
        height = (float)Screen.height / 2.0f;

        //noUIcontrolsInUse = EventSystem.current.currentSelectedGameObject == null;
        uiControlsInUse = EventSystem.current.IsPointerOverGameObject();

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            currentPos = touch.position;

            if (touch.phase == TouchPhase.Began && !uiControlsInUse)
            {
                Timing.KillCoroutines(repositionCameraCoroutineName);
            }
            // Move the cube if the screen has the finger moving.
            if (touch.phase == TouchPhase.Moved)
            {
                currentPos.x = (currentPos.x - width) / width;
                currentPos.y = (currentPos.y - height) / height;
                touchCurrentPosition = new Vector3(currentPos.x, currentPos.y, 0.0f);
                touchMoved = true;
            }
            else
                touchMoved = false;

            if (touch.phase == TouchPhase.Ended && !uiControlsInUse)
                RepositionCamera();
        }

        currentPos = Input.mousePosition;

        if (Input.GetMouseButtonDown(0) && !uiControlsInUse)
        {
            Timing.KillCoroutines(repositionCameraCoroutineName);
            currentPos.x = (currentPos.x - width) / width;
            currentPos.y = (currentPos.y - height) / height;
            touchCurrentPosition = new Vector3(currentPos.x, currentPos.y, 0.0f);
            touchOldPosition = new Vector3(currentPos.x, currentPos.y, 0.0f);
        }

        else if (Input.GetMouseButton(0) && !uiControlsInUse)
        {
            currentPos.x = (currentPos.x - width) / width;
            currentPos.y = (currentPos.y - height) / height;
            touchCurrentPosition = new Vector3(currentPos.x, currentPos.y, 0.0f);
            if ((touchCurrentPosition - touchOldPosition).magnitude > 0)
                touchMoved = true;
            else
                touchMoved = false;

        }

        if (Input.GetMouseButtonUp(0) && !uiControlsInUse)
        {
            touchMoved = false;
            RepositionCamera();
        }
    }

    private void GetTouchOffset()
    {
        touchOffset = (touchCurrentPosition - touchOldPosition);

        touchOldPosition = touchCurrentPosition;
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
            gameObject.transform.rotation = Quaternion.Lerp(currentRotation, cameraMovement.currentCameraIntendedRotation, time / timeToMove);
            time += Time.deltaTime;
            EventManager.DisableIngameButtons();

            yield return 0;
        }
        gameObject.transform.rotation = cameraMovement.currentCameraIntendedRotation;
        EventManager.EnableIngameButtons();
        touchOffset = Vector3.zero;
        touchCurrentPosition = Vector3.zero;
    }
}
