using System.Collections.Generic;
using UnityEngine;
using MEC;
using UnityEngine.EventSystems;

/// <summary>
/// Handles camera rotation when hint is activated and player moves finger accross screen
/// </summary>
public class CameraHintRotation : MonoBehaviour
{
    [Tooltip("Angle that camera shifts when finger is dragged half a screen")]
    [SerializeField]
    private float speed;

    [Tooltip("How long should it take to snap camera back to original rotation")]
    [SerializeField]
    private float repositionTime = 1f;

    private float width, height;
    private bool touchMoved = false, cameraMoved = false;
    private bool uiControlsInUse = false;
    private Vector2 currentPos;
    private Vector3 touchCurrentPosition, touchOldPosition, touchOffset;

    private CameraMovement cameraMovement;

    private const string repositionCameraCoroutineName = "RepositionCameraCoroutine";

    private void Awake()
    {
        cameraMovement = gameObject.GetComponent<CameraMovement>();
    }

    private void Update()
    {
        if (GameManager.Instance.hintActive)
        {
            uiControlsInUse = IsPointerOverUIObject();
            GetTouchPosition();
            if (touchMoved)
                GetTouchOffset();
        }

    }
    //Get finger/mouse position and handle the current and old positions of that
    private void GetTouchPosition()
    {
        width = (float)Screen.width / 2.0f;
        height = (float)Screen.height / 2.0f;
        
        //Get finger movement
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            currentPos = touch.position;

            if (touch.phase == TouchPhase.Began && !uiControlsInUse)
            {
                Timing.KillCoroutines(repositionCameraCoroutineName);
                currentPos.x = (currentPos.x - width) / width;
                currentPos.y = (currentPos.y - height) / height;
                touchCurrentPosition = new Vector3(currentPos.x, currentPos.y, 0.0f);
                touchOldPosition = new Vector3(currentPos.x, currentPos.y, 0.0f);
            }
            // Move the cube if the screen has the finger moving.
            if (touch.phase == TouchPhase.Moved )
            {
                currentPos.x = (currentPos.x - width) / width;
                currentPos.y = (currentPos.y - height) / height;
                touchCurrentPosition = new Vector3(currentPos.x, currentPos.y, 0.0f);
                touchMoved = true;
            }
            else
                touchMoved = false;

            if (touch.phase == TouchPhase.Ended && cameraMoved)
            {
                RepositionCamera();
            }
        }

        //Get mouse movement
        else
        {
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

            if (Input.GetMouseButtonUp(0) && cameraMoved)
            {
                touchMoved = false;
                RepositionCamera();
            }
        }
    }
    //Get position offset to determine how much we should rotate the camera
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
        cameraMoved = true;
        EventManager.DisableIngameButtons();
    }
    private void RepositionCamera()
    {
        cameraMoved = false;
        Timing.RunCoroutine(_RepositionCamera(repositionTime), repositionCameraCoroutineName);

    }
    private IEnumerator<float> _RepositionCamera(float timeToMove)
    {
        Quaternion currentRotation = gameObject.transform.rotation;
        float time = 0;
        while (time < timeToMove)
        {
            gameObject.transform.rotation = Quaternion.Lerp(currentRotation, cameraMovement.GetCurrentIntendedRotation(), time / timeToMove);
            time += Time.deltaTime;
            EventManager.DisableIngameButtons();

            yield return 0;
        }
        gameObject.transform.rotation = cameraMovement.GetCurrentIntendedRotation();
        EventManager.EnableIngameButtons();
        touchOffset = Vector3.zero;
        touchCurrentPosition = Vector3.zero;
    }
    //Magic script that knows is we are touching UI object
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
