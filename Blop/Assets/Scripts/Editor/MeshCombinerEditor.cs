
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MeshCombiner))]
class MeshCombinerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MeshCombiner myScript = (MeshCombiner)target;
        if (GUILayout.Button("Combine Meshes"))
        {
            myScript.CombineMeshes();
        }
        if (GUILayout.Button("Release Meshes"))
        {
            myScript.ReleaseMeshes();
        }
    }
}