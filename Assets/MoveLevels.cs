﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoveLevels : MonoBehaviour {

    public Button nextLevelsButton;
    public Button previousLevelsButton;
    private bool isOnLeft = false;
    public GameObject levels;
    public float smoothTime = 0.1f;
    private Vector2 startPoint;
    private Vector2 movePoint;



    public void Start()
    {
        nextLevelsButton.onClick.AddListener(() => nextButtonPressed());
        startPoint = levels.GetComponent<RectTransform>().anchoredPosition;
        movePoint = new Vector2(levels.GetComponent<RectTransform>().anchoredPosition.x - 1920, levels.GetComponent<RectTransform>().anchoredPosition.y);
    }
    public void Update()
    {
        
    }
    public void nextButtonPressed()
    {
        while (movePoint.x < levels.GetComponent<RectTransform>().anchoredPosition.x)
        {
            moveToLocation();
            return;
        }
            
    }
    public void moveToLocation()
    {
        
        Debug.Log(startPoint.x);
        Debug.Log(movePoint.x);
        Debug.Log(levels.GetComponent<RectTransform>().anchoredPosition.x);

        levels.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(startPoint, movePoint, Time.deltaTime/smoothTime);
        
            
    }

}