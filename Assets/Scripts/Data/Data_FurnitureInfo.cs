using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FurnitureInfo", menuName = "Data/Data_FurnitureInfo", order = 0)]
public class Data_FurnitureInfo : ScriptableObject
{
    public string description;

    public bool bookMark;
    public GameObject furnitureObject;
    public string furnitureName;
    public Sprite furnitureImage;
    public string furnitureSize;
    public int furnitureNum;
}
