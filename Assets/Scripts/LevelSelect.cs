using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

/*
    Handles LevelPack1 and LevelPack2 buttons and buttons stars. Also loads scene when button is pressed.
*/

public class LevelSelect : MonoBehaviour {

    public List<Button> levels = new List<Button>();
    
    private Text levelStars;
    
    public void Start()
    {

        //Get every child object in "Image" gameobject in Menu scene.
        int i = 0;
        foreach(Transform child in transform)
        {
            if (child.gameObject.transform.name.StartsWith("Level1")) // If they are Level1.x buttons
            {
                child.GetChild(1).GetComponent<Text>().text = GameManager.sharedGM.LevelPack1Stars[i].ToString();
                levels.Add(child.GetComponent<Button>());             //Add them to levels list. Creates list to inspector as well
                if (child.GetChild(1).GetComponent<Text>().text != 4.ToString()) // If star amount is not 4 (what is used to appear level has not finised)
                {
                    child.GetComponent<Button>().interactable = true;
                }
                else child.GetComponent<Button>().interactable = false;
                
                //Make stars appear below level button, shitty code but works.
                switch (GameManager.sharedGM.LevelPack1Stars[i])
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
            else if (child.gameObject.transform.name.StartsWith("Level2")) // Do same thing to Levels 2.x
            {
                child.GetChild(1).GetComponent<Text>().text = GameManager.sharedGM.LevelPack2Stars[i].ToString();
                levels.Add(child.GetComponent<Button>());
                if (child.GetChild(1).GetComponent<Text>().text != 4.ToString()  )
                {
                    child.GetComponent<Button>().interactable = true;
                }
                else  child.GetComponent<Button>().interactable = false;
                switch (GameManager.sharedGM.LevelPack2Stars[i])
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
            //Add listeners to every button, so that they work.
            string temp = b.gameObject.transform.name;
            b.onClick.AddListener(() => LoadScene(temp));
        }
        
    }

	public void LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}
