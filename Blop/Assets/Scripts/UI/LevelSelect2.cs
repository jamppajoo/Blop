using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect2 : MonoBehaviour
{

    private void LoadScene(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

}
