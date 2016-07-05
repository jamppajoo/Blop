using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

	private GameObject player;
	public bool isGround;
	public bool isWall;
	public bool isSolid;

	void Start()
	{
		player = GameObject.Find ("Blop/Blop");
	}


	void OnTriggerStay(Collider trigger)
	{
         if (trigger.gameObject.tag == "Player" && isGround == true)
         {
            Debug.Log("Onthefloor");
            if (gameObject.GetComponent<BoxCollider>().center.z == -1)
            {
                BlobMovement.upCanMoveUp = false;
            }
            else if (gameObject.GetComponent<BoxCollider>().center.z == 1)
            {
                BlobMovement.upCanMoveDown = false;
            }

            else if (gameObject.GetComponent<BoxCollider>().center.x == -1)
            {
                BlobMovement.upCanMoveRight = false;
                BlobMovement.canMoveRight = false;
            }
            else if (gameObject.GetComponent<BoxCollider>().center.x == 1)
            {
                BlobMovement.upCanMoveLeft = false;
                BlobMovement.canMoveLeft = false;
            }
            player.transform.parent.gameObject.GetComponent<Rigidbody>().isKinematic = false;

            //BlobMovement.canMoveUp = true;
            BlobMovement.isGrounded = true;

        }

        else if (trigger.gameObject.tag == "Player" && isWall == true)
        {
            player.transform.parent.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            if (gameObject.GetComponent<BoxCollider>().center.z == -1)
            {
                BlobMovement.upCanMoveUp = false;
                BlobMovement.canMoveUp = true;
                BlobMovement.canMoveLeft = true;
                BlobMovement.canMoveRight = true;
                BlobMovement.canMoveDown = true;
                if (isGround)
                    BlobMovement.canMoveDown = false;
                

                Debug.Log("onTheWall");
            }


        }
       



		/*

		if (trigger.gameObject.tag == "wall") {



		}
		else if (trigger.gameObject.tag == "Player") {
			Debug.Log ("onFloorBlock");
			BlobMovement.isGrounded = true;
			BlobMovement.canMoveDown = false;

		}

		else if (trigger.gameObject.tag == "solid") {


			if (trigger.gameObject.GetComponent<BoxCollider> ().center.z == -1) {
				BlobMovement.upCanMoveUp = false;
			}
			else if (trigger.gameObject.GetComponent<BoxCollider> ().center.z == 1) {
				BlobMovement.upCanMoveDown = false;
			}
			else if (trigger.gameObject.GetComponent<BoxCollider> ().center.x == -1) {
				BlobMovement.upCanMoveRight = false;
				BlobMovement.canMoveRight = false;
			}
			else if (trigger.gameObject.GetComponent<BoxCollider> ().center.x == 1) {
				BlobMovement.upCanMoveLeft = false;
				BlobMovement.canMoveLeft = false;
			}
			else if (trigger.gameObject.GetComponent<BoxCollider> ().center.y == -1) {
				BlobMovement.canMoveUp = false;
				Debug.Log ("onSolidTopBlock");
			}
		}
		*/

	}

	void OnTriggerExit(Collider trigger)
	{
		if (trigger.gameObject.tag == "Player" && isWall == true) {

			BlobMovement.isGrounded = false;	
			player.transform.parent.gameObject.GetComponent<Rigidbody> ().isKinematic = false;

			BlobMovement.canMoveUp = false;
			BlobMovement.canMoveDown = false;
			BlobMovement.canMoveLeft = true;
			BlobMovement.canMoveRight = true;
			BlobMovement.upCanMoveUp = true;
			BlobMovement.upCanMoveDown = true;
			BlobMovement.upCanMoveLeft = true;
			BlobMovement.upCanMoveRight = true;
			Debug.Log ("Exitted Wall");


		}
		else if (trigger.gameObject.tag == "Player" && isGround == true) {
			player.transform.parent.gameObject.GetComponent<Rigidbody> ().isKinematic = false;
		} 


	}
	}
