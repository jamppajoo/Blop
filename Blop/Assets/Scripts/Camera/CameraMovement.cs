using UnityEngine;
using System.Collections;
using System;
using MEC;
using System.Collections.Generic;

public class CameraMovement : MonoBehaviour
{
    public Quaternion currentCameraIntendedRotation = Quaternion.identity;

    public bool isUp, isDown, rotatedIsUp, rotatedIsDown;
    public bool cameraRotate = false;
    public bool onMenu = false;
    private BlopMovement playerMovement;
    public float speed = 5f;
    public float rotateTime = 0.5f;
    private Quaternion originalRotation;

    private const string cameraMovementCoroutine = "CameraMovementCoroutine";

    private Vector3 downRotation = Vector3.zero, upRotation = new Vector3(90, 0, 0);



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

    //Move camera to players position
    public void FixedUpdate()
    {
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, playerMovement.gameObject.transform.position, speed * Time.deltaTime);
    }
    public void RestartCamera()
    {
        if (!isDown)
            Timing.RunCoroutine(RotateCamera(downRotation, rotateTime), cameraMovementCoroutine);
    }
    private IEnumerator<float> RotateCamera(Vector3 toRotation, float timeToRotate)
    {
        float time = 0;
        currentCameraIntendedRotation = Quaternion.Euler(toRotation);
        while (time < timeToRotate)
        {
            transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, Quaternion.Euler(toRotation), time / timeToRotate);
            time += Time.deltaTime;
            yield return 0;
        }

        transform.rotation = Quaternion.Euler(toRotation);
        isUp = !isUp;
        isDown = !isDown;
        EventManager.EnableIngameButtons();

    }


}
