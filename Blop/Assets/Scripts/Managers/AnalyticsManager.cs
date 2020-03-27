using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using MEC;

/// <summary>
/// Handles collecting, sending and restarting all custom events.
/// </summary>
public class AnalyticsManager : MonoBehaviour
{
    #region Singleton

    private static AnalyticsManager _instance;

    public static AnalyticsManager Instance { get { return _instance; } }

    #endregion

    public bool sendAnalytics = true;

    #region dataToSend
    private string levelName = "Menu";
    private uint timeUsed = 0;
    private uint totalGameTime = 0;
    private bool hintUsed = false;
    private uint restartAmount = 0;
    private byte starAmount = 0;
    private uint movementAmount = 0;
    private uint rotationAmount = 0;
    private uint playedAmount = 0;
    #endregion

    private float currentTime = 0;
    private const string timerCoroutine = "TimerCoroutine";

    private void OnEnable()
    {
        EventManager.OnNewMovement += NewMovement;
        EventManager.OnNewRotation += NewRotation;

    }
    private void OnDisable()
    {
        EventManager.OnNewMovement -= NewMovement;
        EventManager.OnNewRotation -= NewRotation;

    }

    private void NewMovement()
    {
        movementAmount++;
    }

    private void NewRotation()
    {
        rotationAmount++;
    }

    private void Awake()
    {
        #region Singleton
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
        #endregion
    }
    private void Start()
    {
        RestartValues();
        SendLevelStartedAnalytics();
    }

    public void RestartValues()
    {
        Timing.KillCoroutines(timerCoroutine);
        levelName = "Menu";
        timeUsed = 0;
        hintUsed = false;
        restartAmount = 0;
        starAmount = 0;
        movementAmount = 0;
        rotationAmount = 0;
        playedAmount = 0;
        Timing.RunCoroutine(_TimeUsedTimer(), timerCoroutine);

    }
    public void SetLevelName(string levelName)
    {
        this.levelName = levelName;
    }
    public void SetTimeUsed(int timeInSeconds)
    {
        timeUsed = (uint)timeInSeconds;
    }
    public void HintUsed()
    {
        hintUsed = true;
    }
    public void AddRestartAmount()
    {
        restartAmount++;
    }
    public void SetStarAmount(int starAmount)
    {
        this.starAmount = (byte)starAmount;
    }
    public void SetPlayedAmount(int playedAmount)
    {
        this.playedAmount = (uint)playedAmount;
    }

    public void SendLevelPassingAnalytics()
    {
        Timing.KillCoroutines(timerCoroutine);
        timeUsed = (uint)currentTime;
        totalGameTime = GameManager.Instance.totalGameTime;

        if (sendAnalytics)
        {
            Analytics.CustomEvent("levelPassed", new Dictionary<string, object>
            {
                { "level", levelName }, // 14 byte
                { "levelTime", timeUsed }, // 13 byte
                { "totalTime", totalGameTime }, // 13 byte
                { "hint", hintUsed }, // 5 byte
                { "restarts", restartAmount }, // 12 byte
                { "stars", starAmount }, // 6 byte
                { "movement", movementAmount }, // 12 byte
                { "rotations", rotationAmount }, // 13 byte
                { "playedAmount", playedAmount } // 16 byte
            });
        }
        else
        {
            Debug.Log("Send level passing analytics" +
                "\n Level: " + levelName +
                "\n levelTime: " + timeUsed +
                "\n totalGameTime " + totalGameTime +
                "\n Hint: " + hintUsed +
                "\n Restarts: " + restartAmount +
                "\n Stars: " + starAmount +
                "\n Movements: " + movementAmount +
                "\n Rotations: " + rotationAmount +
                "\n PlayedAmount: " + playedAmount);

        }
    }
    public void SendLevelStartedAnalytics()
    {
        totalGameTime = GameManager.Instance.totalGameTime;

        if (sendAnalytics)
        {
            Analytics.CustomEvent("levelStarted", new Dictionary<string, object>
            {
                { "level", levelName },
                { "time", totalGameTime }
            });
        }
        else
        {
            Debug.Log("Send level started analytics" +
                "\n Level: " + levelName +
                "\n Time: " + totalGameTime);
        }
    }

    public void SendRageQuitAnalytics()
    {
        Timing.KillCoroutines(timerCoroutine);
        timeUsed = (uint)currentTime;
        totalGameTime = GameManager.Instance.totalGameTime;

        if (sendAnalytics)
        {
            Analytics.CustomEvent("levelQuitted", new Dictionary<string, object>
            {
                { "level", levelName }, // 14 byte
                { "levelTime", timeUsed }, // 8 byte
                { "totalTime", totalGameTime }, // 8 byte
                { "hint", hintUsed }, // 5 byte
                { "restarts", restartAmount }, // 12 byte
                { "movement", movementAmount }, // 12 byte
                { "rotations", rotationAmount } // 13 byte
            });
        }
        else
        {
            Debug.Log("Send level quit analytics" +
                "\n Level: " + levelName +
                "\n Time: " + timeUsed +
                "\n Hint: " + hintUsed +
                "\n Restarts: " + restartAmount +
                "\n Stars: " + starAmount +
                "\n Movements: " + movementAmount +
                "\n Rotations: " + rotationAmount);
        }

    }
    private IEnumerator<float> _TimeUsedTimer()
    {
        currentTime = 0;
        while (true)
        {
            currentTime += Time.deltaTime;
            yield return 0;
        }
    }

}
