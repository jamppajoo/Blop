using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintSystem : MonoBehaviour
{
    public bool isShowing = false;
    [SerializeField]
    private Button backButton;


    private void Awake()
    {
        backButton.onClick.AddListener(DisappearMenu);
    }
    private void Start()
    {
        DisappearMenu();

    }
    public void ShowMenu(bool show)
    {
        if (!show)
            DisappearMenu();
        else
            ShowMenu();

    }
    private void DisappearMenu()
    {
        isShowing = false;
        gameObject.SetActive(false);
    }
    private void ShowMenu()
    {
        if (isShowing)
        {
            DisappearMenu();
            return;
        }

        isShowing = true;
        gameObject.SetActive(true);
    }
}
