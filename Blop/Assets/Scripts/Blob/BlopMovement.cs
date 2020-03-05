using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class BlopMovement : MonoBehaviour
{
    private float horizontalMovement, verticalMovement;
    private bool canMove = true;
    private Rigidbody playerRb;
    private bool inAir = false;
    public float timeToMove;
    public static int buttonPresses = 0;
    private CameraMovement cameraMovement;
    private MobileControllers mobileControllers;
    private RaycastHit hit;
    private Vector3 playerOriginalPosition;
    private void OnEnable()
    {
        EventManager.OnUpPressed += UpPressed;
        EventManager.OnDownPressed += DownPressed;
        EventManager.OnLeftPressed += LeftPressed;
        EventManager.OnRightPressed += RightPressed;
    }
    private void OnDisable()
    {
        EventManager.OnUpPressed -= UpPressed;
        EventManager.OnDownPressed -= DownPressed;
        EventManager.OnLeftPressed -= LeftPressed;
        EventManager.OnRightPressed -= RightPressed;
    }

    private void UpPressed()
    {
        verticalMovement = 1;
        CheckRaycasts();
    }

    private void DownPressed()
    {
        verticalMovement = -1;
        CheckRaycasts();
    }

    private void LeftPressed()
    {
        horizontalMovement = -1;
        CheckRaycasts();
    }

    private void RightPressed()
    {
        horizontalMovement = 1;
        CheckRaycasts();
    }

    private void Awake()
    {
        buttonPresses = 0;
        cameraMovement = FindObjectOfType<CameraMovement>();
        mobileControllers = FindObjectOfType<MobileControllers>();
        playerOriginalPosition = gameObject.transform.position;
    }
    void Start()
    {
        playerRb = GameObject.Find("Blop").transform.gameObject.GetComponent<Rigidbody>();
    }
    private void CheckRaycasts()
    {
        //Raycast to every direction

        playerRb.isKinematic = false;
        Ray raycastHitUp = new Ray(transform.position, Vector3.up);
        Ray raycastHitLeft = new Ray(transform.position, Vector3.left);
        Ray raycastHitRight = new Ray(transform.position, Vector3.right);
        Ray raycastHitForward = new Ray(transform.position, Vector3.forward);
        Ray raycastHitBack = new Ray(transform.position, Vector3.back);
        //Down needs to be checked last, because magic
        Ray raycastHitDown = new Ray(transform.position, Vector3.down);
        //Check to up
        if (Physics.Raycast(raycastHitUp, out hit) && hit.distance < 1)
        {
            if (hit.transform.gameObject.tag == "UpWall")
            {
                playerRb.isKinematic = true;
                if (verticalMovement > 0 && cameraMovement.isDown)
                    verticalMovement = 0;
            }
            else if (hit.transform.gameObject.tag != "UpWall" && verticalMovement > 0 && cameraMovement.isDown)
                verticalMovement = 0;
        }

        //Check to left
        if ((Physics.Raycast(raycastHitLeft, out hit)) && hit.distance < 1)
        {
            if (hit.transform.gameObject.tag == "LeftWall")
            {
                playerRb.isKinematic = true;
            }
            if (hit.transform.gameObject.tag == "LeftWall" && inAir)
            {
                StartCoroutine(Move(new Vector3(0, -0.5f, 0), timeToMove / 2));
            }
            if (horizontalMovement < 0 && (cameraMovement.isDown || cameraMovement.isUp))
                horizontalMovement = 0;
            if (verticalMovement < 0 && cameraMovement.rotatedIsUp)
                verticalMovement = 0;
        }

        //Check to right
        if ((Physics.Raycast(raycastHitRight, out hit)) && hit.distance < 1)
        {
            if (hit.transform.gameObject.tag == "RightWall")
            {
                playerRb.isKinematic = true;
            }
            if (hit.transform.gameObject.tag == "RightWall" && inAir)
            {
                StartCoroutine(Move(new Vector3(0, -0.5f, 0), timeToMove / 2));
            }

            if (horizontalMovement > 0 && (cameraMovement.isUp || cameraMovement.isDown))
                horizontalMovement = 0;
            if (verticalMovement > 0 && cameraMovement.rotatedIsUp)
                verticalMovement = 0;
        }

        //Check to front
        if ((Physics.Raycast(raycastHitForward, out hit)) && hit.distance < 1)
        {
            if (hit.transform.gameObject.tag == "FrontWall")
            {
                playerRb.isKinematic = true;
            }

            if (hit.transform.gameObject.tag == "FrontWall" && inAir)
            {
                StartCoroutine(Move(new Vector3(0, -0.5f, 0), timeToMove / 2));
            }

            if (verticalMovement > 0 && cameraMovement.isUp)
                verticalMovement = 0;
            if (horizontalMovement > 0 && cameraMovement.rotatedIsUp)
                horizontalMovement = 0;
        }

        //Check to back
        if ((Physics.Raycast(raycastHitBack, out hit)) && hit.distance < 1)
        {
            if (hit.transform.gameObject.tag == "BackWall")
            {
                playerRb.isKinematic = true;
            }

            if (hit.transform.gameObject.tag == "BackWall" && inAir)
            {
                StartCoroutine(Move(new Vector3(0, -0.5f, 0), timeToMove / 2));
            }
            if (verticalMovement < 0 && cameraMovement.isUp)
                verticalMovement = 0;
            if (horizontalMovement < 0 && cameraMovement.rotatedIsUp)
                horizontalMovement = 0;
        }

        //Check to down
        if ((Physics.Raycast(raycastHitDown, out hit)) && hit.distance < 1)
        {
            if (verticalMovement < 0 && cameraMovement.isDown)
                verticalMovement = 0;

            else if (hit.transform.gameObject.tag != "Floor" && hit.transform.gameObject.name != "Restart" && hit.transform.gameObject.name != "Teleport" && playerRb.isKinematic == false)
            {
                horizontalMovement = 0;
                verticalMovement = 0;
                mobileControllers.restartButton.gameObject.SetActive(true);
            }
        }
        //Check movement and other things
        if (canMove && !inAir && (horizontalMovement != 0 || verticalMovement != 0) && cameraMovement.isDown)
            StartCoroutine(Move(new Vector3(horizontalMovement, verticalMovement, 0), timeToMove));
        //Same but check if camera is rotated only up or up & 90 degrees
        else if (canMove && !inAir && (horizontalMovement != 0 || verticalMovement != 0))
        {
            if (cameraMovement.rotatedIsUp)
                StartCoroutine(Move(new Vector3(verticalMovement * -1, 0, horizontalMovement), timeToMove));

            else if (cameraMovement.isUp)
                StartCoroutine(Move(new Vector3(horizontalMovement, 0, verticalMovement), timeToMove));
        }
        horizontalMovement = 0;
        verticalMovement = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (playerRb.velocity.y != 0)
            inAir = true;
        else inAir = false;
        if (inAir)
            CheckRaycasts();

        //Check if the y velocity is too much, if so, restart level, teleport stuff
        if (playerRb.velocity.y < -15)
        {
            mobileControllers.restartButton.gameObject.SetActive(true);
            playerRb.velocity = Vector3.ClampMagnitude(playerRb.velocity, 15);
        }


    }

    public void RestartPlayer(Vector3 offset)
    {
        gameObject.transform.position = playerOriginalPosition + offset;
        playerRb.velocity = Vector3.zero;
        buttonPresses = 0;
    }
    //Movement script
    IEnumerator Move(Vector3 direction, float movementTime)
    {
        ////Check that player moves whole block, if so, add buttonpresses.
        if (Mathf.Abs(direction.x) == 1 || Mathf.Abs(direction.y) == 1 || Mathf.Abs(direction.z) == 1)
        {
            buttonPresses++;
            EventManager.AddMovement();
        }

        canMove = false;
        float elapsedtime = 0;

        Vector3 startPoint = gameObject.transform.position;
        Vector3 nextPoint = new Vector3(transform.position.x + direction.x, transform.position.y + direction.y, transform.position.z + direction.z);

        while (elapsedtime < movementTime)
        {
            gameObject.transform.position = Vector3.Lerp(startPoint, nextPoint, (elapsedtime / movementTime));
            elapsedtime += Time.deltaTime;

            yield return null;
        }
        gameObject.transform.position = nextPoint;
        canMove = true;
        CheckRaycasts();
        yield return null;

    }
}
