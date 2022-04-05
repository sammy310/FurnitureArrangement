using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FurnitureType
{
    Pillows,
    Lamps,
    Chairs,
    Sofas,
    Gyms,
    Tables
}

[CreateAssetMenu(fileName = "FurnitureInfo", menuName = "Data/Data_FurnitureInfo", order = 0)]
public class Data_FurnitureInfo : ScriptableObject
{
    [ContextMenuItem("�����", "GenerateHashKey")]
    public long hashKey;

    public string furnitureName;
    public string description;

    public bool bookMark; 
    public GameObject furnitureObject;
    public Sprite furnitureImage;
    public string furnitureSize;
    public int furnitureNum;
    public FurnitureType furnitureType;  

    void Reset()
    {
        GenerateHashKey();
    }

    void GenerateHashKey()
    {
        hashKey = (long)(long.MaxValue * (double)Random.value);
    }
}
