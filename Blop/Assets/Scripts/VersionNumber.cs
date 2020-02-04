using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VersionNumber : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Text>().text = gameObject.GetComponent<Text>().text + " " +Application.version;
	}
}
