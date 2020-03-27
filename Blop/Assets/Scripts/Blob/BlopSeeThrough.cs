using UnityEngine;

/// <summary>
/// Handles players vibility through objects by toggling boolean on shader
/// </summary>
public class BlopSeeThrough : MonoBehaviour
{
    [Tooltip("Material to make player visible through objects")]
    [SerializeField]
    private Material seeThroughMaterial;

    private bool hintActive = false;
    private void Awake()
    {
        ToggleSeeThrough(false);
    }
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
