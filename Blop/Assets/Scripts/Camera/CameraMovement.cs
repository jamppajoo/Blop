using UnityEngine;
using System.Collections;
using System;

public class CameraMovement : MonoBehaviour
{
    private Animator animator;
    public bool isUp, isDown, rotatedIsUp, rotatedIsDown;
    public bool cameraRotate = false;
    public bool onMenu = false;
    private BlopMovement playerMovement;
    private GameObject mainCameraObject;
    public float speed = 5f;
    private Quaternion originalRotation;

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
            if (!isUp)
                animator.SetTrigger("ToUp");
            else if (!isDown)
                animator.SetTrigger("ToDown");
        }
        else if (cameraRotate)
        {
            if (!rotatedIsUp)
                animator.SetTrigger("RotateToUp");
            else if (!rotatedIsDown)
                animator.SetTrigger("RotateToDown");
        }
        BlopMovement.buttonPresses++;
    }

    private void Start()
    {
        originalRotation = gameObject.transform.rotation;
        animator = GetComponent<Animator>();
        playerMovement = FindObjectOfType<BlopMovement>();
        mainCameraObject = gameObject.transform.parent.gameObject;
        isUp = false;
        rotatedIsUp = false;
        isDown = true;
        rotatedIsDown = true;
    }

    //Move camera to players position
    public void FixedUpdate()
    {
        mainCameraObject.transform.position = Vector3.Lerp(mainCameraObject.transform.position, playerMovement.gameObject.transform.position, speed * Time.deltaTime);
    }
    public void RestartCamera()
    {
        if (!isDown)
            animator.SetTrigger("ForceDown");
        isDown = true;
        rotatedIsDown = true;
        isUp = false;
        rotatedIsUp = false;
    }
    //Functions are ran from cameras animation events
    private void IsUp()
    {
        isUp = true;
        isDown = false;
        EventManager.EnableIngameButtons();
    }

    private void IsDown()
    {
        isUp = false;
        isDown = true;
        EventManager.EnableIngameButtons();
    }

    private void RotatedIsUp()
    {
        rotatedIsUp = true;
        rotatedIsDown = false;
        isDown = false;
        EventManager.EnableIngameButtons();

    }

    private void RotatedIsDown()
    {
        rotatedIsUp = false;
        rotatedIsDown = true;
        isDown = true;
        EventManager.EnableIngameButtons();
    }


}
