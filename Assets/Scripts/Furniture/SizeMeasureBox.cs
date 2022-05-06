using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class SizeMeasureBox : MonoBehaviour
{
    BoxCollider boxCollider = null;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }


    public Vector3 GetCenter()
    {
        return boxCollider.center;
    }

    public Vector3 GetSize()
    {
        return boxCollider.size;
    }
}
