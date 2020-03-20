using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlopSeeThrough : MonoBehaviour
{
    public Material seeThroughMaterial;

    private bool hintActive = false;
    private void Update()
    {
        //So we dont toggle it every frame, made this way instead of event purely because it's easier, since hintActive value might be toggled elsewhere too
        if (hintActive != GameManager.Instance.hintActive)
        {
            ToggleSeeThrough(GameManager.Instance.hintActive);

            hintActive = GameManager.Instance.hintActive;
        }


    }
    public void ToggleSeeThrough(bool active)
    {
        Debug.Log("Toggle hint");
        if (active)
        {
            seeThroughMaterial.SetFloat("_AllowSeeThrough", 1);
        }
        else
        {
            seeThroughMaterial.SetFloat("_AllowSeeThrough", 0);
        }
    }
}
