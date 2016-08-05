using UnityEngine;
using System.Collections;

public class BlobMovement3 : MonoBehaviour {
    public float horizontalMovement, verticalMovement;
    public float WaitBeforeMoveSeconds;
    private bool canMove = true;
    private bool canMoveDown = true, canMoveUp = true, canMoveRight = true, canMoveLeft= true, canMoveForward = true, canMoveBackward = true;
    private Rigidbody playerRb;
    private bool inAir = false;
    private Vector3 velocity = Vector3.zero;
    public float moveScale = 1f;
    public float smoothFactor = 0.1f;
    
    
	// Use this for initialization
	void Start () {
        playerRb = GameObject.Find("Blop").transform.gameObject.GetComponent<Rigidbody>();

	
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        Ray DownHit = new Ray(transform.position, Vector3.down);
        Ray RightHit = new Ray(transform.position, Vector3.right);
        Ray LeftHit = new Ray(transform.position, Vector3.left);
        Ray BackHit = new Ray(transform.position, Vector3.forward);
        Ray FrontHit = new Ray(transform.position, Vector3.back);
        Ray UpHit = new Ray(transform.position, Vector3.up);
        playerRb.isKinematic = false;
        if (MobileControllers.moveHorizontal == 0 && MobileControllers.moveVertical == 0)
        {
            horizontalMovement = Input.GetAxisRaw("Horizontal");
            verticalMovement = Input.GetAxisRaw("Vertical");
        }
        else
        {
            horizontalMovement = MobileControllers.moveHorizontal;
            verticalMovement = MobileControllers.moveVertical;
        }
            

        if ((Physics.Raycast(DownHit, out hit)) && hit.distance < 1 )
        {
            if (verticalMovement < 0 && CameraMovement.isDown)
                verticalMovement = 0;
        }
        if ((Physics.Raycast(RightHit, out hit))&& hit.distance <1 )
        {
            if (hit.transform.gameObject.tag == "RightWall")
            {
                playerRb.isKinematic = true;
            }
            if (hit.transform.gameObject.tag == "RightWall" && inAir)
            {
                StartCoroutine(Move(new Vector3(0, -0.5f, 0),moveScale));
            }

            if (horizontalMovement > 0)
                horizontalMovement = 0;
        }

        if ((Physics.Raycast(LeftHit, out hit)) && hit.distance < 1)
        {
            if (hit.transform.gameObject.tag == "LeftWall")
            {
                playerRb.isKinematic = true;
            }
            if (hit.transform.gameObject.tag == "LeftWall" && inAir)
            {
                StartCoroutine(Move(new Vector3(0, -0.5f, 0),moveScale));
            }
            if (horizontalMovement < 0)
                horizontalMovement = 0;
        }
        if ((Physics.Raycast(BackHit, out hit)) && hit.distance < 1  )
        {
            if (hit.transform.gameObject.tag == "BackWall")
            {
                playerRb.isKinematic = true;
            }

            if (hit.transform.gameObject.tag == "BackWall" && inAir)
            {
                StartCoroutine(Move(new Vector3(0, -0.5f, 0),moveScale));
            }

            if (verticalMovement > 0 && CameraMovement.isUp)
                    verticalMovement = 0;
        }
        if ((Physics.Raycast(FrontHit, out hit)) && hit.distance < 1)
        {
            if (hit.transform.gameObject.tag == "FrontWall")
            {
                playerRb.isKinematic = true;
            }

            if (hit.transform.gameObject.tag == "FrontWall" && inAir)
            {
                StartCoroutine(Move(new Vector3(0, -0.5f, 0),moveScale));
            }

            if (verticalMovement < 0 && CameraMovement.isUp)
                verticalMovement = 0;
        }
        if (Physics.Raycast(UpHit,out hit) && hit.distance <1)
            {
            if (verticalMovement > 0)
                return;
            }
        // Debug.Log("HorizontalMovement  :" + horizontalMovement + "VerticalMovement    :" + verticalMovement);

        if (playerRb.velocity.y != 0)
            inAir = true;
        else inAir = false;
        
        
        if (canMove && !inAir && (horizontalMovement !=0 || verticalMovement !=0) && CameraMovement.isDown)
            StartCoroutine(Move(new Vector3(horizontalMovement, verticalMovement, 0),moveScale));
        
            
        else if (canMove && !inAir &&(horizontalMovement != 0 || verticalMovement != 0) && CameraMovement.isUp)
            StartCoroutine(Move(new Vector3(horizontalMovement, 0, verticalMovement),moveScale));


        MobileControllers.moveVertical = 0;
        MobileControllers.moveHorizontal = 0;
    }   

    public void MoveBlop()
    { 
}
    IEnumerator Move(Vector3 direction, float Scale)
    {
        Vector3 previousPoint = gameObject.transform.position;
        Vector3 nextPoint = new Vector3(transform.position.x + direction.x, transform.position.y + direction.y, transform.position.z + direction.z);

        canMove = false;
        gameObject.transform.position = nextPoint;
        yield return new WaitForSeconds(WaitBeforeMoveSeconds);

        canMove = true;
    }

}
