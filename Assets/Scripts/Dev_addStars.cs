using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Dev_addStars : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Button>().onClick.AddListener(() => AddStars());
    }
    public void AddStars()
    {
        if (GameManager.totalButtonPressesLeft + 10 <= GameManager.sharedGM.buttonPressesMax)
            GameManager.totalButtonPressesLeft += 10;
        GameManager.sharedGM.Save();
    }
}
