using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelStarSystem : MonoBehaviour {

    private Text buttonPressesText;
    public int threeStarMovementAmountIns, twoStarMovementAmountIns, oneStarMovementAmountIns;
    private Image oneStar, twoStar, threeStar;
    private int buttonPressesAmount = 0;


    // Use this for initialization
    void Start () {
        buttonPressesText = GameObject.Find("ButtonPressesText").GetComponent<Text>();
        oneStar = GameObject.Find("oneStar").GetComponent<Image>();
        twoStar = GameObject.Find("twoStar").GetComponent<Image>();
        threeStar = GameObject.Find("threeStar").GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {
        buttonPressesAmount = BlobMovement.buttonPresses;
        buttonPressesText.text = buttonPressesAmount.ToString();
	}
    //Controlled from Finish script
    public void levelPassed()
    {
         int threeStarMovementAmount, twoStarMovementAmount = twoStarMovementAmountIns, oneStarMovementAmount = oneStarMovementAmountIns;
        threeStarMovementAmount = threeStarMovementAmountIns;
         buttonPressesAmount = BlobMovement.buttonPresses;
        Debug.Log(threeStarMovementAmount);

        if (buttonPressesAmount <= threeStarMovementAmount)
            Debug.Log("Three stars! gg");
        else if (buttonPressesAmount > threeStarMovementAmount)
            Debug.Log("Two stars! iha semi");
        else if (buttonPressesAmount > twoStarMovementAmount)
            Debug.Log("One star, nojaa");
        else if (buttonPressesAmount > oneStarMovementAmount)
            Debug.Log("Juu koitappa uusiks homo");
        
    }
}
