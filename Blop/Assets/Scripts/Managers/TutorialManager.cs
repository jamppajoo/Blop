using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MEC;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    private Button changeViewButton, hintButton;

    [SerializeField]
    private float pulseSpeed = 0.5f;

    private CurrentLevel currentLevel;
    private enum CurrentLevel
    {
        Level1,
        Level2,
        Level3,
        Other
    }

    private RectTransform changeViewButtonRectTransform, hintButtonRectTransform;
    private Vector2 changeViewButtonOriginalScale, hintButtonOriginalScale;
    private bool changeViewButtonPulsing = false, hintButtonPulsing = false;
    private const string pulseButtonCoroutineName = "PulseButtonCoroutine";

    private void Awake()
    {
        changeViewButtonRectTransform = changeViewButton.GetComponent<RectTransform>();
        hintButtonRectTransform = hintButton.GetComponent<RectTransform>();

        changeViewButtonOriginalScale = changeViewButtonRectTransform.sizeDelta;
        hintButtonOriginalScale = hintButtonRectTransform.sizeDelta;
    }
    private void OnEnable()
    {
        EventManager.OnNewLevelLoaded += NewLevelLoaded;
        EventManager.OnLevelRestarted += LevelRestarted;
        EventManager.OnChangeViewPressed += ChangeViewButtonPressed;
        EventManager.OnHintButtonPressed += HintButtonPressed;
    }
    private void OnDisable()
    {
        EventManager.OnNewLevelLoaded -= NewLevelLoaded;
        EventManager.OnLevelRestarted -= LevelRestarted;
        EventManager.OnChangeViewPressed -= ChangeViewButtonPressed;
        EventManager.OnHintButtonPressed += HintButtonPressed;
    }

    private void NewLevelLoaded()
    {
        PulseChangeViewButton(false);
        PulseHintButton(false);
        switch (GameManager.Instance.levelName)
        {
            case "Level1.1":
                currentLevel = CurrentLevel.Level1;
                break;
            case "Level1.2":
                currentLevel = CurrentLevel.Level2;
                break;
            case "Level1.3":
                currentLevel = CurrentLevel.Level3;
                PulseHintButton(true);
                break;
            default:
                currentLevel = CurrentLevel.Other;
                break;
        }
    }
    private void LevelRestarted()
    {
        print("Level restarted");
        if(currentLevel == CurrentLevel.Level1)
        {
            PulseChangeViewButton(true);
        }
        if (currentLevel == CurrentLevel.Level2)
        {

        }
    }
    private void ChangeViewButtonPressed()
    {
        if (changeViewButtonPulsing)
        {
            PulseChangeViewButton(false);
        }
    }
    private void HintButtonPressed()
    {
        if (hintButtonPulsing)
        {
            PulseHintButton(false);
        }
    }
    private void PulseChangeViewButton(bool pulse)
    {
        if (pulse)
        {
            changeViewButtonPulsing = true;
            Timing.RunCoroutine(_PulseButtonSize(pulseSpeed, changeViewButtonRectTransform), pulseButtonCoroutineName);
        }
        else
        {
            changeViewButtonPulsing = false;
            changeViewButtonRectTransform.sizeDelta = changeViewButtonOriginalScale;
            Timing.KillCoroutines(pulseButtonCoroutineName);
        }
    }
    private void PulseHintButton(bool pulse)
    {
        if (pulse)
        {
            hintButtonPulsing = true;
            Timing.RunCoroutine(_PulseButtonSize(pulseSpeed, hintButtonRectTransform), pulseButtonCoroutineName);
        }
        else
        {
            hintButtonPulsing = false;
            hintButtonRectTransform.sizeDelta = hintButtonOriginalScale;
            Timing.KillCoroutines(pulseButtonCoroutineName);
        }
    }


    private IEnumerator<float> _PulseButtonSize(float speed, RectTransform objectToPulse)
    {
        float time = 0;
        bool rising = false;

        Vector2 originalScale = objectToPulse.sizeDelta;
        while (true)
        {
            if (time >= speed)
                rising = false;
            if (time <= 0)
                rising = true;

            if (rising)
            {
                time += Time.deltaTime;
            }
            else
                time -= Time.deltaTime;

            objectToPulse.sizeDelta = Vector2.Lerp(originalScale, originalScale * 1.1f, time / speed);

            yield return 0;

        }
    }

    //TODO
    //Pulse change view button on levels 1 & 2 when player has died once
    //Text?? Helper lines?? to level 2 to indicate player to move on the green

    //Pulse hint button on level3
    //Make first hint free, so that player doesnt need to watch ad


}
