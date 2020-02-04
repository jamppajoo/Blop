using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {
    // Use this for initialization

    public static MenuController MC;

    GameObject[] levelPackLeveles;
    void Start() {

        setInVisible();
    }
	
    public void setVisible()
    {
        
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            print("NNÄKKYYYY");
        }

    }
    public void setInVisible()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
            print("Ei näyyy");
        }
    }
}
