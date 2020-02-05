﻿using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    private Animator animator;
    public bool isUp, isDown, rotatedUp, rotatedDown;
    public bool viewChanged = false;
    public bool cameraRotate = false;
    public bool onMenu = false;
    private BlobMovement playerMovement;
    private GameObject mainCameraObject;
    public float speed = 5f;

    private MobileControllers mobileControllers;
    private void Awake()
    {
        mobileControllers = FindObjectOfType<MobileControllers>();
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = FindObjectOfType<BlobMovement>();
        mainCameraObject = gameObject.transform.parent.gameObject;
        isUp = false;
        rotatedUp = false;
        isDown = true;
        rotatedDown = true;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) || viewChanged)
        {
            BlobMovement.buttonPresses++;
            SetStuff(false);
            if (!cameraRotate)
            {
                if (!isUp)
                    animator.SetBool("ToUp", true);
                else if (!isDown)
                    animator.SetBool("ToDown", true);
            }
            else if (cameraRotate)
            {
                if (!rotatedUp)
                    animator.SetBool("RotateToUp", true);
                else if (!rotatedDown)
                    animator.SetBool("RotateToDown", true);
            }
        }
        viewChanged = false;
    }

    //Move camera to players position
    public void FixedUpdate()
    {
       mainCameraObject.transform.position = Vector3.Lerp(mainCameraObject.transform.position, playerMovement.gameObject.transform.position, speed * Time.deltaTime);
    }
    //Functions are ran from cameras animation events
    private void IsUp()
    {
        isUp = true;
        isDown = false;
        animator.SetBool("ToUp", false);
        SetStuff(true);
    }

    private void IsDown()
    {
        isUp = false;
        isDown = true;
        animator.SetBool("ToDown", false);
        SetStuff(true);
    }

    private void RotatedIsUp()
    {
        rotatedUp = true;
        rotatedDown = false;
        isDown = false;
        animator.SetBool("RotateToUp", false);
        SetStuff(true);

    }

    private void RotatedIsDown()
    {
        rotatedUp = false;
        rotatedDown = true;
        isDown = true;
        animator.SetBool("RotateToDown", false);
        SetStuff(true);

    }

    private void SetStuff(bool active)
    {
        mobileControllers.changeView.interactable = active;
        mobileControllers.up.interactable = active;
        mobileControllers.down.interactable = active;
        mobileControllers.left.interactable = active;
        mobileControllers.right.interactable = active;
        mobileControllers.back.interactable = active;
        mobileControllers.canPress = active;
    }
    
}
