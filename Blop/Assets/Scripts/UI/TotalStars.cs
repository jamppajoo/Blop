using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TotalStars : MonoBehaviour {
    public int maxStarAmount;
    private int totalStarAmount;
	void Start () {

        //Get total star amount from GameManager script and output it to text
        totalStarAmount = GameManager.Instance.ReturnTotalStarAmount();
        gameObject.GetComponent<Text>().text = totalStarAmount.ToString() + "/" + maxStarAmount;

    }
}
