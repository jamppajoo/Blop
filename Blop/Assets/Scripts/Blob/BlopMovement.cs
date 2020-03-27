using UnityEngine;
using System.Collections;

/// <summary>
/// Handles Blop's movement by lerping the current position to next one depending on the current camera angle.
/// </summary>
public class BlopMovement : MonoBehaviour
{
    [Tooltip("Time in seconds that blop takes to move to next block")]
    [SerializeField]
    private float timeToMove;

    public static int buttonPresses = 0;

    private bool canMove = true;
    private bool inAir = false;
    private bool raycastHitting = false;
    private float horizontalMovement, verticalMovement;
    private Vector3 playerOriginalPosition;
    private Rigidbody playerRb;
    private CameraMovement cameraMovement;
    private MobileControllers mobileControllers;
    private RaycastHit hit;

    //Check all events on button presses from mobile controllers or on keyboard
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

    #region ButtonPressedEvents
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
    #endregion

    private void Awake()
    {
        buttonPresses = 0;
        cameraMovement = FindObjectOfType<CameraMovement>();
        mobileControllers = FindObjectOfType<MobileControllers>();
        playerOriginalPosition = gameObject.transform.position;
        playerRb = gameObject.GetComponent<Rigidbody>();
    }

    //Check raycast to all directions, restrinct movmenent based on that, and handle moving blop to correct position if blop falls down
    private void CheckRaycasts()
    {
        raycastHitting = false;
        playerRb.isKinematic = false;
        Ray raycastHitUp = new Ray(transform.position, Vector3.up);
        Ray raycastHitLeft = new Ray(transform.position, Vector3.left);
        Ray raycastHitRight = new Ray(transform.position, Vector3.right);
        Ray raycastHitForward = new Ray(transform.position, Vector3.forward);
        Ray raycastHitBack = new Ray(transform.position, Vector3.back);
        //Down needs to be checked last, because magic
        Ray raycastHitDown = new Ray(transform.position, Vector3.down);
        
        #region Raycasts
        //Raycast to up
        if (Physics.Raycast(raycastHitUp, out hit) && hit.distance < 1)
        {
            if (hit.transform.gameObject.tag == "UpWall")
            {
                playerRb.isKinematic = true;
                raycastHitting = true;
                if (verticalMovement > 0 && cameraMovement.IsDown())
                    verticalMovement = 0;
            }
            else if (hit.transform.gameObject.tag != "UpWall" && verticalMovement > 0 && cameraMovement.IsDown())
                verticalMovement = 0;
        }

        //Raycast to left
        if ((Physics.Raycast(raycastHitLeft, out hit)) && hit.distance < 1)
        {
            if (hit.transform.gameObject.tag == "LeftWall")
            {
                playerRb.isKinematic = true;
                raycastHitting = true;
            }
            //Handles moving half a block if blop falls down
            if (hit.transform.gameObject.tag == "LeftWall" && inAir)
            {
                StartCoroutine(Move(new Vector3(0, -0.5f, 0), timeToMove / 2));
            }
            if (horizontalMovement < 0 && (cameraMovement.IsDown() || cameraMovement.IsUp()))
                horizontalMovement = 0;
            if (verticalMovement < 0 && cameraMovement.RotatedIsUp())
                verticalMovement = 0;
        }

        //Raycast to right
        if ((Physics.Raycast(raycastHitRight, out hit)) && hit.distance < 1)
        {
            if (hit.transform.gameObject.tag == "RightWall")
            {
                playerRb.isKinematic = true;
                raycastHitting = true;
            }
            if (hit.transform.gameObject.tag == "RightWall" && inAir)
            {
                StartCoroutine(Move(new Vector3(0, -0.5f, 0), timeToMove / 2));
            }

            if (horizontalMovement > 0 && (cameraMovement.IsUp() || cameraMovement.IsDown()))
                horizontalMovement = 0;
            if (verticalMovement > 0 && cameraMovement.RotatedIsUp())
                verticalMovement = 0;
        }

        //Raycast to front
        if ((Physics.Raycast(raycastHitForward, out hit)) && hit.distance < 1)
        {
            if (hit.transform.gameObject.tag == "FrontWall")
            {
                playerRb.isKinematic = true;
                raycastHitting = true;
            }

            if (hit.transform.gameObject.tag == "FrontWall" && inAir)
            {
                StartCoroutine(Move(new Vector3(0, -0.5f, 0), timeToMove / 2));
            }

            if (verticalMovement > 0 && cameraMovement.IsUp())
                verticalMovement = 0;
            if (horizontalMovement > 0 && cameraMovement.RotatedIsUp())
                horizontalMovement = 0;
        }

        //Raycast to back
        if ((Physics.Raycast(raycastHitBack, out hit)) && hit.distance < 1)
        {
            if (hit.transform.gameObject.tag == "BackWall")
            {
                playerRb.isKinematic = true;
                raycastHitting = true;
            }

            if (hit.transform.gameObject.tag == "BackWall" && inAir)
            {
                StartCoroutine(Move(new Vector3(0, -0.5f, 0), timeToMove / 2));
            }
            if (verticalMovement < 0 && cameraMovement.IsUp())
                verticalMovement = 0;
            if (horizontalMovement < 0 && cameraMovement.RotatedIsUp())
                horizontalMovement = 0;
        }

        //Raycast to down
        if ((Physics.Raycast(raycastHitDown, out hit)) && hit.distance < 1)
        {
            raycastHitting = true;
            if (verticalMovement < 0 && cameraMovement.IsDown())
                verticalMovement = 0;

            else if (hit.transform.gameObject.tag != "Floor" && hit.transform.gameObject.name != "Restart" && hit.transform.gameObject.name != "Teleport" && playerRb.isKinematic == false)
            {
                horizontalMovement = 0;
                verticalMovement = 0;
            }
        }
        #endregion

        //Move blop based on horizontalMovement and verticalMovement values if camera is currently down
        if (canMove && !inAir && (horizontalMovement != 0 || verticalMovement != 0) && cameraMovement.IsDown() && raycastHitting)
            StartCoroutine(Move(new Vector3(horizontalMovement, verticalMovement, 0), timeToMove));
        //Same but check if camera is rotated only up or rotated up
        else if (canMove && !inAir && (horizontalMovement != 0 || verticalMovement != 0))
        {
            if (cameraMovement.RotatedIsUp())
                StartCoroutine(Move(new Vector3(verticalMovement * -1, 0, horizontalMovement), timeToMove));

            else if (cameraMovement.IsUp())
                StartCoroutine(Move(new Vector3(horizontalMovement, 0, verticalMovement), timeToMove));
        }
        horizontalMovement = 0;
        verticalMovement = 0;
    }
    
