using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BlobMovement : MonoBehaviour {
    private float horizontalMovement, verticalMovement;
    private bool canMove = true;
    private Rigidbody playerRb;
    private bool inAir = false;
    public float moveScale = 1f;
    public float timeToMove;
    public static int buttonPresses = 0;
    private CameraMovement cameraMovement;
    private MobileControllers mobileControllers;

    private void Awake()
    {
        cameraMovement = FindObjectOfType<CameraMovement>();
        mobileControllers = FindObjectOfType<MobileControllers>();
    }
    void Start () {
        playerRb = GameObject.Find("Blop").transform.gameObject.GetComponent<Rigidbody>();
        buttonPresses = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        
        //Raycast to every direction
        RaycastHit hit;
        Ray DownHit = new Ray(transform.position, Vector3.down);
        Ray RightHit = new Ray(transform.position, Vector3.right);
        Ray LeftHit = new Ray(transform.position, Vector3.left);
        Ray BackHit = new Ray(transform.position, Vector3.forward);
        Ray FrontHit = new Ray(transform.position, Vector3.back);
        Ray UpHit = new Ray(transform.position, Vector3.up);
        playerRb.isKinematic = false;

        //If not using mobilecontrollers to move
        if (mobileControllers.moveHorizontal == 0 && mobileControllers.moveVertical == 0)
        {
            horizontalMovement = Input.GetAxisRaw("Horizontal");
            verticalMovement = Input.GetAxisRaw("Vertical");
        }
        else
        {
            horizontalMovement = mobileControllers.moveHorizontal;
            verticalMovement = mobileControllers.moveVertical;
        }

        
        //Pretty much only shitton of raycasting in different directions and restricting movement depend on that
        if ((Physics.Raycast(RightHit, out hit))&& hit.distance <1 )
        {
            if (hit.transform.gameObject.tag == "RightWall")
            {
                playerRb.isKinematic = true;
            }
            if (hit.transform.gameObject.tag == "RightWall" && inAir)
            {
                StartCoroutine(Move(new Vector3(0, -0.5f, 0),moveScale,timeToMove /2));
            }

            if (horizontalMovement > 0 && (cameraMovement.isUp || cameraMovement.isDown))
                horizontalMovement = 0;
            if (verticalMovement < 0 && cameraMovement.rotatedUp)
                verticalMovement = 0;
        }

        if ((Physics.Raycast(LeftHit, out hit)) && hit.distance < 1)
        {
            if (hit.transform.gameObject.tag == "LeftWall")
            {
                playerRb.isKinematic = true;
            }
            if (hit.transform.gameObject.tag == "LeftWall" && inAir)
            {
                StartCoroutine(Move(new Vector3(0, -0.5f, 0),moveScale, timeToMove /2));
            }
            if (horizontalMovement < 0 && (cameraMovement.isDown || cameraMovement.isUp) )
                horizontalMovement = 0;
            if (verticalMovement > 0 && cameraMovement.rotatedUp)
                verticalMovement = 0;
        }
        if ((Physics.Raycast(BackHit, out hit)) && hit.distance < 1  )
        {
            if (hit.transform.gameObject.tag == "BackWall")
            {
                playerRb.isKinematic = true;
            }

            if (hit.transform.gameObject.tag == "BackWall" && inAir)
            {
                StartCoroutine(Move(new Vector3(0, -0.5f, 0),moveScale, timeToMove/2));
            }

            if (verticalMovement > 0 && cameraMovement.isUp)
                    verticalMovement = 0;
            if (horizontalMovement > 0 && cameraMovement.rotatedUp)
                horizontalMovement = 0;
        }
        if ((Physics.Raycast(FrontHit, out hit)) && hit.distance < 1)
        {
            if (hit.transform.gameObject.tag == "FrontWall")
            {
                playerRb.isKinematic = true;
            }

            if (hit.transform.gameObject.tag == "FrontWall" && inAir)
            {
                StartCoroutine(Move(new Vector3(0, -0.5f, 0),moveScale, timeToMove/2));
            }

            if (verticalMovement < 0 && cameraMovement.isUp)
                verticalMovement = 0;
            if (horizontalMovement < 0 && cameraMovement.rotatedUp)
                horizontalMovement = 0;
        }

        if (Physics.Raycast(UpHit, out hit) && hit.distance < 1)
        {
            if (hit.transform.gameObject.tag == "UpWall")
            {
                playerRb.isKinematic = true;
                if (verticalMovement > 0 && cameraMovement.isDown)
                    verticalMovement = 0;
            }
            else if (hit.transform.gameObject.tag != "UpWall" && verticalMovement > 0 &&  cameraMovement.isDown)
                verticalMovement = 0;
        }
        if ((Physics.Raycast(DownHit, out hit)) && hit.distance < 1)
        {
            if (verticalMovement < 0 && cameraMovement.isDown)
                verticalMovement = 0;

            else if (hit.transform.gameObject.tag != "Floor" && hit.transform.gameObject.name != "Restart" && hit.transform.gameObject.name != "Teleport" && playerRb.isKinematic == false)
            {
                print("Blob stuck!");
                horizontalMovement = 0;
                verticalMovement = 0;
                mobileControllers.restartButton.gameObject.SetActive(true);
            }
        }
        if (playerRb.velocity.y != 0)
            inAir = true;
        else inAir = false;
        //Check movement and other things
        if (canMove && !inAir && (horizontalMovement !=0 || verticalMovement !=0) && cameraMovement.isDown)
            StartCoroutine(Move(new Vector3(horizontalMovement, verticalMovement, 0),moveScale, timeToMove));
        //Same but check if camera is rotated only up or up & 90 degrees
        else if (canMove && !inAir && (horizontalMovement != 0 || verticalMovement != 0) )
        {
            if (cameraMovement.rotatedUp)
                StartCoroutine(Move(new Vector3(verticalMovement * -1, 0, horizontalMovement), moveScale, timeToMove));

            else if (cameraMovement.isUp)
                StartCoroutine(Move(new Vector3(horizontalMovement, 0, verticalMovement), moveScale, timeToMove));
        }
        mobileControllers.moveVertical = 0;
        mobileControllers.moveHorizontal = 0;

        //Check if the y velocity is too much, if so, restart level, teleport stuff
        if (playerRb.velocity.y < -15)
        {
            mobileControllers.restartButton.gameObject.SetActive(true);
            playerRb.velocity = Vector3.ClampMagnitude(playerRb.velocity, 15);
            print(playerRb.velocity.y);
        }

        
    }   
    //Movement script
    IEnumerator Move(Vector3 direction, float Scale, float movementTime)
    {
        //Check that player moves whole block, if so, add buttonpresses.
        if(Mathf.Abs(direction.x) == 1 || Mathf.Abs(direction.y) == 1 || Mathf.Abs(direction.z) == 1)
        {
            buttonPresses++;
        }

        canMove = false;
        float elapsedtime = 0;

        Vector3 startPoint = gameObject.transform.position;
        Vector3 nextPoint = new Vector3(transform.position.x + direction.x, transform.position.y + direction.y, transform.position.z + direction.z);

        while (elapsedtime <movementTime )
        {
            gameObject.transform.position = Vector3.Lerp(startPoint, nextPoint, (elapsedtime / movementTime));
            elapsedtime += Time.fixedDeltaTime;

            yield return null;

        }
        canMove = true;
        
    }
}
