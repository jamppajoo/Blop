using UnityEngine;
using System.Collections;

public class CameraMovement : Singleton<CameraMovement>
{
    private Animator animator;
    public bool isUp, isDown, rotatedUp, rotatedDown;
    public bool viewChanged = false;
    public bool cameraRotate = false;
    public bool onMenu = false;
    private GameObject player, mainCamera;
    public float speed = 5f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("Blop");
        mainCamera = Camera.main.gameObject;
        isUp = false;
        rotatedUp = false;
        isDown = true;
        rotatedDown = true;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) || viewChanged)
        {
            BlobMovement.buttonPresses++;
            SetStuff(false);
            if (!cameraRotate)
            {
                if (!isUp)
                    animator.SetBool("ToUp", true);
                else if (!isDown)
                    animator.SetBool("ToDown", true);
            }
            else if (cameraRotate)
            {
                if (!rotatedUp)
                    animator.SetBool("RotateToUp", true);
                else if (!rotatedDown)
                    animator.SetBool("RotateToDown", true);
            }
        }
        viewChanged = false;
    }

    //Move camera to players position
    public void FixedUpdate()
    {
        mainCamera.gameObject.transform.position = Vector3.Lerp(mainCamera.gameObject.transform.position, player.gameObject.transform.position, speed * Time.deltaTime);
    }
    private void IsUp()
    {
        isUp = true;
        isDown = false;
        animator.SetBool("ToUp", false);
        SetStuff(true);
    }

    private void IsDown()
    {
        isUp = false;
        isDown = true;
        animator.SetBool("ToDown", false);
        SetStuff(true);
    }

    private void RotatedIsUp()
    {
        rotatedUp = true;
        rotatedDown = false;
        isDown = false;
        animator.SetBool("RotateToUp", false);
        SetStuff(true);

    }

    private void RotatedIsDown()
    {
        rotatedUp = false;
        rotatedDown = true;
        isDown = true;
        animator.SetBool("RotateToDown", false);
        SetStuff(true);

    }

    private void SetStuff(bool active)
    {
        MobileControllers.ChangeView.interactable = active;
        MobileControllers.Up.interactable = active;
        MobileControllers.Down.interactable = active;
        MobileControllers.Left.interactable = active;
        MobileControllers.Right.interactable = active;
        MobileControllers.Back.interactable = active;
        MobileControllers.canPress = active;
    }
    
}
