using UnityEngine;

public class Restart : MonoBehaviour {
    
	void OnTriggerEnter(Collider c)
	{
        GameManager.Instance.RestartScene();
    }
}
