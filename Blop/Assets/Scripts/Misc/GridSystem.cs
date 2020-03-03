﻿using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class GridSystem: MonoBehaviour
{

    public float cell_size = 1f; // = larghezza/altezza delle celle
    private float x, y, z;
    

    void Start()
    {
        x = 0f;
        y = 0f;
        z = 0f;
        Debug.Log("ASD:", gameObject);

    }

    void Update()
    {
        x = Mathf.Round(transform.position.x / cell_size) * cell_size;
        y = Mathf.Round(transform.position.y / cell_size) * cell_size;
        z = Mathf.Round(transform.position.z / cell_size) * cell_size;
        transform.position = new Vector3(x, y, z);
    }
}