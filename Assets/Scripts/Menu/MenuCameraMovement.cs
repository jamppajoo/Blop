﻿using UnityEngine;
using System.Collections;

public class MenuCameraMovement : MonoBehaviour
{
    private Animator animator;
    public static bool isUp, isDown, rotatedUp, rotatedDown;
    public static bool changeMade = false;
    public bool cameraRotate = false;
    private GameObject Blop, MainCamera;
    public float speed = 5f;
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        Blop = GameObject.Find("Blop");
        MainCamera = GameObject.Find("MainCamera");
        isUp = false;
        rotatedUp = false;
        isDown = true;
        rotatedDown = true;

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) || changeMade)
        {
            BlobMovement.buttonPresses++;
            if (GameManager.totalButtonPressesLeft > 0)
                GameManager.totalButtonPressesLeft--;
            GameManager.sharedGM.Save();
            MobileControllers.ChangeView.interactable = false;
            MobileControllers.Up.interactable = false;
            MobileControllers.Down.interactable = false;
            MobileControllers.Left.interactable = false;
            MobileControllers.Right.interactable = false;
            MobileControllers.canPress = false;
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
        changeMade = false;

    }
    void IsUp()
    {
        isUp = true;
        isDown = false;
        animator.SetBool("ToUp", false);
        setStuff();
    }
    void IsDown()
    {
        isDown = true;
        isUp = false;
        animator.SetBool("ToDown", false);
        setStuff();
    }
    void rotatedIsUp()
    {
        rotatedUp = true;
        rotatedDown = false;
        isDown = false;
        animator.SetBool("RotateToUp", false);
        setStuff();

    }
    void rotatedIsDown()
    {
        rotatedDown = true;
        isDown = true;
        rotatedUp = false;
        animator.SetBool("RotateToDown", false);
        setStuff();

    }
    void setStuff()
    {
        MobileControllers.ChangeView.interactable = true;
        MobileControllers.Up.interactable = true;
        MobileControllers.Down.interactable = true;
        MobileControllers.Left.interactable = true;
        MobileControllers.Right.interactable = true;
        MobileControllers.Back.interactable = true;
        MobileControllers.canPress = true;
    }
    public void FixedUpdate()
    {
        MainCamera.gameObject.transform.position = Vector3.Lerp(MainCamera.gameObject.transform.position, Blop.gameObject.transform.position, speed * Time.deltaTime);


    }
}
