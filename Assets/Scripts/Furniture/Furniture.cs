using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : ObjectController
{
    public Data_FurnitureInfo furnitureInfo { get; private set; } = null;

    //GameObject furnitureObject = null;
    //public ObjectController Controller { get; private set; } = null;

    public void InitFurniture(Data_FurnitureInfo furnitureInfo)
    {
        this.furnitureInfo = furnitureInfo;

        SetObject(Instantiate(furnitureInfo.furnitureObject));

        //furnitureObject = Instantiate(furnitureInfo.furnitureObject, transform);
        //Controller = Instantiate(info.furnitureObject, transform).GetComponent<ObjectController>();
        //EnableRendererColliderCanvas(gameObject, true);

        //DetachObjectFromAnchor();
    }
}