    private void FixedUpdate()
    {
        if (playerRb.velocity.y != 0 && canMove)
            inAir = true;
        else inAir = false;
        if (inAir)
            CheckRaycasts();

        //Check if the y velocity is too much, clamp velocity to make sure game does not glitch. Levelpack3 teleport stuff
        if (playerRb.velocity.y < -15)
        {
            playerRb.velocity = Vector3.ClampMagnitude(playerRb.velocity, 15);
        }
    }

    //Ran if scene is "restarted", resets all values to original
    public void RestartPlayer(Vector3 offset)
    {
        gameObject.transform.position = playerOriginalPosition + offset;
        playerRb.velocity = Vector3.zero;
        buttonPresses = 0;
    }
    //Movement script
    IEnumerator Move(Vector3 direction, float movementTime)
    {
        canMove = false;
        float elapsedtime = 0;
     
        //Check that blop moves whole block, if so, add buttonpress. So we dont add button press if blop falls down
        if (Mathf.Abs(direction.x) == 1 || Mathf.Abs(direction.y) == 1 || Mathf.Abs(direction.z) == 1)
        {
            buttonPresses++;
            EventManager.AddMovement();
            GameManager.Instance.SmallVibrate();
        }

        //Round starting point to nearest 0.5f
        Vector3 startPoint = new Vector3(
            Mathf.Round(gameObject.transform.position.x / 0.5f) * 0.5f,
            Mathf.Round(gameObject.transform.position.y / 0.5f) * 0.5f,
            Mathf.Round(gameObject.transform.position.z / 0.5f) * 0.5f
            );

        Vector3 nextPoint = startPoint + direction;

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
