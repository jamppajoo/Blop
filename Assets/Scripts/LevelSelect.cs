using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelSelect : MonoBehaviour {

    public List<Button> levels = new List<Button>();
    
    private Text levelStars;
    private GameManager GameManager;
    
    public void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        int i = 0;
        foreach(Transform child in transform)
        {
            if (child.gameObject.transform.name.StartsWith("Level1"))
            {
                child.GetChild(1).GetComponent<Text>().text = GameManager.LevelPack1Stars[i].ToString();
                levels.Add(child.GetComponent<Button>());
                if (child.GetChild(1).GetComponent<Text>().text != 4.ToString())
                {
                    child.GetComponent<Button>().interactable = true;
                }
                else child.GetComponent<Button>().interactable = false;
                
                switch (GameManager.LevelPack1Stars[i])
                {
                    case 1:
                        child.GetChild(1).GetChild(0).GetComponent<Image>().enabled = false;
                        child.GetChild(1).GetChild(1).GetComponent<Image>().enabled = true;
                        break;
                    case 2:
                        child.GetChild(1).GetChild(2).GetComponent<Image>().enabled = false;
                        child.GetChild(1).GetChild(0).GetComponent<Image>().enabled = false;
                        child.GetChild(1).GetChild(3).GetComponent<Image>().enabled = true;
                        child.GetChild(1).GetChild(1).GetComponent<Image>().enabled = true;
                        break;
                    case 3:
                        child.GetChild(1).GetChild(4).GetComponent<Image>().enabled = false;
                        child.GetChild(1).GetChild(2).GetComponent<Image>().enabled = false;
                        child.GetChild(1).GetChild(0).GetComponent<Image>().enabled = false;
                        child.GetChild(1).GetChild(5).GetComponent<Image>().enabled = true;
                        child.GetChild(1).GetChild(3).GetComponent<Image>().enabled = true;
                        child.GetChild(1).GetChild(1).GetComponent<Image>().enabled = true;
                        break;

                }
                i++;


            }
            else if (child.gameObject.transform.name.StartsWith("Level2"))
            {
                child.GetChild(1).GetComponent<Text>().text = GameManager.LevelPack2Stars[i].ToString();
                levels.Add(child.GetComponent<Button>());
                if (child.GetChild(1).GetComponent<Text>().text != 4.ToString())
                {
                    child.GetComponent<Button>().interactable = true;
                }
                else child.GetComponent<Button>().interactable = false;
                switch (GameManager.LevelPack2Stars[i])
                {
                    case 1:
                        child.GetChild(1).GetChild(0).GetComponent<Image>().enabled = false;
                        child.GetChild(1).GetChild(1).GetComponent<Image>().enabled = true;
                        break;
                    case 2:
                        child.GetChild(1).GetChild(2).GetComponent<Image>().enabled = false;
                        child.GetChild(1).GetChild(0).GetComponent<Image>().enabled = false;
                        child.GetChild(1).GetChild(3).GetComponent<Image>().enabled = true;
                        child.GetChild(1).GetChild(1).GetComponent<Image>().enabled = true;
                        break;
                    case 3:
                        child.GetChild(1).GetChild(4).GetComponent<Image>().enabled = false;
                        child.GetChild(1).GetChild(2).GetComponent<Image>().enabled = false;
                        child.GetChild(1).GetChild(0).GetComponent<Image>().enabled = false;
                        child.GetChild(1).GetChild(5).GetComponent<Image>().enabled = true;
                        child.GetChild(1).GetChild(3).GetComponent<Image>().enabled = true;
                        child.GetChild(1).GetChild(1).GetComponent<Image>().enabled = true;
                        break;

                }
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
