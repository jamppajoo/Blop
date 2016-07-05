using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	private Animator animator;
	public static bool isUp, isDown;
	
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		isUp = false;
		isDown = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (!isUp)
				animator.SetBool ("ToUp", true);
			else if (!isDown)
				animator.SetBool ("ToDown", true);
		}
	
	}
	void IsUp()
	{
		isUp = true;
		isDown = false;
		animator.SetBool ("ToUp", false);
	}
	void IsDown(){
		isDown = true;
		isUp = false;
		animator.SetBool ("ToDown", false);
	}
}
