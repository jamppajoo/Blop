using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelSelect : MonoBehaviour {

    public List<Button> levels = new List<Button>();
    
    private Text levelStars;
    
    public void Start()
    {
        int i = 0;
        foreach(Transform child in transform)
        {
            if (child.gameObject.transform.name.StartsWith("Level"))
            {
                levels.Add(child.GetComponent<Button>());
                child.GetChild(1).GetComponent<Text>().text = GameObject.Find("GameManager").GetComponent<GameManager>().LevelPack1Stars[i].ToString();
                i++;    

            }
        }
        foreach (Button b in levels)
        {
            string temp = b.gameObject.transform.name;
            b.onClick.AddListener(() => LoadScene(temp));
        }
    }

	public void LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}
