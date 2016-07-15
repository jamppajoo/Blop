using UnityEngine;
using System.Collections;

public class BlobMovement3 : MonoBehaviour {
    float horizontalMovement, verticalMovement;
    public float WaitBeforeMoveSeconds;
    private bool canMove = true;
    
	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        Ray DownHit = new Ray(transform.position, Vector3.down);
        Ray RightHit = new Ray(transform.position, Vector3.right);
        Ray LeftHit = new Ray(transform.position, Vector3.left);
        Ray BackHit = new Ray(transform.position, Vector3.forward);
        Ray FrontHit = new Ray(transform.position, Vector3.back);


        if ((Physics.Raycast(DownHit, out hit)) && hit.distance < 1 && hit.transform.gameObject.tag == "Floor")
            Debug.Log("DownHit");

        if ((Physics.Raycast(RightHit, out hit))&& hit.distance <1 && hit.transform.gameObject.tag == "RightWall")
                Debug.Log("RightHit");

        if ((Physics.Raycast(LeftHit, out hit)) && hit.distance < 1  && hit.transform.gameObject.tag == "LeftWall")
            Debug.Log("LeftHit");

        if ((Physics.Raycast(BackHit, out hit)) && hit.distance < 1 && hit.transform.gameObject.tag == "BackWall")
            Debug.Log("BackHit");

        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
       // Debug.Log("HorizontalMovement  :" + horizontalMovement + "VerticalMovement    :" + verticalMovement);
        // Debug.Log(canMove);

        if (canMove && (horizontalMovement !=0 || verticalMovement !=0) && CameraMovement.isDown)
            StartCoroutine(Move(new Vector3(horizontalMovement,verticalMovement,0)));
        else if (canMove && (horizontalMovement != 0 || verticalMovement != 0) && CameraMovement.isUp)
            StartCoroutine(Move(new Vector3(horizontalMovement, 0, verticalMovement)));

    }
    IEnumerator Move(Vector3 direction)
    {
        canMove = false;
        gameObject.transform.position = new Vector3(transform.position.x + direction.x, transform.position.y + direction.y, transform.position.z + direction.z);

        yield return new WaitForSeconds(WaitBeforeMoveSeconds);
        canMove = true;
    }
}
