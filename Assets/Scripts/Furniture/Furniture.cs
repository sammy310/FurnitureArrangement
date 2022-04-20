using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : ObjectController
{
    public Data_FurnitureInfo FurnitureInfo { get; private set; } = null;

    public void InitFurniture(Data_FurnitureInfo furnitureInfo)
    {
        FurnitureInfo = furnitureInfo;

        SetObject(Instantiate(furnitureInfo.furnitureObject));
    }
}
