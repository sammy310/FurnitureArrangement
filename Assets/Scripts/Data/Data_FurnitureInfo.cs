using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FurnitureInfo", menuName = "Data/Data_FurnitureInfo", order = 0)]
public class Data_FurnitureInfo : ScriptableObject
{
    [ContextMenuItem("�����", "GenerateHashKey")]
    public long hashKey;

    public string furnitureName;
    public string description;

    public bool bookMark; // ���ʿ�
    public GameObject furnitureObject;
    public Sprite furnitureImage;
    public string furnitureSize;
    public int furnitureNum; // ���ʿ�

    void Reset()
    {
        GenerateHashKey();
    }

    void GenerateHashKey()
    {
        hashKey = (long)(long.MaxValue * (double)Random.value);
    }
}
