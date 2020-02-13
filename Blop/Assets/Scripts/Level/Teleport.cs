﻿using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {

    private GameObject upperTeleport;
    private GameObject Blop;

	// Use this for initialization
	void Start () {
        Blop = GameObject.Find("Blop");
        upperTeleport = GameObject.Find("UpperTeleport");
	
	}
	

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.name == "Blop")
        {
            c.transform.position = new Vector3(c.transform.position.x, upperTeleport.transform.position.y, c.transform.position.z);
        }
    }
}