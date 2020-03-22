using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoveChilds : MonoBehaviour
{
    public Vector2 spacing;

    public void AddSpacing()
    {
        GameObject[] childObjects = new GameObject[gameObject.transform.childCount];
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            childObjects[i] = gameObject.transform.GetChild(i).gameObject;
        }
        int xAmount = Mathf.CeilToInt(Mathf.Sqrt(childObjects.Length));
        int yAmount = xAmount;
        for (int x = 0; x < xAmount; x++)
        {
            for (int y = 0; y < yAmount; y++)
            {
                int i = (x * yAmount) + y;
                if (i >= childObjects.Length)
                    return;
                childObjects[i].transform.localPosition = new Vector3(x * spacing.x, 0, y * spacing.y);
            }
        }
    }

}
//[CustomEditor(typeof(MoveChilds))]
//public class MyPlayerEditor : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        DrawDefaultInspector();
//        MoveChilds myScript = (MoveChilds)target;
//        if (GUILayout.Button("Add spacing"))
//            myScript.AddSpacing();
//    }

//}
