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
    public string furnitureName;
    public string description;
    
    public GameObject furnitureObject;
    public Sprite furnitureImage;
    public string furnitureSize;
    public int furniturePrice;

    public FurnitureType furnitureType;
}
