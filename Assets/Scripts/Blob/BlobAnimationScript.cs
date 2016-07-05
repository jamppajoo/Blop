using UnityEngine;
using System.Collections;

public class BlobAnimationScript : MonoBehaviour {

	private Animator animator;
	private GameObject player;
	private bool moveLeft, moveRight, jumpUp, jumpDown, upJumpUp, upJumpDown, upJumpLeft, upJumpRight;
	void Start()
	{
		player = GameObject.Find ("Blop/Blop");
		animator = GetComponent<Animator> ();

	}

	void enableAnimation()
	{	
		moveRight = animator.GetBool ("MoveRight");
		moveLeft = animator.GetBool ("MoveLeft");
		jumpUp = animator.GetBool ("JumpUp");
		jumpDown = animator.GetBool ("JumpDown");
		upJumpUp = animator.GetBool ("UpJumpUp");
		upJumpDown = animator.GetBool ("UpJumpDown");
		upJumpLeft = animator.GetBool ("UpJumpLeft");
		upJumpRight = animator.GetBool ("UpJumpRight");


		animator.SetBool ("MoveRight", false);	
		animator.SetBool ("MoveLeft", false);
		animator.SetBool ("JumpUp", false);
		animator.SetBool ("JumpDown", false);
		animator.SetBool ("UpJumpUp", false);
		animator.SetBool ("UpJumpDown", false);
		animator.SetBool ("UpJumpRight", false);
		animator.SetBool ("UpJumpLeft", false);

	}

	void blopIdle()
	{	
		
		if (moveRight && !animator.GetBool("MoveRight"))
			player.transform.parent.position = new Vector3 (player.transform.parent.position.x + 1, player.transform.parent.position.y, player.transform.parent.position.z);
		else if (moveLeft && !animator.GetBool("MoveLeft"))
			player.transform.parent.position = new Vector3 (player.transform.parent.position.x - 1, player.transform.parent.position.y, player.transform.parent.position.z);
		else if (jumpUp && !animator.GetBool("JumpUp"))
			player.transform.parent.position = new Vector3 (player.transform.parent.position.x , player.transform.parent.position.y +1, player.transform.parent.position.z);
		else if (jumpDown && !animator.GetBool("JumpDown"))
			player.transform.parent.position = new Vector3 (player.transform.parent.position.x , player.transform.parent.position.y -1, player.transform.parent.position.z);
		else if (upJumpUp && !animator.GetBool("UpJumpUp"))
			player.transform.parent.position = new Vector3 (player.transform.parent.position.x , player.transform.parent.position.y , player.transform.parent.position.z +1);
		else if (upJumpDown && !animator.GetBool("UpJumpDown"))
			player.transform.parent.position = new Vector3 (player.transform.parent.position.x , player.transform.parent.position.y , player.transform.parent.position.z -1);
		else if (upJumpLeft && !animator.GetBool("UpJumpLeft"))
			player.transform.parent.position = new Vector3 (player.transform.parent.position.x -1, player.transform.parent.position.y , player.transform.parent.position.z );
		else if (upJumpRight && !animator.GetBool("UpJumpRight"))
			player.transform.parent.position = new Vector3 (player.transform.parent.position.x +1, player.transform.parent.position.y , player.transform.parent.position.z );

		BlobMovement.animationHasEnded = true;
		moveLeft = false;
		moveRight = false;
		jumpUp = false;
		jumpDown = false;
		upJumpUp = false;
		upJumpDown = false;
		upJumpLeft = false;
		upJumpRight = false;
		BlobMovement.isGrounded = false;
	}
}
