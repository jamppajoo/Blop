using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DeletePersonalData : MonoBehaviour {
    
	void Start () {

        gameObject.GetComponent<Button>().onClick.AddListener(() => DeleteData());
	}
    public void DeleteData()
    {
        GameManager.sharedGM.DeletePersonalData();
    }
}
