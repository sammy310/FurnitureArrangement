using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMesh : MonoBehaviour
{
    public ObjectController Controller { get; private set; } = null;

    public void SetController(ObjectController controller)
    {
        Controller = controller;
    }
}
