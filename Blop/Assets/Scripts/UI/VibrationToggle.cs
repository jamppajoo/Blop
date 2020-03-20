using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VibrationToggle : MonoBehaviour
{
    private Toggle myToggle;
    private void Awake()
    {
        myToggle = gameObject.GetComponent<Toggle>();
        myToggle.onValueChanged.AddListener(ToggleValueChanged);
    }

    private void ToggleValueChanged(bool arg0)
    {
        SettingsManager.Instance.usingVibrate = arg0;
        SaveAndLoad.Instance.Save();
        if (arg0)
            GameManager.Instance.SmallVibrate();
    }

    private void Start()
    {
        myToggle.isOn = SettingsManager.Instance.usingVibrate;
    }


}
