using UnityEngine;
using System.Collections;

public class BlobMovement : MonoBehaviour {
	private Animator animator;
	private GameObject player;
	private GameObject playerRB;
	private IsFloor isfloor;
	private bool moveRight, moveLeft;
	public static bool canMoveUp, canMoveDown, canMoveLeft,canMoveRight;
	public static bool  upCanMoveRight,upCanMoveUp,upCanMoveDown, upCanMoveLeft;
	private float position;

	public static bool isGrounded = false;
	public static bool animationHasEnded;

	


	void Start()
	{
		player = GameObject.Find ("Blop/Blop");
		animator = GetComponent<Animator> ();
		canMoveUp = false;
		canMoveDown = false;
		canMoveLeft = true;
		canMoveRight = true;
		upCanMoveRight = true;
		upCanMoveLeft = true;
		upCanMoveUp = true;
		upCanMoveDown= true;
	}
	

	
	// Update is called once per frame
	void Update () {
        Debug.Log(isGrounded);
		
		player.transform.rotation = Quaternion.AngleAxis (0, Vector3.right);
		if (isGrounded && animationHasEnded && CameraMovement.isDown) {
			SideView ();
		}
		else if (isGrounded && animationHasEnded && CameraMovement.isUp)
			UpView ();
		
	}

	void SideView()
	{
		//player.transform.parent.gameObject.GetComponent<Rigidbody>().AddForce(transform.up*-1);
		float horizontalMovement = Input.GetAxisRaw ("Horizontal") ;
		float verticalMovement = Input.GetAxisRaw ("Vertical") ;
        
		
		if (horizontalMovement > 0 && animationHasEnded && canMoveRight) {
			animator.SetBool ("MoveRight", true);
			animationHasEnded = false;
		} 
		else if (horizontalMovement < 0 && animationHasEnded && canMoveLeft) {
			animator.SetBool ("MoveLeft", true);
			animationHasEnded = false;
		}
		else if (verticalMovement > 0 && animationHasEnded && canMoveUp) {
			animator.SetBool ("JumpUp", true);
			animationHasEnded = false;
		}
		else if (verticalMovement < 0 && animationHasEnded && canMoveDown) {
			animator.SetBool ("JumpDown", true);
			animationHasEnded = false;
		}
		isGrounded = true;
	}
	void UpView()
	{
		

		float horizontalMovement = Input.GetAxisRaw ("Horizontal") ;
		float verticalMovement = Input.GetAxisRaw ("Vertical") ;

		if (horizontalMovement > 0 && animationHasEnded && upCanMoveRight) {
			animator.SetBool ("UpJumpRight", true);
			animationHasEnded = false;
		} 
		else if (horizontalMovement < 0 && animationHasEnded && upCanMoveLeft ) {
			animator.SetBool ("UpJumpLeft", true);
			animationHasEnded = false;
		}
		else if (verticalMovement > 0 && animationHasEnded && upCanMoveUp ) {
			animator.SetBool ("UpJumpUp", true);
			animationHasEnded = false;
		}
		else if (verticalMovement < 0 && animationHasEnded&& upCanMoveDown) {
			animator.SetBool ("UpJumpDown", true);
			animationHasEnded = false;
		}
	}

	
	void OnTriggerStay(Collider trigger)
	{
		if (trigger.gameObject.tag == "wall") {
			

			if (trigger.gameObject.GetComponent<BoxCollider> ().center.z == -1) {
				upCanMoveUp = false;
				Debug.Log ("onWallBlock");
			}
			else if (trigger.gameObject.GetComponent<BoxCollider> ().center.z == 1) {
				upCanMoveDown = false;
			}
			else if (trigger.gameObject.GetComponent<BoxCollider> ().center.x == -1) {
				upCanMoveRight = false;
				canMoveRight = false;
			}
			else if (trigger.gameObject.GetComponent<BoxCollider> ().center.x == 1) {
				upCanMoveLeft = false;
				canMoveLeft = false;
			}
			player.transform.parent.gameObject.GetComponent<Rigidbody> ().isKinematic = true;

			canMoveUp = true;
			canMoveDown = true;
			isGrounded = true;	

		}
		else if (trigger.gameObject.tag == "floor") {
			isGrounded = true;
			canMoveDown = false;

		}

		else if (trigger.gameObject.tag == "solid") {
			

			if (trigger.gameObject.GetComponent<BoxCollider> ().center.z == -1) {
				upCanMoveUp = false;
			}
			else if (trigger.gameObject.GetComponent<BoxCollider> ().center.z == 1) {
				upCanMoveDown = false;
			}
			else if (trigger.gameObject.GetComponent<BoxCollider> ().center.x == -1) {
				upCanMoveRight = false;
				canMoveRight = false;
			}
			else if (trigger.gameObject.GetComponent<BoxCollider> ().center.x == 1) {
				upCanMoveLeft = false;
				canMoveLeft = false;
			}
			else if (trigger.gameObject.GetComponent<BoxCollider> ().center.y == -1) {
				canMoveUp = false;
				Debug.Log ("onSolidTopBlock");
			}
		}
	}
	void OnTriggerExit(Collider trigger)
	{
		if (trigger.gameObject.tag == "wall" || trigger.gameObject.tag == "solid") {
			
			isGrounded = false;	
			player.transform.parent.gameObject.GetComponent<Rigidbody> ().isKinematic = false;

			canMoveUp = false;
			canMoveDown = false;
			canMoveLeft = true;
			canMoveRight = true;
			upCanMoveUp = true;
			upCanMoveDown = true;
			upCanMoveLeft = true;
			upCanMoveRight = true;


		}
		else if (trigger.gameObject.tag == "floor") {
			player.transform.parent.gameObject.GetComponent<Rigidbody> ().isKinematic = false;
            isGrounded = false;

		} 

		
	}

	
}
