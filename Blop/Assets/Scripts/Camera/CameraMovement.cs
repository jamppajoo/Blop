using UnityEngine;
using MEC;
using System.Collections.Generic;

/// <summary>
/// Handles cameras change view functionality
/// </summary>
public class CameraMovement : MonoBehaviour
{
    [Tooltip("Should we rotate the camera 90 degrees on the sideways")]
    [SerializeField]
    private bool cameraRotate = false;

    

    [Tooltip("How fast should we track the player")]
    [SerializeField]
    private float trakingSpeed = 5f;

    [Tooltip("How fast the camera rotates (in seconds)")]
    [SerializeField]
    private float rotateTime = 0.5f;

    private bool currentlyRotating = false;
    private bool isUp, isDown, rotatedIsUp, rotatedIsDown;

    private Vector3 downRotation = Vector3.zero, upRotation = new Vector3(90, 0, 0);
    private Quaternion originalRotation;
    private Quaternion currentCameraIntendedRotation = Quaternion.identity;

    private BlopMovement playerMovement;
    private const string cameraMovementCoroutine = "CameraMovementCoroutine";

    private void OnEnable()
    {
        EventManager.OnChangeViewPressed += ChangeCameraView;
    }

    private void OnDisable()
    {
        EventManager.OnChangeViewPressed -= ChangeCameraView;
    }

    private void ChangeCameraView()
    {
        EventManager.DisableIngameButtons();
        if (!cameraRotate)
        {
            Timing.KillCoroutines(cameraMovementCoroutine);
            if (!isUp)
                Timing.RunCoroutine(RotateCamera(upRotation, rotateTime), cameraMovementCoroutine);
            else if (!isDown)
                Timing.RunCoroutine(RotateCamera(downRotation, rotateTime), cameraMovementCoroutine);
        }
        else if (cameraRotate)
        {
            //if (!rotatedIsUp)
            //    animator.SetTrigger("RotateToUp");
            //else if (!rotatedIsDown)
            //    animator.SetTrigger("RotateToDown");
        }
        BlopMovement.buttonPresses++;
        EventManager.AddRotation();
    }

    private void Start()
    {
        originalRotation = gameObject.transform.rotation;
        playerMovement = FindObjectOfType<BlopMovement>();
        isUp = false;
        rotatedIsUp = false;
        isDown = true;
        rotatedIsDown = true;
    }

    //Follow players position
    public void FixedUpdate()
    {
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, playerMovement.gameObject.transform.position, trakingSpeed * Time.deltaTime);
    }
    //Restart all camera settings to default, used when scene is "restarted"
    public void RestartCamera()
    {
        if (currentlyRotating)
        {
            Timing.KillCoroutines(cameraMovementCoroutine);
            Timing.RunCoroutine(RotateCamera(downRotation, rotateTime), cameraMovementCoroutine);
            return;
        }

        if (!isDown)
            Timing.RunCoroutine(RotateCamera(downRotation, rotateTime), cameraMovementCoroutine);
    }
    //Return value to CameraHintRotation
    public Quaternion GetCurrentIntendedRotation(){ return currentCameraIntendedRotation;}

    //Return values to BlopMovement
    public bool IsUp(){ return isUp; }
    public bool IsDown(){ return isDown; }
    public bool RotatedIsUp(){ return rotatedIsUp; }
    public bool RotatedIsDown(){ return rotatedIsDown; }

    //Rotate camera to up/down
    private IEnumerator<float> RotateCamera(Vector3 toRotation, float timeToRotate)
    {
        float time = 0;
        currentlyRotating = true;
        currentCameraIntendedRotation = Quaternion.Euler(toRotation);
        while (time < timeToRotate)
        {
            transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, Quaternion.Euler(toRotation), time / timeToRotate);
            time += Time.deltaTime;
            yield return 0;
        }

        transform.rotation = Quaternion.Euler(toRotation);
        if (toRotation == downRotation)
        {
            isDown = true;
            isUp = false;
        }
        else
        {
            isDown = false;
            isUp = true;
        }
        
        currentlyRotating = false;
        EventManager.EnableIngameButtons();
    }
}
