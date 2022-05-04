using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : ObjectController
{
    public Data_FurnitureInfo FurnitureInfo { get; private set; } = null;
    public SizeMeasureBox SizeMeasure { get; private set; } = null;
    
    protected override void OnSetObject(GameObject newObject)
    {
        SizeMeasure = newObject.GetComponentInChildren<SizeMeasureBox>();
    }

    public void InitFurniture(Data_FurnitureInfo furnitureInfo)
    {
        FurnitureInfo = furnitureInfo;

        SetObject(Instantiate(furnitureInfo.furnitureObject));
    }
}
