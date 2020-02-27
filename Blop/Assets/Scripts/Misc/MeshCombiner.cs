using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MeshCombiner : MonoBehaviour
{
    public void CombineMeshes()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        int i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);

            i++;
        }
        transform.GetComponent<MeshFilter>().mesh = new Mesh();
        transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine, true, true, true);
        transform.gameObject.SetActive(true);
    }
    public void ReleaseMeshes()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();

        int i = 0;
        while (i < meshFilters.Length)
        {
            meshFilters[i].gameObject.SetActive(true);

            i++;
        }
        transform.GetComponent<MeshFilter>().mesh = null;
        transform.gameObject.SetActive(true);
    }
}