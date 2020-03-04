using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelNumber : MonoBehaviour
{

    private void OnEnable()
    {
        EventManager.OnNewLevelLoaded += SetLevelName;
    }
    private void OnDisable()
    {
        EventManager.OnNewLevelLoaded -= SetLevelName;
    }
    private void Start()
    {
        SetLevelName();
    }

    private void SetLevelName()
    {
        gameObject.GetComponent<Text>().text = GameManager.Instance.levelName;
    }

}
