using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public static PrefabManager Instance { get; private set; } = null;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }


    [Header("- Furniture")]
    public Data_Furniture furnitureInfos;
    public GameObject furniturePrefab;

    [Header("- UI")]
    public GameObject furnitureSelectItem;
    public GameObject furnitureTypeItem;

}
