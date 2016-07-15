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
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
        Debug.Log("HorizontalMovement  :" + horizontalMovement + "VerticalMovement    :" + verticalMovement);
        Debug.Log(canMove);

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
