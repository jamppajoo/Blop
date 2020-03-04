using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectMoving : MonoBehaviour
{
    [SerializeField]
    private float movingSpeed;

    [SerializeField]
    [Tooltip("Amount that velocity is reduced when player releases touch")]
    private float levelSelectVelocityDampening = 60;

    [SerializeField]
    [Tooltip("Levelselect velocity moving speed")]
    private float levelSelectVelocitySpeed = 0.04f;

    [SerializeField]
    private Vector2 movingAmountMin, movingAmountMax;

    private GameObject mainCamera;

    private float width, height;
    private Vector2 currentPos = Vector2.zero, touchCurrentPosition = Vector2.zero, touchOldPosition = Vector2.zero, touchOffset = Vector2.zero;
    private Vector2[] velocitySamples = new Vector2[5];
    private int currentSampleCount;

    private const string levelSelectVelocityMovementCoroutine = "levelSelectVelocityMovementCoroutine";

    private void Awake()
    {
        mainCamera = Camera.main.gameObject;
    }

    private void Update()
    {
        GetTouchPosition();
    }

    private void GetTouchPosition()
    {
        width = (float)Screen.width / 2.0f;
        height = (float)Screen.height / 2.0f;


        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            currentPos = touch.position;
            if (touch.phase == TouchPhase.Began)
            {
                touchCurrentPosition = new Vector3(currentPos.x, currentPos.y, 0.0f);
                Timing.KillCoroutines(levelSelectVelocityMovementCoroutine);
            }

            // Move the cube if the screen has the finger moving.
            if (touch.phase == TouchPhase.Moved)
            {
                touchOldPosition = touchCurrentPosition;
                currentPos.x = (currentPos.x - width) / width;
                currentPos.y = (currentPos.y - height) / height;
                touchCurrentPosition = new Vector3(currentPos.x, currentPos.y, 0.0f);
                touchOffset = touchCurrentPosition - touchOldPosition;
                EstimateVelocity();
                MoveLevelSelect();
            }
            if (touch.phase == TouchPhase.Ended)
            {
                AddVelocityToLevelSelect();
            }
        }

        currentPos = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            touchCurrentPosition = new Vector3(currentPos.x, currentPos.y, 0.0f);
            Timing.KillCoroutines(levelSelectVelocityMovementCoroutine);
        }

        else if (Input.GetMouseButton(0))
        {
            touchOldPosition = touchCurrentPosition;
            currentPos.x = (currentPos.x - width) / width;
            currentPos.y = (currentPos.y - height) / height;
            touchCurrentPosition = new Vector3(currentPos.x, currentPos.y, 0.0f);
            touchOffset = touchCurrentPosition - touchOldPosition;
            EstimateVelocity();
            MoveLevelSelect();
        }

        if (Input.GetMouseButtonUp(0))
        {
            AddVelocityToLevelSelect();
        }
    }

    private void AddVelocityToLevelSelect()
    {
        Timing.RunCoroutine(_LevelSelectVelocityMovement(GetVelocityEstimate()).CancelWith(gameObject), levelSelectVelocityMovementCoroutine);
    }
    private void MoveLevelSelect()
    {
        Vector3 newPosition = gameObject.transform.localPosition + new Vector3(touchOffset.x, 0, touchOffset.y) * movingSpeed;
        if (newPosition.x > movingAmountMin.x && newPosition.x < movingAmountMax.x && newPosition.z > movingAmountMin.y && newPosition.z < movingAmountMax.y)
            gameObject.transform.localPosition = newPosition;

    }
    private void EstimateVelocity()
    {
        float velocityFactor = 1.0f / Time.deltaTime;

        int v = currentSampleCount % velocitySamples.Length;
        currentSampleCount++;

        velocitySamples[v] = velocityFactor * (touchCurrentPosition - touchOldPosition);

    }
    private Vector2 GetVelocityEstimate()
    {
        Vector2 velocity = Vector3.zero;
        int velocitySampleCount = Mathf.Min(currentSampleCount, velocitySamples.Length);
        if (velocitySampleCount != 0)
        {
            for (int i = 0; i < velocitySampleCount; i++)
            {
                velocity += velocitySamples[i];
            }
            velocity *= (1.0f / velocitySampleCount);
        }
        return velocity;
    }

    private IEnumerator<float> _LevelSelectVelocityMovement(Vector2 velocity)
    {
        Vector2 currentVelocity = velocity;
        while (currentVelocity.magnitude > 0.05f)
        {
            currentVelocity -= currentVelocity / levelSelectVelocityDampening;
            Vector3 newPosition = gameObject.transform.localPosition + (new Vector3(currentVelocity.x, 0, currentVelocity.y) * levelSelectVelocitySpeed);
            if (newPosition.x > movingAmountMin.x && newPosition.x < movingAmountMax.x && newPosition.z > movingAmountMin.y && newPosition.z < movingAmountMax.y)
                gameObject.transform.localPosition = newPosition;
            yield return 0;
        }
    }
    //Handle movement system based on touch or mouse dragging
    //Handle all level buttons and their assigns to correct levels
    //Handle stars showed on top of the levels

}
