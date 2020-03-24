using UnityEngine;
/// <summary>
/// Restart scene if player hits collider
/// </summary>
public class Restart : MonoBehaviour {
    
	void OnTriggerEnter(Collider c)
	{
        GameManager.Instance.RestartScene();
    }
}
