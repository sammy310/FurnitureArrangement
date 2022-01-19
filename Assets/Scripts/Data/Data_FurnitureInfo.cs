using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FurnitureInfo", menuName = "Data/Data_FurnitureInfo", order = 0)]
public class Data_FurnitureInfo : ScriptableObject
{
    public string furnitureName;
    public string description;
    public GameObject furnitureObject;
}
