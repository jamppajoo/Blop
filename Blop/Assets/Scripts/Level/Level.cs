using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
