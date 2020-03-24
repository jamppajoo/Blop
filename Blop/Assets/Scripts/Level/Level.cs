using UnityEngine;

/// <summary>
/// Enable and disable level
/// </summary>
public class Level : MonoBehaviour
{
    [HideInInspector]
    public string levelName = "";

    private void Awake()
    {
        levelName = gameObject.name;
    }

    public void RestartLevel()
    {
    }
    public void LoadLevel()
    {
        gameObject.SetActive(true);
    }
    public void UnLoadLevel()
    {
        gameObject.SetActive(false);
    }
}
