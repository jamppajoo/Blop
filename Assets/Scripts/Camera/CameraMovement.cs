using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	private Animator animator;
	public static bool isUp, isDown;
    public static bool changeMade = false;
    public bool cameraRotate = false;
	
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		isUp = false;
		isDown = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space) || changeMade ) {
            if(!cameraRotate)
            {
                if (!isUp)
                    animator.SetBool("ToUp", true);
                else if (!isDown)
                    animator.SetBool("ToDown", true);
            }
            else if(cameraRotate)
            {
                if (!isUp)
                    animator.SetBool("RotateToUp", true);
                else if (!isDown)
                    animator.SetBool("RotateToDown", true);
            }
		}
        changeMade = false;
	
	}
	void IsUp()
	{
        Debug.Log("asdads");
		isUp = true;
		isDown = false;
		animator.SetBool ("ToUp", false);
        animator.SetBool("RotateToUp", false);
    }
	void IsDown(){
		isDown = true;
		isUp = false;
        animator.SetBool ("ToDown", false);
        animator.SetBool("RotateToDown", false);
    }
}
